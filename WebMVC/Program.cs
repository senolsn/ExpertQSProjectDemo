using Repository.Abstract;
using Repository.Concrete;
using Service.Abstract;
using Service.Concrete;

namespace ExpertQSProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(); //Session'u kullanacaðýmý belirtiyorum.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<IProductService, ProductManager>();
            builder.Services.AddSingleton<IProductRepository, ProductRepositoryDal>();
            builder.Services.AddSingleton<IUserRepository, UserRepositoryDal>();
            builder.Services.AddSingleton<IUserService, UserManager>();
            builder.Services.AddSingleton<IAuthService, AuthManager>();
            builder.Services.AddSingleton<IChangeRepository, ChangeRepository>();
            builder.Services.AddSingleton<IChangeService, ChangeManager>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");
            app.UseSession(); //Session Middleware'i buraya ekliyorum.
            app.Run();
        }
    }
}