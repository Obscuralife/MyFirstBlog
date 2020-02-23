using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyBlog.DataAccessLayer;
using MyBlog.Services;
<<<<<<< HEAD
=======
using MyBlog.Services.Abstract;
>>>>>>> Service
using MyBlog.Services.Models;

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
<<<<<<< HEAD
=======

>>>>>>> Service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.01", new OpenApiInfo { Title = "My first Blog", Version = "v0.01" });
            });
<<<<<<< HEAD
            services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper());
            services.AddSingleton<IEntryService, EntryService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddTransient<IValidator<EntryRequest>, EntryRequestValidator>();
            services.AddTransient<IValidator<CommentRequest>, CommentRequestValidator>();
=======

            services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper());
            services.AddSingleton<IEntryService, EntryService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddTransient<IValidator<EntryRequest>, EntryRequestValidator>();
            services.AddTransient<IValidator<CommentRequest>, CommentRequestValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddTransient<IValidator<LogInRequest>, LogInRequestValidator>();
>>>>>>> Service
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
