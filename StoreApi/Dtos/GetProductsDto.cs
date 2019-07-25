using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Dtos
{
    public class GetProductsDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public Sorter SortBy { get; set; } = Sorter.Name;
    }

    public enum Sorter
    {
        Name = 0,
        Likes = 1
    }
}
