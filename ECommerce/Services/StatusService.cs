using eCommerce.Interface;
using eCommerce.Datas.Entities;
using eCommerce.Datas;
using eCommerce.ViewModels;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services{
public class StatusService : BaseDbService, IStatusService
{
    public StatusService(eCommerceDbContext dbContext) : base(dbContext)
    {
    }

        public async Task<List<StatusOrder>> Get()
        {
            return await DbContext.StatusOrders.ToListAsync();
        }
    }
}
