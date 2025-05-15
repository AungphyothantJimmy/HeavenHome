using HeavenHome.Models;
using HeavenHome.Data.Base;
using HeavenHome.Models;
using System.Linq.Expressions;
using HeavenHome.Data.ViewModels;

namespace HeavenHome.Data.Services
{
    public interface IProductsService:IEntityBaseRepository<Product>
    {
        Task<Product> GetProductbyIdAsync(int id);
        Task<NewProductDropdownsVM> GetNewProductDropdownsValues();
        Task AddNewProductAsync(NewProductVM data);
        Task UpdateProductAsync(NewProductVM data);
    }
}
