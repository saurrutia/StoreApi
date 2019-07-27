using Microsoft.AspNetCore.Mvc;

namespace StoreApi.Dtos
{
    public class UpdateProductDto
    {
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}
