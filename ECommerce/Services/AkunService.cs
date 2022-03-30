using eCommerce.Datas;
using eCommerce.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using eCommerce.Interface;

namespace eCommerce.Services;
public class AkunService : BaseDbService, IAkunService
{
    public AkunService(eCommerceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Admin> Login(string username, string password)
    {
        var result = await DbContext.Admins.FirstOrDefaultAsync(x=>x.Username == username && x.Password == password);

        return result;
    }

    public async Task<Customer> Register(Customer obj)
    {
        if(await DbContext.Admins.AnyAsync(x=>x.IdAdmin == obj.IdCustomer)){
            throw new InvalidOperationException($"Admin with ID {obj.IdCustomer} is already exist");
        }

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<Customer> LoginCustomer(string username, string password)
    {
        var result = await DbContext.Customers.FirstOrDefaultAsync(x=>x.Username == username && x.Password == password);

        return result;
    }
}