using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Hosting;
using MVCProject.BLL.Interfaces;
using MVCProject.BLL.Repositories;
using MVCProject.DAL.Models;
using MVCProject.PL.ViewModels;
using System;
using System.Collections.Generic;
namespace MVCProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_env = env;
			_mapper = mapper;
		}
        public IActionResult Index()
        {
            var departments = _unitOfWork.Repository<Department>().GetAll();
			var mappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepartment);
        }
        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentMV)
        {
            var mappedDepartment = _mapper.Map<DepartmentViewModel,Department>(departmentMV);
            if (ModelState.IsValid)
            {
                
                _unitOfWork.Repository<Department>().Add(mappedDepartment);
                var Count = _unitOfWork.Complete();
				if (Count > 0)
					TempData["AddSuccess"] = "Department Is Created Successfuly";
				else
					TempData["AddFail"] = "An Error Has Occured, Department Not Created :(";
				return RedirectToAction(nameof(Index));
			}
			return View(departmentMV);
        }
        // /Department/Details/id
        //[HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _unitOfWork.Repository<Department>().Get(id.Value);
            if (department is null)
                return NotFound();
            var mappedDepartment = _mapper.Map<Department,DepartmentViewModel>(department);
            return View(ViewName, mappedDepartment);
        }
        // /Department/Edit/10
        //[HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(departmentVM);
            try
            {
				var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
				_unitOfWork.Repository<Department>().Update(mappedDepartment);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating the Department");
                return View(departmentVM);
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
				var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
				_unitOfWork.Repository<Department>().Delete(mappedDepartment);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Deleting the Department");
                return View(departmentVM);
            }
        }
    }
}
