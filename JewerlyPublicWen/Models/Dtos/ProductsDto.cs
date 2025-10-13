namespace JewerlyPublicWen.Models.Dtos
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
        public decimal SellingPrice { get; set; }
        public bool? HasGem { get; set; }
        public string GemType { get; set; }
        public string MainImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }

        
        }
    }

