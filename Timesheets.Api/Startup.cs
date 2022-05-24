using AuthenticateService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Timesheets.Api.AutoMapperProfiles;
using Timesheets.Api.Models;
using Timesheets.Models;
using Timesheets.Storage;
using Timesheets.Storage.Repositories;
using Timesheets.Tokens;
using Timesheets.Tokens.Models;

namespace Timesheets.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            OptionsForGenToken optionsForGenToken = Configuration.GetSection("DataToken").Get<OptionsForGenToken>();

            SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(optionsForGenToken.Key));

            services.AddSingleton<IOptions<BaseDataForGenAccessToken>>(
                Options.Create(
                    new BaseDataForGenAccessToken(
                        optionsForGenToken.Issuer, 
                        optionsForGenToken.Audience, 
                        TimeSpan.FromSeconds(optionsForGenToken.LifeTimeSecond),
                        key)));


            services.AddSingleton<IOptions<BaseDataForGetRefreshToken>>(
                Options.Create(
                    new BaseDataForGetRefreshToken(
                        optionsForGenToken.Issuer,
                        optionsForGenToken.Audience,
                        TimeSpan.FromSeconds(optionsForGenToken.LifeTimeSecond * 20),
                        key)));



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = true,
                        ValidIssuer = optionsForGenToken.Issuer,
                        ValidateAudience = true,
                        ValidAudience = optionsForGenToken.Audience,
                        ValidateLifetime = true
                    };
                });

            services.AddAuthorization(opts => 
            {
                opts.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireClaim("token_type", "access")
                    .Build();
            });

            services.AddControllers();

            services.AddAutoMapper(cfg => {
                cfg.AddProfile<AutoMapperToUserProfile>();
                cfg.AddProfile<AutoMapperToEmployeeProfile>();
            });

            services.AddDbContext<TimeSheetDbContext>(opts => opts.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRepository<User>, RepositoryUser>();
            services.AddScoped<IRepository<Employee>, RepositoryEmployee>();

            services.AddScoped<IUserAuthenticate, UserAuthenticate>();

            services.AddSingleton<ITokenGenerator<DataForGenAccessToken, CommonDataTokenWithExpire<DataForGenAccessToken>>,
                TokenGeneratorWithBaseData<BaseDataForGenAccessToken, DataForGenAccessToken>>();

            services.AddSingleton<ITokenGenerator<DataForGenRefreshToken, CommonDataTokenWithExpire<DataForGenRefreshToken>>,
                TokenGeneratorWithBaseData<BaseDataForGetRefreshToken, DataForGenRefreshToken>>();

            services.AddSwaggerGen(opts => 
                opts.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Version = "v1",
                    Title = "API TimesSheets",
                    Description = "API TimesSheets",
                    /*TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kadyrov",
                        Email = string.Empty,
                        Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "можно указать под какой лицензией все опубликовано",
                        Url = new Uri("https://example.com/license"),
                    }*/
                }));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "API TimesSheets"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
