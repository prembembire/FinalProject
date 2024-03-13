using AmazeCareProject.Controllers;
using AmazeCareProject.Data;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private IConfigurationRoot config;
        [SetUp]
        public void Setup()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }


        [Test]
        public async Task GetPatientById()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var patientsController = new PatientsController(context);
            var result = await patientsController.GetPatient();
            Assert.IsNotNull(result);
        }



        [Test]
        public async Task GetPatientByIdTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var patientsController = new PatientsController(context);
                var getById = await context.Patient.FirstOrDefaultAsync(p => p.PatientId == 5);
                if (getById != null)
                {
                    var result = await patientsController.GetPatient(getById.PatientId);
                    Assert.IsNotNull(result.Value, "Patient should not be null");
                }
                else
                {
                    Assert.IsNull(getById);
                }
            }
        }



        [Test]
        public async Task updatePatientTest()
        {

            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var controller = new PatientsController(context);

            var patient = new Patient
            {
                    PatientId= 3,
                    FullName= "Jami sai Ram Rohan",
                    DOB= new DateTime(1978 - 12 - 03),
                    Gender= "Male",
                    ContactNumber= "9876543212",
                    UserName= "Rohan",
                    Password= "Rohan@123"
            };
            var result = await controller.PutPatient(3, patient);
            var updatedPatient = await context.Patient.FindAsync(3);
            Assert.IsNotNull(updatedPatient); 
            Assert.AreEqual(patient.FullName,updatedPatient.FullName);     
        }


        [Test]
        public async Task AddPatientTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var addPatient = new PatientsController(context);
            var newPatient = new Patient() 
            {
                FullName = "John Doe",
                DOB = new DateTime(2001/10/10), 
                Gender = "Male",  
                ContactNumber = "9898767654",
                UserName = "john_doe",
                Password = "password123" 
};
            await Task.Run(() => addPatient.PostPatient(newPatient));
            var addedPatient = context.Patient.FirstOrDefault(p => p.FullName == "John Doe");
            Assert.IsNotNull(addedPatient);
            Assert.AreEqual(newPatient.UserName, addedPatient.UserName);
            Assert.AreEqual(newPatient.Password, addedPatient.Password);
        }

        [Test]
        public async Task DelPatientTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>()
                .UseSqlServer(config.GetConnectionString("AmazeCare"))
                .Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var patientsController = new PatientsController(context);

                var patientToDelete = await context.Patient.FirstOrDefaultAsync(p => p.PatientId == 5);
                await patientsController.DeletePatient(patientToDelete.PatientId);

                var deletedPatient = await context.Patient.FindAsync(patientToDelete.PatientId);

                Assert.IsNull(deletedPatient);
            }
        }

    }
} 
