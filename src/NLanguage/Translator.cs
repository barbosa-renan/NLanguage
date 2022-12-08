using Microsoft.Extensions.Localization;

namespace NLanguage
{
    public static class Translator<T>
    {
        private static readonly IStringLocalizer<T> _localizer;

        public static string Translate(string term)
        {
            if (string.IsNullOrEmpty(term)) return term;

            return _localizer[term].ToString();
        }
    }
}
