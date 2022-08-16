using CoreMVCIntro_0.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro_0
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
            services.AddControllersWithViews();

            //Hangi servisin ekleneceðini belirliyorsunuz. Bazý servisler eklendiklerinde otomatik olarak kullanýmý alýnýrken bazý servisleri de ekledikten sonra alttaki Configure metodunda özellikle kullanacaðýnýzý belirtmeniz gerekiyor.

            //Burada standart bir Sql baðlantýsý belirtmek istiyorsanýz (sýnýf içerisinde optionsBuilder'ýn belirtilmesindense bu tercih edilir) þu ifadeyi yazmalýsýnýz.

            //Pool kullanmak bir Singleton Pattern görevi görür
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies()); //Böylece baðlantý ayarlarýný burada belirtmiþ olduk.

            //Yukarýdaki ifadede dikkat ederseniz UseLazyLoadingProxies kullanýlmýþtýr. Bu durum .Net Core'daki Lazy Loading'in sürekli tetiklenebilmesi adýna enviroment'ýnýzý garanti altýna almanýzý saðlar.

            //Todo : Sessions iþlemleri
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles(); //wwwroot isimli klasörün projeye açýlmasý için gerekli olan ifadedir.

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ozel",
                    pattern: "Kategori/Urunler",
                    new { Controller = "Category", Action = "CategoryProductList" }
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
