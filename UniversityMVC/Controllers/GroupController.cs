using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniversityMVC.Models;

namespace UniversityMVC.Controllers
{
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private IUnitOfWork UnitOfWork { get; set; }

        public GroupController(ILogger<GroupController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
        }
        public IActionResult SelectedGroup(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("SelectedGroup method was called but got no id");
                return NotFound();
            }
            _logger.LogInformation("SelectedGroup method was called with an id = {groupId}", id);
            ViewBag.GroupName = $"{UnitOfWork.Groups.GetById(id).Name}";
            return View(UnitOfWork.Students.GetByGroup(id));
        }

        [HttpGet]
        public IActionResult ChangeGroup(int id)
        {
            _logger.LogInformation("ChangeGroup method was called with an id = {groupId}", id);
            return View(UnitOfWork.Groups.GetById(id));
        }

        [HttpPost]
        public IActionResult ChangeGroup(string name, int id)
        {
            UnitOfWork.Groups.GetById(id).Name = name;
            UnitOfWork.Save();
            _logger.LogInformation("Group's (id = {groupId}) name was changed to {newName}", id, name);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteGroup(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("DeleteGroup method was called but got no id");
                return NotFound();
            }
            try
            {

                _logger.LogInformation("DeleteGroup method was called with an id = {groupId}", id);
                return View(UnitOfWork.Groups.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred");
                return NotFound();
            }
        }

        [HttpPost, ActionName("DeleteGroup")]
        public IActionResult ConfirmDeleteGroup(int id)
        {
            try
            {
                _logger.LogInformation("Attempt to delete a group with an id = {groupId}", id);
                UnitOfWork.Groups.Remove(UnitOfWork.Groups.GetById(id));
                UnitOfWork.Save();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex, "Attempt failed");
                ViewBag.Message = "Невозможно удалить группу, в которой есть студенты!";
                return View("ReturnHomePage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Attempt failed");
                ViewBag.Message = "Во время выполнения запроса произошла ошибка! Изменения не были внесены! ";
                return View("ReturnHomePage");
            }

            _logger.LogInformation("Group was successfully deleted");
            ViewBag.Message = "Группа была удалена!";
            return View("ReturnHomePage");
        }
    }
}
