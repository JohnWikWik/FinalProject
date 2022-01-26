
using System;
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

        [DataType(DataType.Date, ErrorMessage = "Date of Birth must be filled")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        [DobValidation(5, ErrorMessage = "Must be older than 5")]
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
        [IsGreaterDate(ErrorMessage = "Vaccine date must be greater than current date")]
        [Required(ErrorMessage = "Vaccine Date must be filled")]
        public DateTime VaccineDate { get; set; }

        public AnswerModel AnswerModel { get; set; }
    }
}