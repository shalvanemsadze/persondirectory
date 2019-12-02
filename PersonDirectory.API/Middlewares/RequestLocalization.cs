using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace PersonDirectory.API.Middlewares
{
    public class RequestLocalization
    {
        private readonly RequestDelegate _next;
        private readonly RequestLocalizationOptions _options;

        public RequestLocalization(RequestDelegate next, IOptions<RequestLocalizationOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var requestCulture = _options.DefaultRequestCulture;

            var userLangs = context.Request.Headers["Accept-Language"].ToString();
            var firstLang = userLangs.Split(',').FirstOrDefault();
            var defaultLang = string.IsNullOrEmpty(firstLang) ? "en" : firstLang;

            IRequestCultureProvider winningProvider = null;

            if (_options.RequestCultureProviders != null)
            {
                foreach (var provider in _options.RequestCultureProviders)
                {
                    var providerResultCulture = await provider.DetermineProviderCultureResult(context);
                    if (providerResultCulture == null)
                        continue;
                   
                    var result = new RequestCulture(defaultLang, defaultLang);

                    if (result != null)
                    {
                        requestCulture = result;
                        winningProvider = provider;
                        break;
                    }
                }
            }

            context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(new RequestCulture(defaultLang, defaultLang), winningProvider));
            await _next(context);
        }
    }
    public static class RequestLocalizationExtensions
    {
        public static IApplicationBuilder UseCustomRequestLocalization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLocalization>();
        }
    }
}

