using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PTDuc.WhereHouse.BL.BusinessLayer;
using PTDuc.WhereHouse.BL.BusinessLayer.Login;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.BL.Interfaces.Login;
using PTDuc.WhereHouse.BL.MIddlewares;
using PTDuc.WhereHouse.ChatHub;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.DatabaseLayer.Login;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.DL.Interfaces.Login;
using PTDuc.WhereHouse.MapperProfile;
using PTDuc.WhereHouse.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PTDuc.WhereHouse", Version = "v1" });
            });
            //Sử dụng signalR làm thư viện chat
            services.AddSignalR();
            services.AddScoped<IDLUser, DLUser>();
            services.AddScoped<IBLUser, BLUser>();
            

            services.AddScoped<IBLLogin, BLLogin>();
            services.AddScoped<IDLLogin, DLLogin>();

            services.AddScoped<IBLHouse, BLHouse>();
            services.AddScoped<IDLHouse, DLHouse>();

            services.AddScoped<IBLAddress, BLAddress>();

            services.AddScoped<IBLFile, BLFile>();
            services.AddScoped<IDLFile, DLFile>();

            services.AddScoped<IDLWishlist, DLWishlist>();
            services.AddScoped<IBLWishlist, BLWishlist>();

            services.AddScoped<IDLPost, DLPost>();
            services.AddScoped<IBLPost, BLPost>();
            
            services.AddScoped<IDLMessage, DLMessage>();
            services.AddScoped<IBLMessage, BLMessage>();
            
            services.AddScoped<IDLConversation, DLConversation>();
            services.AddScoped<IBLConversation, BLConversation>();

            services.AddScoped<IDLHouseType, DLHouseType>();
            services.AddScoped<IBLHouseType, BLHouseType>();

            services.AddScoped<IDLReport, DLReport>();
            services.AddScoped<IBLReport, BLReport>();

            services.AddDbContext<WhereHouseContext>(
            options => options.UseSqlServer("name=ConnectionStrings:WhereHouseDatabase"));

            //lấy ra key lưu trong appsettings.json
            var key = Configuration.GetSection("Authentication").GetSection("Key").Value;
            services.AddAuthentication(x =>
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
            services.AddSingleton<IAuthenticationManager>(new AuthenticationManager(key));

            services.AddAutoMapper((typeof(AutoMapperProfile).Assembly));

            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<User, EntityModels.DTO.User>();
            //    cfg.CreateMap<File, EntityModels.DTO.File>();
            //    cfg.CreateMap<House, EntityModels.DTO.House>();
            //    cfg.CreateMap<HouseType, EntityModels.DTO.HouseType>();
            //    cfg.CreateMap<Post, EntityModels.DTO.Post>();
            //    cfg.CreateMap<Wishlist, EntityModels.DTO.Wishlist>();
            //});
            //// only during development, validate your mappings; remove it before release
            //configuration.AssertConfigurationIsValid();
            //// use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
            //var mapper = configuration.CreateMapper();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PTDuc.WhereHouse v1"));
            }

            //app.UseHttpsRedirection();
            //cors
            //lấy ra server host con vuejs
            var serverClient = Configuration.GetSection("ServerClient").GetSection("Key").Value;
            app.UseCors(builder =>
            {
                builder.WithOrigins(serverClient)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            //Nhận các file static từ wwwroot
            app.UseStaticFiles();

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChattingHub>("/chathub");
            });
        }
    }
}
