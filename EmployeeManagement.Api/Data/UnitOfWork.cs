using EmployeeManagement.Api.Core.Interfaces;

namespace EmployeeManagement.Api.Data {
  public class UnitOfWork: IUnitOfWork {
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    public IEmployeeRepository Employees { get; private set; }
    public IDepartmentRepository Departments { get; private set; }

    public UnitOfWork(ApplicationDbContext context) {
      _context = context;

      Employees = new EmployeeRepository(_context);
      Departments = new DepartmentRepository(_context);
    }

    public async Task<int> CompleteAsync() {
      return await _context.SaveChangesAsync();
    }

    public virtual void Dispose(bool disposing) {
      if(!_disposed) {
        if(disposing) {
          _context.Dispose();
        }
        _disposed = true;
      }
    }

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}