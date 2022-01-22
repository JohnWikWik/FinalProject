using Microsoft.AspNetCore.Http;
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
        public IActionResult Register2()
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
                var patientId = vaccineRegistree.PatientId;
                return View(Quesionaire(patientId));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Quesionaire(int? PatientId)
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Questionaire([Bind("Id, PatientId, isAllergies, isAutoimune, isImmunosuppresant, isHeartdisease, isDiabetes, isHypertension, isCovid")] AnswerModel answerModel)
        {

            if (ModelState.IsValid)
            {
                _context.Add(answerModel);
                await _context.SaveChangesAsync();
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}
