using HospitalManagementSystem.Model;
using HospitalManagementSystem.Services.AppoinmentService;
using HospitalManagementSystem.Services.DoctorService;
using HospitalManagementSystem.Services.PatientService;
using HospitalManagementSystem.Services.TokenManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly IPatientService _patientService;
        private readonly IJWTTokenManager _configuration;
        private readonly IAppoinmentService _appoinmentService;
        private readonly IDoctorService _doctorService;

        public PatientsController(IPatientService patientService, IDoctorService doctorService, IJWTTokenManager configuration, IAppoinmentService appoinmentService)
        {

            _patientService = patientService;
            _configuration = configuration;
            _appoinmentService = appoinmentService;
            _doctorService = doctorService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Patient>> login([FromBody] Login patient)
        {
            var users = await _patientService.GetByEmailIdAsync(patient.EmailId);

            if (users != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(patient.Password, users.Password);
                if (isValidPassword)
                {
                    var token = _configuration.Authenticate(patient.EmailId, users.Role);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Password must have 8 characters, one uppercase," +
                        " one lowercase, one special character and one digit");
                }
            }
            else
            {
                return BadRequest("user not found");
            }

        }

        // GET: api/Patients

        [HttpGet]
        public async Task<List<Patient>> GetPatients()
        {
            return await _patientService.GetAllAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return patient;
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _patientService.UpdateAsync(patient);

            return NoContent();

        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            if (patient != null)
            {
                var UserCheck = await _patientService.GetByEmailIdAsync(patient.EmailId);
                if (UserCheck == null)
                {
                    if (new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(patient.Password) && new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(patient.EmailId))
                    {
                        var saltSecret = BCrypt.Net.BCrypt.GenerateSalt();
                        patient.Password = BCrypt.Net.BCrypt.HashPassword(patient.Password, saltSecret);
                        await _patientService.CreateAsync(patient);
                        return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);

                    }
                    else
                    {
                        return BadRequest("Incoreect Email or password");
                    }
                }
                else
                {
                    return BadRequest("User already registered");
                }
            }
            else
            {
                return BadRequest("No details enetered");
            }

        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public Task<string> DeletePatient(int id)
        {
            return _patientService.DeleteById(id);
        }

        [HttpPost]
        [Authorize]
        [Route("BookAppoinment")]
        public async Task<IActionResult> BookAppointment(Appoinment appoinment)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var patient = _patientService.getUserFromToken(token).Result;
            appoinment.PatientId = patient.Id;
            if (appoinment.AppointmentDate == null)
                appoinment.AppointmentDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            var doctor = _doctorService.GetByIdAsync(appoinment.DoctorId).Result;
            if (doctor == null)
            {
                return NotFound($"The doctor with id {appoinment.DoctorId} is not found");
            }
            else if (doctor.Value.speciality != appoinment.speciality)
            {
                return NotFound("Doctor specified is not from the same speciality");
            }
            else
            {

                await _appoinmentService.CreateAsync(appoinment);
                return Ok("Appoinment allotted");
            }
        }

    }
}
