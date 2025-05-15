using HeavenHome.Data.Base;
using HeavenHome.Models;

namespace HeavenHome.Data.Services
{
    public class CompaniesService:EntityBaseRepository<Company>, ICompaniesService
    {
        public CompaniesService(AppDbContext context) : base(context)
        {
            
        }
    }
}
