using AmazeCareProject.Controllers;
using AmazeCareProject.Data;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace TestProject1
{
    public class DoctorsTest
    {
        private IConfigurationRoot config;
        [SetUp]
        public void Setup()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        [Test]
        public async Task GetDoctor()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var doctorController = new DoctorsController(context);
            var result = await doctorController.GetDoctors();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetDoctorByIdTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var doctorsController = new DoctorsController(context);
                var getById = await context.Doctors.FirstOrDefaultAsync(p => p.DoctorId == 5);
                if (getById != null)
                {
                    var result = await doctorsController.GetDoctors(getById.DoctorId);
                    Assert.IsNotNull(result.Value, "Doctor should not be null");
                }
                else
                {
                    Assert.IsNull(getById);
                }
            }
        }

        [Test]
        public async Task AddDoctorTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var addDoctor = new DoctorsController(context);
            var newDoctor = new Doctors()
            {
                Name = "test",
                Specialty = "Dentist",
                ExperienceYears = 1,
                Qualification = "Phd",
                Designation = "Junior dentist",
                UserName = "John_Doe1",
                Password = "password123"
            };
            await Task.Run(() => addDoctor.PostDoctors(newDoctor));
            var addedDoctor = context.Doctors.FirstOrDefault(p => p.UserName == "John_Doe1");
            Assert.IsNotNull(addedDoctor);
            Assert.AreEqual(newDoctor.UserName, addedDoctor.UserName);
            Assert.AreEqual(newDoctor.Password, addedDoctor.Password);
        }




        [Test]
        public async Task PutDoctorsTest()
        {

            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var controller = new DoctorsController(context);

            var modifiedDoctor = new Doctors
            {
                DoctorId = 7,
                Name = "test",
                Specialty = "Dentist",
                ExperienceYears = 1,
                Qualification = "Phd",
                Designation = "Junior dentist",
                UserName = "John_Doe",
                Password = "password123"

            };
            var result = await controller.PutDoctors(7, modifiedDoctor);

            var updatedDoctor = await context.Doctors.FindAsync(7);

            Assert.IsNotNull(updatedDoctor);
            Assert.AreEqual(modifiedDoctor.DoctorId, updatedDoctor.DoctorId);
            Assert.AreEqual(modifiedDoctor.UserName, updatedDoctor.UserName);


        }

        [Test]
        public async Task DelDoctorTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>()
                .UseSqlServer(config.GetConnectionString("AmazeCare"))
                .Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var doctorsController = new DoctorsController(context);

                var doctorToDelete = await context.Doctors.FirstOrDefaultAsync(p => p.DoctorId == 5);
                await doctorsController.DeleteDoctors(doctorToDelete.DoctorId);

                var deletedDoctor = await context.Doctors.FindAsync(doctorToDelete.DoctorId);

                Assert.IsNull(deletedDoctor);
            }
        }
    }
}