﻿using MVCProject.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace MVCProject.PL.ViewModels
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		[MinLength(5)]
		public string Name { get; set; }

		[Range(22, 30)]
		public int? Age { get; set; }

		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
			ErrorMessage = "Address must be like 123-Street-City-Country")]
		public string Address { get; set; }

		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[Display(Name = "Phone Number")]
		[Phone]
		public string PhoneNumber { get; set; }

		[Display(Name = "Hiring Date")]
		public DateTime HiringDate { get; set; }

		public Gender Gender { get; set; }

		[Display(Name = "Employee Type")]
		public EmpType EmployeeType { get; set; }

		public int? DepartmentId { get; set; }

		
		public virtual Department Department { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public IFormFile Video { get; set; }
		public string VideoName { get; set; }
        public IFormFile Pdf { get; set; }
		public string PdfName { get; set; }

    }
}
