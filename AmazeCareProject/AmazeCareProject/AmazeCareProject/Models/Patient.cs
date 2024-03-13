using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key] 
        public int PatientId { get; set; }   
        public string FullName {  get; set; }
        public DateTime DOB { get; set; }
        public string? Gender {  get; set; }
        public string ContactNumber {  get; set; }
        public string UserName {  get; set; }
        public string Password {  get; set; }

    }
}
