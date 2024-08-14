using FilmZone.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.Service.Implementations
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        public CacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void Set(string key, object value)
        {
            _cache.Set(key, value);
        }

        public bool TryGetValue(string key, out object? value)
        {
            if(_cache.TryGetValue(key, out value))
            {
                return true;
            }
            return false;
        }
    }
}