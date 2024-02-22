using System.Data;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using SmartWayTest.Contracts;
using SmartWayTest.Data;
using SmartWayTest.Models.DTO;
using SmartWayTest.Models.Entities;

namespace SmartWayTest.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DapperContext _context;

    public EmployeeRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<int> CreateEmployee(EmployeeDto employee)
    {
        string query =
            "INSERT INTO Employees(Name, Surname, Phone, CompanyId, DepartmentId) VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId)" +
            "SELECT CAST(SCOPE_IDENTITY() AS int)";                 

        var parameters = new DynamicParameters();
        parameters.Add("Name", employee.Name, DbType.String);
        parameters.Add("Surname", employee.Surname, DbType.String);
        parameters.Add("Phone", employee.Phone, DbType.String); 
        parameters.Add("CompanyId", employee.CompanyId, DbType.Int32);
        parameters.Add("DepartmentId", employee.DepartmentId, DbType.Int32);

        using (var connection = _context.CreateConnection())
        {
            int id = await connection.QuerySingleAsync<int>(query, parameters);

            return id;
        }
    }
    
    public async Task<IEnumerable<Employee>> GetEmployeesByCompanyName(string company)
    {
        string query =
            "SELECT com.Id, com.Name, emp.*\n" +
            "FROM Companies com\n" +
            "JOIN Employees emp ON com.Name = @Company and emp.CompanyId = com.Id\n";
            

        using (var connection = _context.CreateConnection())
        {
            var companyDict = new Dictionary<int, Company>();
            var companyEmployees = await connection.QueryAsync<Company, Employee, Company>(
                query, (com, employee) =>       
                {
                    if (!companyDict.TryGetValue(com.Id, out var currentCom))
                    {
                        currentCom = com;
                        companyDict.Add(currentCom.Id, currentCom);
                    }

                    currentCom.Employees.Add(employee);
                    return currentCom;
                }, new {company}
            );
            
            return companyDict.Count() == 0 ? new List<Employee>() : companyDict.Values.First().Employees;
        }
    }
    
    public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string department)
    {
        string query =
            "SELECT dep.Id, dep.Name, emp.*\n" +
            "FROM Departments dep\n" +    
            "JOIN Employees emp ON dep.Id = emp.DepartmentId and dep.Name = @Department\n";
       

        using (var connection = _context.CreateConnection())
        {
            var depDict = new Dictionary<int, Department>();    
            var depEmployees = await connection.QueryAsync<Department, Employee, Department>(
                query, (dep, employee) =>       
                {
                    if (!depDict.TryGetValue(dep.Id, out var currentDep))
                    {
                        currentDep = dep;
                        depDict.Add(currentDep.Id, currentDep);
                    }

                    currentDep.Employees.Add(employee);
                    return currentDep;
                }, new {department}
            );
            
            return depDict.Count() == 0 ? new List<Employee>() : depDict.Values.First().Employees;
        }
    }
    
    public async Task DeleteEmployee(int id)
    {
        string query = "DELETE FROM Employees where Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            int count = await connection.ExecuteAsync(query, new {id});
        }
    }

    public async Task UpdateEmployee(int id, UpdateEmployeeDto employeeUpdate)
    {
        using (var connection = _context.CreateConnection())
        {
            
            string query = "UPDATE Employees SET ";

            var parameters = new DynamicParameters();
            if (employeeUpdate.Name != null)
            {
                query += "Name = @Name,";
                parameters.Add("Name", employeeUpdate.Name, DbType.String);
            }
            
            if (employeeUpdate.Surname != null)
            {
                query += "Surname = @Surname,";
                parameters.Add("Surname", employeeUpdate.Surname, DbType.String);
            }
            
            if (employeeUpdate.Phone != null)
            {
                query += "Phone = @Phone,";
                parameters.Add("Phone", employeeUpdate.Phone, DbType.String);
            }

            if (employeeUpdate.CompanyId != null)
            {
                query += "CompanyId = @CompanyId,";
                parameters.Add("CompanyId", employeeUpdate.CompanyId, DbType.Int32);
            }
            
            if (employeeUpdate.DepartmentId != null)
            {
                query += "DepartmentId = @DepartmentId,";
                parameters.Add("DepartmentId", employeeUpdate.DepartmentId, DbType.Int32);
            }
            
            query = query.TrimEnd(',');
            
            query += " WHERE Id = @Id";
            parameters.Add("Id", id, DbType.Int32);

            await connection.ExecuteAsync(query, parameters);
        }
        
    }
}