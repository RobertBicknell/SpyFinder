using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpyFinderData;

namespace SpyFinder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //TODO: check this file...
        // This method gets called by the runtime. Use this method to add services to the container.
        //        public void ConfigureServices(IServiceCollection services)
        //      {
        //        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        //  }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
            //.AddDbContext<SpyDBContext>(options => options.)
            .AddDbContext<SpyDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PROD")));
            //.BuildServiceProvider();
            services.AddScoped<ISpyDBContext, SpyDBContext>(); //TODO singleton? / scoped / transient?
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) //TODO: check this
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
