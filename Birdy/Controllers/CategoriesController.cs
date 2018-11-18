using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Data;
using Birdy.Extensions;
using Birdy.Models;
using Birdy.Models.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        // TempData used to display data only 1 time
        // on refresh view, the message will go away
        [TempData]
        public string StatusMessage { get; set; }

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET : Action Index
        public async Task<IActionResult> Index()
        {
            var categories = _db.Category.Include(s => s.CategoryGroup);

            return View(await categories.ToListAsync());
        }

        // GET : Action Create
        public async Task<IActionResult> Create ()
        {
            CategoryAndCategoryGroupViewModel model = new CategoryAndCategoryGroupViewModel
            {
                CategoryGroupList = _db.CategoryGroup.ToList(),
                Category = new Category(),
                CategoryList = _db.Category.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToList()
            };

            return View(model);
        }

        // POST : Action Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryAndCategoryGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                // check if Category exists or not & check if Combination of Category and CategoryGroup exists
                var doesCategoryExist = _db.Category.Where(s => s.Name == model.Category.Name).Count();
                var doesCategoryAndCategoryGroupExist = _db.Category.Where(s => s.Name == model.Category.Name && s.CategoryGroupId == model.Category.CategoryGroupId).Count();



                if (doesCategoryExist > 0 && model.isNew)
                {
                    //error
                    StatusMessage = "Error : Category Name already exists";
                }
                else
                {
                    if (doesCategoryExist == 0 && !model.isNew)
                    {
                        // error
                        StatusMessage = "Error : Category does not exist";
                    }
                    else
                    {
                        if (doesCategoryAndCategoryGroupExist > 0)
                        {
                            // error
                            StatusMessage = "Error : CategoryGroup and Category combination exists already";
                        }
                        else
                        {
                            _db.Add(model.Category);
                            await _db.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }

            // If ModelState is not valid
            CategoryAndCategoryGroupViewModel modelVM = new CategoryAndCategoryGroupViewModel()
            {
                CategoryGroupList = _db.CategoryGroup.ToList(),
                Category = model.Category,
                CategoryList = _db.Category.OrderBy(p => p.Name).Select(p => p.Name).ToList(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);
        }


        // GET : Action Edit
        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _db.Category.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryAndCategoryGroupViewModel model = new CategoryAndCategoryGroupViewModel()
            {
                CategoryGroupList = _db.CategoryGroup.ToList(),
                Category = category,
                CategoryList = _db.Category.Select(p => p.Name).Distinct().ToList()
            };
            return View(model);
        }

        // POST : Action Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryAndCategoryGroupViewModel model)
        {
            // check if Category exists or not & check if Combination of Category and CategoryGroup exists
            var doesCategoryExist = _db.Category.Where(s => s.Name == model.Category.Name).Count();
            var doesCategoryAndCategoryGroupExist = _db.Category.Where(s => s.Name == model.Category.Name && s.CategoryGroupId == model.Category.CategoryGroupId).Count();

            if(ModelState.IsValid)
            {
                if (doesCategoryExist == 0)
                {
                    StatusMessage = "Error : Category does not exist. You cannot add a new category here.";
                }
                else
                {
                    if (doesCategoryAndCategoryGroupExist > 0)
                    {
                        StatusMessage = "Error : Categorygroup and Category combination already exists.";
                    }
                    else
                    {
                        var catFromDb = _db.Category.Find(id);
                        // Update Name
                        catFromDb.Name = model.Category.Name;
                        catFromDb.CategoryGroupId = model.Category.CategoryGroupId;

                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            // if Modelstate is not valid
            CategoryAndCategoryGroupViewModel modelVM = new CategoryAndCategoryGroupViewModel()
            {
                CategoryGroupList = _db.CategoryGroup.ToList(),
                Category = model.Category,
                CategoryList = _db.Category.Select(p => p.Name).Distinct().ToList(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);

        }


        // GET : Action Details
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var category = await _db.Category.Include(s => s.CategoryGroup).SingleOrDefaultAsync(m => m.Id == id);
            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET : Action Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _db.Category.Include(s => s.CategoryGroup).SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST : Action Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _db.Category.SingleOrDefaultAsync(m => m.Id == id);
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}