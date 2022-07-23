using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(AppDbContext context, ILogger<DoctorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            if(_context.Doctors == null)
            {
                return null;
            }
            var doctors = await _context.Doctors.ToListAsync();
            return doctors;
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            if (_context.Doctors == null)
            {
                return null;
            }
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return null;
            }

            return doctor;
        }

        public async Task<Doctor>? GetByEmailIdAsync(string EmailId)
        {
            if (_context.Doctors == null)
            {
                return null;
            }
            var doctor = _context.Doctors.FirstOrDefault(x => x.EmailId == EmailId);

            if (doctor == null)
            {
                return null;
            }

            return doctor;
        }

        public async void UpdateAsync(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<Doctor>?> CreateAsync(Doctor doctor)
        {
            if (_context.Doctors == null)
            {
                return null;
            }
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(doctor.Id);
        }
        public async Task<string> DeleteById(int id)
        {
            if (_context.Doctors == null)
            {
                return "No Doctors are available to delete";
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return $"No Doctor found with id {id}";
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return "User deleted";
        }
        public async void UpdateByAgeAsync(int id, int age)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(i => i.Id == id);
            if (doctor != null)
            {
                doctor.Age = age;
                _context.Entry(doctor).State = EntityState.Modified;


                await _context.SaveChangesAsync();

            }
            else
            {
                _logger.LogInformation($"No user found with the id {id}");
                
            }

            _logger.LogInformation("User updated successfully");
        }
    }
        
    }

