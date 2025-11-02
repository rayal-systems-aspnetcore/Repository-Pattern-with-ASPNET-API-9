namespace EmployeeManagement.Api.Models {
  public class Department {
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    public Department() {
      Employees = new HashSet<Employee>();
    }
  }
}