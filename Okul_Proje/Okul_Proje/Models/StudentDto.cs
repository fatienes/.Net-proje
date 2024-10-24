using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Okul_Proje.Models
{
    public class StudentDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int tcNumber { get; set; }  
        [Required]
        public int Number { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }
        [Required]
        public string classroom { get; set; }

       
    }
}
