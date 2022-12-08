using Microsoft.AspNetCore.Builder;
using NLanguage.Middlewares;

namespace NLanguage.Extensions
{
    public static class NLanguageApplicationBuildExtensions
    {
        public static IApplicationBuilder UseNLanguage(this IApplicationBuilder app)
        {
            app.UseMiddleware<TermLocalizerMiddleware>();
            return app;
        }
    }
}
