
using Entities;

namespace Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Patient> Patients { get;  }
    IRepository<PatientVisit> PatientVisits { get;  }
    IRepository<Diagnosis> Diagnosiss { get;  }
    IRepository<DesAccount> DesAccounts { get;  }
    IRepository<DesPage> DesPages { get; }
    IRepository<RolePage> RolePages { get; }
    IRepository<DesRole> DesRoles { get; }
    IRepository<Area> Areas { get; }
    IRepository<CaseStatus> CaseStatuss { get; }

    int Save();

    //views
     IRepository<PatientsV> PatientsV { get; }

}
