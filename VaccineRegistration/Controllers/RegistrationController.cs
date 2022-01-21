using Microsoft.AspNetCore.Mvc;
using VaccineRegistration.Models;

namespace VaccineRegistration.Controllers
{
    public class RegistrationController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

       
    }
}
