using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Model
{
    public class Patient
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(150)]
        public string Address { get; set; }
        [Required]
        public int Weight { get; set; }
        public string BloodType { get; set; }
        public string? DiseasesHistory { get; set; }
        public int? DoctorId { get; set; }
        public string? Role { get; set; } = "Patient";
    }
}
