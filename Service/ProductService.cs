using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ProductApplication.Models;
using ProductApplication.Models.Product;
using ProductApplication.Service.Product;

namespace ProductApplication.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductAppContext _Context;

        public ProductService(ProductAppContext context)
        {
            _Context = context;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _Context.Product.ToListAsync();
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            return await _Context.Product.FindAsync(id);
        }

        public async Task<List<ProductModel>> SearchProducts(string Filter)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Filter", Filter));

            var result = await _Context.Product
                .FromSqlRaw("EXEC SearchProduct @Filter", parameters)
                .ToListAsync();

            return result;
        }

        public async Task AddProduct(ProductModel product)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@NamaBarang", product.NamaBarang));
                parameters.Add(new SqlParameter("@KodeBarang", product.KodeBarang));
                parameters.Add(new SqlParameter("@JumlahBarang", product.JumlahBarang));
                parameters.Add(new SqlParameter("@Tanggal", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));                

                var data = await _Context.Database.ExecuteSqlRawAsync("EXEC AddProduct @NamaBarang, @KodeBarang, @JumlahBarang, @Tanggal", parameters);

            }
            catch (Exception ex)
            {
                
            }
        }

        public async Task EditProduct(ProductModel product)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", product.Id));
                parameters.Add(new SqlParameter("@NamaBarang", product.NamaBarang));
                parameters.Add(new SqlParameter("@KodeBarang", product.KodeBarang));
                parameters.Add(new SqlParameter("@JumlahBarang", product.JumlahBarang));
                parameters.Add(new SqlParameter("@Tanggal", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                var data = await _Context.Database.ExecuteSqlRawAsync("EXEC UpdateProduct @Id, @NamaBarang, @KodeBarang, @JumlahBarang, @Tanggal", parameters);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteProduct(int Id)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", Id.ToString()));
                

                var data = await _Context.Database.ExecuteSqlRawAsync("EXEC DeleteProduct @Id", parameters);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
