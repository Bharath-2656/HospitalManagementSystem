using HospitalManagementSystem.Model;
using HospitalManagementSystem.Services.DoctorService;
using HospitalManagementSystem.Services.TokenManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IJWTTokenManager _configuration;

        public DoctorsController(IJWTTokenManager congiguration, IDoctorService doctorService)
        {

            _configuration = congiguration;
            _doctorService = doctorService;
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

        [Authorize(Roles = "Doctor")]
        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _doctorService.GetAllAsync();
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            return await _doctorService.GetByIdAsync(id);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            //if (id != doctor.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(doctor).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DoctorExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            _doctorService.UpdateAsync(doctor);
            return NoContent();
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            if (doctor != null)
            {
                var UserCheck = await _doctorService.GetByEmailIdAsync(doctor.EmailId);
                if (UserCheck == null)
                {
                    if (new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(doctor.Password) && new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(doctor.EmailId))
                    {
                        var saltSecret = BCrypt.Net.BCrypt.GenerateSalt();
                        doctor.Password = BCrypt.Net.BCrypt.HashPassword(doctor.Password, saltSecret);
                        await _doctorService.CreateAsync(doctor);
                        return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);

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
        [HttpPatch]
        public async Task<IActionResult> PatchDoctor(int id, int age)
        {
            _doctorService.UpdateByAgeAsync(id, age);

            return Ok("User updated successfully");
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteDoctor(int id)
        {
            return await _doctorService.DeleteById(id);
        }



    }
}
