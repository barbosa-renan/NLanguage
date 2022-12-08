using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;

namespace NLanguage
{
    public class TermLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache _cache;

        public TermLocalizerFactory(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IStringLocalizer Create(Type resourceSource) =>
            new TermLocalizer(_cache);

        public IStringLocalizer Create(string baseName, string location) =>
            new TermLocalizer(_cache);
    }
}