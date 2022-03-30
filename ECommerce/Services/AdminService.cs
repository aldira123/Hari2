using eCommerce.Interface;
using eCommerce.Datas;
using eCommerce.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services;
public class AdminService : BaseDbService, IAdminService
{
    public AdminService(eCommerceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Admin> Add(Admin obj)
    {
         if(await DbContext.Admins.AnyAsync(x=>x.IdAdmin == obj.IdAdmin)){
            throw new InvalidOperationException($"Admin with ID {obj.IdAdmin} is already exist");
        }

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var Admin = await DbContext.Admins.FirstOrDefaultAsync(x=>x.IdAdmin == id);

        if(Admin == null) {
            throw new InvalidOperationException($"Admin with ID {id} doesn't exist");
        }

        DbContext.Remove(Admin);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Admin>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Admins
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Admin?> Get(int id)
    {
        var result = await DbContext.Admins.FirstOrDefaultAsync(x=>x.IdAdmin == id);

        if(result == null)
        {
            throw new InvalidOperationException($"Admin with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Admin?> Get(Expression<Func<Admin, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Admin>> GetAll()
    {
        return await DbContext.Admins.ToListAsync();
    }

    public async Task<Admin> Update(Admin obj)
    {
       if(obj == null)
        {
            throw new ArgumentNullException("Admin cannot be null");
        }

        var admin = await DbContext.Admins.FirstOrDefaultAsync(x=>x.IdAdmin == obj.IdAdmin);

        if(admin == null) {
            throw new InvalidOperationException($"Admin with ID {obj.IdAdmin} doesn't exist in database");
        }

        admin.Nama = obj.Nama;
        admin.NoHp = obj.NoHp;
        admin.Username = obj.Username;
        admin.Password = obj.Password;
        admin.Email = obj.Email;
        
        DbContext.Update(admin);
        await DbContext.SaveChangesAsync();

        return admin;
    }
}