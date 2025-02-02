using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/verify-employment")]
public class EmploymentVerificationController : ControllerBase
{
    private static readonly List<Employee> Employees = new()
    {
        new Employee { EmployeeId = 101, CompanyName = "TechCorp", VerificationCode = "ABC123" },
        new Employee { EmployeeId = 102, CompanyName = "SoftSolutions", VerificationCode = "XYZ789" },
        new Employee { EmployeeId = 103, CompanyName = "Innovatech", VerificationCode = "LMN456" }
    };

    [HttpPost]
    public IActionResult VerifyEmployment([FromBody] VerificationRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.CompanyName) || string.IsNullOrWhiteSpace(request.VerificationCode))
        {
            return BadRequest(new { verified = false, message = "Invalid input" });
        }

        var employee = Employees.FirstOrDefault(e => e.EmployeeId == request.EmployeeId && e.CompanyName == request.CompanyName && e.VerificationCode == request.VerificationCode);
        
        if (employee != null)
        {
            return Ok(new { verified = true, message = "Verified" });
        }
        return NotFound(new { verified = false, message = "Not Verified" });
    }
}

public class Employee
{
    public int EmployeeId { get; set; }
    public required string CompanyName { get; set; }
    public required string VerificationCode { get; set; }
}

public class VerificationRequest
{
    public int EmployeeId { get; set; }
    public required string CompanyName { get; set; }
    public required string VerificationCode { get; set; }
}
