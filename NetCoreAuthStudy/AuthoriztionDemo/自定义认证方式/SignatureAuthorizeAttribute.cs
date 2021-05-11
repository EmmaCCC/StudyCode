using AuthoriztionDemo.CustomAuthentication;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthoriztionDemo.自定义认证方式
{
    public class SignatureAuthorizeAttribute : AuthorizeAttribute
    {
        public SignatureAuthorizeAttribute()
        {
            this.AuthenticationSchemes = SignatureAuthenticationDefaults.AuthenticationScheme;
        }
    }
}
