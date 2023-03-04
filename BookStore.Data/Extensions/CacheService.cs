﻿using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using ServiceStack;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;
namespace BookStore.Data.Extensions
{
   
    public class CacheService : ICacheService
    {
        //private IDistributedCache _distributedCache;
        //public CacheService(IDistributedCache distributed)
        //{
        //    _distributedCache=distributed;
        //}
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly TimeSpan expirity = TimeSpan.FromMinutes(5);
        public CacheService( IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public  T GetCacheValue<T>(string key)
        {
            var cache = _connectionMultiplexer.GetDatabase();
            var value = cache.StringGet(key);
            if (value.IsNull)
                return default;
            return JsonSerializer.Deserialize<T>(value);
        }

        public void SetCacheValue<T>(string key, T value)
        {
            var cache = _connectionMultiplexer.GetDatabase();
            cache.StringSet(key, JsonSerializer.Serialize(value), expirity);
        }

        public void RemoveData<T>(string key)
        {
            var cache = _connectionMultiplexer.GetDatabase();
            var exist = cache.StringGet(key);
            if (!exist.IsNull)
                 cache.StringGetDeleteAsync(key);
        }



        //public T GetData<T>(string key)
        //{
        //    try
        //    {
        //        byte[]? data = _distributedCache.Get(key);
        //        if (data == null)
        //        {
        //            return default;
        //        }
        //        return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(data, 0, data.Length));
        //        var value = _distributedCache?.GetString(key);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }


        //    return default;
        //}

        //public object RemoveData(string key)
        //{
        //    var _exist=_distributedCache.GetString(key);
        //    if(_exist!=null)
        //        return _distributedCache.RemoveAsync(key);
        //    return false;
        //}

        //public void SetData<T>(string key, T value, DateTimeOffset expirationTime)
        //{
        //    var expirty = expirationTime.DateTime.Subtract(DateTime.Now);
        //     _distributedCache.SetString(key,JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow =expirty 
        //    });
        //}
    }
}
