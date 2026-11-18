using College.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using College.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Extensions.Configuration;


namespace College.Data
{
    public class DataContext :DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Student> Students { get;  set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(student => student.User)
                .WithOne(user => user.Student)
                .HasForeignKey<Student>(student => student.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            
        }
    


    }

}

