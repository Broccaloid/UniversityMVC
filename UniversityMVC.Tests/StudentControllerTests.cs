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
    public class StudentControllerTests
    {
        [Fact]
        public void ChangeStudentSuccessfully()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<StudentController>>();
            var testStudent = new Student { Id = 0, FirstName = "FirstName", LastName = "LastName" };
            mock.Setup(unit => unit.Students.GetById(0)).Returns(testStudent);
            var controller = new StudentController(mockLogger.Object, mock.Object);

            //Act
            var result = controller.ChangeStudent("NewName", "NewLastName", 0);

            //Assert
            Assert.Equal("NewName", testStudent.FirstName);
            Assert.Equal("NewLastName", testStudent.LastName);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(unit => unit.Save());
        }

        [Fact]
        public void DeleteStudentReturnsViewResult()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<StudentController>>();
            var testStudent = new Student { Id = 0, FirstName = "FirstName", LastName = "LastName" };
            mock.Setup(unit => unit.Students.GetById(0)).Returns(testStudent);
            var controller = new StudentController(mockLogger.Object, mock.Object);

            //Act
            var result = controller.ConfirmDeleteStudent(0);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("ReturnHomePage", viewResult.ViewName);
            mock.Verify(unit => unit.Save());
            mock.Verify(unit => unit.Students.Remove(testStudent));
        }

        [Fact]
        public void DeleteStudentGetsNullReturnsErrorCode404()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<StudentController>>();
            var controller = new StudentController(mockLogger.Object, mock.Object);

            //Act
            var result = controller.DeleteStudent(null);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
