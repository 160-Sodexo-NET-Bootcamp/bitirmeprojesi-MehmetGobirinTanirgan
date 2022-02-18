using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace PCS.Core.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetDataAsync<T>(this IDistributedCache distributedCache, string dataKey, T data,
             TimeSpan slidingExpireTime, TimeSpan absoluteExpireTime)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(absoluteExpireTime);
            options.SetSlidingExpiration(slidingExpireTime);
            var serializedData = JsonConvert.SerializeObject(data);
            await distributedCache.SetStringAsync(dataKey, serializedData, options);
        }

        public static async Task<T> GetDataAsync<T>(this IDistributedCache distributedCache, string dataKey)
        {
            var serializedData = await distributedCache.GetStringAsync(dataKey);
            if (serializedData is null)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(serializedData);
        }
    }
}
