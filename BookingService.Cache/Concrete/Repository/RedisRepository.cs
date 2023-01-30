using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Cache.Concrete.Repository
{
    public class RedisRepository
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public RedisRepository(string url)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url);
        }
        public IDatabase GetDatabase(int indexOfDb)
        {
            return _connectionMultiplexer.GetDatabase(indexOfDb);
        }
    }
}
