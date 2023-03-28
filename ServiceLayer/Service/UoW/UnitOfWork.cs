﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using RepositoryLayer;
using ServiceLayer.Service.Contract;
using ServiceLayer.Service.Implementation;

namespace ServiceLayer.Service.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _appContext;

        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext app)
        {
            this._appContext = app;
        }

        public void Dispose()
        {

        }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if (hasCustomRepository)
            {
                var customRepo = _appContext.GetService<IRepository<TEntity>>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_appContext);
            }

            return (IRepository<TEntity>)_repositories[type];

        }
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            return _appContext.SaveChanges(ensureAutoHistory);
        }
    }
}
