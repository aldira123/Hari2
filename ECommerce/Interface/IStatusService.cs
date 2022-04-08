using eCommerce.Datas.Entities;

namespace eCommerce.Interface
{
    public interface IStatusService 
    {
        Task<List<StatusOrder>>Get();
    }
}
