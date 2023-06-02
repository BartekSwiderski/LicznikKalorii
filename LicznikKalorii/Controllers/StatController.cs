using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LicznikKalorii.Data;
using System.Globalization;
using LicznikKalorii.Models;
using System.Linq;

namespace LicznikKalorii.Controllers
{
    [Authorize]
    public class StatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StatController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            DateTime startDate = DateTime.Now.Subtract(TimeSpan.FromDays(14));

            ViewBag.Stat = _context.Meal.Where(u => u.User == user).Select(em => new
            {

                name = em.MealName,
                kcal = em.Kcal,
                MealCuount = (int?)_context.MealEaten.Where(e => e.MealId == em.Id && DateTime.Compare(e.MealEatenDay, startDate) >= 0 && em.User == user).Count(),

                maxWeight = (int?)_context.MealEaten
                                                   .Where(e => e.MealId == em.Id && DateTime.Compare(e.MealEatenDay, startDate) > 0 && em.User == user)
                                                   .Select(e => e.MealEatenWeight).Max(),

                porcja = em.Kcal * 0.01 * (int?)_context.MealEaten
                                                   .Where(e => e.MealId == em.Id && DateTime.Compare(e.MealEatenDay, startDate) > 0 && em.User == user)
                                                   .Select(e => e.MealEatenWeight).Max()
            });
            return View();
        }
    }
}
