using MVCProject.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MVCProject.PL.ViewModels
{
	public class DepartmentViewModel
	{
        public int Id { get; set; }
		[Required]
		public string Code { get; set; }
		[Required]
		public string Name { get; set; }
		[Display(Name = "Date Of Creation")]
		public DateTime DateOfCreation { get; set; }
	}
}
