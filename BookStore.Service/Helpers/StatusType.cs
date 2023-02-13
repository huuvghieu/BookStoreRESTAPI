using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Helpers
{
    public  static class StatusType
    {
        public enum StatusOrder
        {
            Borrowing = 0,
            Returned = 1,
            Overdue = 2
        }
    }
}
