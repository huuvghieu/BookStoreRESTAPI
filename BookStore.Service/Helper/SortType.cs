using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Helper
{
    public static class SortDirectionType
    {
        public enum SortOrder
        {
            [Description("asc")]
            Ascending = 0,
            [Description("desc")]
            Descending = 1,
        }
    }
}
