using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Core.Interfaces {
  public interface IDepartmentRepository: IRepository<Department> {
    Task<Department?> GetDepartmentWithEmployeesAsync(int id);
  }
}