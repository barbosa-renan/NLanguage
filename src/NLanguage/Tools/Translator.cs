using Microsoft.Extensions.Localization;

namespace NLanguage.Tools
{
    public class Translator : ITranslator
    {
        private readonly IStringLocalizer<Translator> _localizer;

        public Translator(IStringLocalizer<Translator> localizer)
        {
            _localizer = localizer;
        }

        public string Translate(string key)
        {
            if (string.IsNullOrEmpty(key)) return key;
            return _localizer[key];
        }
    }
}