namespace Store.Core.Product
{
    public class PriceUpdateLogEntity : LogEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double PriceBefore { get; set; }
        public double PriceAfter { get; set; }
    }
}
