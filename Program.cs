using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TrabajoPracticoBit.DAL.DataContext;
using TrabajoPracticoBit.DAL.Implementaciones;
using TrabajoPracticoBit.DAL.Interfaces;
using TrabajoPracticoBit.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//cache
builder.Services.AddControllers(opt =>
{
opt.CacheProfiles.Add("PorDefecto", new CacheProfile() { Duration = 90 });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
                        "Autenticaion JWT usando el esquema Bearer. \r\n\r\n " +
                        "Ingresa la Palabra 'Bearer' seguida de un [espacio] y despues el token \r\n\r\n " +
                        "Ejemplo \"Bearer yrcuia123siutasn4235aerbter\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },

                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();


builder.Services.AddAutoMapper(typeof(AutomapperProfile));


var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


/*Para Levantar el Frontend 
NOMBRE : PolicyCros  
WithOrigins (https://Localhost:5000/) se pueden poner las que quieras
                   - * - esta abierta a cualquier url

 */
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PolicyCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
