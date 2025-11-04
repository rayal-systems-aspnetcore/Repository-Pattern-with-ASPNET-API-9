using EmployeeManagement.Api.Core.Interfaces;
using EmployeeManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeesController: ControllerBase {
    private readonly IUnitOfWork _unitOfWork;

    public EmployeesController(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees() {
      var employees = await _unitOfWork.Employees.GetEmployeesWithDepartmentsAsync();
      return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id) {
      var employee = await _unitOfWork.Employees.GetEmployeeWithDepartmentAsync(id);
      if(employee == null) {
        return NotFound();
      }
      return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto) {
      if(!ModelState.IsValid) {
        return BadRequest(ModelState);
      }

      try {
        var department = await _unitOfWork.Departments.GetByIdAsync(createEmployeeDto.DepartmentId);

        if(department == null) {
          return BadRequest($"Department with ID {createEmployeeDto.DepartmentId} does not exist.");
        }

        var newEmployee = new Models.Employee {
          FirstName = createEmployeeDto.FirstName,
          LastName = createEmployeeDto.LastName,
          Email = createEmployeeDto.Email,
          DepartmentId = createEmployeeDto.DepartmentId
        };

        await _unitOfWork.Employees.AddAsync(newEmployee);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
      } catch(DbUpdateException ex) {
        return StatusCode(500, $"An error occurred while creating the employee: {ex.Message}");
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] CreateEmployeeDto updateEmployeeDto) {
      if(!ModelState.IsValid) {
        return BadRequest(ModelState);
      }

      var existing = await _unitOfWork.Employees.GetByIdAsync(id);
      if(existing == null) {
        return NotFound();
      }

      try {
        // If department changed, validate it exists
        if(existing.DepartmentId != updateEmployeeDto.DepartmentId) {
          var department = await _unitOfWork.Departments.GetByIdAsync(updateEmployeeDto.DepartmentId);
          if(department == null) {
            return BadRequest($"Department with ID {updateEmployeeDto.DepartmentId} does not exist.");
          }
          existing.DepartmentId = updateEmployeeDto.DepartmentId;
        }

        existing.FirstName = updateEmployeeDto.FirstName;
        existing.LastName = updateEmployeeDto.LastName;
        existing.Email = updateEmployeeDto.Email;

        await _unitOfWork.CompleteAsync();

        // 204 No Content is conventional for successful PUT without returning the resource
        return NoContent();
      } catch(DbUpdateException ex) {
        return StatusCode(500, $"An error occurred while updating the employee: {ex.Message}");
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id) {
      var existing = await _unitOfWork.Employees.GetByIdAsync(id);
      if(existing == null) {
        return NotFound();
      }

      try {
        _unitOfWork.Employees.Remove(existing);
        await _unitOfWork.CompleteAsync();
        return NoContent();
      } catch(DbUpdateException ex) {
        return StatusCode(500, $"An error occurred while deleting the employee: {ex.Message}");
      }
    }
  }
}