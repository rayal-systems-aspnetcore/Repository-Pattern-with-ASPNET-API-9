namespace EmployeeManagement.Api.Models {
  public class Employee {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    // Foreign Key property
    public int DepartmentId { get; set; }

    // Navigation Property: An Employee belongs to one Department
    public virtual Department Department { get; set; }
  }
}