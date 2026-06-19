using MiniERP.Application.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<(bool Success, string Message)> CreateAsync(
            string code,
            string name,
            string description,
            decimal costPrice,
            decimal salePrice,
            decimal stock,
            int categoryId)
        {
            if (await _productRepository.ExistsCodeAsync(code))
            {
                return (false, "Ya existe un producto con ese código.");
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return (false, "La categoría seleccionada no existe.");
            }

            var product = new Product
            {
                Code = code,
                Name = name,
                Description = description,
                CostPrice = costPrice,
                SalePrice = salePrice,
                Stock = stock,
                CategoryId = categoryId
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return (true, "Producto creado exitosamente.");
        }

        public async Task<(bool Success, string Message)> UpdateAsync(
            int id,
            string code,
            string name,
            string description,
            decimal costPrice,
            decimal salePrice,
            decimal stock,
            int categoryId)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return (false, "Producto no encontrado.");
            }

            if (await _productRepository.ExistsCodeAsync(code, id))
            {
                return (false, "Ya existe otro producto con ese código.");
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return (false, "La categoría seleccionada no existe.");
            }

            product.Code = code;
            product.Name = name;
            product.Description = description;
            product.CostPrice = costPrice;
            product.SalePrice = salePrice;
            product.Stock = stock;
            product.CategoryId = categoryId;

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return (true, "Producto actualizado exitosamente.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return (false, "Producto no encontrado.");
            }

            await _productRepository.DeleteAsync(id);
            await _productRepository.SaveChangesAsync();

            return (true, "Producto eliminado exitosamente.");
        }
    }
}
