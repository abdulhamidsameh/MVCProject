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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(MVCProjectDbContext dbContext):base(dbContext)
        {
            
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower()).AsNoTracking();
        }
		public new IEnumerable<Employee> GetAll()
		{
			return _dbContext.Set<Employee>().AsNoTracking().Include(E => E.Department).ToList();
		}
        public IQueryable<Employee> SearchByName(string searchInput)
        {
			return _dbContext.Employees.Where(E => E.Name.Contains(searchInput)).AsNoTracking().Include(E => E.Department);
		}
	}
}
  