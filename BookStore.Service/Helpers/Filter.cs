using NTQ.Sdk.Core.Attributes;
using NTQ.Sdk.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Helpers
{
    public static class Filter
    {
        public static IQueryable<TEntity> Filters<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!(entity.GetType().GetProperty(propertyInfo.Name) != null))
                {
                    continue;
                }

                object obj = entity.GetType().GetProperty(propertyInfo.Name)?.GetValue(entity, null);
                if (obj == null || propertyInfo.CustomAttributes.Any((CustomAttributeData x) => x.AttributeType == typeof(SkipAttribute)))
                {
                    continue;
                }
                if(propertyInfo.CustomAttributes.Any((CustomAttributeData x) => x.AttributeType == typeof(DateTimeFieldAttribute)))
                {
                    source = source.Where(propertyInfo.Name + "== DateTime.ParseE(@0)", obj.ToString);
                }

            }

            return source;
        }
    }
}

