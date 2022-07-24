using HospitalManagementSystem.Controllers;
using HospitalManagementSystem.Model;
using HospitalManagementSystem.Services.AppoinmentServices;
using HospitalManagementSystem.Services.TokenManager;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Services.DoctorService;
namespace HospitalManagementSystemTests.Controller
{
    public class DoctorControllerTests
    {
        private readonly DoctorsController _sut;
        Mock<IDoctorService> Dbmock = new Mock<IDoctorService>();
        Mock<IAppoinmentService> appoinmentServiceMock = new Mock<IAppoinmentService>();
        Mock<IJWTTokenManager> jwtTokenMangerMock = new Mock<IJWTTokenManager>();
        public DoctorControllerTests()
        {
            _sut = new DoctorsController(jwtTokenMangerMock.Object, Dbmock.Object, appoinmentServiceMock.Object);
        }
        [Fact]
        public async Task  GetDoctor_Should_Return_Success()
        {
            Doctor expected_doctor = new Doctor
            {
                Id = 0,
                FirstName = "Ram",
                LastName = "kumar",
                Address = "Chennai",
                Age = 34,
                EmailId = "ramkumar@doctor.com",
                Gender = "Male",
                Password = "Password@123",
                specialityName = "Pediatrician",
                Role = "Doctor"
            };

            Dbmock.Setup(m => m.GetByIdAsync(expected_doctor.Id)).ReturnsAsync(expected_doctor);
            Doctor actualDoctor = await _sut.GetDoctor(expected_doctor.Id);

            Assert.Equal(expected_doctor, actualDoctor);
        }
    }
}
