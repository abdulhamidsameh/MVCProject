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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MVCProjectDbContext _dbContext;
        public EmployeeRepository(MVCProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Employee> GetAll()
            => _dbContext.Employees.AsNoTracking().ToList();
        public Employee Get(int id)
            => _dbContext.Employees.Find(id);
        public int Add(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Employee entity) 
        {
            _dbContext.Employees.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
