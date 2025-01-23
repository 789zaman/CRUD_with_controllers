using Microsoft.EntityFrameworkCore;
using WebApplication1.DM.Entities;

namespace WebApplication1.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { 

        }
        public DbSet<MstCourses> MstCourses { get; set; }   
        public DbSet<MstStudents> MstStudents {  get; set; }    
    }
}
