using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var ListOfObject = _unitOfWork.Product.GetAll();

            return View(ListOfObject);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            ViewData["CategoryList"] = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfuly";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var productObj = _unitOfWork.Product.Get(x => x.Id == id);
            //var categoryObj1 = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            //var categoryObj2 = _dbContext.Categories.Where(x => x.Id == id);
            if (productObj is null)
            {
                return NotFound();
            }
            return View(productObj);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Updated Successfuly";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            var productobject = _unitOfWork.Product.Get(x => x.Id == id);
            if (productobject is null)
            {
                return NotFound();
            }
            return View(productobject);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var productobject = _unitOfWork.Product.Get(x => x.Id == id);
            if (productobject is null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(productobject);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfuly";
            return RedirectToAction("Index");
        }
    }
}
