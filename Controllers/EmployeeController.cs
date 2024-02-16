using Microsoft.AspNetCore.Mvc;
using SmartWayTest.Contracts;
using SmartWayTest.Models.DTO;
using SmartWayTest.Models.Entities;

namespace SmartWayTest.Controllers;

[Produces("application/json")]
public class EmployeeController : BaseController
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> Create(EmployeeDto employeeDto)
    {
        var id = await _employeeRepository.CreateEmployee(employeeDto);

        return Ok(id);
    }

    [HttpGet("company/{company}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetByCompany(string company)
    {
        var employees = await _employeeRepository.GetEmployeesByCompanyName(company);

        return Ok(employees);
    }

    [HttpGet("department/{department}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetByDepartment(string department)
    {
        var employees = await _employeeRepository.GetEmployeesByDepartmentName(department);

        return Ok(employees);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeDto employeeDto)
    {
        await _employeeRepository.UpdateEmployee(id, employeeDto);
        
        return NoContent();
    }
        
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeRepository.DeleteEmployee(id);

        return NoContent();
    }
    
}