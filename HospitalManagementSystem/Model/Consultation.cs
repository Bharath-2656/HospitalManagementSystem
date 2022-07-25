namespace HospitalManagementSystem.Model
{
    public class Consultation
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Symptoms { get; set; } = default!;
        public string Diagnosis { get; set; }
        public string Medicine { get; set; }
    }
}
