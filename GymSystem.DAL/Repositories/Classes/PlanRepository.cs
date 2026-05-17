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
    public class PlanRepository : IPlanRepository
    {

        private readonly GYMDbContext dbContext ;
        public PlanRepository(GYMDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Plan>> GetAll(bool isTracked, CancellationToken ct = default)
        {

            var Plans = isTracked ? dbContext.Plans : dbContext.Plans.AsNoTracking();

            return await Plans.ToListAsync();
        }
        public void Add(Plan plan)
        {
            dbContext.Plans.Add(plan);
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var Product = dbContext.Plans.FirstOrDefault(p => p.Id == id);
            if (Product != null)
                dbContext.Plans.Remove(Product);
        }

        public async Task<IEnumerable<Plan>> GetAll()
        {
          return await dbContext.Plans.ToListAsync();
        }

        public async Task<Plan?> GetById(int id, CancellationToken ct = default)
        {

            var Plan = await dbContext.Plans.FirstOrDefaultAsync(p => p.Id == id);
            return Plan;
        }
        public void Update(Plan plan)
        {
            dbContext.Plans.Update(plan);
        }
    }
}
