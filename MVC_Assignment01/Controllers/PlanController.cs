using GymSystem.DAL.Contexts;
using GymSystem.DAL.Repositories.Classes;
using GymSystem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Assignment01.Models;

namespace MVC_Assignment01.Controllers
{
    public class PlanController : Controller
    {
        //Plan Repo || Test Plan Repo
        private readonly IPlanRepository planRepository;

       public PlanController(IPlanRepository _planRepository)
        {
            planRepository = _planRepository;
        }
        public async Task<IActionResult> Index()
        {

            var Plans = await planRepository.GetAll();
            return View(Plans);

        }
        public async Task<IActionResult> Details(int id)
        {
            var Plan = await planRepository.GetById(id);
            if (Plan == null)
        {
                return RedirectToAction(nameof(Index));

        }
            return View(Plan);
        }

     }
}
