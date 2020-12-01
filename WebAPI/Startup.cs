using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Careers;
using Aplicacion.Login;
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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
using WebAPI.Middleware;
using Aplicacion.Inscriptions;
using Aplicacion.DetallesInscriptions;
using Aplicacion.Groups;
using Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Aplicacion.Users;

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
            services.AddMediatR(typeof(ConsultaUser.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaIdUser.Manejador).Assembly);
            services.AddMediatR(typeof(AgregarUser.Manejador).Assembly);
            services.AddMediatR(typeof(EditarUser.Manejador).Assembly);
            services.AddMediatR(typeof(EliminarUser.Manejador).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<AgregarUser>());

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

            //services from Inscriptions
            services.AddMediatR(typeof(ConsultaInscription.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaIdInscription.Manejador).Assembly);
            services.AddMediatR(typeof(AgregarInscription.Manejador).Assembly);
            services.AddMediatR(typeof(EditarInscription.Manejador).Assembly);
            services.AddMediatR(typeof(EliminarInscription.Manejador).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<AgregarInscription>());

            //services from Groups
            services.AddMediatR(typeof(ConsultaGroup.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaIdGroup.Manejador).Assembly);
            services.AddMediatR(typeof(AgregarGroup.Manejador).Assembly);
            services.AddMediatR(typeof(EditarGroup.Manejador).Assembly);
            services.AddMediatR(typeof(EliminarGroup.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaGroupsByTeacher.Manejador).Assembly);
            services.AddMediatR(typeof(EstudiantesByGroup.Manejador).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<AgregarGroup>());

            //services from DetallesInscriptions
            services.AddMediatR(typeof(ConsultaDet.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaIdDet.Manejador).Assembly);
            services.AddMediatR(typeof(AgregarDet.Manejador).Assembly);
            services.AddMediatR(typeof(EditarDet.Manejador).Assembly);
            services.AddMediatR(typeof(EliminarDet.Manejador).Assembly);
            services.AddMediatR(typeof(ConsultaDetailsByInscription.Manejador).Assembly);
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<AgregarDet>());

            //services from Identity
            var builder = services.AddIdentityCore<T_user>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            identityBuilder.AddEntityFrameworkStores<PowerCampus2Context>();
            identityBuilder.AddSignInManager<SignInManager<T_user>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
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
