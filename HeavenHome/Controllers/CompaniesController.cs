// CompaniesController.cs
// This controller manages the CRUD operations for companies within the application.
// It uses ICompaniesService to interact with the database and provides methods for listing, creating, editing, and deleting companies.

using HeavenHome.Data.Services;
using HeavenHome.Data.Static;
using HeavenHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HeavenHome.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CompaniesController : Controller
    {
        public readonly ICompaniesService _service;

        public CompaniesController(ICompaniesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allcompanies = await _service.GetAllAsync();
            return View(allcompanies);
        }

        //Get: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("Logo,Name,Description")]Company company)
        {
            if(ModelState.IsValid) return View(company);
            await _service.AddAsync(company);
            return RedirectToAction(nameof(Index));
        }

        //Get: Companies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("NotFound");
            return View(companyDetails);
        }

        //Get: Companies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("NotFound");
            return View(companyDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Company company)
        {
            if (ModelState.IsValid) return View(company);
            await _service.UpdateAsync(id, company);
            return RedirectToAction(nameof(Index));
        }

        //Get: Companies/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("NotFound");
            return View(companyDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}