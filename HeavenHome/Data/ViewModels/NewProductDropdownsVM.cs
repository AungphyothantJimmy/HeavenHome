using HeavenHome.Models;

namespace HeavenHome.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public NewProductDropdownsVM()
        {
            Companies = new List<Company>();
            Materials = new List<Material>();
        }
        public List<Company> Companies { get; set; }
        public List<Material> Materials { get; set; }
    }
}
