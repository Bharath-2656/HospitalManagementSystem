using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Admin";
    }
}
