using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AuthoriztionDemo.CustomAuthentication
{
    /// <summary>
    /// 自定义认证
    /// </summary>
    public class SignatureAuthenticationHandler : AuthenticationHandler<SignatureOptions>
    {
        private readonly HttpContext httpContext;

        public SignatureAuthenticationHandler(
         IOptionsMonitor<SignatureOptions> options,
         ILoggerFactory logger,
         UrlEncoder encoder,
         ISystemClock clock,
         IHttpContextAccessor HttpContextAccessor)
         : base(options, logger, encoder, clock)
        {
            this.httpContext = HttpContextAccessor.HttpContext;
        }

        /// <summary>
        /// 处理是否认证成功
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            await Task.CompletedTask;
            var headers = httpContext.Request.Headers;
            if (headers.ContainsKey("signature"))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "123"),
                    new Claim(ClaimTypes.Name,  "songlin"),
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("no auth");
            }

        }

        /// <summary>
        /// 认证不通过的处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {

            //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换
            var payload = JsonConvert.SerializeObject(new { Code = "401", Message = "签名认证错误" });
            //自定义返回的数据类型
            Response.ContentType = "application/json";
            //自定义返回状态码，默认为401 我这里改成 200
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //输出Json数据结果
            Response.WriteAsync(payload);
            return Task.CompletedTask;
        }

    }
}