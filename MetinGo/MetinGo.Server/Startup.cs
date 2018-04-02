using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.Fight;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Filters;
using MetinGo.Server.Infrastructure.Session;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MetinGo.Server
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
            services.AddMvc();
            var connection = @"Server=localhost\MSSQLENTERPRISE;Database=MetinGo;Trusted_Connection=False;ConnectRetryCount=0;User ID=sa;Password=Qwerty54123!";
            services.AddDbContext<MetinGoDbContext>(options => options.UseSqlServer(connection));
	        services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICharacterService, CharacterService>();
	        services.AddScoped<ISessionManager, SessionManager>();
            services.AddScoped<IMonsterService, MonsterService>();
            services.AddScoped<IFightService, FightService>();
            services.AddScoped<IFightSimulator, FightSimulator>();
            services.AddScoped<IFightProcessor, FightProcessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper();
	        services.AddScoped<UserContextFilter>();
	        services.AddScoped<CharacterContextFilter>();
            services.AddScoped<PositionContextFilter>();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var cultureInfo = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
