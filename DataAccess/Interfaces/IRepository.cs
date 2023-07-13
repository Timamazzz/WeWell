namespace DataAccess.Interfaces;

internal interface IRepository<DAL> where DAL : class
{
    Task<int?> CreateAsync(DAL dal);
    Task<List<DAL>?> GetAllAsync();
    Task<DAL?> GetByIdAsync(int? id);
    Task UpdateAsync(DAL dal);
    Task DeleteAsync(int id);
}
