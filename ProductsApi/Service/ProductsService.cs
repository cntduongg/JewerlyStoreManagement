using ProductsApi.Models;
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
            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductsDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? MapToDto(product) : null;
        }

        public async Task<ProductsDto> GetByProductCodeAsync(string productCode)
        {
            var product = await _productRepository.GetByProductCodeAsync(productCode);
            return product != null ? MapToDto(product) : null;
        }

        public async Task<ProductsDto> CreateAsync(CreateProductDto createDto)
        {
            // Validate product code uniqueness
            if (await _productRepository.ProductCodeExistsAsync(createDto.ProductCode))
            {
                throw new InvalidOperationException($"Product code '{createDto.ProductCode}' already exists.");
            }

            var product = new Product
            {
                Productcode = createDto.ProductCode,
                Productname = createDto.ProductName,
                Categoryid = createDto.CategoryId,
                Model = createDto.Model,
                Material = createDto.Material,
                Goldweight = createDto.GoldWeight,
                Gemweight = createDto.GemWeight,
                Totalweight = createDto.TotalWeight,
                Goldprice = createDto.GoldPrice,
                Laborcost = createDto.LaborCost,
                Stoneprice = createDto.StonePrice,
                Costprice = createDto.CostPrice,
                Sellingprice = createDto.SellingPrice,
                Hasgem = createDto.HasGem,
                Gemtype = createDto.GemType,
                Gemquality = createDto.GemQuality,
                Gemcolor = createDto.GemColor,
                Gemcut = createDto.GemCut,
                Stockquantity = createDto.StockQuantity,
                Minstocklevel = createDto.MinStockLevel,
                Maxstocklevel = createDto.MaxStockLevel,
                Imageurl = createDto.ImageUrl,
                Shortdescription = createDto.ShortDescription,
                Detaildescription = createDto.DetailDescription,
                Tags = createDto.Tags,
                Isactive = createDto.IsActive ?? true
            };

            var createdProduct = await _productRepository.CreateAsync(product);
            return MapToDto(createdProduct);
        }

        public async Task<ProductsDto> UpdateAsync(UpdateProductDto updateDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updateDto.ProductId);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {updateDto.ProductId} not found.");
            }

            // Validate product code uniqueness (exclude current product)
            if (await _productRepository.ProductCodeExistsAsync(updateDto.ProductCode, updateDto.ProductId))
            {
                throw new InvalidOperationException($"Product code '{updateDto.ProductCode}' already exists.");
            }

            existingProduct.Productcode = updateDto.ProductCode;
            existingProduct.Productname = updateDto.ProductName;
            existingProduct.Categoryid = updateDto.CategoryId;
            existingProduct.Model = updateDto.Model;
            existingProduct.Material = updateDto.Material;
            existingProduct.Goldweight = updateDto.GoldWeight;
            existingProduct.Gemweight = updateDto.GemWeight;
            existingProduct.Totalweight = updateDto.TotalWeight;
            existingProduct.Goldprice = updateDto.GoldPrice;
            existingProduct.Laborcost = updateDto.LaborCost;
            existingProduct.Stoneprice = updateDto.StonePrice;
            existingProduct.Costprice = updateDto.CostPrice;
            existingProduct.Sellingprice = updateDto.SellingPrice;
            existingProduct.Hasgem = updateDto.HasGem;
            existingProduct.Gemtype = updateDto.GemType;
            existingProduct.Gemquality = updateDto.GemQuality;
            existingProduct.Gemcolor = updateDto.GemColor;
            existingProduct.Gemcut = updateDto.GemCut;
            existingProduct.Stockquantity = updateDto.StockQuantity;
            existingProduct.Minstocklevel = updateDto.MinStockLevel;
            existingProduct.Maxstocklevel = updateDto.MaxStockLevel;
            existingProduct.Imageurl = updateDto.ImageUrl;
            existingProduct.Shortdescription = updateDto.ShortDescription;
            existingProduct.Detaildescription = updateDto.DetailDescription;
            existingProduct.Tags = updateDto.Tags;
            existingProduct.Isactive = updateDto.IsActive;

            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
            return MapToDto(updatedProduct);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return await _productRepository.DeleteAsync(id);
        }

        private ProductsDto MapToDto(Product product)
        {
            return new ProductsDto
            {
                ProductId = product.Productid,
                ProductCode = product.Productcode,
                ProductName = product.Productname,
                CategoryId = product.Categoryid,
                CategoryName = product.Category?.Categoryname,
                Model = product.Model,
                Material = product.Material,
                GoldWeight = product.Goldweight,
                GemWeight = product.Gemweight,
                TotalWeight = product.Totalweight,
                GoldPrice = product.Goldprice,
                LaborCost = product.Laborcost,
                StonePrice = product.Stoneprice,
                CostPrice = product.Costprice,
                SellingPrice = product.Sellingprice,
                HasGem = product.Hasgem,
                GemType = product.Gemtype,
                GemQuality = product.Gemquality,
                GemColor = product.Gemcolor,
                GemCut = product.Gemcut,
                StockQuantity = product.Stockquantity,
                MinStockLevel = product.Minstocklevel,
                MaxStockLevel = product.Maxstocklevel,
                ImageUrl = product.Imageurl,
                ShortDescription = product.Shortdescription,
                DetailDescription = product.Detaildescription,
                Tags = product.Tags,
                IsActive = product.Isactive,
                CreatedAt = product.Createdat,
                UpdatedAt = product.Updatedat
            };
        }
    }
}