using CustomersMVC.DTO;
using CustomersMVC.Models;
using CustomersMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomersMVC.Controllers
{
	public class CustomerController : Controller
	{
		private readonly IApplicationService _applicationService;
        public List<Error> ErrorsArray { get; set; } = new();
		public List<OrderLine> orderlines { get; set; } = new();

        public CustomerController(IApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Index()
		{
            ClaimsPrincipal principal = HttpContext.User;
            var claims = principal.Claims;
			var claim = claims.FirstOrDefault();
            
			string username = claim!.Value;

            orderlines = await _applicationService.OrderService.GetAllOrders(username!);

			if (orderlines.Count == 0)
			{
				ViewData["OrderLines"] = "Empty Orders";
			}
			ViewData["OrderLines"] = orderlines;

			return View();
		}

		[HttpGet]
		public IActionResult Login()
		{
			ClaimsPrincipal principal = HttpContext.User;

			if (principal.Identity!.IsAuthenticated) 
			{ 
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpGet]
		public IActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Signup(CustomerSignupDTO request)
		{
			if (!ModelState.IsValid)
			{
				foreach(var entry in ModelState.Values)
				{
					foreach(var error in  entry.Errors)
					{
						ErrorsArray.Add(new Error("", error.ErrorMessage, ""));
					}
					ViewData["ErrorsArray"] = ErrorsArray;
				}
				return View();
			}

			try
			{
				await _applicationService.CustomerService.SignupCustomerAsync(request);
			} catch (Exception e)
			{
                await Console.Out.WriteLineAsync(e.StackTrace);
                ErrorsArray.Add(new Error("", e.Message, ""));
				ViewData["ErrorsArray"] = ErrorsArray;
				return View();
			}

			return RedirectToAction("Login", "Customer");
		}

		[HttpPost]
		public async Task<IActionResult> Login(CustomerLoginDTO credentials)
		{
			var customer = await _applicationService.CustomerService.LoginCustomerAsync(credentials);
			if (customer is null)
			{
				ViewData["ValidateMessage"] = "Error: Username / Password invalid";
				return View();
			}

			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, credentials.Username!)
			};

			ClaimsIdentity identity = new ClaimsIdentity(claims, 
				CookieAuthenticationDefaults.AuthenticationScheme);

			AuthenticationProperties properties = new()
			{
				AllowRefresh = true,
				IsPersistent = credentials.KeepLoggedIn,
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(identity), properties);

			return RedirectToAction("Index", "Customer");
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> UpdateCustomerInfoAsync(CustomerPatchDTO request)
		{
			var customer = await _applicationService.CustomerService.GetCustomerByUsername(request.Username!);
			await _applicationService.CustomerService.UpdateCustomerAccountInfoAsync(request, customer!.Id);
			return RedirectToAction("Index", "Home");
		}
	}
}
