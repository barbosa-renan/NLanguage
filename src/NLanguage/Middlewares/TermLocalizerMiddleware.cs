using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NLanguage.Middlewares
{
    public class TermLocalizerMiddleware : IMiddleware
    {
        private const string AcceptLanguage = "Accept-Language";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var cultureKey = context.Request.Headers[AcceptLanguage];

            if (!string.IsNullOrEmpty(cultureKey))
            {
                if (DoesCultureExist(cultureKey))
                {
                    var culture = new CultureInfo(cultureKey);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
            }

            await next(context);
        }

        private static bool DoesCultureExist(string cultureName)
        {
            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            return allCultures.Any(x => string.Equals(x.Name,
                                                      cultureName,
                                                      StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
