using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NLanguage.Middlewares;
using NLanguage.Tools;

namespace NLanguage.Extensions
{
    public static class NLanguageServiceCollectionExtensions
    {
        public static IServiceCollection AddNLanguage(this IServiceCollection services)
        {
            services.AddLocalization();
            services.AddSingleton<TermLocalizerMiddleware>();
            services.AddSingleton<IStringLocalizerFactory, TermLocalizerFactory>();
            services.AddScoped<ITranslator, Translator>();
            return services;
        }
    }
}
