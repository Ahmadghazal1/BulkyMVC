using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly math the Name.");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category Created Successfuly";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id is null || id == 0)
            {
                return NotFound();
            }
            var categoryObj = _dbContext.Categories.Find(id);
            //var categoryObj1 = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            //var categoryObj2 = _dbContext.Categories.Where(x => x.Id == id);
            if (categoryObj is null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category Updated Successfuly";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if(id is null || id == 0)
            {
                return NotFound();
            }

            var categoryObj = _dbContext.Categories.Find(id);
            if(categoryObj is null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var categoryObj = _dbContext.Categories.Find(id); 
            if(categoryObj is null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(categoryObj);
            _dbContext.SaveChanges();
            TempData["success"] = "Category Deleted Successfuly";
            return RedirectToAction("Index");
        }
    }
}
