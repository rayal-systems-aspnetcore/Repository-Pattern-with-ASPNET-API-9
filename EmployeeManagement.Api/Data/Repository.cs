using EmployeeManagement.Api.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Api.Data {
  public class Repository<T>: IRepository<T> where T : class {
    protected readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context) {
      _context = context;
      _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) {
      return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync() {
      return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) {
      return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity) {
      await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities) {
      await _dbSet.AddRangeAsync(entities);
    }

    public void Remove(T entity) {
      _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities) {
      _dbSet.RemoveRange(entities);
    }
  }
}