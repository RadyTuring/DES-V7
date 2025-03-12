using Azure;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class DesAppDbContext : DbContext
{
    public DesAppDbContext(DbContextOptions<DesAppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<PatientsV>()
            .ToView("PatientsV");
    }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientVisit> PatientVisits { get; set; }
    
    public DbSet<Diagnosis> Diagnosiss { get; set; }
    
    public DbSet<CaseStatus> CaseStatuss { get; set; }
    public DbSet<DesAccount> DesAccounts { get; set; }

    public DbSet<DesPage> DesPages { get; set; }
    public DbSet<RolePage> RolePages { get; set; }
    public DbSet<DesRole> DesRoles { get; set; }
    public DbSet<Area> Areas { get; set; }
    //views
    public DbSet<PatientsV> PatientsV { get; set; }

}
