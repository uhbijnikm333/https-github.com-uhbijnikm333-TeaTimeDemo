using Microsoft.AspNetCore.Mvc;
using TeaTimeDemo.Data;
using TeaTimeDemo.Models;

namespace TeaTimeDemo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbcontext _db;
        public CategoryController(ApplicationDbcontext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj) {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "類別名稱不能跟顯示順序一樣");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "類別新增成功!";
                return RedirectToAction("Index");

            }
            return View();
        }
        public IActionResult Edit(int? id) 
        { 
            if(id==0||id == null)
            {
                return NotFound();
            }
            Category?categoryFromDb =_db.Categories.Find(id);
            if (categoryFromDb == null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "類別編輯成功!";
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if(id ==0||id == null)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Category? obj= _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            TempData["success"] = "類別刪除成功!";
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
