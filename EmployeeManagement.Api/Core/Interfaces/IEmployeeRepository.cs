using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Core.Interfaces {
  public interface IEmployeeRepository: IRepository<Employee> {
    Task<IEnumerable<Employee>> GetEmployeesWithDepartmentsAsync();
    Task<Employee?> GetEmployeeWithDepartmentAsync(int id);
  }
}