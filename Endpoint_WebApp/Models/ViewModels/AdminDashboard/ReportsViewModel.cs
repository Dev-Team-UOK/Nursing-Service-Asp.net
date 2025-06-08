namespace Endpoint_WebApp.Models.ViewModels.AdminDashboard
{
    public class ReportsViewModel
    {
        public ulong? NumberOfPatients { get; set; }
        public ulong? NumberOfNurses { get; set; }
        public ulong? NumberOfServices { get; set; }
        public ulong? NumberOfPNSInThisMonth { get; set; }
    }
}
