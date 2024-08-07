using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SignalR.Contexts;
using SignalR.Hubs;
using SignalR.Models.Services;

namespace SignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var mvcBuilder = builder.Services.AddControllersWithViews();

#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif

            string conectionString = "Data Source=.;Initial Catalog=SignalRDB;Integrated Security=True;TrustServerCertificate=True";
            builder.Services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(conectionString));

            builder.Services.AddScoped<IChatRoomService, ChatRoomService>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(Options =>
                {
                    Options.LoginPath = "/Home/login";
                });

            builder.Services.AddSignalR();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<SiteChatHub>("/chathub");
            app.MapHub<SupportHub>("/supporthub");

            app.Run();
        }
    }
}
