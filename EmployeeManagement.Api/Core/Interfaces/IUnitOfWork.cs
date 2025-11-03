namespace EmployeeManagement.Api.Core.Interfaces {
  public interface IUnitOfWork: IDisposable {
    IEmployeeRepository Employees { get; }
    IDepartmentRepository Departments { get; }
    Task<int> CompleteAsync();
  }
}