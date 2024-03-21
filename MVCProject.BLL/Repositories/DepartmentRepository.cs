using Microsoft.EntityFrameworkCore;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Data;
using MVCProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly MVCProjectDbContext _dbContext;
        public DepartmentRepository(MVCProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Department> GetAll()
            => _dbContext.Departments.AsNoTracking().ToList();
        public Department Get(int id)
            => _dbContext.Departments.Find(id); 
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges(); 
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
