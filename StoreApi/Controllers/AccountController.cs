using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Common;
using Store.Core.Account;
using Store.Core.Product;
using StoreApi.Dtos;
using StoreApi.Utils;

namespace StoreApi.Controllers
{
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenFactory _tokenFactory;

        public AccountController(IAccountRepository accountRepository, ITokenFactory tokenFactory)
        {
            _accountRepository = accountRepository;
            _tokenFactory = tokenFactory;
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
                Password = item.Password.ToSha256(),
                Role = item.Role
            });
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto item)
        {
            var account = await _accountRepository.GetByUserNameAndPassword(item.UserName, item.Password.ToSha256());
            if (account == null)
                return Error($"Account with username :{item.UserName} not found.");

            var token = _tokenFactory.GenerateToken(account.UserName, account.Role);

            return token == null ? Unauthorized() : Ok(token);
        }
    }
}
