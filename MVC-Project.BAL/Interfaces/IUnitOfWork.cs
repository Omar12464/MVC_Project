using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeeRepo employee { get; set; }
        public IDepartmentRepo department { get; set; }

       public int complete();

    }
}
