using System.Text.Json;
using ECommerce.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Services
{
    public class MongoCacheService : ICacheService
    {
        private readonly IMongoCollection<CacheItem> _cache;
        
        public MongoCacheService(IMongoClient mongoClient, IConfiguration config)
        {
            var database = mongoClient.GetDatabase(config["MongoDB:DatabaseName"]);
            _cache = database.GetCollection<CacheItem>("cache");
            
            var indexKeysDefinition = Builders<CacheItem>.IndexKeys.Ascending(x => x.ExpiryTime);
            var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };
            _cache.Indexes.CreateOne(new CreateIndexModel<CacheItem>(indexKeysDefinition, indexOptions));
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var item = await _cache.Find(x => x.Key == key && x.ExpiryTime > DateTime.UtcNow)
                                  .FirstOrDefaultAsync();
            return item != null ? JsonSerializer.Deserialize<T>(item.Value) : default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            var item = new CacheItem
            {
                Key = key,
                Value = JsonSerializer.Serialize(value),
                ExpiryTime = DateTime.UtcNow.Add(expirationTime ?? TimeSpan.FromMinutes(5))
            };
            
            await _cache.ReplaceOneAsync(
                x => x.Key == key,
                item,
                new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.DeleteOneAsync(x => x.Key == key);
        }
    }
}