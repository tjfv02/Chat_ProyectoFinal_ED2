using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMongoDB.Models.DatabaseSettings;
using ApiMongoDB.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiMongoDB
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
            services.Configure<UserDatabaseSettings>(Configuration.GetSection(nameof(UserDatabaseSettings)));
            services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);
            services.AddSingleton<UserService>();

            services.Configure<RoomDatabaseSettings>(Configuration.GetSection(nameof(RoomDatabaseSettings)));
            services.AddSingleton<IRoomDatabaseSettings>(sp => sp.GetRequiredService<IOptions<RoomDatabaseSettings>>().Value);
            services.AddSingleton<RoomService>();

            services.Configure<MessageDatabaseSettings>(Configuration.GetSection(nameof(MessageDatabaseSettings)));
            services.AddSingleton<IMessageDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MessageDatabaseSettings>>().Value);
            services.AddSingleton<MessageService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
