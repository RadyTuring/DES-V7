namespace Entities;
public class PatientVisit :BaseEntity
{
    public int Id { get; set; }
    public DateOnly VisitDate { get; set; }
    public int InitialDiagnosisId { get; set; }
    public int FinalDiagnosisId { get; set; }
    public int CaseStatusId { get; set; }
    public int PatientId { get; set; }
    public string ReportSource { get; set; }
}

