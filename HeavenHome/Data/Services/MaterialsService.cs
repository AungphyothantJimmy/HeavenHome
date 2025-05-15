using HeavenHome.Data.Base;
using HeavenHome.Models;
using Microsoft.EntityFrameworkCore;

namespace HeavenHome.Data.Services
{
    public class MaterialsService : EntityBaseRepository<Material>, IMaterialsService
    {
        public MaterialsService(AppDbContext context) :base(context) { }
    }
}
