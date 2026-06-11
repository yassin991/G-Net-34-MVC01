using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Repositories.Classes
{
    public class PlanRepository :GenericRepository<Plan>, IPlanRepository
    {

        private readonly GYMDbContext dbContext ;
        public PlanRepository(GYMDbContext _dbContext):base (_dbContext)
        {
            dbContext = _dbContext;
        }
     
    }
}
