using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VaccineRegistration.Models;

namespace VaccineRegistration.Controllers
{
    public class QuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Ques()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Ques([Bind("Id, PatientId, isAllergies, isAutoimmune, isMedication, isImmunosuppressant, isHeartdisease, isDiabetes, isHypertension, isCovid")] AnswerModel answerModel)
        {

            if (ModelState.IsValid)
            {
                _context.Add(answerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(answerModel));
            }
            else
            {
                return View();
            }
        }
    }
}
