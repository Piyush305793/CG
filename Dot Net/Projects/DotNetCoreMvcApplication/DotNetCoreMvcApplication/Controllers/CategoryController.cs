using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMvcApplication.Data;
using DotNetCoreMvcApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMvcApplication.Controllers
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
            List<Category> objCategotyList = _db.Categories.ToList();
            return View(objCategotyList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Category Name");
            }
            if(ModelState.IsValid)
            {
                 _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully..!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
       
       public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }   
            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);            //We can use any column to have condition and if not found then return Default value
            Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();   //We can use any column to have condition
            Category? categoryFromDb = _db.Categories.Find(id);                                //Used to find records using only Promary key column value(id)
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully..!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }   
            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);            //We can use any column to have condition and if not found then return Default value
            Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();   //We can use any column to have condition
            Category? categoryFromDb = _db.Categories.Find(id);                                //Used to find records using only Promary key column value(id)
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully..!";
            return RedirectToAction("Index", "Category");
        }
    }
}