using eCommerce.Datas;
using eCommerce.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using eCommerce.Interface;
using eCommerce.ViewModels;

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

    public async Task<Customer> Register(RegisterViewModel request)
    {
         //check username sudah ada atau belum di db
        if(await DbContext.Customers.AnyAsync(x=>x.Username == request.Username)){
            throw new InvalidOperationException($"{request.Username} already exist");
        }

        //check email exist or not
        if(await DbContext.Customers.AnyAsync(x=>x.Email == request.Email)){
            throw new InvalidOperationException($"{request.Email} already exist");
        }
        
        //check nohp exist or not
        if(await DbContext.Customers.AnyAsync(x=>x.NoHp == request.NoHp)){
            throw new InvalidOperationException($"{request.NoHp} already exist");
        }

        var newCustomer = request.ConvertToDbModel();
        await DbContext.Customers.AddAsync(newCustomer);

        await DbContext.SaveChangesAsync();

        return newCustomer; 
    }

    public async Task<Customer> LoginCustomer(string username, string password)
    {
        var result = await DbContext.Customers.FirstOrDefaultAsync(x=>x.Username == username && x.Password == password);

        return result;
    }

    public async Task<List<Tuple<int, string>>> GetAlamat(int idCustomer){
        return await DbContext.Alamats.Where(x=>x.IdCustomer == idCustomer)
        .Select(x => new Tuple<int, string>(x.IdAlamat, x.Detail))
        .ToListAsync();
    }
}