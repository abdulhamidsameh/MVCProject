using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.DAL.Models;
using MVCProject.PL.ViewModels.User;
using System.Threading.Tasks;

namespace MVCProject.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is null)
				{
					user = new ApplicationUser()
					{
						UserName = model.UserName,
						Email = model.Email,
						IsAgree = model.IsAgree,
						FName = model.FName,
						LName = model.LName,
					};
					var result = await _userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
						RedirectToAction(nameof(SignIn));
					else
						foreach (var error in result.Errors)
							ModelState.AddModelError(string.Empty, error.Description);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "this User Name is already in use for another account");
					return View(model);
				}
			}
			return View(model);
		}

		public IActionResult SignIn()
		{
			return View();
		}
	}
}
