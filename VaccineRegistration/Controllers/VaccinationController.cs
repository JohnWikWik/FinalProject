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
                var VaccineDose = vaccineRegistree.VaccineDose;
                if(VaccineDose.Equals("1st Dose"))
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
                //await _context.SaveChangesAsync();
            var PatientId = answerModel.PatientId;
            return RedirectToAction("RegisterUpdate", new { PatientId = PatientId });
        }

        public IActionResult RegisterUpdate(int PatientId)
        {
            VaccineRegistreeModel regis = new VaccineRegistreeModel();
            regis.PatientId = PatientId;
            return View(regis);
        }



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
                new DataColumn("VaccineDate")
            });

            var patients = from patient in _context.Patient.Take(10) select patient;

            foreach (var patient in patients)
            {
                regis.Rows.Add(patient.PatientId, patient.PatientName, patient.PoB, patient.DoB, patient.NIK
                    , patient.Address, patient.Province, patient.City, patient.VaccineType
                    , patient.VaccineDose, patient.VaccineDate);
            }

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
        }


    }
}
