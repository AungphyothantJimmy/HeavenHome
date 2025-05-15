// ProductsController.cs
// This controller manages the CRUD operations for products in the HeavenHome application. 
// It allows users to view a list of all products, filter products based on a search string, 
// view details of a specific product, and perform create and edit operations on products. 
// It interacts with the IProductsService to retrieve and manipulate product data. 

using HeavenHome.Data.Services;
using HeavenHome.Data.Static;
using HeavenHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HeavenHome.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync(n => n.Company);
            return View(allProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync(n => n.Company);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allProducts);
        }

        //Get: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductbyIdAsync(id);
            return View(productDetail);
        }

        //Get: Products/Create
        public async Task <IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "Name");
            ViewBag.Materials = new SelectList(productDropdownsData.Materials, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "Name");
                ViewBag.Materials = new SelectList(productDropdownsData.Materials, "Id", "Name");

                return View(product);
            }

            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }


        //Get: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {

            var productDetails = await _service.GetProductbyIdAsync(id);
            if(productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                ImageURL = productDetails.ImageURL,
                ProductCategory = productDetails.ProductCategory,
                CompanyId = productDetails.CompanyId,
                MaterialIds = productDetails.Materials_Products.Select(n => n.MaterialId).ToList(),
            };

            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "Name");
            ViewBag.Materials = new SelectList(productDropdownsData.Materials, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("notFound");

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "Name");
                ViewBag.Materials = new SelectList(productDropdownsData.Materials, "Id", "Name");

                return View(product);
            }

            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}