
namespace Dto;

public class PatientDto
{
    public string PatientName { get; set; }
    public string? NID { get; set; }
    public DateOnly? BirtDate { get; set; }
    public int? AreaId { get; set; }
}
