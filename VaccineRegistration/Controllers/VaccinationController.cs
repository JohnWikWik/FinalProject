using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VaccineRegistration.Models;

namespace VaccineRegistration.Controllers
{
    public class VaccinationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccinationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
     
        [HttpPost]

        public async Task<IActionResult> Register([Bind("PatientId, PatientName, PoB, DoB, NIK, Address, Province, City, VaccineType, VaccineDose, VaccineDate")] VaccineRegistreeModel vaccineRegistree)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(vaccineRegistree);
                await _context.SaveChangesAsync();
                var PatientId = vaccineRegistree.PatientId;
                return RedirectToAction("Ques", new {PatientId = PatientId});
            }
            else
            {
                return View();
            }
        }

        public IActionResult Ques(int PatientId)
        {
            AnswerModel answer = new AnswerModel();
            answer.PatientId = PatientId;
            return View(answer);
        }


        [HttpPost]

        public async Task<IActionResult> Ques([Bind("Id, PatientId, isAllergies, isAutoimmune, isMedication, isImmunosuppressant, isHeartdisease, isDiabetes, isHypertension, isCovid")] AnswerModel answerModel)
        {

            if (ModelState.IsValid)
            { 
                _context.Add(answerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
