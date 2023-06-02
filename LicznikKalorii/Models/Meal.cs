using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LicznikKalorii.Models
{
    public class Meal
    {
        public int Id { get; set; }

        [Display(Name = "Posiłek")]
        public string MealName { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Kalorie na 100g")]
        public float Kcal { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
