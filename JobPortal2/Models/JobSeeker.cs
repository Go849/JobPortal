using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal2.Models
{
    public class JobSeeker
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string JobSeekerEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public string? Qualification { get; set; }
        [Required]
        public string? CV { get; set; }
       
        

        [NotMapped]
        public IFormFile UploadCV { get; set; }
    }
}
