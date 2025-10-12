namespace ProductsApi.Models.Dtos
{
    public class ProductsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal SellingPrice { get; set; }
        public string MainImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string CategoryName { get; set; }
    }
}
