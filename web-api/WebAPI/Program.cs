
using LogicaAplicacion.CU;
using LogicaAplicacion.InterfacesCU;
using LogicaDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IBuscarAtletasPorDisciplina, BuscarAtletasPorDisciplina>();
            builder.Services.AddScoped<IListadoDisciplinas, ListadoDisciplinas>();
            builder.Services.AddScoped<IBuscarDisciplinaPorId, BuscarDisciplinaPorId>();
            builder.Services.AddScoped<IAltaDisciplina, AltaDisciplina>();
            builder.Services.AddScoped<IModificarDisciplina, ModificarDisciplina>();
            builder.Services.AddScoped<IBajaDisciplina, BajaDisciplina>();
            builder.Services.AddScoped<IBuscarDisciplinaPorNombre, BuscarDisciplinaPorNombre>();
            builder.Services.AddScoped<IBuscarEventos, BuscarEventos>();
            builder.Services.AddScoped<ILogin, Login>();
            builder.Services.AddScoped<IAltaAuditoria, AltaAuditoria>();


            builder.Services.AddScoped<IRepositorioAuditorias, RepositorioAuditorias>();
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosBD>();
            builder.Services.AddScoped<IRepositorioEventos, RepositorioEventosBD>();
            builder.Services.AddScoped<IRepositorioAtletas, RepositorioAtletasBD>();
            builder.Services.AddScoped <IRepositorioDisciplinas, RepositorioDisciplinasBD>();


            string strCon = builder.Configuration.GetConnectionString("ConexionLocal");
            builder.Services.AddDbContext<OlimpiadasContext>(options => options.UseSqlServer(strCon));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ///PARA AUTENTICACIÓN JWT
            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            ////////////////////////////////////////////////////////////////////////////////////


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
