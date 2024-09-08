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
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _dbContext;
        public EmployeeRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public int Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();

        }

        public Employee GetById(int id)
        {
            return
            _dbContext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            return _dbContext.SaveChanges();

        }
    }
}
