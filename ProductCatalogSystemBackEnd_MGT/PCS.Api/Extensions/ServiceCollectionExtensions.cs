using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PCS.Core.Settings;
using System;
using System.Text;

namespace PCS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppDbSettings>(configuration.GetSection(nameof(AppDbSettings)));
            services.Configure<CloudinarySettings>(configuration.GetSection(nameof(CloudinarySettings)));
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));
            services.Configure<RedisSettings>(configuration.GetSection(nameof(RedisSettings)));
            return services;
        }

        public static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            var byteKey = Encoding.ASCII.GetBytes(jwtSettings.JwtKey);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(byteKey),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero,
                };
            });
            return services;
        }

        public static IServiceCollection AddCorsWithOptions(this IServiceCollection services, string pcsCorsPolicy)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: pcsCorsPolicy,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://example.com")
                                      .AllowAnyHeader().AllowAnyMethod();
                                  });
            });
            return services;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisSettings.ConStr;
                options.InstanceName = redisSettings.InstanceName;
            });
            return services;
        }

        public static IServiceCollection AddControllersWithLoopIgnore(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PCS.Api", Version = "v1" });
            });
            return services;
        }
    }
}
