using Microsoft.EntityFrameworkCore;

namespace CoreMVCIntro_0.Models.Context
{

    //EntityFrameworkCore.SqlServer kütüphanesini indirmeyi unutmayın. Options ayarları yoksa gelmeyecektir.
    public class MyContext : DbContext
    {
        //Dependency Injection yapısı Core platformunuzun arkasında otomatik olarak entegre gelir. Dolayısıyla siz bir veritabanı sınıfınızın constructor'ına parametre olarak bir DbContextOptions<> tipinde bir yapı verirseniz bu parametreye argüman DI sayesinde Startup'dan gönderilir.

        //public MyContext(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(connectionString: "server=.;database=CoreDB;uid=sa;pwd=123");
        //}

        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
    }
}
