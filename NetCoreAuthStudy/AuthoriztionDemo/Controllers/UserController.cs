using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthoriztionDemo.Attrs;
using AuthoriztionDemo.CustomAuthentication;
using AuthoriztionDemo.Models;
using AuthoriztionDemo.自定义认证方式;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthoriztionDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize()]
    public class UserController : ControllerBase
    {
        private readonly JwtConfig jwtConfig;

        public UserController(IOptions<JwtConfig> jwtConfig)
        {
            this.jwtConfig = jwtConfig.Value;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string username, string password)
        {
            var claims = new[]
           {
                new Claim(ClaimTypes.Name,username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(jwtConfig.Issuer, jwtConfig.Audience, claims, expires: DateTime.Now.AddMinutes(jwtConfig.Expire), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(token);
        }


        [SignatureAuthorize]
        public IActionResult Info()
        {
            var info = (from u in HttpContext.User.Claims select new { u.Type, u.Value }).ToList();
            return Ok(info);
        }

        [AllowAnonymous]
        public IActionResult Info2()
        {
            var info = (from u in HttpContext.User.Claims select new { u.Type, u.Value }).ToList();
            return Ok(info);
        }

        public IActionResult Info3()
        {
            var info = (from u in HttpContext.User.Claims select new { u.Type, u.Value }).ToList();
            return Ok(info);
        }
    }
}
