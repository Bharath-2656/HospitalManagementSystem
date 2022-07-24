using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services.Consultaion
{
    public class ConsultationService : IConsultaionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ConsultationService> _logger;

        public ConsultationService(AppDbContext context, ILogger<ConsultationService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ActionResult<Consultation>?> GetByIdAsync(int id)
        {
            if (_context.Consultations == null)
            {
                return null;
            }
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
            {
                return null;
            }

            return consultation;
        }
        public async Task<ActionResult<Consultation>?> CreateAsync(Consultation consultation)
        {
            if (_context.Consultations == null)
            {
                return null;
            }
            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(consultation.Id);
        }
        public async Task<string> DeleteById(int id)
        {
            if (_context.Consultations == null)
            {
                return "No Consultations are available to delete";
            }
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return $"No Consultation found with id {id}";
            }

            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();
            return "User deleted";
        }
        public List<Consultation> GetConsultationByDoctorId(int id)
        {
            if (_context.Consultations == null)
            {
                return null;
            }
            var consultation = _context.Consultations.FromSqlRaw("SELECT * FROM hospital.consultations")
                .Where(m => m.DoctorId == id)
                .OrderByDescending(b => b.Id)
                .ToList();

            if (consultation == null)
            {
                return null;
            }
           
            return consultation;
        }
        public List<Consultation> GetConsultationByPatientId(int id)
        {
            if (_context.Consultations == null)
            {
                return null;
            }
            var consultation = _context.Consultations.FromSqlRaw("SELECT * FROM hospital.consultations")
                .Where(m => m.PatientId == id)
                .OrderByDescending(b => b.Id)
                .ToList();

            if (consultation == null)
            {
                return null;
            }
           
            return consultation;
        }
    }
}
