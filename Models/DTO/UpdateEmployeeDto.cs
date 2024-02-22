namespace SmartWayTest.Models.DTO;

public class UpdateEmployeeDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
    public int? DepartmentId { get; set; }
}