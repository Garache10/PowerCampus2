using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Careers;
using Aplicacion.Login;
using Aplicacion.Users;
using Aplicacion.Courses;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<PowerCampus2Context>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //services from Users
            services.AddMediatR(typeof(Consulta.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaId.Manejador).Assembly);
            services.AddMediatR(typeof(Agregar.Manejador).Assembly);
            services.AddMediatR(typeof(Editar.Manejador).Assembly);
            services.AddMediatR(typeof(Eliminar.Manejador).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Agregar>());

            //services from Login
            services.AddMediatR(typeof(Log_In.Manejador).Assembly);

            //services from Careers
            services.AddMediatR(typeof(ConsultaCareer.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaIdCareer.Manejador).Assembly);
            services.AddMediatR(typeof(AgregarCareer.Manejador).Assembly);
            services.AddMediatR(typeof(EditarCareer.Manejador).Assembly);
            services.AddMediatR(typeof(EliminarCareer.Manejador).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<AgregarCareer>());

            //services from Courses
            services.AddMediatR(typeof(ConsultaCourse.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaIdCourse.Manejador).Assembly);
            services.AddMediatR(typeof(AgregarCourse.Manejador).Assembly);
            services.AddMediatR(typeof(EditarCourse.Manejador).Assembly);
            services.AddMediatR(typeof(EliminarCourse.Manejador).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<AgregarCourse>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ManejadorErrorMiddleware>();

            app.UseCors(opt =>
            {
                opt.WithOrigins("http://localhost:4200");
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
