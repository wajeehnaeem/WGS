using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WGS.Models
{
    public class WgsDbContext : IdentityDbContext<User>
    {

        public WgsDbContext() : base("Server=(local);User ID=sa;Password=Wajeeh_ahmed93;Initial Catalog=WGSTESTING;Persist Security Info=true")
        {

        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Choice> Choices { get; set; }

        public static WgsDbContext Create()
        {
            return new WgsDbContext();
            
        }
    }


}