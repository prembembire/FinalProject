using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("MedicalRecords")]
    public class MedicalRecords
    {
        [Key]
        public int RecordId { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? AppointmentId { get; set; }
        public string Symptoms { get; set; }
        public string PhysicalExamination {  get; set; }
        public string TreatmentPlan {  get; set; }
        public string TestsRecommended {  get; set; }   
        public string Prescription {  get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        [ForeignKey("DoctorId")]
        public Doctors? Doctors { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointments? Appointments { get; set; }
    }
}
