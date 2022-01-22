
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

        [Required]
        public string PoB { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime DoB { get; set; }

        [Required]
        [MinLength(16)]
        [MaxLength(16)]
        public string NIK { get; set; }

        [Required]
        public string Address { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        [Required]
        public string VaccineType { get; set; }

        [Required]
        public string VaccineDose { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime VaccineDate { get; set; }

        public AnswerModel AnswerModel { get; set; }
    }
}