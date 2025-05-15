// MaterialsController.cs
// This controller manages the CRUD operations for materials within the application.
// It uses IMaterialsService to interact with the database and provides methods for listing, creating, editing, and deleting materials.

using HeavenHome.Data.Services;
using Microsoft.AspNetCore.Mvc;
using HeavenHome.Models;
using Microsoft.AspNetCore.Authorization;
using HeavenHome.Data.Static;

namespace HeavenHome.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MaterialsController : Controller
    {
        private readonly IMaterialsService _service;

        public MaterialsController(IMaterialsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data); 
        }

        //Get: Materials/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("Name,PictureURL,Bio")] Material material)
        {
            if (ModelState.IsValid)
            {
                return View(material);
            }
            await _service.AddAsync(material);
            return RedirectToAction(nameof(Index));
        }

        //Get; Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var materialDetails = await _service.GetByIdAsync(id);

            if (materialDetails == null) return View("NotFound");
            return View(materialDetails);
        }

        //Get: Materials/Edit/1
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int id)
        {
            var materialDetails = await _service.GetByIdAsync(id);
            if (materialDetails == null) return View("NotFound");
            return View(materialDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PictureURL,Bio")] Material material)
        {
            await _service.UpdateAsync(id, material);
            return RedirectToAction(nameof(Index));
        }

        //Get: Materials/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var materialDetails = await _service.GetByIdAsync(id);
            if (materialDetails == null) return View("NotFound");
            return View(materialDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialDetails = await _service.GetByIdAsync(id);
            if (materialDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}