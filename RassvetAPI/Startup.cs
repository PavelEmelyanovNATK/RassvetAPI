using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RassvetAPI.Models.RassvetDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RassvetAPI.Services;
using RassvetAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.CookiePolicy;

namespace RassvetAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<RassvetDBContext>(
                options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            var jwtConfigurationModel = new JwtConfigurationModel();
            Configuration.Bind("Authentication", jwtConfigurationModel);
            services.AddSingleton(jwtConfigurationModel);

            services.AddCors();
            services.AddPasswordHasher();
            services.AddAccessTokenGeneratorService();
            services.AddRefreshTokenGeneratorService();
            services.AddRefreshTokensRepository();
            services.AddRefreshTokenValidator();
            services.AddClientsRepository();
            services.AddClientTrainingsRepository();
            services.AddSectionsRepository();
            services.AddTrenersRepository();
            services.AddGroupsRepository();
            services.AddRegistrationService();
            services.AddAuthService();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    //options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = jwtConfigurationModel.Issuer,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = jwtConfigurationModel.Audience,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfigurationModel.AccessTokenSecret)),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => 
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
