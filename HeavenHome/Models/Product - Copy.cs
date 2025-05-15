using HeavenHome.Data.Base;
using HeavenHome.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeavenHome.Models
{
    public class Product:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public ProductCategory ProductCategory { get; set; }

        //relationships
        public List<Material_Product> Materials_Products { get; set; }

        //company
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
    }
}
