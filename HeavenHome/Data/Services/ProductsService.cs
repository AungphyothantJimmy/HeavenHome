using HeavenHome.Data.Base;
using HeavenHome.Data.ViewModels;
using HeavenHome.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HeavenHome.Data.Services
{
    public class ProductsService:EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CompanyId = data.CompanyId,
                ProductCategory = data.ProductCategory,
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            //Add Product Materials
            foreach(var materialId in data.MaterialIds)
            {
                var newMaterialProduct = new Material_Product()
                {
                    ProductId = newProduct.Id,
                    MaterialId = materialId
                };
                await _context.Materials_Products.AddAsync(newMaterialProduct);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductbyIdAsync(int id)
        {
            var productDetails = await _context.Products
                .Include(c => c.Company)
                .Include(am => am.Materials_Products).ThenInclude(a => a.Material)
                .FirstOrDefaultAsync(n => n.Id == id);

            return productDetails;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Materials = await _context.Materials.OrderBy(n => n.Name).ToListAsync(),
                Companies = await _context.Companies.OrderBy(n => n.Name).ToListAsync()
            };
            
            return response;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.CompanyId = data.CompanyId;
                dbProduct.ProductCategory = data.ProductCategory;

                await _context.SaveChangesAsync();
            }
            
            //Remove existing materials
            var existingMaterialsDb = _context.Materials_Products.Where(n => n.ProductId == data.Id).ToList();
            _context.Materials_Products.RemoveRange(existingMaterialsDb);
            await _context.SaveChangesAsync();


            //Add Product Materials
            foreach (var materialId in data.MaterialIds)
            {
                var newMaterialProduct = new Material_Product()
                {
                    ProductId = data.Id,
                    MaterialId = materialId
                };
                await _context.Materials_Products.AddAsync(newMaterialProduct);
            }
            await _context.SaveChangesAsync();
        }
    }
}
