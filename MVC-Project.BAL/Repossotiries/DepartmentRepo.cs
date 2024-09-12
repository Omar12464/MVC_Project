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
    public class DepartmentRepo : GenericRepo<Department>,IDepartmentRepo
    {
        //private readonly AppDbContext _dbContext;
        public DepartmentRepo(AppDbContext dbContext):base(dbContext)
        {
            //_dbContext = dbContext;

        }


    }
}
