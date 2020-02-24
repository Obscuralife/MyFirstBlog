using AspNetCore.Identity.Mongo;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyBlog.DataAccessLayer;
using MyBlog.DataAccessLayer.Models.Identity;
using MyBlog.Services;
using MyBlog.Services.Abstract;
using MyBlog.Services.Models;
using MyBlog.Services.Models.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyBlog.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().AddFluentValidation();
            services.AddControllers();
            services.Configure<Settings>(Configuration.GetSection("MongoConnection"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.01", new OpenApiInfo { Title = "My first Blog", Version = "v0.01" });
                //  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //  {
                //      In = ParameterLocation.Header,
                //      Description = "Please insert JWT with Bearer into field",
                //      Name = "Authorization",
                //      Type = SecuritySchemeType.ApiKey
                //  });
                //  c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                // {
                //   new OpenApiSecurityScheme
                //   {
                //     Reference = new OpenApiReference
                //     {
                //       Type = ReferenceType.SecurityScheme,
                //       Id = "Bearer"
                //     }
                //    },
                //    new string[] { }
                //  }
                //});
            });

            services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper());
            services.AddSingleton<IEntryService, EntryService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IValidator<EntryRequest>, EntryRequestValidator>();
            services.AddTransient<IValidator<CommentRequest>, CommentRequestValidator>();
            services.AddTransient<IValidator<UpdateRequest>, UpdateRequestValidator>();
            services.AddTransient<IValidator<LogInRequest>, LogInRequestValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

            services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper());

            // Configure Identity MongoDB
            services.AddIdentityMongoDbProvider<DbUser, UserRole>(identityOptions =>
            {
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireDigit = false;
            }, mongoIdentityOptions =>
            {
                mongoIdentityOptions.ConnectionString = "mongodb://localhost/IdentityBlog";
            });

            // Add Jwt Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddAuthentication(options =>
            {
                //Set default Authentication Schema as Bearer
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters =
                       new TokenValidationParameters
                       {
                           ValidIssuer = Configuration["JwtIssuer"],
                           ValidAudience = Configuration["JwtIssuer"],
                           IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                           ClockSkew = TimeSpan.Zero // remove delay of token when expire
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v0.01/swagger.json", "My API V0.01");
            });

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
