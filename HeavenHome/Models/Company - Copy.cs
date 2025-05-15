using HeavenHome.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HeavenHome.Models
{
    public class Company:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Company logo")]
        [Required(ErrorMessage = "Company logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name is required")]
        public string Name { get; set; }

        [Display(Name = "Company Description")]
        [Required(ErrorMessage = "Company description is required")]
        public string Description { get; set; }

        //relationships
        public List<Product> Products { get; set; }
    }
}
