using GymSystem.BLL.Services.Interfaces;
using GymSystem.BLL.ViewModels.MemberViewModels;
using GymSystem.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
//using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace MVC_Assignment01.Controllers
{
    public class MemberController : Controller
    {
        private IMemberServices memberServices;

        public MemberController(IMemberServices memberServices)
        {
            this.memberServices = memberServices;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var tempResult = TempData["Result"];
            var Members = await memberServices.GetAllMembersAsync(ct);
            return View(Members);
        }
        [HttpGet]

        public IActionResult Create ()
      {
           return View(); 
      }
        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model,CancellationToken ct)
        {



            if (!ModelState.IsValid) return View(nameof(Create), model);

           var Result= await memberServices.CreateMemberAsync(model, ct);
            if(Result)
            TempData["Success"] = "Member Created Successfully";
                 else
            TempData["Failed"] = "Failed to Create Member";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
            var member = await memberServices.GetMemberDetailsAsync(id, ct);

            if (member is null)
            {
                TempData["ErrorMessage"] = " Member Not Found";
                return RedirectToAction(nameof(Index));
            }
          return  View(member);
        }

        [HttpGet]

        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        {
            var healthRecord = await memberServices.GetMemberHealthRecordAsync(id, ct);
            if (healthRecord is null)
            {
                TempData["ErrorMessage"] = " Health Record Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);

        }


    }
}
