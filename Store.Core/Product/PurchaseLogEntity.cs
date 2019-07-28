namespace Store.Core.Product
{
    public class PurchaseLogEntity : LogEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
