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
            _context.Add(vaccineRegistree);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Register));
        }
       
    }
}
