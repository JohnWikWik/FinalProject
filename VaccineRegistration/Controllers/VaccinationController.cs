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
            var vm = new ();
            vm.EmployeesList = new List<Employee>
            {
            new Employee { Id = 1, FullName = "Shyju" },
            new Employee { Id = 2, FullName = "Bryan" }
            };
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register([Bind("PatientId, PatientName, PoB, DoB, NIK, Address, Province, City, VaccineType, VaccineDose, VaccineDate")] VaccineRegistreeModel vaccineRegistree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccineRegistree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Register));
            }
            else
            {
                return View();
            }
        }
       
    }
}
