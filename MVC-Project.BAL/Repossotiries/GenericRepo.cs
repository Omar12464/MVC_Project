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
    public class GenericRepo<T> : IGenericRepo<T> where T : ModelBase
    {
        private protected readonly AppDbContext _context;


        public GenericRepo(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Delete(T item)
        {
            _context.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return(IEnumerable<T>) _context.Set<Employee>().Include(E=>E.department).ToList();

            }
            else
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }
    }
}
