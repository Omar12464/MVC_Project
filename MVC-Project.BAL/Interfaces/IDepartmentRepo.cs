using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BAL.Interfaces
{
    public interface IDepartmentRepo:IGenericRepo<Department>
    {
        public IQueryable<Department> GetDepartmentName(string searchInp);
    }
}
