using Faroosom.BLL.Interfaces;
using Faroosom.BLL.Services;
using Faroosom.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Faroosom.API
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
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Faroosom.API", Version = "v1" }));

            var connectionString = Configuration["ConnectionString"];
            services.AddDbContext<FaroosomContext>(options =>
                options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(typeof(FaroosomContext).Assembly.GetName().Name)));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMessageService, MessageServise>();

            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Faroosom.API v1"));
            }
            app.UseCors("AnyOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}