using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShipmentsAPI.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShipmentsAPI.Services;
using ShipmentsAPI.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.DtoModels.Validators;
using FluentValidation.AspNetCore;

namespace ShipmentsAPI
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
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddDbContext<ShipmentsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ShipmentsDbConnection")));
            services.AddScoped<ShipmentsDataSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IForwarderService, ForwarderService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IWarehouseAreaService, WarehouseAreaService >();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<ICmrDataService, CmrDataService>();
            services.AddScoped<IValidator<QueryShipments>, QueryShipmentsValidator>();
            services.AddScoped<IValidator<QueryForwarders>, QueryForwardersValidator>();
            services.AddScoped<IValidator<QueryCustomers>, QueryCustomersValidator>();
            services.AddScoped<IValidator<QueryPurchaseOrders>, QueryPurchaseOrdersValidator>();
            services.AddHttpContextAccessor();

            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>
                    builder.AllowAnyMethod()
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShipmentsDataSeeder seeder)
        {
            app.UseCors("FrontEndClient");
            //seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShipmentsAPI");
            });

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
