using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MVCProject.BLL.Interfaces;
using MVCProject.BLL.Repositories;
using MVCProject.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL
{ 
    public class UnitOfWork : IUnitOfWork
	{
		private readonly MVCProjectDbContext _dbContext;

		public UnitOfWork(MVCProjectDbContext dbContext)
		{
			EmployeeRepository = new EmployeeRepository(dbContext);
			DepartmentRepository = new DepartmentRepository(dbContext);
			_dbContext = dbContext;
		}
		public IEmployeeRepository EmployeeRepository { get; set; }
		public IDepartmentRepository DepartmentRepository { get; set; }
		public int Complete()
		{
			return _dbContext.SaveChanges();
		}
		public void Dispose() 
		{
			_dbContext.Dispose();
		}
	}
}
