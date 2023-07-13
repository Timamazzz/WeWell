namespace Domain.Interfaces;

internal interface IService<DTO> where DTO : class
{
    Task<int?> CreateAsync(DTO dto);
    Task<List<DTO>?> GetAllAsync();
    Task<DTO?> GetByIdAsync(int id);
    Task UpdateAsync(DTO dto);
    Task DeleteAsync(int id);
}
