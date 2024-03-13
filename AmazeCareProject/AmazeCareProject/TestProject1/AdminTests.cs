using AmazeCareProject.Controllers;
using AmazeCareProject.Data;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TestProject1
{
   


    public class AdminControllerTests
    {
        private IConfigurationRoot config;
        [SetUp]
        public void Setup()
        {
            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        [Test]
        public async Task GetAdminById()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var adminController = new AdminsController(context);
            var result = await adminController.GetAdmin();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAdminByIdTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var patientsController = new AdminsController(context);
                var getById = await context.Admin.FirstOrDefaultAsync(p => p.AdminId == 5);
                if (getById != null)
                {
                    var result = await patientsController.GetAdmin(getById.AdminId);
                    Assert.IsNotNull(result.Value, "Admin should not be null");
                }
                else
                {
                    Assert.IsNull(getById);
                }
            }
        }

        [Test]

        public async Task AddAdminTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>().UseSqlServer(config.GetConnectionString("AmazeCare")).Options;
            using var context = new AmazeCareDBContext(options);
            context.Database.EnsureCreated();
            var addAdmin = new AdminsController(context);
            var newAdmin = new Admin()
            {
                UserName = "John_Doe",
                Password = "password123"
            };
            await Task.Run(() => addAdmin.PostAdmin(newAdmin));
            var addedAdmin = context.Admin.FirstOrDefault(p => p.UserName == "John_Doe");
            Assert.IsNotNull(addedAdmin);
            Assert.AreEqual(newAdmin.UserName, addedAdmin.UserName);
            Assert.AreEqual(newAdmin.Password, addedAdmin.Password);
        }


        [Test]
        public async Task DelAdminTest()
        {
            var options = new DbContextOptionsBuilder<AmazeCareDBContext>()
                .UseSqlServer(config.GetConnectionString("AmazeCare"))
                .Options;

            using (var context = new AmazeCareDBContext(options))
            {
                context.Database.EnsureCreated();
                var adminsController = new AdminsController(context);

                var adminToDelete = await context.Admin.FirstOrDefaultAsync(p => p.AdminId == 2);
                await adminsController.DeleteAdmin(adminToDelete.AdminId);

                var deletedAdmin = await context.Admin.FindAsync(adminToDelete.AdminId);

                Assert.IsNull(deletedAdmin);
            }
        }

    }
    }
