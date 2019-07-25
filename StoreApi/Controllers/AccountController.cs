using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Account;
using Store.Core.Product;
using StoreApi.Dtos;

namespace StoreApi.Controllers
{
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccountDto item)
        {
            var account = await _accountRepository.GetByUserName(item.UserName);
            if (account != null)
                return Error($"Account with username :{item.UserName} already registered.");

            await _accountRepository.CreateAccount(new AccountEntity
            {
                UserName = item.UserName,
            });
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]RegisterAccountDto item)
        {
            var account = await _accountRepository.GetByUserName(item.UserName);
            if (account == null)
                return Error($"Account with username :{item.UserName} not found.");

            var signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"]);
            var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("userid", account.Id.ToString()),
                    new Claim("role", account.Role.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            if(token == null)
                return Error("Unauthorized.");
            return Ok(token);
        }
    }
}
