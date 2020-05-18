using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Umbraco_InternShip_MVC.Controllers;
using Umbraco_InternShip_MVC.Data;
using Umbraco_InternShip_MVC.Models;
using Xunit;
using Xunit.Sdk;
using DrawLogic;

namespace XUnitTestProjectUmbracoDeveloperInternship
{
    public class Umbraco_InternShip_UnitTest : IDisposable
    {
        protected readonly MvcDrawContext _context;
        private DrawController _controller;
        

        public Umbraco_InternShip_UnitTest()
        {
            var options = new DbContextOptionsBuilder<MvcDrawContext>()
                    .UseInMemoryDatabase<MvcDrawContext>(Guid.NewGuid().ToString())
                    .Options;
            _context = new MvcDrawContext(options);
            _context.Database.EnsureCreated();
            _controller = new DrawController(_context);
            DbInitializer.Initialize(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task ControllerActionMethodCanCreateSubmissionForm()
        {
            //Arrange 
            string FirstName = "MuslimTest";
            string LastName = "MuslimTest";
            string email = "MuslimTest@test.dk";
            DateTime age = DateTime.Now.AddYears(-18);

            var serialNumber = await _context.SerialNumbers.FirstOrDefaultAsync();

            SubmissionForm submissionForm = new SubmissionForm(FirstName, LastName, email, serialNumber.SerialNumberValue, age);

            //Act
            var result = await _controller.Create(submissionForm);

            //Assert
            var objectResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(objectResult.Model);

            var model = Assert.IsType<SubmissionForm>(objectResult.Model);
            Assert.True(model.Entered);
        }

        [Fact]
        public async Task ReturnBadRequestIfAgeUnder18()
        {
            //arrange
            string FirstName = "MuslimTest";
            string LastName = "MuslimTest";
            string email = "Email@Test";
            DateTime age = DateTime.Now.AddYears(-16);

            var serialNumber = await _context.SerialNumbers.FirstOrDefaultAsync();

            SubmissionForm submissionForm = new SubmissionForm(FirstName, LastName, email, serialNumber.SerialNumberValue, age);

            //Act
            var result = await _controller.Create(submissionForm);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = bad request age under 18 }", objectResult.Value.ToString());
        }

        [Fact]
        public async Task BadRequestIfInvalidSerialNumber()
        {
            //arrange
            string FirstName = "MuslimTest";
            string LastName = "MuslimTest";
            string email = "Email@Test";
            DateTime age = DateTime.Now.AddYears(-18);
            string SerialNumber = "ThisIsAnInvalidSerialNumber";

           

            SubmissionForm submissionForm = new SubmissionForm(FirstName, LastName, email, SerialNumber, age);

            //Act
            var result = await _controller.Create(submissionForm);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = bad request INVALID serial number }", objectResult.Value.ToString());
        }

        [Fact]
        public async Task BadRequestIfSerialNumberIsInActiveAfterUsed2Times()
        {
            //arrange
            string FirstName = "MuslimTest";
            string LastName = "MuslimTest";
            string email = "Email@Test";
            DateTime age = DateTime.Now.AddYears(-18);

            var serialNumber = await _context.SerialNumbers.FirstOrDefaultAsync();

            SubmissionForm submissionForm = new SubmissionForm(FirstName, LastName, email, serialNumber.SerialNumberValue, age);

            //Act
            await _controller.Create(submissionForm);
            await _controller.Create(submissionForm);
            var result = await _controller.Create(submissionForm);

            //Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ message = bad request INVALID serial number }", objectResult.Value.ToString());
        }

        [Fact]
        public async Task FirstNameModelValidationRequired()
        {
            //Arrange 
            string FirstName = "";
            string LastName = "MuslimTest";
            string email = "Email@Test";
            DateTime age = DateTime.Now.AddYears(-18);

            var serialNumber = await _context.SerialNumbers.FirstOrDefaultAsync();

            SubmissionForm submissionForm = new SubmissionForm(FirstName, LastName, email, serialNumber.SerialNumberValue, age);
            ValidationContext _vContext = new ValidationContext(submissionForm);
            _vContext.MemberName = "FirstName";
            var results = new List<ValidationResult>();
            

            //Act
            var actual = Validator.TryValidateProperty(submissionForm.FirstName, _vContext, results);

            //Assert
            Assert.Single(results);
            Assert.Equal("The FirstName field is required.", results[0].ErrorMessage);            
        }

        [Fact]
        public async Task FirstNameModelValidationMustBeCharactersNotNumbers()
        {
            //Arrange 
            string FirstName = "123456";
            string LastName = "MuslimTest";
            string email = "Email@Test";
            DateTime age = DateTime.Now.AddYears(-18);

            var serialNumber = await _context.SerialNumbers.FirstOrDefaultAsync();

            SubmissionForm submissionForm = new SubmissionForm(FirstName, LastName, email, serialNumber.SerialNumberValue, age);
            ValidationContext _vContext = new ValidationContext(submissionForm);
            _vContext.MemberName = "FirstName";
            var results = new List<ValidationResult>();


            //Act
            var actual = Validator.TryValidateProperty(submissionForm.FirstName, _vContext, results);

            //Assert
            Assert.Single(results);
            Assert.Equal("Use letters only please", results[0].ErrorMessage);
        }


    }
}
