using Store.Core.Product;

namespace Store.Core.Events
{
    public class ProductBuyed : IDomainEvent
    {
        public ProductEntity Product { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
    }
}
