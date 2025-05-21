using Microsoft.EntityFrameworkCore;
using Nursing_Service.Domain.Entities.Nurse;
using Nursing_Service.Domain.Entities.Patient;
using Nursing_Service.Domain.Entities.Service;
using Nursing_Service.Domain.Entities.SuperVisor;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<PatientNeedService> PatientNeedService { get; set; }
        DbSet<Nurse> Nurses { get; set; }
        DbSet<NurseCanDoService> NurseCanDoService { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<SuperVisor> SuperVisors { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
