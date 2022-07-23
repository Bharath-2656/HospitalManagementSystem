using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Services.AppoinmentService
{
    public interface IAppoinmentService
    {
        public Task<List<Appoinment>> GetAllAsync();
        public Task<ActionResult<Appoinment>?> GetByIdAsync(int id);
        public Task<ActionResult<Appoinment>?> CreateAsync(Appoinment appoinment);
        public Task<string> DeleteById(int id);

    }
}
