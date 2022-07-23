using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Services.PatientService
{
    public interface IPatientService
    {
        public Task<List<Patient>> GetAllAsync();
        public Task<ActionResult<Patient>?> GetByIdAsync(int id);
        public Task<Patient>? GetByEmailIdAsync(string EmailId);
        public Task<ActionResult<Patient>?> CreateAsync(Patient patient);
        public void UpdateAsync(Patient patient);
        public Task<string> DeleteById(int id);
        public Task<Patient> getUserFromToken(string token);
    }
}
