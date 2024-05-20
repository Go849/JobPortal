using System.ComponentModel.DataAnnotations;

namespace JobPortal2.Models
{
    public class Apply
    {
        [Key]
        public int Applyid { get; set; }
         public string JobSeekerEmail { get; set; }
       











        public DateTime DateofApply { get; set; }
        [Required]
        public int Jobid { get; set; }
    }
}
