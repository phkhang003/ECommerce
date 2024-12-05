using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Infrastructure.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public MongoRepository(IMongoDatabase database, IMemoryCache cache)
        {
            _collection = database.GetCollection<T>(typeof(T).Name);
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var cacheKey = $"{typeof(T).Name}_all";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<T>? items) && items != null)
            {
                return items;
            }
            
            items = await _collection.Find(_ => true).ToListAsync();
            if (items != null)
            {
                _cache.Set(cacheKey, items, _cacheOptions);
            }
            return items ?? Enumerable.Empty<T>();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }
    }
} 