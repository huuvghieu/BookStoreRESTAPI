using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Extensions
{
    public interface ICacheService
    {
        //T GetData<T>(string key);
        //void SetData<T>(string key, T value, DateTimeOffset expirationTime);
        //object RemoveData(string key);
        public T GetCacheValue<T>(string key);
        public void SetCacheValue<T>(string key, T value);
        public void RemoveData<T>(string key);
    }
}
