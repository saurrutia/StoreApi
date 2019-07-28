using Store.Core.Product;

namespace Store.Core.Events
{
    public class PriceUpdated : IDomainEvent
    {
        public ProductEntity Product { get; set; }
        public double LastPrice { get; set; }
        public string UserName { get; set; }
    }
}
