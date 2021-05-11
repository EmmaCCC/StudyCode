using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthoriztionDemo.Attrs
{

    /// <summary>
    /// 自定义授权
    /// </summary>
    public class SignatureRequiredFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            if (!headers.ContainsKey("signature"))
            {
                context.Result = new UnauthorizedObjectResult(new { code = 1, message = "sdfsf" });
            }

        }
    }
}