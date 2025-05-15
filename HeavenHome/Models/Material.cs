using HeavenHome.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HeavenHome.Models
{
    public class Material:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Picture URL")]
        [Required(ErrorMessage = "Picture is required")]
        public string PictureURL { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Biology")]
        [Required(ErrorMessage = "Biology is required")]
        public string Bio { get; set; }

        //relationships
        public List<Material_Product> Materials_Products { get; set; }

    }
}