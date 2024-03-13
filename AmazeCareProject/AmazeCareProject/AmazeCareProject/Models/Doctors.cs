using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Doctor")]
    public class Doctors
    {
        [Key]
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int ExperienceYears { get; set; }
        public string Qualification { get; set; }
        public string Designation { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
