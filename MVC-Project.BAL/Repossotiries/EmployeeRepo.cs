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
    public class EmployeeRepo : GenericRepo<Employee> ,IEmployeeRepo
    {
        //private readonly AppDbContext _dbContext;
        public EmployeeRepo(AppDbContext appDbContext):base(appDbContext)
        {
            //_dbContext = appDbContext;
        }

        public IQueryable<Employee> GetEmployeeAddress(string address)
        {
            return _context.Employees.Where(E => E.Address.ToLower().Contains(address.ToLower()));
        }
    }
}
