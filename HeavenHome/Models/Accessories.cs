using System.ComponentModel.DataAnnotations;

namespace HeavenHome.Models
{
    public class Accessories
    {
        [Key]
        public int Id { get; set; }

        public string QuantityAndName { get; set; }
        public string PictureURL { get; set; }
    }
}
