using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Assignment01.DbContexts;
using MVC_Assignment01.Models;

namespace MVC_Assignment01.Controllers
{
    public class PlanController1(GYMDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var Plans = await _context.Plans.ToListAsync();
            return View(Plans);

        }
        public async Task<IActionResult> Details(int id)
        {
            var Plan = await _context.Plans.FirstOrDefaultAsync(x => x.Id == id);
            if (Plan == null)
        {
                return RedirectToAction(nameof(Index));

        }
            return View(Plan);
        }

     }
}
