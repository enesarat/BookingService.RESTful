using BookingService.Cache.Concrete.Repository;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using BookingService.Entity.Concrete.DTO;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Concrete.EntityFramework.Cache
{
    public class EfAppartmentRepositoryWithCache : IAppartmentsDAL
    {
        private const string _appartmentsKey = "appartmentCache";
        private const string _pagedAppartmentsKey = "pagedAppartmentCache";
        private readonly IAppartmentsDAL _appartmentsRepository;
        private readonly RedisRepository _redisRepository;
        private readonly IDatabase _cacheDb;
        public EfAppartmentRepositoryWithCache(IAppartmentsDAL appartmentsRepository, RedisRepository redisRepository)
        {
            _appartmentsRepository = appartmentsRepository;
            _redisRepository = redisRepository;
            _cacheDb = _redisRepository.GetDatabase(0);
        }
        public Task DeleteItem(int id)
        {
            return _appartmentsRepository.DeleteItem(id);
        }

        public async Task<List<Appartments>> GetAllItems()
        {
            var cacheStatus = await _cacheDb.KeyExistsAsync(_appartmentsKey);
            if (!cacheStatus)
            {
                return await CachingFromDatabase();
            }

            var appartments = new List<Appartments>();
            var cachedAppartments = await _cacheDb.HashGetAllAsync(_appartmentsKey);
            foreach (var item in cachedAppartments.ToList())
            {
                var appartment = JsonConvert.DeserializeObject<Appartments>(item.Value);
                appartments.Add(appartment);
            }

            return appartments;
        }

        public List<Appartments> GetAllItemsByFilter(Expression<Func<Appartments, bool>> filter)
        {
            return _appartmentsRepository.GetAllItemsByFilter(filter);
        }

        public async Task<List<Appartments>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            var cachedPageKey = $$"""{{_pagedAppartmentsKey}}:{{pagingParameters.PageNumber}}""";
            
            var cacheStatus = await _cacheDb.KeyExistsAsync(cachedPageKey);
            if (!cacheStatus)
            {
                return await CachingFromDatabaseWithPaging(pagingParameters,cachedPageKey);
            }

            var appartments = new List<Appartments>();
            var cachedAppartments = await _cacheDb.HashGetAllAsync(cachedPageKey);
            foreach (var item in cachedAppartments.ToList())
            {
                var appartment = JsonConvert.DeserializeObject<Appartments>(item.Value);
                appartments.Add(appartment);
            }

            return appartments;
        }

        public async Task<Appartments> GetItemById(int id)
        {
            if (_cacheDb.KeyExists(_appartmentsKey))
            {
                var serializedAppartment = await _cacheDb.HashGetAsync(_appartmentsKey, id);
                return serializedAppartment.HasValue ? JsonConvert.DeserializeObject<Appartments>(serializedAppartment) : null;
            }
            
            var appartments = await CachingFromDatabase();
            var appartment = appartments.FirstOrDefault(x => x.id == id);
            return appartment;
        }

        public async Task<Appartments> InsertItem(Appartments item)
        {
            var newAppartment = await _appartmentsRepository.InsertItem(item);
            if(await _cacheDb.KeyExistsAsync(_appartmentsKey))
            {
                await _cacheDb.HashSetAsync(_appartmentsKey, item.id, JsonConvert.SerializeObject(newAppartment));
            }

            return newAppartment;
        }

        public Task<Appartments> UpdateItem(Appartments item)
        {
            return _appartmentsRepository.UpdateItem(item);
        }

        private async Task<List<Appartments>> CachingFromDatabase()
        {
            var appartments = await _appartmentsRepository.GetAllItems();

            appartments.ForEach(p =>
            {
                string serializedAppartment = JsonConvert.SerializeObject(p);
                _cacheDb.HashSetAsync(_appartmentsKey,p.id,serializedAppartment);
            });

            return appartments;
        }
        private async Task<List<Appartments>> CachingFromDatabaseWithPaging(PagingParameters pagingParameters, String pagedCacheKey)
        {
            var appartments = await _appartmentsRepository.GetElementsByPaging(pagingParameters);

            appartments.ForEach(p =>
            {
                string serializedAppartment = JsonConvert.SerializeObject(p);
                _cacheDb.HashSetAsync(pagedCacheKey, p.id, serializedAppartment);
            });

            return appartments;
        }
    }
}
