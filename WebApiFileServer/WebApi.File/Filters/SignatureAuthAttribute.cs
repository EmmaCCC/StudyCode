using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.File.Filters
{
    public class SignatureAuthAttribute : AuthorizeAttribute
    {
        private bool _timeError;
        private bool _signError;
        private static string _key = "dkg7832f&$*T@(#7890";

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {

                var context = (HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"];
                var timestamp = context.Request.Headers["timestamp"];
                var signature = context.Request.Headers["signature"];
                var timestampStartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
                DateTime time = timestampStartTime.AddSeconds(long.Parse(timestamp));
                if ((DateTime.Now - time).TotalMinutes > 30)
                {
                    _timeError = true;
                    return false;
                }

                if (string.IsNullOrEmpty(signature))
                {
                    _signError = true;
                    return false;
                }

                string signature2 = GetMD5Hash(GetMD5Hash(_key + timestamp).ToLower());

                if (signature.ToLower() != signature2.ToLower())
                {
                    _signError = true;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            HttpResponseMessage rm = new HttpResponseMessage(HttpStatusCode.Forbidden);
            string msg = "非法请求";
            if (_timeError)
            {
                msg = "当前设备时间不准确";
            }
            if (_signError)
            {
                msg = "签名错误";
            }
            rm.Content = new StringContent("{\"reCode\": 210, \"errorMsg\":\"" + msg + "\"}", Encoding.UTF8, "appliction/json");
            actionContext.Response = rm;

        }


        private string GetMD5Hash(string str)
        {
            try
            {
                MD5 md5Provider = new MD5CryptoServiceProvider();
                byte[] buffer = md5Provider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
                md5Provider.Clear();
                return BitConverter.ToString(buffer).Replace("-", "");
            }
            catch { return null; }
        }
    }
}
