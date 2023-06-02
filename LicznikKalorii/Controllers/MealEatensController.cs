using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LicznikKalorii.Data;
using LicznikKalorii.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LicznikKalorii.Controllers
{
    [Authorize]
    public class MealEatensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MealEatensController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MealEatens
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.MealEaten.Include(m => m.Meal).Where(e => e.Meal.User == user);
            var meals = _context.Meal.Where(e => e.User == user);
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "MealName");
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MealEatens/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            
           
            if (id == null || _context.MealEaten == null)
            {
                return NotFound();
            }

            var mealEaten = await _context.MealEaten
                .Include(m => m.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealEaten == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "MealName");
            return View(mealEaten);
        }

        // GET: MealEatens/Create
        public async Task<IActionResult> Create()
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var meals = _context.Meal.Where(e => e.User == user);
            ViewData["MealId"] = new SelectList(meals, "Id", "MealName");
            return View();
        }

        // POST: MealEatens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MealEatenDay,MealEatenWeight,MealId")] MealEaten mealEaten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealEaten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "MealName");
            return View(mealEaten);
        }

        // GET: MealEatens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id == null || _context.MealEaten == null)
            {
                return NotFound();
            }

            var mealEaten = await _context.MealEaten.FindAsync(id);
            if (mealEaten == null)
            {
                return NotFound();
            }
            var meals = _context.Meal.Where(e => e.User == user);
            ViewData["MealId"] = new SelectList(meals, "Id", "MealName");
            return View(mealEaten);
        }

        // POST: MealEatens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MealEatenDay,MealEatenWeight,MealId")] MealEaten mealEaten)
        {
            if (id != mealEaten.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealEaten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealEatenExists(mealEaten.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "MealName", mealEaten.MealId);
            return View(mealEaten);
        }

        // GET: MealEatens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MealEaten == null)
            {
                return NotFound();
            }

            var mealEaten = await _context.MealEaten
                .Include(m => m.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealEaten == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "MealName");
            return View(mealEaten);
        }

        // POST: MealEatens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MealEaten == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MealEaten'  is null.");
            }
            var mealEaten = await _context.MealEaten.FindAsync(id);
            if (mealEaten != null)
            {
                _context.MealEaten.Remove(mealEaten);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealEatenExists(int id)
        {
          return (_context.MealEaten?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
