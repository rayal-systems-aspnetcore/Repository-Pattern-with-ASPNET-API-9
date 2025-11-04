using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Api.Models {
  public record CreateEmployeeDto(
    string FirstName,
    string LastName,
    [EmailAddress] string Email,
    int DepartmentId
  );
}