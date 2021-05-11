using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthoriztionDemo.CustomAuthentication
{
    public static class SignatureAuthenticationExtensions
    {
        public static AuthenticationBuilder AddSignature(this AuthenticationBuilder builder)
             => builder.AddSignature(SignatureAuthenticationDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddSignature(this AuthenticationBuilder builder, Action<SignatureOptions> configureOptions)
            => builder.AddSignature(SignatureAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddSignature(this AuthenticationBuilder builder, string authenticationScheme, Action<SignatureOptions> configureOptions)
        => builder.AddSignature(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddSignature(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<SignatureOptions> configureOptions)
        {
            return builder.AddScheme<SignatureOptions, SignatureAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
