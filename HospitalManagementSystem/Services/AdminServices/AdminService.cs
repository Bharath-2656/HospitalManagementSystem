using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdminService> _logger;

        public AdminService(AppDbContext context, ILogger<AdminService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Admin>? GetByEmailIdAsync(string EmailId)
        {
            if (_context.Admins == null)
            {
                return null;
            }
            var admin = _context.Admins.FirstOrDefault(x => x.EmailId == EmailId);

            if (admin == null)
            {
                return null;
            }

            return admin;
        }
        public async Task<ActionResult<Admin>?> CreateAsync(Admin admin)
        {
            if (_context.Admins == null)
            {
                return null;
            }
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return await GetByEmailIdAsync(admin.EmailId);
        }
    }
}