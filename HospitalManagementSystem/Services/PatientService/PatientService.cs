using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace HospitalManagementSystem.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PatientService> _logger;

        public PatientService(AppDbContext context, ILogger<PatientService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }

        public async Task<ActionResult<Patient>?> GetByIdAsync(int id)
        {
            if (_context.Patients == null)
            {
                return null;
            }
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return null;
            }

            return patient;
        }
        public async Task<Patient>? GetByEmailIdAsync(string EmailId)
        {
            if (_context.Patients == null)
            {
                return null;
            }
            var patient = _context.Patients.FirstOrDefault(x => x.EmailId == EmailId);

            if (patient == null)
            {
                return null;
            }

            return patient;
        }
        public async Task<ActionResult<Patient>?> CreateAsync(Patient patient)
        {
            if (_context.Patients == null)
            {
                return null;
            }
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(patient.Id);
            //return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        public async void UpdateAsync(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<string> DeleteById(int id)
        {
            if (_context.Patients == null)
            {
                return "No Patients are available to delete";
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return $"No patient found with id {id}";
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return "User deleted";
        }
        public async Task<Patient> getUserFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
            return await GetByEmailIdAsync(email);
        }


    }
}

