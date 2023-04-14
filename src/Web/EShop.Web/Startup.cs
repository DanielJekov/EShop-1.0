namespace EShop.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using EShop.Data;
    using EShop.Data.Models;
    using EShop.Data.Seeding;
    using EShop.Services.Categories;
    using EShop.Services.Subcategory;
    using EShop.Services.Articles;
    using EShop.Services.Bag;
    using EShop.Services.Cloudinary;
    using EShop.Services.User;
       
    using CloudinaryDotNet;
     
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EShopDbContext>(OptionsBuilder =>
                OptionsBuilder.UseSqlServer(connectionString));

            services.AddDefaultIdentity<EShopUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<EShopRole>().AddEntityFrameworkStores<EShopDbContext>();

            var cloudinaryCredentials = new CloudinaryDotNet.Account(
            this.configuration["Cloudinary:CloudName"],
            this.configuration["Cloudinary:ApiKey"],
            this.configuration["Cloudinary:ApiSecret"]);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry();

            services.Configure<CookiePolicyOptions>(
              options =>
              {
                  options.CheckConsentNeeded = context => true;
                  options.MinimumSameSitePolicy = SameSiteMode.Strict;
              });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/vhod");

            services.AddControllersWithViews(options =>
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddSingleton(cloudinaryUtility);
            services.AddSingleton(this.configuration);

            services.AddRazorPages();

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();

            services.AddSingleton(this.configuration);

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubcategoryService, SubcategoryService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IBagService, BagService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EShopDbContext>();
                dbContext.Database.Migrate();
                new EShopDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithRedirects("/Home/Error{0}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
