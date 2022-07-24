using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Services.Consultaion
{
    public interface IConsultaionService
    {
        public Task<ActionResult<Consultation>?> GetByIdAsync(int id);
        public Task<ActionResult<Consultation>?> CreateAsync(Consultation consultation);
        public Task<string> DeleteById(int id);
        public List<Consultation> GetConsultationByDoctorId(int id);
        public List<Consultation> GetConsultationByPatientId(int id);
    }
}
