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

            //Hangi servisin eklenece�ini belirliyorsunuz. Baz� servisler eklendiklerinde otomatik olarak kullan�m� al�n�rken baz� servisleri de ekledikten sonra alttaki Configure metodunda �zellikle kullanaca��n�z� belirtmeniz gerekiyor.

            //Burada standart bir Sql ba�lant�s� belirtmek istiyorsan�z (s�n�f i�erisinde optionsBuilder'�n belirtilmesindense bu tercih edilir) �u ifadeyi yazmal�s�n�z.

            //Pool kullanmak bir Singleton Pattern g�revi g�r�r
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies()); //B�ylece ba�lant� ayarlar�n� burada belirtmi� olduk.

            //Yukar�daki ifadede dikkat ederseniz UseLazyLoadingProxies kullan�lm��t�r. Bu durum .Net Core'daki Lazy Loading'in s�rekli tetiklenebilmesi ad�na enviroment'�n�z� garanti alt�na alman�z� sa�lar.

            //Todo : Sessions i�lemleri
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

            app.UseStaticFiles(); //wwwroot isimli klas�r�n projeye a��lmas� i�in gerekli olan ifadedir.

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
