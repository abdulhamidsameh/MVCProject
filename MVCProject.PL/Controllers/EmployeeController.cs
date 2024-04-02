using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using MVCProject.BLL;
using MVCProject.BLL.Interfaces;
using MVCProject.BLL.Repositories;
using MVCProject.DAL.Models;
using MVCProject.PL.Helpers;
using MVCProject.PL.ViewModels;
using NToastNotify;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_env = env;
			_mapper = mapper;
		}

		//[HttpGet]
		public async Task<IActionResult> Index(string searchInput)
		{
			var EmpRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

			var employees = Enumerable.Empty<Employee>();
			if (string.IsNullOrEmpty(searchInput))
				employees = await EmpRepo.GetAllAsync();
			else
				employees = EmpRepo.SearchByName(searchInput);
			var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
			return View(mappedEmps);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
		{
			if (ModelState.IsValid)
			{
				var ImageName = await DocumentSettings.UploadFile(employeeVM.Image, "Images");
				var VideoName = await DocumentSettings.UploadFile(employeeVM.Video, "Videos");
				var PdfName = await DocumentSettings.UploadFile(employeeVM.Pdf, "Pdfs");

				employeeVM.ImageName = ImageName;
				employeeVM.VideoName = VideoName;
				employeeVM.PdfName = PdfName;

				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

				_unitOfWork.Repository<Employee>().Add(mappedEmp);
				var Count = await _unitOfWork.Complete();
				if (Count > 0)
					TempData["AddSuccess"] = "Employee Is Created Successfuly";
				else
				{
					TempData["AddFail"] = "An Error Has Occured, Employee Not Created :(";
				}
				return RedirectToAction(nameof(Index));
			}
			return View(employeeVM);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int? id, string ViewName = "Details")
		{
			if (!id.HasValue)
				return BadRequest();
			var employee = await _unitOfWork.Repository<Employee>().GetAsync(id.Value);
			if (employee is null)
				return NotFound();
			var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
			if(ViewName.Equals("Delete",StringComparison.OrdinalIgnoreCase))
			{
				TempData["ImageName"] = employee.ImageName;
				TempData["VideoName"] = employee.VideoName;
				TempData["PdfName"] = employee.PdfName;
			}
			return View(ViewName, mappedEmp);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			return await Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
		{
			if (id != employeeVM.Id)
				return BadRequest();
			if (!ModelState.IsValid)
				return View(employeeVM);
			try
			{
				var mappedEmplyee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
				_unitOfWork.Repository<Employee>().Update(mappedEmplyee);
				await _unitOfWork.Complete();
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
		public async Task<IActionResult> Delete(int? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
		{
			try
			{
				employeeVM.ImageName = TempData["ImageName"] as string;
				employeeVM.VideoName = TempData["VideoName"] as string;
				employeeVM.PdfName = TempData["PdfName"]  as string;
				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
				_unitOfWork.Repository<Employee>().Delete(mappedEmp);
				var Count = await _unitOfWork.Complete();
				if(Count > 0)
				{
					DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
					DocumentSettings.DeleteFile(employeeVM.VideoName, "Videos");
					DocumentSettings.DeleteFile(employeeVM.PdfName, "Pdfs");
					return RedirectToAction(nameof(Index));
				}
				return View(employeeVM);

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
