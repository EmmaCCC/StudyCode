using IdentityServer4.Validation;
using Ids4.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ids4Server.Extensions
{
    public class DbResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly GwlDbContext gwlDb;

        public DbResourceOwnerPasswordValidator(IHttpContextAccessor httpContextAccessor
            , GwlDbContext gwlDb)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.gwlDb = gwlDb;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            var httpContext = httpContextAccessor.HttpContext;

            var username = context.UserName;
            var password = context.Password;

            var user = gwlDb.Users.FirstOrDefault(a => a.UserName == username && a.Password == password);
            if (user == null)
            {
                context.Result.IsError = true;
                context.Result.CustomResponse = new Dictionary<string, object>()
                {
                    {"info","user not found" }
                };
            }
            else
            {
                var client = context.Request.Client;
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,client.ClientName),
                    new Claim("AccountType","College"),
                    new Claim("AccountType2","Teacher"),
                };

                // 验证账号
                context.Result = new GrantValidationResult
                (
                    subject: username,
                    authenticationMethod: "custom",
                    claims: claims.ToArray()
                 );
            }

            return Task.CompletedTask;
        }
    }
}
