
using System;
using System.ComponentModel.DataAnnotations;

namespace VaccineRegistration.Models
{
    public class VaccineRegistreeModel
    {   
        [Key]
        public int PatientId  { get; set; }

        [Required]
        [MinLength(1)]
        public string PatientName { get; set; }

        [Required]
        [MinLength(1)]
        public string PoB { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DoB { get; set; }

        public string NIK { get; set; }

        [Required]
        [MinLength(1)]
        public string Address { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        [Required]
        [MinLength(1)]
        public string VaccineType { get; set; }

        [Required]
        [MinLength(1)]
        public string VaccineDose { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime VaccineDate { get; set; }

    }
}
