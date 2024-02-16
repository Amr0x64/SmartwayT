using SmartWayTest.Models.DTO;
using SmartWayTest.Models.Entities;

namespace SmartWayTest.Contracts;

public interface IEmployeeRepository
{
    Task<int> CreateEmployee(EmployeeDto employee);
    Task DeleteEmployee(int id);
    Task UpdateEmployee(int id, UpdateEmployeeDto employeeDto);
    Task<IEnumerable<Employee>> GetEmployeesByCompanyName(string company);
    Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string department);
}