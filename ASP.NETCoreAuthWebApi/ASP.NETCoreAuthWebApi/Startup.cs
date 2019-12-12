using ASP.NETCoreAuthWebApi.DataAcces;
using ASP.NETCoreAuthWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASP.NETCoreAuthWebApi
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
            services.AddDbContext<CartContext>(x => x.UseInMemoryDatabase("Server=DESKTOP-I7816G0;Database=ShopDb;Trusted_Connection=true;"));
            services.AddDbContext<ProductsContext>(x => x.UseInMemoryDatabase("Server=DESKTOP-I7816G0;Database=ShopDb;Trusted_Connection=true;"));
            services.AddTransient<ProductServices>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
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

            app.UseMvc(options => options.MapRoute("default", "api/{controller}/{action}"));
        }
    }
}
