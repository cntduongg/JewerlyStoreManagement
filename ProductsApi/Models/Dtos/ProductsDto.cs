namespace ProductsApi.Models.Dtos
{
    public class ProductsDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Model { get; set; }
        public string Material { get; set; }
        public decimal? GoldWeight { get; set; }
        public decimal? GemWeight { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? GoldPrice { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? StonePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool? HasGem { get; set; }
        public string GemType { get; set; }
        public string GemQuality { get; set; }
        public string GemColor { get; set; }
        public string GemCut { get; set; }
        public int StockQuantity { get; set; }
        public int? MinStockLevel { get; set; }
        public int? MaxStockLevel { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string Tags { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string DisplayImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl) || !ImageUrl.StartsWith("http"))
                {
                    return "/images/default-product.jpg";
                }
                return ImageUrl;
            }
        }
    }

    public class CreateProductDto
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Model { get; set; }
        public string Material { get; set; }
        public decimal? GoldWeight { get; set; }
        public decimal? GemWeight { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? GoldPrice { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? StonePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool? HasGem { get; set; }
        public string GemType { get; set; }
        public string GemQuality { get; set; }
        public string GemColor { get; set; }
        public string GemCut { get; set; }
        public int StockQuantity { get; set; }
        public int? MinStockLevel { get; set; }
        public int? MaxStockLevel { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string Tags { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Model { get; set; }
        public string Material { get; set; }
        public decimal? GoldWeight { get; set; }
        public decimal? GemWeight { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? GoldPrice { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? StonePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool? HasGem { get; set; }
        public string GemType { get; set; }
        public string GemQuality { get; set; }
        public string GemColor { get; set; }
        public string GemCut { get; set; }
        public int StockQuantity { get; set; }
        public int? MinStockLevel { get; set; }
        public int? MaxStockLevel { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string Tags { get; set; }
        public bool? IsActive { get; set; }
    }
}