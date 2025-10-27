using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tostao.Application.Interfaces;
using Tostao.Infraestructure.Context;
using Tostao.Infraestructure.Repository;
using Tostao.Infrastructure;

namespace Tostao.Api
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddControllers();

            //Documentación API
            services.AddSwaggerGen(options =>
            options.SwaggerDoc(
                "v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tostao Test",
                    Description = "Tostao Test John Culma",
                    TermsOfService = new Uri("https://example.com/contact"),
                    Contact = new OpenApiContact
                    {
                        Name = "Tostao Test John Culma",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licence",
                        Url = new Uri("https://example.com/license")
                    }
                })
            );

            // Dependency Injection
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            services.AddScoped<IDocumentoService, DocumentoService>();

            //Mapeo de base de datos en perfiles a la configuración
            //services.AddAutoMapper(typeof(Startup));

            //////Configuración de logs de errores
            //services.AddSingleton<ILoggerManager, LoggerManager>();

            ////Configuración de la base de datos
            services.AddDbContext<TostaoAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();
            //Uso de Cors
            app.UseCors("CorsApi");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
