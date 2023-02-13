using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Helper
{
    public static class StatusType
    {
        public enum Status
        {
            None = 0,
            Borrowing = 1,
            Overdue = 2,
        }
    }
}
