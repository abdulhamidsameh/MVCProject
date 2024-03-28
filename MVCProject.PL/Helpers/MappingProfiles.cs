using AutoMapper;
using MVCProject.DAL.Models;
using MVCProject.PL.ViewModels;

namespace MVCProject.PL.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
