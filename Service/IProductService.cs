using ProductApplication.Models.Product;

namespace ProductApplication.Service.Product
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(int id);
        Task AddProduct(ProductModel product);
        Task EditProduct(ProductModel product);
        Task DeleteProduct(int Id);
    }
}
