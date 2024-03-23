using Microsoft.AspNetCore.Mvc;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Models;
namespace MVCProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                var Count = _departmentRepo.Add(department);
                if (Count > 0)
                    return RedirectToAction(nameof(Index));

            }
            return View(department);
        }
        // /Department/Details/id
        [HttpGet]
        public IActionResult Details(int? id) 
        {
            if(!id.HasValue)
                return BadRequest();
            var department = _departmentRepo.Get(id.Value);
            if(department is null)
                return NotFound();

            return View(department);
        }
    }
}
