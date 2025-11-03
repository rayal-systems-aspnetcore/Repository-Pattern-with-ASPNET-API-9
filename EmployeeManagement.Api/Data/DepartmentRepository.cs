using EmployeeManagement.Api.Core.Interfaces;
using EmployeeManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Data {
  public class DepartmentRepository: Repository<Department>, IDepartmentRepository {
    public DepartmentRepository(ApplicationDbContext context) : base(context) {
    }

    public async Task<Department?> GetDepartmentWithEmployeesAsync(int id) {
      return await _context.Departments
        .Include(d => d.Employees)
        .FirstOrDefaultAsync(d => d.Id == id);
    }
  }
}