using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Model
{
    public class Speciality
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
