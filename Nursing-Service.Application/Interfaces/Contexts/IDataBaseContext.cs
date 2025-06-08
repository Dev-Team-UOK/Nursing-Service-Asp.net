using Microsoft.EntityFrameworkCore;
using Nursing_Service.Domain.Entities.Nurse;
using Nursing_Service.Domain.Entities.Patient;
using Nursing_Service.Domain.Entities.RequestForm;
using Nursing_Service.Domain.Entities.Service;
using Nursing_Service.Domain.Entities.SuperVisor;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientNeedService> PatientNeedService { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<NurseCanDoService> NurseCanDoService { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SuperVisor> SuperVisors { get; set; }
        public DbSet<RequestForm> RequestForms { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
