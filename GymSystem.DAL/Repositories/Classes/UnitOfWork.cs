using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Repositories.Classes
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GYMDbContext dbContext;
        private readonly Dictionary<string, object> _Repos = [];
        public UnitOfWork(GYMDbContext dbContext) 
        {
           dbContext =dbContext;
        }
        public async Task<int> CompeleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public IGenericrepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var TypeName = typeof(TEntity).Name;//String Key

            if (_Repos.TryGetValue(TypeName, out object OldRepository))
                return (IGenericrepository<TEntity>)OldRepository;


            var NewRepository = new GenericRepository<TEntity>(dbContext);

            _Repos[TypeName] = NewRepository;

            return NewRepository; 
        }
    }
}
