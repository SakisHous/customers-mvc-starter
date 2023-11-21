using CustomersMVC.Configuration;
using CustomersMVC.Data;
using CustomersMVC.Repositories;
using CustomersMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace CustomersMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<CustomersDbContext>(options => options.UseSqlServer(connString));
			
			builder.Services.AddAutoMapper(typeof(MapperConfig));
			builder.Services.AddScoped<IApplicationService, ApplicationService>();
			builder.Services.AddRepositories();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			// Authentication with cookies
			builder.Services.AddAuthentication(
					CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(option =>
				{
					option.LoginPath = "/Customer/Login";
					option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				});

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

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}