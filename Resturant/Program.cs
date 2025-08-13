using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using Resturant_DAL.Repository;
using Resturant_DAL.ImplementRepository;
using Resturant_DAL.DataBase;
using Microsoft.EntityFrameworkCore;
using Chief_BLL.Services;
using Microsoft.AspNetCore.Identity;
using Resturant_BLL.ImplementServices;
using Resturant_BLL.Mapperly;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Resturant_PL.Language;
using System.Configuration;



namespace Resturant_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
});
            ;
  
    
            builder.Services.AddHttpContextAccessor();
            var con = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<SharaawyContext>(options => options.UseSqlServer(con));
            builder.Services.AddDbContext<ResturantContext>(options =>
            options.UseSqlServer(
                    con,
                    b => b.MigrationsAssembly("Resturant_DAL")  // Specify migrations assembly here
                )
            );
            // add register for Repository
            builder.Services.AddScoped<IRepository<Branch>, BranchRepo>();
            builder.Services.AddScoped<IRepository<table>, TableRepo>();
            builder.Services.AddScoped<IRepository<Review>, ReviewRepo>();
            builder.Services.AddScoped<IRepository<Chief>, ChiefRepo>();
            builder.Services.AddScoped<IRepository<Order>, OrderRepo>();
            builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepo>();
            builder.Services.AddScoped<IRepository<MenueItem>, MenueItemRepo>();
            builder.Services.AddScoped<IRepository<Payment>, PaymentRepo>();
            builder.Services.AddScoped<IRepository<Reservation>, ReservationRepo>();
            builder.Services.AddScoped<IRepository<ReservedTable>, ReservedTableRepo>();

            // add register for service interfaces
            builder.Services.AddScoped<IBranchService, BranchService>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IChiefService, ChiefService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IMenueItemService, MenueItemService>();
           
            builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IReservedTableService, ReservedTableService>();
            builder.Services.AddScoped<IPaymentService,PaymentService>();

            builder.Services.AddScoped<IEmailSenderService,EmailSenderService>();

            builder.Services.AddSingleton<OrderMapper>();
            builder.Services.AddSingleton<OrderItemMapper>();
            builder.Services.AddSingleton<MenueItemMapper>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<GeminiService>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var apiKey = configuration["Gemini:ApiKey"];
                return new GeminiService(apiKey);
            });
            var apiKey = builder.Configuration["Gemini:ApiKey"];
            builder.Services.AddScoped<GeminiService>(_ => new GeminiService(apiKey));

          

            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 4;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.SignIn.RequireConfirmedAccount = true;
                

            }).AddEntityFrameworkStores<ResturantContext>().AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                   policy.RequireRole("Admin"));
                options.AddPolicy("ConfirmedEmail", policy =>
    policy.RequireAssertion(context =>
        context.User.HasClaim(c =>
            c.Type == "EmailConfirmed" &&
            c.Value == "true")));
            });
            builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:SecretKey"];
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };



            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
            });
            app.Run();
        }
    }
}
