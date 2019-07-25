using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Dtos
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

    }
}
