using HospitalManagementSystem.Model;
using HospitalManagementSystem.Services.AppoinmentServices;
using HospitalManagementSystem.Services.Consultaion;
using HospitalManagementSystem.Services.DoctorService;
using HospitalManagementSystem.Services.TokenManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppoinmentService _appoinmentSerivce;
        private readonly IJWTTokenManager _configuration;
        private readonly IConsultaionService _consultationService;

        public DoctorsController(IJWTTokenManager congiguration, IDoctorService doctorService, IAppoinmentService appoinmentService, IConsultaionService consultaionService)
        {

            _configuration = congiguration;
            _doctorService = doctorService;
            _appoinmentSerivce = appoinmentService;
            _consultationService = consultaionService;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Doctor>> login([FromBody] Login doctor)
        {
            var users = await _doctorService.GetByEmailIdAsync(doctor.EmailId);
            if (users != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(doctor.Password, users.Password);
                if (isValidPassword)
                {
                    var token = _configuration.Authenticate(doctor.EmailId, users.Role);
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

        [Authorize(Roles = "Doctor,Admin")]
        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _doctorService.GetAllAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<Doctor> GetDoctor(int id)
        {
            return await _doctorService.GetByIdAsync(id);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            _doctorService.UpdateAsync(doctor);
            return NoContent();
        }

        // POST: api/Doctors
        [HttpPost]
        [Authorize(Roles = "Doctor,Admin")]
        public string PostDoctor(Doctor doctor)
        {
            if (doctor != null)
            {
                var UserCheck = _doctorService.GetByEmailIdAsync(doctor.EmailId);
                if (UserCheck == null)
                {
                    if (new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(doctor.Password) && new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(doctor.EmailId))
                    {
                        var saltSecret = BCrypt.Net.BCrypt.GenerateSalt();
                        doctor.Password = BCrypt.Net.BCrypt.HashPassword(doctor.Password, saltSecret);
                        _doctorService.CreateAsync(doctor);
                        return "User created Successfully";
                    }
                    else
                    {
                        return ("Incoreect Email or password");
                    }
                }
                else
                {
                    return ("User already registered");
                }
            }
            else
            {
                return ("No details enetered");
            }

        }

        // PATCH: api/Doctors/
        [HttpPatch]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> PatchDoctor(int id, int age)
        {
            _doctorService.UpdateByAgeAsync(id, age);

            return Ok("User updated successfully");
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<string> DeleteDoctor(int id)
        {
            return await _doctorService.DeleteById(id);
        }

        [HttpGet]
        [Route("ActiveAppoiments")]
        [Authorize(Roles = "Doctor,Admin")]
        public List<Appoinment> ActiveAppoinments()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var doctor = _doctorService.getUserFromToken(token).Result;
            if (doctor != null)
            {
                List<Appoinment> appointment = _appoinmentSerivce.GetAppoinmentByDoctorId(doctor.Id);
                List<Appoinment> activeAppoinments = new List<Appoinment>();
                foreach (Appoinment appointmentItem in appointment)
                {
                    if (DateTime.Compare(DateTime.Parse(appointmentItem.AppointmentDate), DateTime.Now) > 0)
                    {
                        activeAppoinments.Add(appointmentItem);
                    }
                }
                return activeAppoinments;
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        [Route("Consultation")]
        public Consultation DoctorConsultation(Consultation consultation)
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var doctor = _doctorService.getUserFromToken(token).Result;
            if (doctor != null)
            {
                consultation.DoctorId = doctor.Id;
                _consultationService.CreateAsync(consultation);
                return consultation;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("ConsultationInfo")]
        public Consultation? GetConsultationInfo()
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var doctor = _doctorService.getUserFromToken(token).Result;
            if (doctor != null)
            {
                List<Consultation> consultations = _consultationService.GetConsultationByDoctorId(doctor.Id);
                var latestConsultation = consultations.FirstOrDefault();
                return latestConsultation;
            }
            else
            {
                return null;
            }
        }
    }
}
