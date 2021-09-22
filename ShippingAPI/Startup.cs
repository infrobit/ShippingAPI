using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShippingAPI.Service;
using Newtonsoft.Json;
using ShippingAPI.Model;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace ShippingAPI
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
            services.AddControllers();
            services.AddDbContextPool<ShippingContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Shipping")));

            //Injeção de dependencia nos controllers
            services.AddScoped<IVeiculoService<Veiculo>, VeiculoService>();
            services.AddScoped<ICargaService<Carga>, CargaService>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shipping API",
                    Description = "Teste prático Seglabs ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Guilherme Vasconcelos (@codelheira)",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/codelheira"),
                    }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                foreach (var name in Directory.GetFiles(basePath, "*.XML", SearchOption.AllDirectories))
                {
                    c.IncludeXmlComments(name);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shipping API V1");
                c.RoutePrefix = string.Empty;              
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
