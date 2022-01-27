﻿using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Demo.Web.Domain
{
    public interface IMyCacheService
    {
        T Get<T>(string key);
        T Set<T>(string key, T value);
    }

    class MyCacheService : IMyCacheService
    {
        private readonly IDistributedCache _cache;

        public MyCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var fullKey = $"{typeof(T).FullName}:{key}";
            var value = _cache.GetString(fullKey);

            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            var fullKey = $"{typeof(T).FullName}:{key}";
            var timeOut = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
                SlidingExpiration = TimeSpan.FromSeconds(6)
            };

            _cache.SetString(fullKey, JsonSerializer.Serialize(value), timeOut);
            return value;
        }
    }
}