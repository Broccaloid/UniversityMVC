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
    public class CourseControllerTests
    {
        [Fact]
        public void SelectedCourseViewGetsGroupsByCourseId()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<CourseController>>();
            mock.Setup(unit => unit.Groups.GetByCourse(0)).Returns(GetGroupsById());
            mock.Setup(unit => unit.Courses.GetById(0)).Returns(GetCourseById());
            var controller = new CourseController(mockLogger.Object, mock.Object);

            // Act
            var result = controller.SelectedCourse(0);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Group>>(viewResult.Model);
            Assert.Equal(GetGroupsById().Count, model.Count());
        }

        private List<Group> GetGroupsById()
        {
            return new List<Group> { new Group{ Id = 0, Name = "Name0", Course = new Course()}, new Group { Id = 1, Name = "Name1", Course = new Course()}, new Group { Id = 2, Name = "Name2", Course = new Course()}, };
        }

        private Course GetCourseById()
        {
            return new Course { Id = 0, Name = "NameSample" };
        }
    }
}
