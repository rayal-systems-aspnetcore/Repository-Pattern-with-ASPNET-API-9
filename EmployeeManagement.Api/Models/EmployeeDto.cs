namespace EmployeeManagement.Api.Models {
  public record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    int DepartmentId,
    DepartmentDto? Department
  );
}
