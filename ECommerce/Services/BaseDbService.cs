using eCommerce.Interface;
using eCommerce.Datas;
using eCommerce.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services;
public class BaseDbService
{
    protected readonly eCommerceDbContext DbContext;
    public BaseDbService(eCommerceDbContext dbContext)
    {
        DbContext = dbContext;
    }
}