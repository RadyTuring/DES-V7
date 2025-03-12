

using System.ComponentModel.DataAnnotations;

namespace  Entities; 

public class PatientsV
{
    [Key]
    public int Id { get; set; }
    public string PatientName { get; set; }
    public string NationalId { get; set; }
    public DateTime BirtDate { get; set; }
    public int RepSourceId { get; set; }
    public string RepSourceLevel { get; set; }
    public string RepSourceName { get; set; }
    public string DisName { get; set; }
    public string GovName { get; set; }
}
