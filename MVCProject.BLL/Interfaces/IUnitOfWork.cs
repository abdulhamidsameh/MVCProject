﻿using MVCProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
	{
        IGenericRepository<T> Repository<T>() where T : ModelBase;
        int Complete();
    }
}
