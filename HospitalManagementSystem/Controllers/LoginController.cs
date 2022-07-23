using HospitalManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Patient>> login(Patient user)
        {
            var users = await _context.Patients.FirstOrDefaultAsync(e => e.EmailId == user.EmailId);
            if (user != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound("user not found");
            }
            //var userData = new List<User>();
            //using (var context = new corporatetutorialmanagementContext())
            //{
            //    userData = await context.Users.FindAsync(user);
            //}
        }
    }
}
