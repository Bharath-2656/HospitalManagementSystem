using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services.AppoinmentService
{
    public class AppoinmentService: IAppoinmentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AppoinmentService> _logger;

        public AppoinmentService(AppDbContext context, ILogger<AppoinmentService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Appoinment>> GetAllAsync()
        {
            if (_context.Appoinments == null)
            {
                return null;
            }
            var appoinments = await _context.Appoinments.ToListAsync();
            return appoinments;
        }

        public async Task<ActionResult<Appoinment>?> GetByIdAsync(int id)
        {
            if (_context.Appoinments == null)
            {
                return null;
            }
            var appoinment = await _context.Appoinments.FindAsync(id);

            if (appoinment == null)
            {
                return null;
            }

            return appoinment;
        }
        public async Task<ActionResult<Appoinment>?> CreateAsync(Appoinment appoinment)
        {
            if (_context.Appoinments == null)
            {
                return null;
            }
            _context.Appoinments.Add(appoinment);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(appoinment.Id);
        }
        public async Task<string> DeleteById(int id)
        {
            if (_context.Appoinments == null)
            {
                return "No Appoinments are available to delete";
            }
            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment == null)
            {
                return $"No Appoinment found with id {id}";
            }

            _context.Appoinments.Remove(appoinment);
            await _context.SaveChangesAsync();
            return "User deleted";
        }
    }
}
