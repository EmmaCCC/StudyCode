using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ids4Server.Extensions
{
    public class WeChatLoginGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => CustomGrantTypes.WeChat;

        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            return Task.CompletedTask;
        }
    }
}
