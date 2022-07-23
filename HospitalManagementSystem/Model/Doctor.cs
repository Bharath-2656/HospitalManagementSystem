using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Model
{
    public class Doctor
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
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public Speciality speciality { get; set; }
        public string Role { get; set; } = "Doctor";
    }
}
