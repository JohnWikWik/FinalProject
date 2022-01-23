
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaccineRegistration.Models
{
    public class VaccineRegistreeModel
    {   
        [Key]
        public int PatientId  { get; set; }

        [Required(ErrorMessage = "Patient Name is required.")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Place of Birth Name is required.")]
        public string PoB { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "NIK Must Be Numeric and 16-Digit")]
        [MinLength(16, ErrorMessage = "NIK Must Be Numeric and 16-Digit")]
        [MaxLength(16, ErrorMessage = "NIK Must Be Numeric and 16-Digit")]
        public string NIK { get; set; }

        [Required(ErrorMessage = "Address Name is required")]
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

        //public AnswerModel AnswerModel { get; set; }
        public List<AnswerModel> AnswerModel { get; set;}
    }
}