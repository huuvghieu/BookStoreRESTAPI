using System.ComponentModel;

namespace BookStore.Service.Helpers
{
    public static class SortType
    {
        public enum SortOrder
        {
            [Description("asc")]
            Ascending = 0,
            [Description("desc")]
            Descending = 1,
            None = 2
        }
    }
}