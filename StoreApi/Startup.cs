﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Account;
using Store.Persistence;
using Store.Core.Interfaces;
using Store.Core.Product;
using Store.Persistence.Repositories;
using StoreApi.Middlewares;
using StoreApi.Utils;
using Swashbuckle.AspNetCore.Swagger;
using Store.Core.Events.Common;
using StoreApi.Handlers;
using Store.Core.Events;

namespace StoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            services.AddScoped<IRepository<PriceUpdateLogEntity>, Repository<PriceUpdateLogEntity>>();
            services.AddScoped<IRepository<PurchaseLogEntity>, Repository<PurchaseLogEntity>>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductLikeRepository, ProductLikeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IDomainHandler<PriceUpdated>, PriceUpdatedHandler>();
            services.AddScoped<IDomainHandler<ProductBuyed>, ProductBuyedHandler>();
            services.AddScoped<IEventDispatcher, NetCoreEventContainer>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ITokenFactory, JwtFactory>();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "StoreApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
                c.DescribeAllEnumsAsStrings();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var signingKey = Convert.FromBase64String(Configuration["Jwt:SigningSecret"]);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKey)
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("StoreConnection");
            if (!string.IsNullOrEmpty(connectionString))
                services.AddDbContext<StoreDbContext>(opt => opt.UseSqlServer(connectionString));
            else
                services.AddDbContext<StoreDbContext>(opt => opt.UseInMemoryDatabase("StoreDb"));

        }
    }
}
