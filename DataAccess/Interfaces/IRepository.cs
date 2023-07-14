namespace DataAccess.Interfaces;

internal interface IRepository<DAL> where DAL : class
{
    Task<int?> CreateAsync(DAL entity);
    Task<List<DAL>?> GetAllAsync();
    Task<DAL?> GetByIdAsync(int? id);
    Task UpdateAsync(DAL entity);
    Task DeleteAsync(int id);
}
