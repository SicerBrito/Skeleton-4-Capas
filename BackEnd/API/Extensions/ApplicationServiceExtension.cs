using System.Text;
using API.Helpers;
using API.Helpers.Errors;
using API.Services;
using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

    public static class ApplicationServiceExtension{

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",builder=>
                    builder.AllowAnyOrigin()        //WithOrigins(http://domini.com)
                    .AllowAnyMethod()               //WithMethods(*GET*, POST)
                    .AllowAnyHeader());             //WithHeaders(*accept*, content-type)
            });


        public static void AddAplicacionServices(this IServiceCollection services){
            services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IAuthorizationHandler, GlobalVerbRoleHandler>();
        }

        //definimos la configuracion del JWT
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration from AppSettings
            services.Configure<JWT>(configuration.GetSection("JWT"));

            //Adding Athentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                    };
                });
        }

        public static void AddValidationErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {

                    var errors = actionContext.ModelState.Where(u => u.Value!.Errors.Count > 0)
                                                    .SelectMany(u => u.Value!.Errors)
                                                    .Select(u => u.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidation()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        //definimos el limite de peticiones que podemos hacer a un EndPoint
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(options => 
            {
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "6s",
                        Limit = 7
                    }
                };

            });
            
        }

        //Control de versiones de Appis (ver versiones de las apis creadas o Enpoint)
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options => {

                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new QueryStringApiVersionReader("v");
                options.ApiVersionReader = new HeaderApiVersionReader("X-Version");
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("X-Version")
                );
                options.ReportApiVersions = true;

            });
        }
        
    }