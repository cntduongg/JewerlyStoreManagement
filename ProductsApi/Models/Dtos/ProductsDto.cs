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
        public decimal? GoldWeight { get; set; }
        public decimal? GemWeight { get; set; }
        public decimal? TotalWeight { get; set; }
        public string Material { get; set; }
        public bool? HasGem { get; set; }
        public string GemType { get; set; }
        public string DisplayImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(MainImageUrl) || !MainImageUrl.StartsWith("http"))
                {
                    // Return default image based on category or product type
                    return "/images/default-product.jpg";
                }
                return MainImageUrl;
            }
        }
    }
}
