namespace DataAccess.Interfaces
{
    public interface IRepository<DTO, DAL> where DTO : class where DAL : class
    {
        Task<int?> CreateAsync(DTO dto);
        Task<List<DTO>> GetAllAsync();
        Task<DTO?> GetByIdAsync(int? id);
        Task UpdateAsync(DTO dto);
        Task DeleteAsync(int id);
    }
}
