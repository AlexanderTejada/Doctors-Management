using API.Extensiones;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios
builder.Services.AddControllers();
builder.Services.AgregarServiciosAplicacion(builder.Configuration);
builder.Services.AgregarServiciosIdentidad(builder.Configuration);

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
        c.RoutePrefix = string.Empty; // Acceso desde la ra�z
    });
}

// Configuraci�n de CORS, autenticaci�n y autorizaci�n
app.UseCors(x => x.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();

// Ejecutar la aplicaci�n
app.Run();