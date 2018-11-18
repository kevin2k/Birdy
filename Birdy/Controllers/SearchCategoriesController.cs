using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Data;
using Birdy.Models;
using Birdy.Models.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdy.Controllers
{
    public class SearchCategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public string strGet;

        public SearchCategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string str)
        {
            IEnumerable<Category> searchValue;

            if(!String.IsNullOrEmpty(str))
            {
                searchValue = await _db.Category.Include(s => s.CategoryGroup).Where(s => s.Name.Contains(str)).ToListAsync();
            }
            else
            {
                searchValue = await _db.Category.Include(s => s.CategoryGroup).ToListAsync();
            }

            SearchViewModel model = new SearchViewModel
            {
                CategoryGroupList = _db.CategoryGroup.ToList(),
                Category = new Category(),
                CategoryList = _db.Category.OrderBy(p => p.Name).Select(p => p.Name).Where(p => p.Contains(str)).Distinct().ToList(),
                SearchEntry = new SearchEntry(),
                CategoriesIEnumerable = searchValue,
            };

            return View(model);
        }

        // POST : Action Index
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexVM (SearchViewModel model)
        {

            if (ModelState.IsValid)
            {
                if(model.SearchEntry.SearchText != null)
                {
                    strGet = model.SearchEntry.SearchText;
                    model.SearchEntry.SearchDate = System.DateTime.Now;
                    _db.Add(model.SearchEntry);
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index), "SearchCategories", new { str = strGet });
        }
    }
}