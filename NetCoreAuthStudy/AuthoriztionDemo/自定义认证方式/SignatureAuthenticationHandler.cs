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
    /// �Զ�����֤
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
        /// �����Ƿ���֤�ɹ�
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
        /// ��֤��ͨ���Ĵ���
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {

            //�Զ����Լ���Ҫ���ص����ݽ����������Ҫ���ص���Json����ͨ������Newtonsoft.Json�����ת��
            var payload = JsonConvert.SerializeObject(new { Code = "401", Message = "ǩ����֤����" });
            //�Զ��巵�ص���������
            Response.ContentType = "application/json";
            //�Զ��巵��״̬�룬Ĭ��Ϊ401 ������ĳ� 200
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //���Json���ݽ��
            Response.WriteAsync(payload);
            return Task.CompletedTask;
        }

    }
}