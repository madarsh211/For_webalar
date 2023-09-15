
using Bulky.DataAcess.Data;
using Bulkyweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulkyweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<category> objcategorylist = _db.categories.ToList();
            return View(objcategorylist);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
				TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index");

            }
            return View();

        }


		public IActionResult Edit(int? id)
		{   if(id==null || id == 0)
            {
                return NotFound();
            }
            category? categoryFormDb = _db.categories.Find(id);
            if (categoryFormDb == null) 
            {
                return NotFound();
            }
			return View(categoryFormDb);
		}
		[HttpPost]
		public IActionResult Edit(category obj)
		{
			if (ModelState.IsValid)
			{
				_db.categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category updated sucessfully";
				return RedirectToAction("Index");

			}
			return View();

		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			category? categoryFormDb = _db.categories.Find(id);
			if (categoryFormDb == null)
			{
				return NotFound();
			}
			return View(categoryFormDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			category obj = _db.categories.Find(id); 
			if (obj == null)
			{
				return NotFound();
			}
			_db.categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");

			

		}
	}
}
