using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVCProject.BLL.Interfaces;
using MVCProject.BLL.Repositories;
using MVCProject.DAL.Models;
using MVCProject.PL.ViewModels;
using NToastNotify;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVCProject.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployeeRepository _employeeRepo;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env ,IMapper mapper)
		{
			_employeeRepo = employeeRepo;
			_env = env;
			_mapper = mapper;
		}
		//[HttpGet]
		public IActionResult Index(string searchInput)
		{
			var employees = Enumerable.Empty<Employee>();
			if (string.IsNullOrEmpty(searchInput))
				employees = _employeeRepo.GetAll();
			else
				employees = _employeeRepo.SearchByName(searchInput);
			var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
			return View(mappedEmps);
		} 
		[HttpGet]
		public IActionResult Create()
		{
            return View();
		}
		[HttpPost]
		public IActionResult Create(EmployeeViewModel employeeVM)
		{
			var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
			if (ModelState.IsValid)
			{
				var Count = _employeeRepo.Add(mappedEmp);
				if (Count > 0)
					TempData["AddSuccess"] = "Employee Is Created Successfuly";
				else
					TempData["AddFail"] = "An Error Has Occured, Employee Not Created :(";
				return RedirectToAction(nameof(Index));
			}
			return View(employeeVM);
		}
		[HttpGet]
		public IActionResult Details(int? id, string ViewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest();
			var employee = _employeeRepo.Get(id.Value);
			if (employee is null)
				return NotFound();
			var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
			return View(ViewName, mappedEmp);
		}
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
		{
			if (id != employeeVM.Id)
				return BadRequest();
			if (!ModelState.IsValid)
				return View(employeeVM);
			try
			{
				var mappedEmplyee = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
				_employeeRepo.Update(mappedEmplyee);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating the Employee");
				return View(employeeVM);
			}
		}
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(EmployeeViewModel employeeVM)
		{
			try
			{
				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
				_employeeRepo.Delete(mappedEmp);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has Occured during Deleting the Employee");
				return View(employeeVM);
			}
		}
	}
}
