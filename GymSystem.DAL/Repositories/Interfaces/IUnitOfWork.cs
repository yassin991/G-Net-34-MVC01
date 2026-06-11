
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        //unitOfWork.GetRepos<Member>().GetALLC);

public IGenericrepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();


public Task<int> CompeleteAsync();






    }
}
