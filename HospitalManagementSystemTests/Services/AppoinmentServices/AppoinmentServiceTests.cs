//using HospitalManagementSystem.Model;
//using HospitalManagementSystem.Services.AppoinmentServices;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace HospitalManagementSystemTests.Services.AppoinmentServices
//{
//    public class AppoinmentServiceTests
//    {
//        private readonly IAppoinmentService _sut;
//        Mock<AppDbContext> Dbmock = new Mock<AppDbContext>();
//        Mock<ILogger<AppoinmentService>> loggerMock = new Mock<ILogger<AppoinmentService>>();

//        public AppoinmentServiceTests()
//        {
//            _sut = new AppoinmentService(Dbmock.Object,loggerMock.Object);
//        }

//        public async void rep()
//        {


//            Doctor doctor = new Doctor { Id = 0, FirstName = "Ram", LastName = "kumar", Address = "Chennai", Age = 34,
//                EmailId = "ramkumar@doctor.com", Gender = "Male", Password = "Password@123", speciality = "Pediatrician",
//                Role = "Doctor" };

//           Dbmock.Setup(m => m.Get

//        }
//    }
//}
