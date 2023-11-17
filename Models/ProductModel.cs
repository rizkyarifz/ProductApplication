using System;
using System.ComponentModel.DataAnnotations;

namespace ProductApplication.Models.Product
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "NamaBarang is required.")]
        public string NamaBarang { get; set; }

        [Required(ErrorMessage = "KodeBarang is required.")]
        public string KodeBarang { get; set; }

        [Required(ErrorMessage = "JumlahBarang is required.")]
        public int JumlahBarang { get; set; }

        [Required(ErrorMessage = "Tanggal is required.")]
        public DateTime Tanggal { get; set; }
    }
}
