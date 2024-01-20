using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StreamingPlatform.Domain;
using StreamingPlatform.Domain.Models;
using System.Globalization;

namespace StreamingPlatform.WebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host =>
                {
                    host.UseKestrel(options =>
                    {
                        options.ConfigureHttpsDefaults(https =>
                        {
                            https.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13;
                        });
                    });
                });

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });
            
            builder.Services.AddDbContext<StreamingPlatformContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDatabase")));
            builder.Services.AddScoped<SongService>();
            builder.Services.AddScoped<AlbumService>();
            builder.Services.AddScoped<MemberService>();
            builder.Services.AddScoped<PlaylistService>();
            builder.Services.AddScoped<SingerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //var options = new RewriteOptions()
            //    .AddRewrite("", "", skipRemainingRules: true)
            //    .AddRedirect("", "");

            //app.UseRewriter(options);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.Use(async (context, next) =>
            //{
            //    var cultureQuery = context.Request.Query["culture"];
            //    if (!string.IsNullOrWhiteSpace(cultureQuery))
            //    {
            //        var culture = new CultureInfo(cultureQuery);

            //        CultureInfo.CurrentCulture = culture;
            //        CultureInfo.CurrentUICulture = culture;
            //    }

            //    //透過呼叫next()管道中的下一個Middleware。
            //    //await next(context);
            //    await next.Invoke();
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello world");
            //});
            app.Run();
        }
    }
}