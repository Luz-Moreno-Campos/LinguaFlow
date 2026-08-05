using Linguaflow.DAL;
using LinguaFlow.BLL;
using Microsoft.EntityFrameworkCore;

namespace LinguaFlow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LinguaFlowContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<TutorRepository>();
            builder.Services.AddScoped<TutorService>();

            builder.Services.AddScoped<CourseRepository>();
            builder.Services.AddScoped<CourseService>();

            builder.Services.AddScoped<LanguageRepository>();
            builder.Services.AddScoped<LanguageService>();

            builder.Services.AddScoped<StudentRepository>();
            builder.Services.AddScoped<StudentService>();

            builder.Services.AddScoped<EnrollmentRepository>();
            builder.Services.AddScoped<EnrollmentService>();

            builder.Services.AddScoped<TutorFeeRepository>();
            builder.Services.AddScoped<TutorFeeService>();

            builder.Services.AddScoped<PaymentRepository>();
            builder.Services.AddScoped<PaymentService>();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
