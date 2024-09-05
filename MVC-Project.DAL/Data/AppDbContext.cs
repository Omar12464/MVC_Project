using Microsoft.EntityFrameworkCore;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.DAL.Data
{
    internal class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; DataBase=C42_MVC; Trusted_Connection=True; MultipleActiveResultSets = True"); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Apis
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments { get; set; }
    }
}
