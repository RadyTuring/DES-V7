
using DAL;
using Entities;
using Interfaces;
using System.Collections.Generic;

namespace Data;

public class UnifOfWork : IUnitOfWork
{
    private readonly DesAppDbContext _context;
    public UnifOfWork(DesAppDbContext context)
    {
        _context = context;
        Patients = new Repository<Patient>(_context);
        PatientVisits = new Repository<PatientVisit>(_context);
        Diagnosiss = new Repository<Diagnosis>(_context);
        CaseStatuss = new Repository<CaseStatus>(_context);

        DesAccounts = new Repository<DesAccount>(_context);
        DesPages = new Repository<DesPage>(_context);
        RolePages = new Repository<RolePage>(_context);
        DesRoles = new Repository<DesRole>(_context);
        Areas = new Repository<Area>(_context);

        //views

        PatientsV = new Repository<PatientsV>(_context);

    }
    public IRepository<Patient> Patients { get; }
    public IRepository<PatientVisit> PatientVisits { get; }
    public IRepository<Diagnosis> Diagnosiss { get; }
    
    public IRepository<CaseStatus> CaseStatuss { get; }
    public IRepository<DesAccount> DesAccounts { get; }
    public IRepository<DesPage> DesPages { get; }
    public IRepository<RolePage> RolePages { get; }
    public IRepository<DesRole> DesRoles { get; }
    public IRepository<Area> Areas { get; }

    public IRepository<PatientsV> PatientsV { get; }
    public void Dispose()
    {
        _context.Dispose();
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}
