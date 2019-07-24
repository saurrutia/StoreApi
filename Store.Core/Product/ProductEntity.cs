using System.ComponentModel.DataAnnotations;

namespace Store.Core.Product
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public int Likes { get; set; }
    }
}
