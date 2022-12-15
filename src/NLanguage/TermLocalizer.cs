using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace NLanguage
{
    internal class TermLocalizer : IStringLocalizer
    {
        private readonly IDistributedCache _cache;

        private readonly JsonSerializer _serializer = new JsonSerializer();

        public TermLocalizer(IDistributedCache cache)
        {
            _cache = cache;
        }

        public LocalizedString this[string name]
        {
            get
            {
                string value = GetTermByKey(name);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments), false)
                    : actualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            string filePath = $"Languages/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var streamReader = new StreamReader(fileStream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType != JsonToken.PropertyName) continue;

                    string key = (string)jsonReader.Value;
                    jsonReader.Read();
                    string value = _serializer.Deserialize<string>(jsonReader);
                    yield return new LocalizedString(key, value, false);
                }
            }
        }

        private string GetTermByKey(string key)
        {
            string relativeFilePath = $"Languages/{Thread.CurrentThread.CurrentCulture.Name}.json";
            string fullFilePath = Path.GetFullPath(relativeFilePath);
            if (File.Exists(fullFilePath))
            {
                string cacheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
                string cacheValue = _cache.GetString(cacheKey);

                if (!string.IsNullOrEmpty(cacheValue)) 
                    return cacheValue;

                string result = GetValueFromJSON(key, Path.GetFullPath(relativeFilePath));

                if (!string.IsNullOrEmpty(result))
                    _cache.SetString(cacheKey, result);

                return result;
            }

            return default(string);
        }

        private string GetValueFromJSON(string propertyName, string filePath)
        {
            if (propertyName == null) return default;
            if (filePath == null) return default;
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var streamReader = new StreamReader(fileStream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                while (jsonReader.Read())
                    if (jsonReader.TokenType == JsonToken.PropertyName && (string)jsonReader.Value == propertyName)
                    {
                        jsonReader.Read();
                        return _serializer.Deserialize<string>(jsonReader);
                    }

                return default;
            }
        }
    }
}