using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityMVC.Controllers;
using UniversityMVC.Models;
using Xunit;

namespace UniversityMVC.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexGetAllObj()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            mock.Setup(unit => unit.Courses.GetAll()).Returns(GetAllCourses());
            var controller = new HomeController(mockLogger.Object, mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Course>>(viewResult.Model);
            Assert.Equal(GetAllCourses().Count, model.Count());
        }

        private List<Course> GetAllCourses()
        {
            return new List<Course> { new Course { Id = 0, Name = "Name0", Description = "SampleText0" }, new Course { Id = 1, Name = "Name1", Description = "SampleText1" }, new Course { Id = 2, Name = "Name2", Description = "SampleText2" } };
        }
    }
}
