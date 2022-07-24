using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Services.DoctorService
{
    public interface IDoctorService
    {
        public Task<List<Doctor>> GetAllAsync();
        public Task<Doctor>? GetByIdAsync(int id);
        public Task<Doctor>? GetByEmailIdAsync(string EmailId);
        public Task<ActionResult<Doctor>?> CreateAsync(Doctor doctor);
        public void UpdateAsync(Doctor doctor);
        public void UpdateByAgeAsync(int id, int age);
        public Task<string> DeleteById(int id);
        public Task<Doctor> getUserFromToken(string token);

    }
}
