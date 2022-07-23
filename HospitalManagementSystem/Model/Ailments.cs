using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Model
{
    public class Ailments
    {

        public int Id { get; set; }
        [Key]
        public string AilmentName { get; set; }
    }
}
