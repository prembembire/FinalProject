




using AmazeCareProject.Controllers;
using AmazeCareProject.Data;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace NUnitTest
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
            


        }



        //namespace TestProject
        //    {
        //        public class Tests
        //        {
        //            private IConfigurationRoot config;
        //            private JobPortalCFContext context;
        //            private EmployersController empCtr;
        //            [SetUp]
        //            public void Setup()
        //            {
        //                config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //                var options = new DbContextOptionsBuilder<JobPortalCFContext>().UseSqlServer(config.GetConnectionString("ConnectionStr")).Options;
        //                context = new JobPortalCFContext(options);
        //                context.Database.EnsureCreated();
        //                empCtr = new EmployersController(context);
        //            }

        //            [Test]
        //            public async Task GetEmployersTestWhenNotNull()
        //            {
        //                var result = await empCtr.GetEmployers(); // Call the GetEmployers method
        //                Assert.IsNotEmpty(result.Value);
        //            }



        //[Test]
        //public async Task AddProTest()
        //{
        //    var options = new DbContextOptionsBuilder<JobPortalCFContext>().UseSqlServer(config.GetConnectionString("ConnectionStr")).Options;
        //    using var context = new JobPortalCFContext(options);
        //    context.Database.EnsureCreated();
        //    var proCtr = new EmployersController(context);
        //    var newPro = new Employer() { UserName="hello", CompanyName="hllo", ContactEmail="awd@ad.com", ContactPhone="1234567980", CwebsiteUrl="adwf", EmployerName="wad", Password="awdefs"};
        //    await Task.Run(() => proCtr.PostEmployer(newPro));
        //    var actualPro = context.Employers.FirstOrDefault(p => p.UserName == "hello");
        //    Assert.IsNotNull(actualPro);
        //    Assert.AreEqual(newPro.UserName, actualPro.UserName);
        //}

        //[Test]
        //public async Task DelProTest()
        //{
        //    var options = new DbContextOptionsBuilder<JobPortalCFContext>().UseSqlServer(config.GetConnectionString("ConnectionStr")).Options;
        //    using var context = new JobPortalCFContext(options);
        //    context.Database.EnsureCreated();
        //    var proCtr = new EmployersController(context);

        //    var proToDelete = await context.Employers.SingleOrDefaultAsync(p => p.EmployerId == 2);

        //    await proCtr.DeleteEmployer(proToDelete.EmployerId);
        //    var deletedPro = await context.Employers.FindAsync(proToDelete.EmployerId);
        //    Assert.IsNull(deletedPro);
        //}
    }
    }














   
        