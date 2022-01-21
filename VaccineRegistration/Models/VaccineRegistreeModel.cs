
using System;
using System.ComponentModel.DataAnnotations;

namespace VaccineRegistration.Models
{
    public class VaccineRegistreeModel
    {   
        [Key]
        public int PatientId  { get; set; }
        [Required]
        public string PatientName { get; set; }
        public string PoB { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DoB { get; set; }

        public string NIK { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string VaccineType { get; set; }

        public string VaccineDose { get; set; }

        public DateTime VaccineDate { get; set; }



    }
}
