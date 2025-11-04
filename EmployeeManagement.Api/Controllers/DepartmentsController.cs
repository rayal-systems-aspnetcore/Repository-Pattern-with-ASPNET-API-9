using EmployeeManagement.Api.Core.Interfaces;
using EmployeeManagement.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class DepartmentsController: ControllerBase {
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentsController(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartments() {
      var departments = await _unitOfWork.Departments.GetAllAsync();
      return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id) {
      var department = await _unitOfWork.Departments.GetDepartmentWithEmployeesAsync(id);
      if(department == null) {
        return NotFound();
      }
      return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] Department department) {
      if(department == null || string.IsNullOrWhiteSpace(department.Name)) {
        return BadRequest("Department name is required.");
      }

      try {
        var newDepartment = new Department {
          Name = department.Name
        };

        await _unitOfWork.Departments.AddAsync(newDepartment);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.Id }, newDepartment);
      } catch(DbUpdateException ex) {
        return StatusCode(500, $"An error occurred while creating the department: {ex.Message}");
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department update) {
      if(update == null || string.IsNullOrWhiteSpace(update.Name)) {
        return BadRequest("Department name is required.");
      }

      var existing = await _unitOfWork.Departments.GetByIdAsync(id);
      if(existing == null) {
        return NotFound();
      }

      try {
        existing.Name = update.Name;
        await _unitOfWork.CompleteAsync();
        return NoContent();
      } catch(DbUpdateException ex) {
        return StatusCode(500, $"An error occurred while updating the department: {ex.Message}");
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id) {
      var existing = await _unitOfWork.Departments.GetByIdAsync(id);
      if(existing == null) {
        return NotFound();
      }

      try {
        _unitOfWork.Departments.Remove(existing);
        await _unitOfWork.CompleteAsync();
        return NoContent();
      } catch(DbUpdateException ex) {
        return StatusCode(500, $"An error occurred while deleting the department: {ex.Message}");
      }
    }
  }
}
