using MicroservisProject.Web.Models.Catalog;
using MicroservisProject.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharedLibrary.Services;

namespace MicroservisProject.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserId(_sharedIdentityService.GetUserId));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput input)
        {
            ModelState.Remove(nameof(input.Picture));
            ModelState.Remove(nameof(input.UserId));
            if (!ModelState.IsValid)
            {
                var categories = await _catalogService.GetAllCategories();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View();
            }

            input.UserId = _sharedIdentityService.GetUserId;
            input.Picture = string.Empty;

            await _catalogService.CreateCourse(input);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetCourseById(id);
            var categories = await _catalogService.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", course.CategoryId);

            var courseUpdateInput = new CourseUpdateInput
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                CategoryId = course.CategoryId,
                Feature = course.Feature,
                Description = course.Description,
                Picture = course.Picture,
                UserId = course.UserId,
            };

            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                var course = await _catalogService.GetCourseById(courseUpdateInput.Id);
                var categories = await _catalogService.GetAllCategories();
                ViewBag.Categories = new SelectList(categories, "Id", "Name", course.CategoryId);
                return View();
            }
            await _catalogService.UpdateCourse(courseUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourse(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
