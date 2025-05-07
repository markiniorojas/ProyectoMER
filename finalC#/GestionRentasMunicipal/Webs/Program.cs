using Business;
using Data;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexi�n a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer("name=DefaultConnection")
);

// Configurar CORS para permitir solicitudes desde cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Definici�n de Servicios del Rol
builder.Services.AddScoped<RolBusiness>();
builder.Services.AddScoped<RolData>();

// Definici�n de Servicios del Usuario
builder.Services.AddScoped<UserBusiness>();
builder.Services.AddScoped<UserData>();

// Definici�n de Servicios de los Permisos
builder.Services.AddScoped<PermissionBusiness>();
builder.Services.AddScoped<PermissionData>();

// Definici�n de Servicios de los M�dulos
builder.Services.AddScoped<ModuleBusiness>();
builder.Services.AddScoped<ModuleData>();

// Definici�n de Servicios de los RolUsers
builder.Services.AddScoped<RolUserBusiness>();
builder.Services.AddScoped<RolUserData>();

// Definici�n de Servicios de los formularios
builder.Services.AddScoped<FormBusiness>();
builder.Services.AddScoped<FormData>();

// Definici�n de Servicios de los Modulos formularios
builder.Services.AddScoped<ModuleFormBusiness>();
builder.Services.AddScoped<ModuleFormData>();

// Definici�n de Servicios de los RolFormPermissions
builder.Services.AddScoped<RolFormPermissionBusiness>();
builder.Services.AddScoped<RolFormPermissionData>();

var app = builder.Build();

// Configurar el middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS antes de la autorizaci�n
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
