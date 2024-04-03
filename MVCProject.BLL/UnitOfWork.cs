using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MVCProject.BLL.Interfaces;
using MVCProject.BLL.Repositories;
using MVCProject.DAL.Data;
using MVCProject.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MVCProjectDbContext _dbContext;
		private Hashtable _repositories;

		public UnitOfWork(MVCProjectDbContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new Hashtable();
		}

		public IGenericRepository<T> Repository<T>() where T : ModelBase
		{
			var key = typeof(T).Name;
			if (!_repositories.ContainsKey(key))
			{
				if (key == nameof(Employee))
				{
					var repository = new EmployeeRepository(_dbContext);
					_repositories.Add(key, repository);
				}
				else
				{
					var repository = new GenericRepository<T>(_dbContext);
					_repositories.Add(key, repository);
				}
			}
			return _repositories[key] as IGenericRepository<T>;
		}

		public async Task<int> Complete()
			=> await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
		{
			await _dbContext.DisposeAsync();
		}

	}
}
