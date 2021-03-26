using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ids4Server.Extensions
{
    public class SmsLoginGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => CustomGrantTypes.Sms;

        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var claims = new List<Claim>()
            {
                  new Claim("mobile","13140167513")
            };
            context.Result = new GrantValidationResult("smsuser", "sms", claims);

            return Task.CompletedTask;
        }
    }
}
