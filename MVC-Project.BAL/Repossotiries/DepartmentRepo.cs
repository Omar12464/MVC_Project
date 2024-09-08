using Microsoft.EntityFrameworkCore;
using MVC_Project.BAL.Interfaces;
using MVC_Project.DAL.Data;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BAL.Repossotiries
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext _dbContext;
        public DepartmentRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.ToList();

        }

        public Department GetById(int id)
        {
            return 
            _dbContext.Departments.Find(id);
        }

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();

        }
    }
}
