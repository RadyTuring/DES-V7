 
namespace Entities;
public class Patient:BaseEntity
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public string? NID { get; set; }
    public DateOnly? BirtDate   { get; set; }
    public int AreaId { get; set; }
  
}

