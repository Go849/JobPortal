using System.ComponentModel.DataAnnotations;

namespace JobPortal2.Models
{
    public class JobProvider
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string EmailId  { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyWebsite { get; set; }
    }
}
