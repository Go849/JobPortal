using Microsoft.EntityFrameworkCore;

namespace JobPortal2.Models
{
    public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }
        public DbSet<JobSeeker> Seekers { get; set; }
        public DbSet<Job> Jobs {  get; set; }
        public DbSet<JobProvider> Providers { get; set; }
        public DbSet<Apply>Applies { get; set; }

    }
}
