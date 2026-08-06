using Linguaflow.DAL;
using LinguaFlow.BLL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LinguaFlowUI.Data;

namespace LinguaFlowUI
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

            //builder.Services.AddDbContext<ApplicationIdentityContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<LinguaFlowContext>();



            /* builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddEntityFrameworkStores<LinguaFlowContext>()
                 .AddDefaultTokenProviders();
            */


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

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



            static async Task SeedRolesAndAdminUserAsync(IServiceProvider serviceProvider)
            {
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    RoleManager<IdentityRole> roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    UserManager<IdentityUser> userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    // Define roles
                    string[] roles = { "Admin", "Tutor", "Student" };
                    foreach (string role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                    // Create an admin user
                    IdentityUser adminUser = new IdentityUser
                    {
                        UserName = "LFAdmin",
                        Email = "admin@linguaflow.com",
                        EmailConfirmed = true
                    };
                    if (await userManager.FindByEmailAsync(adminUser.Email) == null)
                    {
                        await userManager.CreateAsync(adminUser, "AdminPassword123!");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }

                    // Create a tutor user
                    IdentityUser tutorUser = new IdentityUser
                    {
                        UserName = "LFTutor",
                        Email = "tutor@linguaflow.com",
                        EmailConfirmed = true
                    };
                    if (await userManager.FindByEmailAsync(tutorUser.Email) == null)
                    {
                        await userManager.CreateAsync(tutorUser, "TutorPassword123!");
                        await userManager.AddToRoleAsync(tutorUser, "Tutor");
                    }

                    // Create a student user
                    IdentityUser studentUser = new IdentityUser
                    {
                        UserName = "LFStudent",
                        Email = "student@linguaflow.com",
                        EmailConfirmed = true
                    };
                    if (await userManager.FindByEmailAsync(studentUser.Email) == null)
                    {
                        await userManager.CreateAsync(studentUser, "StudentPassword123!");
                        await userManager.AddToRoleAsync(studentUser, "Student");
                    }
                }
            }


            var app = builder.Build();

            SeedRolesAndAdminUserAsync(app.Services).GetAwaiter().GetResult();


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

            app.MapRazorPages();

            app.Run();
        }
    }
}
