using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineRegistration.Models
{
    public class AnswerModel
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public VaccineRegistreeModel VaccineRegistree { get; set; }
        [Required]
        public string isAllergies { get; set; }
        public string isAutoimmune { get; set; }
        public string isMedication { get; set; }
        public string isImmunosuppressant { get; set; }
        public string isHeartdisease { get; set; }
        public string isDiabetes { get; set; }
        public string isHypertension { get; set; }
        public string isCovid { get; set; }
    }
}
