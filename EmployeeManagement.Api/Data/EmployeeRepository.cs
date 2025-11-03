using EmployeeManagement.Api.Core.Interfaces;
using EmployeeManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Data {
  public class EmployeeRepository: Repository<Employee>, IEmployeeRepository {
    public EmployeeRepository(ApplicationDbContext context) : base(context) {
    }

    public async Task<IEnumerable<Employee>> GetEmployeesWithDepartmentsAsync() {
      return await _context.Employees
        .Include(e => e.Department)
        .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeWithDepartmentAsync(int id) {
      return await _context.Employees
        .Include(e => e.Department)
        .FirstOrDefaultAsync(e => e.Id == id);
    }
  }
}