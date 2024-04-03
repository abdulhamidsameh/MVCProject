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
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private protected readonly MVCProjectDbContext _dbContext;
		
		public GenericRepository(MVCProjectDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Add(T entity)
			=> _dbContext.Set<T>().Add(entity);

		public void Delete(T entity)
			=> _dbContext.Set<T>().Remove(entity);

		public async Task<T> GetAsync(int id)
			=> await _dbContext.Set<T>().FindAsync(id);

		public async Task<IEnumerable<T>> GetAllAsync()
			=> await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		
		public void Update(T entity)
			=> _dbContext.Set<T>().Update(entity);

	}
}
