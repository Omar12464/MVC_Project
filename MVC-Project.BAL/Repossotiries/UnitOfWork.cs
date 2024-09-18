using MVC_Project.BAL.Interfaces;
using MVC_Project.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BAL.Repossotiries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbcontext;

        public UnitOfWork(AppDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
            employee = new EmployeeRepo(dbcontext);
            department = new DepartmentRepo(dbcontext);
        }
        public IEmployeeRepo employee { get ; set ; }
        public IDepartmentRepo department { get; set ; }

        public int complete()
        {
           return dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            dbcontext.Dispose();
        }
    }
}
