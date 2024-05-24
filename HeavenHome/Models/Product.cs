using HeavenHome.Data;
using System.ComponentModel.DataAnnotations;

namespace HeavenHome.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
