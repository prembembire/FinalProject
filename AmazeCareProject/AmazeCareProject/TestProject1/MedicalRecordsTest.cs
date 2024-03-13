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
    public class MedicalRecordsTest
    {
        private IConfigurationRoot config;
        [SetUp]
        public void Setup()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }




        [Test]
        public async Task GetMedicalRecordById()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var medicalRecordsController = new MedicalRecordsController(context);
            var result = await medicalRecordsController.GetMedicalRecords();
            Assert.IsNotNull(result);
        }



        [Test]
        public async Task GetMedicalRecordByIdTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var medicalRecordsController = new MedicalRecordsController(context);
                
                var getById = await context.MedicalRecords.FirstOrDefaultAsync(p => p.PatientId == 5);
                if (getById != null)
                {
                    var result = await medicalRecordsController.GetMedicalRecords(getById.RecordId);
                    Assert.IsNotNull(result.Value, "MedicalRecord should not be null");
                }
                else
                {
                    Assert.IsNull(getById);
                }
            }
        }







        [Test]
        public async Task PutMedicalRecordsTest()
        {
            
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var controller = new MedicalRecordsController(context);
            var modifiedRecord = new MedicalRecords
            {
                RecordId = 7, 
                DoctorId = 2,
                AppointmentId = 2, 
                PatientId = 2,
                Symptoms = "vomitings",
                PhysicalExamination = "Fever (100°F), Mild headache",
                TreatmentPlan = "Antipyretics, Fluids",
                TestsRecommended = "Blood tests",
                Prescription = "Paracetamol 500mg - 1-1-1"
            };
            var updatedRecord = await context.MedicalRecords.FindAsync(7);

            Assert.IsNotNull(updatedRecord); 
            Assert.AreEqual(modifiedRecord.RecordId, updatedRecord.RecordId);
            Assert.AreEqual(modifiedRecord.AppointmentId, updatedRecord.AppointmentId);
            Assert.AreEqual(modifiedRecord.DoctorId, updatedRecord.DoctorId);
            Assert.AreEqual(modifiedRecord.PatientId, updatedRecord.PatientId);
            Assert.AreEqual(modifiedRecord.Symptoms, updatedRecord.Symptoms);

            
        }





        [Test]
        public async Task AddRecordTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var addRecord = new MedicalRecordsController(context);
            var newRecord = new MedicalRecords()
            {
                PatientId =4,
                DoctorId=4,
                AppointmentId=7,
                Symptoms ="Fever,vomitings",
                PhysicalExamination= "Fever (100°F), Mild headache",
                TreatmentPlan= "Antipyretics, Fluids",
                TestsRecommended= "Blood tests",
                Prescription="Paracetamol 500mg - 1-1-1"};
                await Task.Run(() => addRecord.PostMedicalRecords(newRecord));
                var addedRecord = context.MedicalRecords.FirstOrDefault(p => p.AppointmentId == 7);
                Assert.IsNotNull(addedRecord);
                Assert.AreEqual(newRecord.AppointmentId, addedRecord.AppointmentId);
                Assert.AreEqual(newRecord.DoctorId, addedRecord.DoctorId);
                Assert.AreEqual(newRecord.PatientId, addedRecord.PatientId);
                
        }









        [Test]
        public async Task DelMedicalRecordTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>()
                .UseSqlServer(config.GetConnectionString("AmazeCare"))
                .Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var medicalRecordsController = new MedicalRecordsController(context);

                var medicalRecordToDelete = await context.MedicalRecords.FirstOrDefaultAsync(p => p.RecordId == 5);
                await medicalRecordsController.DeleteMedicalRecords(medicalRecordToDelete.RecordId);

                var deletedRecord = await context.MedicalRecords.FindAsync(medicalRecordToDelete.RecordId);

                Assert.IsNull(deletedRecord);
            }
        }

    }
} 
