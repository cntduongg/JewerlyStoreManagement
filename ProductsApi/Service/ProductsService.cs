using ProductsApi.Models.Dtos;
using ProductsApi.Repo.IRepo;
using ProductsApi.Service.IService;

namespace ProductsApi.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepo _productRepository;

        public ProductsService(IProductsRepo productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductsDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = new List<ProductsDto>();
            foreach (var product in products)
            {
                productDtos.Add(new ProductsDto
                {
                    ProductId = product.Productid,

                    ProductName = product.Productname,
                    SellingPrice = product.Sellingprice,
                    MainImageUrl = product.Imageurl,
                    ShortDescription = product.Shortdescription,
                    CategoryName = product.Category?.Categoryname,
                    GoldWeight = product.Goldweight,
                    GemWeight = product.Gemweight,
                    TotalWeight = product.Totalweight,
                    Material = product.Material,
                    HasGem = product.Hasgem,
                    GemType = product.Gemtype
                   

                });

            }
            return productDtos;
        }
    }
}
