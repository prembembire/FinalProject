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
    public class AppointmentsTest
    {
        private IConfigurationRoot config;
        [SetUp]
        public void Setup()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }




        [Test]
        public async Task GetAppointments()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var appointmentsController = new AppointmentsController(context);
            var result = await appointmentsController.GetAppointments();
            Assert.IsNotNull(result);
        }



        [Test]
        public async Task GetAppointmentById()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var appointmentsController = new AppointmentsController(context);

                var getById = await context.Appointments.FirstOrDefaultAsync(p => p.AppointmentId == 7);
                if (getById != null)
                {
                    var result = await appointmentsController.GetAppointments(getById.AppointmentId);
                    Assert.IsNotNull(result.Value, "Appointments should not be null");
                }
                else
                {
                    Assert.IsNull(getById);
                }
            }
        }







        [Test]
        public async Task UpdateAppointmentDetails()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var controller = new AppointmentsController(context);

           
            

            var modifiedAppointment = new Appointments()
            {
                AppointmentId = 7,
                DoctorId = 2,
                PatientId = 2,
                AppointmentDate = new DateTime(2024-08-04),
                Status = "Pending",
                VisitType = "General Checkup"
            };

           
            var result = await controller.PutAppointments(7, modifiedAppointment);

           

            
            var updatedAppointment = await context.Appointments.FindAsync(7);

            Assert.IsNotNull(updatedAppointment);
            Assert.AreEqual(modifiedAppointment.AppointmentId, updatedAppointment.AppointmentId);
            Assert.AreEqual(modifiedAppointment.DoctorId, updatedAppointment.DoctorId);
            Assert.AreEqual(modifiedAppointment.PatientId, updatedAppointment.PatientId);
            Assert.AreEqual(modifiedAppointment.AppointmentDate, updatedAppointment.AppointmentDate);
            Assert.AreEqual(modifiedAppointment.Status, updatedAppointment.Status);
            Assert.AreEqual(modifiedAppointment.VisitType, updatedAppointment.VisitType);
        }






        [Test]
        public async Task AddAppointment()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var addRecord = new AppointmentsController(context);
            var newRecord = new Appointments()
            {
                
                DoctorId = 2,
                PatientId = 2,
                AppointmentDate = new DateTime(2025 - 01- 10),
                Status = "Pending",
                VisitType = "General Checkup"
            };
            await Task.Run(() => addRecord.PostAppointments(newRecord));

            //Note : Here im checking with the patient and doctor id of recently added one using the FirstorDefault
            var addedRecord= context.Appointments.Where(p=>p.PatientId==2 && p.DoctorId==2).OrderByDescending(p=>p.AppointmentId).FirstOrDefault();
            Assert.IsNotNull(addedRecord);
            Assert.AreEqual(newRecord.AppointmentId, addedRecord.AppointmentId);
            Assert.AreEqual(newRecord.DoctorId, addedRecord.DoctorId);
            Assert.AreEqual(newRecord.PatientId, addedRecord.PatientId);

        }









        [Test]
        public async Task DelAppointment()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>()
                .UseSqlServer(config.GetConnectionString("AmazeCare"))
                .Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var appointmentsController = new AppointmentsController(context);

                var medicalRecordToDelete = await context.Appointments.FirstOrDefaultAsync(p => p.AppointmentId == 7);
                await appointmentsController.DeleteAppointments(medicalRecordToDelete.AppointmentId);

                var deletedRecord = await context.Appointments.FindAsync(medicalRecordToDelete.AppointmentId);

                Assert.IsNull(deletedRecord);
            }
        }

    }
}
