using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Model
{
    public class Appoinment
    {
        public int Id { get; set; }
      
        public int PatientId { get; set; }
        public Speciality speciality { get; set; }
        public int DoctorId { get; set; }
        [Display(Name = "Appointment Date")]
        []
        public String AppointmentDate { get; set; }
        public string Problem { get; set; }
    }
}
