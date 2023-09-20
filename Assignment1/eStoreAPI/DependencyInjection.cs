using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Commons;
using System.Text;
using System.Text.Json.Serialization;

namespace eStoreAPI
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(opt =>
			{
				opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description = "Bearer Generated JWT-Token",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"

				});
				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = JwtBearerDefaults.AuthenticationScheme
							}
						}, new string[] { }
					}
				});
			});
			services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			services.AddValidatorsFromAssembly(typeof(Program).Assembly);
			services.AddFluentValidationAutoValidation();
			services.AddFluentValidationClientsideAdapters();


			return services;
		}

		public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("ApiSettings:JWTOptions"));
			var settingsSection = builder.Configuration.GetSection("ApiSettings:JwtOptions");
			var secret = settingsSection.GetValue<string>("Secret");
			var issuer = settingsSection.GetValue<string>("Issuer");
			var audience = settingsSection.GetValue<string>("Audience");
			var key = Encoding.ASCII.GetBytes(secret!);
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidIssuer = issuer,
					ValidAudience = audience,
					ValidateAudience = true
				};
			});
			builder.Services.AddAuthentication();
			return builder;
		}
	}
}
