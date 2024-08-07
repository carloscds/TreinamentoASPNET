using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json.Serialization;
using NetDevPack.Security.Jwt.Core.Jwa;
using APIBanco.InfraEstrutura.Models;
using APIBanco.InfraEstrutura.Repository;
using APIBanco.Middlewares;
using APIBanco.Domain.Entidade;
using APIBanco.Core.Interfaces;
using APIBanco.Core.Services;
using APIBanco.API.Services;
using AspNetCore.Scalar;

namespace APIBanco.API
{
    public static class IoC
    {
        public static void AddCustomServices(this IServiceCollection app, IConfiguration configuration)
        {
            app.AddControllers();

            app.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            app.AddDbContext<OABContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Banco")));
            app.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            app.AddScoped<IClienteService, ClienteService>();
            app.AddScoped<ITokenService, TokenService>();
            app.AddMemoryCache();
        }

        public static void AddSecurity(this IServiceCollection app, IConfiguration configuration)
        {
            app.AddJwksManager(o =>
            {
                o.Jws = Algorithm.Create(DigitalSignaturesAlgorithm.RsaSha256);
                o.Jwe = Algorithm.Create(EncryptionAlgorithmKey.RsaOAEP).WithContentEncryption(EncryptionAlgorithmContent.Aes128CbcHmacSha256);
            })
            .UseJwtValidation()
            .PersistKeysToDatabaseStore<OABContext>();

            app.AddDefaultIdentity<Usuario>(options =>
            { 
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<OABContext>()
            .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });

            app.AddAuthorization();
            app.AddHttpClient();
        }

        public static void AddCustomSwagger(this IServiceCollection app)
        {
            app.AddEndpointsApiExplorer();
            app.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Treinamento ASP.NET - www.carloscds.net",
                        Description = "API Base",
                        Version = "v1"
                    });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Entre em com um token valido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                options.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
            });
        }

        public static void UseCustomEndpoints(this WebApplication app)
        {
            app.MapControllers();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "DribionPay.API v1");
            });


            // Scalar
            app.UseScalar(options =>
            {
                options.UseTheme(Theme.Default);
                options.RoutePrefix = "docs";
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseJwksDiscovery();

            app.UseMiddleware<MiddlewareException>();
            app.UseMiddleware<MiddlewareToken>();

            app.MapGet("/", async context =>
            {
                await context.Response.WriteAsync($"Treinamento ASP.NET ({DateTime.Now}) - Update: {AssemblyBuildDate()}");
            });
        }

        private static DateTime AssemblyBuildDate()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var fileInfo = new FileInfo(entryAssembly.Location);
            return fileInfo.LastWriteTime;
        }
    }
}
