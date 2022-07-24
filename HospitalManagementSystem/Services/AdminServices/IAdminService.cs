using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Services.AdminServices
{
    public interface IAdminService
    {
        public Task<Admin>? GetByEmailIdAsync(string EmailId);
        public Task<ActionResult<Admin>?> CreateAsync(Admin admin);

    }
}
