using ClinicPlus.Contexts;
using ClinicPlus.Validators.Medicos;
using ClinicPlus.Validators.Monitoramentos;
using ClinicPlus.Validators.Pacientes;
using ClinicPlus.ViewModels.Medicos;
using ClinicPlus.ViewModels.Monitoramentos;
using ClinicPlus.ViewModels.Pacientes;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddDbContext<ClinicPlusContext>();
builder.Services.AddScoped<IValidator<AddMedicoViewModel>, AddMedicoValidator>();
builder.Services.AddScoped<IValidator<UpdateMedicoViewModel>, UpdateMedicoValidator>();
builder.Services.AddScoped<IValidator<AddPacienteViewModel>, AddPacienteValidator>();
builder.Services.AddScoped<IValidator<UpdatePacienteViewModel>, UpdatePacienteValidator>();
builder.Services.AddScoped<IValidator<AddMonitoramentoPacienteViewModel>, AddMonitoramentoValidator>();
builder.Services.AddScoped<IValidator<UpdateMonitoramentoPacienteViewModel>, UpdateMonitoramentoValidator>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();