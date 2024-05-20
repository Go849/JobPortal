using System.ComponentModel.DataAnnotations;

namespace JobPortal2.Models
{
    public class Job
    {
        [Key]
        public int jobid { get; set; }
        [Required]
        [StringLength(100)]
        public string? JobDescription { get; set; }
        [Required]
        [StringLength(100)]
        public string?category { get; set; }
        [Required]
        [StringLength(100)]
        public string? ExperienceRequired { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ApplyDate { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }


    }
}
