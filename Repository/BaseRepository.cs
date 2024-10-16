using Office.Context;
using Office.Repository.Interfaces;

namespace Office.Repository;

public class BaseRepository : IBaseRepository
{
    private readonly OfficeContext _context;
    public BaseRepository(OfficeContext context)
    {
        _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }
}
