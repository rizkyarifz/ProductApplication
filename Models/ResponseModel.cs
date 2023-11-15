using System.ComponentModel.DataAnnotations;

namespace ProductApplication.Models.Response
{
    public class ResponseModel
    {
        public int status_code { get; set; }
        public string message { get; set; }
    }
}
