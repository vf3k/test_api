using System;
using System.Linq;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TestApi.Core.Services;
using TestApi.Core.Validators;
using TestApi.DTO;
using TestApi.Infrastructure.DAL;

namespace TestApi
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
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("TestApiDb");
            });
            services.AddLogging();
            services.AddControllers()
                .AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddFluentValidation(opt=>
                {
                    opt.RegisterValidatorsFromAssemblyContaining<PersonValidator>();
                    opt.ImplicitlyValidateChildProperties = true;
                })
                .ConfigureApiBehaviorOptions(api =>
                {
                    api.InvalidModelStateResponseFactory = ctx => CustomValidationErrorResponse(ctx);
                });
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "TestApi", Version = "v1"}); });
            services.AddTransient<RegistrationService>();
        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
             
            });
                
                
        }
        private BadRequestObjectResult CustomValidationErrorResponse(ActionContext ctx)
        {
            var er = new ErrorResponse(null, ErrorCodes.ValidationFailed);
            er.FieldErrors = ctx.ModelState.Where(x => x.Value.Errors.Count > 0)
                .Select(x => new FieldError
                {
                    Field = x.Key,
                    Message = String.Join(", ",x.Value.Errors.Select(c=>c.ErrorMessage)),// FirstOrDefault()?.ErrorMessage,
                    Code = FieldErrorCodes.IsRequired.ToString("G")
                }).ToArray();
            return new BadRequestObjectResult(er);

        }
    }
}