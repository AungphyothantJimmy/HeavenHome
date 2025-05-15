namespace HeavenHome.Models
{
    public class Material_Product
    {
        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
