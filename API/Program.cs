using API.Extensiones;
using Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AgregarServiciosAplicacion(builder.Configuration);
builder.Services.AgregarServiciosIdentidad(builder.Configuration);
builder.Services.AddScoped<IdbInicializador, DbInicializador>();

var app = builder.Build();

// Usar ExceptionMiddleware personalizado
app.UseMiddleware<API.Middleware.ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errores/{0}"); // Redirige a la ruta de errores

// Otros middlewares
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoctorApp API v1");
        c.InjectStylesheet("/css/swagger-custom.css"); // Inyecta el CSS personalizado
        c.RoutePrefix = string.Empty; // Acceso desde la raíz
    });
}

// Configuración de CORS, autenticación y autorización
app.UseCors(x => x.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
    {
        var dbInicializador = services.GetRequiredService<IdbInicializador>();
        dbInicializador.Inicializar();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrio un error al ejecutar la migracion");
    }
}

// Mapeo de controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
