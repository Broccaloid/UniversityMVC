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
    public class GroupControllerTests
    {
        [Fact]
        public void ChangeGroupSuccessfully()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<GroupController>>();
            var testGroup = new Group { Id = 0, Name = "Name0" };
            mock.Setup(unit => unit.Groups.GetById(0)).Returns(testGroup);
            var controller = new GroupController(mockLogger.Object, mock.Object);

            //Act
            var result = controller.ChangeGroup("NewName", 0);

            //Assert
            Assert.Equal("NewName", testGroup.Name);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(unit => unit.Save());
        }

        [Fact]
        public void DeleteGroupReturnsViewResult()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<GroupController>>();
            var testGroup = new Group { Id = 0, Name = "Name0" };
            mock.Setup(unit => unit.Groups.GetById(0)).Returns(testGroup);
            var controller = new GroupController(mockLogger.Object, mock.Object);

            //Act
            var result = controller.ConfirmDeleteGroup(0);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("ReturnHomePage", viewResult.ViewName);
            mock.Verify(unit => unit.Save());
            mock.Verify(unit => unit.Groups.Remove(testGroup));
        }

        [Fact]
        public void DeleteGroupGetsNullReturnsErrorCode404()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<GroupController>>();
            var controller = new GroupController(mockLogger.Object, mock.Object);

            //Act
            var result = controller.DeleteGroup(null);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void SelectedGroupViewGetsStudentsByGroupId()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<GroupController>>();
            mock.Setup(unit => unit.Students.GetByGroup(0)).Returns(GetStudentsById());
            mock.Setup(unit => unit.Groups.GetById(0)).Returns(new Group());
            var controller = new GroupController(mockLogger.Object, mock.Object);

            // Act
            var result = controller.SelectedGroup(0);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.Model);
            Assert.Equal(GetStudentsById().Count, model.Count());
        }
        

        private List<Student> GetStudentsById()
        {
            return new List<Student>{ new Student(), new Student()};
        }
    }
}
