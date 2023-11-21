using AutoMapper;
using CustomersMVC.DTO;
using CustomersMVC.Models;
using CustomersMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomersMVC.Controllers
{
    public class ProductsController : Controller
    {
		private readonly IApplicationService _applicationService;
		private readonly IMapper? _mapper;

		public List<ProductReadOnlyDTO> ProductsReadOnlyDto { get; set; } = new();

		public ProductsController(IApplicationService applicationService, IMapper mapper)
		{
			_applicationService = applicationService;
			_mapper = mapper;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Index()
        {
			var products = await _applicationService.ProductService.GetAllProductsAsync();
			if (products is null)
			{
				ViewData["EmptyProducts"] = "There are no products";
				return View();
			}

			foreach (var product in products)
			{
				ProductReadOnlyDTO? productReadOnlyDto = _mapper!.Map<ProductReadOnlyDTO>(product);
				ProductsReadOnlyDto.Add(productReadOnlyDto);
			}

			ViewData["ProductDto"] = ProductsReadOnlyDto;

			return View();
        }
    }
}
