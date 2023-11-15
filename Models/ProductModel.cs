using System.ComponentModel.DataAnnotations;

namespace ProductApplication.Models.Product
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string NamaBarang { get; set; }
        public string KodeBarang { get; set; }
        public int JumlahBarang { get; set; }
        public DateTime Tanggal { get; set; }
    }
}
