using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LicznikKalorii.Models
{
    public class MealEaten
    {
        public int Id { get; set; }

        [Display(Name = "Data posiłku")]
        
        public DateTime MealEatenDay { get; set; }

        [Display(Name = "Waga posiłku w gramach")]
        [Range(1, 1000)]
        public float MealEatenWeight { get; set; }
        [Display(Name = "Posiłek")]
        public int MealId { get; set; }

        
        public virtual Meal? Meal { get; set; }

      
    }
}
