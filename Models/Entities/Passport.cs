using System.Text.Json.Serialization;

namespace SmartWayTest.Models.Entities;

public class Passport
{
    public string Number { get; set; }
    public string Type { get; set; }
    
    [JsonIgnore]
    public int EmployeeId { get; set; }
}