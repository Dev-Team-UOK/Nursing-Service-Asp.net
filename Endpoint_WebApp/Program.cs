using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Authentication.Command.SignUp;
using Nursing_Service.Application.Services.Nurse.Command.Create;
using Nursing_Service.Application.Services.Nurse.Command.Delete;
using Nursing_Service.Application.Services.Nurse.Command.Update;
using Nursing_Service.Application.Services.Nurse.Query.GetNurses;
using Nursing_Service.Application.Services.Patient.Command.Create;
using Nursing_Service.Application.Services.Patient.Command.Delete;
using Nursing_Service.Application.Services.Patient.Command.Update;
using Nursing_Service.Application.Services.Patient.Query.GetPatient;
using Nursing_Service.Application.Services.Patient.Query.GetPatients;
using Nursing_Service.Application.Services.PatinetNeedService.Command.CreatePatientNeedService;
using Nursing_Service.Application.Services.PatinetNeedService.Command.DeletePatientNeedService;
using Nursing_Service.Application.Services.PatinetNeedService.Command.UpdatePatientNeedService;
using Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices;
using Nursing_Service.Application.Services.RequestForm.Command;
using Nursing_Service.Application.Services.Service.Command.Create;
using Nursing_Service.Application.Services.Service.Command.Delete;
using Nursing_Service.Application.Services.Service.Command.Update;
using Nursing_Service.Application.Services.Service.Query.GetServices;
using Nursing_Service.Application.Services.SuperVisor.Command.Create;
using Nursing_Service.Application.Services.SuperVisor.Command.Delete;
using Nursing_Service.Application.Services.SuperVisor.Command.Update;
using Nursing_Service.Application.Services.SuperVisor.Query.GetSuperVisors;
using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Application.Services.Users.Commands.Delete;
using Nursing_Service.Application.Services.Users.Commands.Update;
using Nursing_Service.Application.Services.Users.Queries.SignIn;
using Nursing_Service.Application.Services.Users.Query;
using Nursing_Service.Infrastructure.SMS.Ir;
using Nursing_Service.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7010") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/authentication/");
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.SlidingExpiration = true;
});

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option =>
    option.UseSqlServer(@"Data Source=localHost\DEVINSTANCE; Initial Catalog=NursingService; User Id=sa; Password=aA123456; Encrypt=false;")
);

builder.Services.Configure<SmsIrConfig>(builder.Configuration.GetSection("SmsIrConfig"));

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<ICreateUserService, CreateUserService>();
builder.Services.AddScoped<ISignUpUserService, SignUpUserService>();
builder.Services.AddScoped<ISignInUserService, SignInUserService>();
builder.Services.AddScoped<IGetUserService, GetUserService>();
builder.Services.AddScoped<ICreateNurseService, CreateNurseService>();
builder.Services.AddScoped<ICreateSuperVisor, CreateSuperVisor>();
builder.Services.AddScoped<IGetPatients, GetPatients>();
builder.Services.AddScoped<IGetPatientNeedServices, GetPatientNeedSevices>();
builder.Services.AddScoped<IGetNursesService, GetNursesService>();
builder.Services.AddScoped<IGetSuperVisors, GetSuperVisors>();
builder.Services.AddScoped<IGetServices, GetService>();
builder.Services.AddScoped<ICreateRequestForm, CreateRequestForm>();
builder.Services.AddScoped<IGetPatientService, GetPatientService>();
builder.Services.AddScoped<ICreatePatientService, CreatePatientService>();
builder.Services.AddScoped<IDeletePatient, DeletePatient>();
builder.Services.AddScoped<IUpdatePatientService, UpdatePatientService>();
builder.Services.AddScoped<IDeleteUserService, DeleteUserService>();
builder.Services.AddScoped<IUpdateUserService, UpdateUserService>();
builder.Services.AddScoped<IGetNursesService, GetNursesService>();
builder.Services.AddScoped<ICreateNurseService, CreateNurseService>();
builder.Services.AddScoped<IDeleteNurseService, DeleteNurseService>();
builder.Services.AddScoped<IUpdateNurseService, UpdateNurseService>();
builder.Services.AddScoped<IUpdateSuperVisor, UpdateSuperVisor>();
builder.Services.AddScoped<IDeleteSuperVisor, DeleteSuperVisor>();
builder.Services.AddScoped<ICreateService, CreateService>();
builder.Services.AddScoped<IUpdateService, UpdateService>();
builder.Services.AddScoped<IDeleteService, DeleteService>();
builder.Services.AddScoped<ICreatePatientNeedService, CreatePatientNeedService>();
builder.Services.AddScoped<IUpdatePatientNeedService, UpdatePatientNeedService>();
builder.Services.AddScoped<IDeletePatientNeedService, DeletePatientNeedService>();


builder.Services.AddTransient<ISMSIr, SMSIr>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
