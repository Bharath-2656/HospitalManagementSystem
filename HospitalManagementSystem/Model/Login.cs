using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Model
{
    public class Login
    {
        [Key]
        public string EmailId { get; set; }
        public string Password { get; set; }
      
    }
}
