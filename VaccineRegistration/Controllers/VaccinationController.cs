using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VaccineRegistration.Models;


using System.IO;
using System.Data;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;


namespace VaccineRegistration.Controllers
{
    public class VaccinationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccinationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register(int PatientId = 0)
        {
            VaccineRegistreeModel  regis = new VaccineRegistreeModel();
            if(PatientId == 0)
            {
                return View(regis);
            }
            else
            {
                regis = _context.Patient.Where(_context => _context.PatientId == PatientId).FirstOrDefault();
                return View(regis);
            }
        }
     
        [HttpPost]

        public async Task<IActionResult> Register([Bind("PatientId, PatientName, PoB, DoB, NIK, Address, Province, City, VaccineType, VaccineDose, VaccineDate")] VaccineRegistreeModel vaccineRegistree)
        {


            if (ModelState.IsValid)
            {
                if (vaccineRegistree.PatientId != 0)
                {
                    _context.Update(vaccineRegistree);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Add(vaccineRegistree);
                    await _context.SaveChangesAsync();
                }
                var PatientId = vaccineRegistree.PatientId;
                var VaccineDose = vaccineRegistree.VaccineDose;
                if (VaccineDose.Equals("1st Dose"))
                {
                    return RedirectToAction("Ques", new { PatientId = PatientId });
                }
                else
                {
                    return RedirectToAction("Ques2", new { PatientId = PatientId });
                }
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

        public IActionResult Ques2(int PatientId)
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


        //backbutton pharsing PatientId
        public async Task<IActionResult> BackButton([Bind("Id, PatientId, isAllergies, isAutoimmune, isMedication, isImmunosuppressant, isHeartdisease, isDiabetes, isHypertension, isCovid")] AnswerModel answerModel)
        {
            _context.Add(answerModel);
            var PatientId = answerModel.PatientId;
            return RedirectToAction("Register", new { PatientId = PatientId });
        }

        /*
        public IActionResult RegisterUpdate(int PatientId)
        {
            VaccineRegistreeModel regis = new VaccineRegistreeModel();
            regis.PatientId = PatientId;
            return View(regis);
        }
        */

        /*
        [HttpPost]

        public async Task<IActionResult> UpdateData([Bind("PatientId, PatientName, PoB, DoB, NIK, Address, Province, City, VaccineType, VaccineDose, VaccineDate")] VaccineRegistreeModel vaccineRegistree)
        {

            if (ModelState.IsValid)
            {
                _context.Update(vaccineRegistree);
                await _context.SaveChangesAsync();
                var PatientId = vaccineRegistree.PatientId;
                var VaccineDose = vaccineRegistree.VaccineDose;
                if (VaccineDose.Equals("1st Dose"))
                {
                    return RedirectToAction("Ques", new { PatientId = PatientId });
                }
                else
                {
                    return RedirectToAction("Ques2", new { PatientId = PatientId });
                }
            }
            else
            {
                _context.Add(vaccineRegistree);
                //await _context.SaveChangesAsync();
                var PatientId = vaccineRegistree.PatientId;
                return View(new { PatientId = PatientId });
            }
        }
        */



        [HttpPost]
        public IActionResult ExportRegis()
        {
            DataTable regis = new DataTable("Grid");
            regis.Columns.AddRange(new DataColumn[] {
                new DataColumn("PatientId"),
                new DataColumn("PatientName"),
                new DataColumn("PoB"),
                new DataColumn("DoB"),
                new DataColumn("NIK"),
                new DataColumn("Address"),
                new DataColumn("Province"),
                new DataColumn("City"),
                new DataColumn("VaccineType"),
                new DataColumn("VaccineDose"),
                new DataColumn("VaccineDate"),
                new DataColumn("isAllergies"),
                new DataColumn("isAutoimmune"),
                new DataColumn("isMedication"),
                new DataColumn("isImmunosuppressant"),
                new DataColumn("isHeartdisease"),
                new DataColumn("isDiabetes"),
                new DataColumn("isHypertension"),
                new DataColumn("isCovid")
            });

            //   var patients = from patient in _context.Patient.Take(10) select patient;
            //   var questionaires = from questionaire in _context.Questionaire.Take(10) select questionaire;
            var Registration = from p in _context.Patient join q in _context.Questionaire on p.PatientId equals q.PatientId select new 
            {
                PatientId = p.PatientId,
                PatientName = p.PatientName,
                PoB = p.PoB,
                DoB = p.DoB,
                NIK = p.NIK,
                Address = p.Address,
                Province = p.Province,
                City = p.City,
                VaccineType = p.VaccineType,
                VaccineDose = p.VaccineDose,
                VaccineDate = p.VaccineDate,
                Allergy = q.isAllergies,
                Autoimmune = q.isAutoimmune,
                Medication = q.isMedication,
                Immunosuppresant = q.isImmunosuppressant,
                HeartDisease = q.isHeartdisease,
                Diabetes = q.isDiabetes,
                Hypertension = q.isHypertension,
                Covid = q.isCovid
            };
            
    
            foreach (var p in Registration)
            {
                regis.Rows.Add(p.PatientId, p.PatientName, p.PoB, p.DoB, p.NIK
                    , p.Address, p.Province, p.City, p.VaccineType
                    , p.VaccineDose, p.VaccineDate, p.Allergy, p.Autoimmune, p.Medication, p.Immunosuppresant, p.HeartDisease, p.Diabetes, p.Hypertension, p.Covid);
            }
           
            /*
            foreach (var q in Registration)
            {
                regis.Rows.Add(q.Allergy, q.Autoimmune, q.Medication, q.Immunosuppresant, q.HeartDisease, q.Diabetes, q.Hypertension, q.Covid);
            }
            */
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(regis);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Registration.xlsx");
                }
            }
        }
        
        /*
        [HttpPost]
        public IActionResult ExportQues()
        {
            {
                DataTable ques = new DataTable("Grid");
                ques.Columns.AddRange(new DataColumn[] {
                new DataColumn("Id"),
                new DataColumn("PatientId"),
                new DataColumn("isAllergies"),
                new DataColumn("isAutoimmune"),
                new DataColumn("isMedication"),
                new DataColumn("isImmunosuppressant"),
                new DataColumn("isHeartdisease"),
                new DataColumn("isDiabetes"),
                new DataColumn("isHypertension"),
                new DataColumn("isCovid")
                
            });

                var questions = from question in _context.Questionaire.Take(10) select question;

                foreach (var question in questions)
                {
                    ques.Rows.Add(question.Id, question.PatientId, question.isAllergies, question.isAutoimmune, question.isMedication
                        , question.isImmunosuppressant, question.isHeartdisease, question.isDiabetes, question.isHypertension
                        , question.isCovid);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ques);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Quesioner.xlsx");
                    }
                }
            }
        }*/


    }
}
