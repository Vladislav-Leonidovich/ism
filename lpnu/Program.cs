using lpnu.Data;
using lpnu.Services.Implementations;
using lpnu.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LpnuContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("LpnuContext")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(
	options =>
	{
		options.Password.RequireDigit = true;
		options.Password.RequireUppercase = true;
		options.Password.RequiredLength = 5;
		options.Lockout.MaxFailedAccessAttempts = 5;
		options.User.RequireUniqueEmail = true;
		options.SignIn.RequireConfirmedEmail = false;
	})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IStructurePostService, StructurePostService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IEducationProgramService, EducationProgramService>();
builder.Services.AddScoped<IAccreditationProgramService, AccreditationProgramService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<IConferenceService, ConferenceService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
// Add services to the container.
builder.Services.AddControllersWithViews()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supportedCultures = new[]
		{
			new CultureInfo("en"),
			new CultureInfo("uk")
		};

	options.DefaultRequestCulture = new RequestCulture("uk");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

var task = CreateRolesAndUsers(app.Services);
task.Wait();

app.Run();

async Task CreateRolesAndUsers(IServiceProvider serviceProvider)
{
	using (var scope = serviceProvider.CreateScope())
	{
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

		// Створення ролі "Admin"
		if (!await roleManager.RoleExistsAsync("Admin"))
		{
			await roleManager.CreateAsync(new IdentityRole("Admin"));
		}

		// Створення користувача-адміна
		var adminEmail = "admin@example.com";
		var adminUser = await userManager.FindByEmailAsync(adminEmail);
		if (adminUser == null)
		{
			adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
			await userManager.CreateAsync(adminUser, "AdminPassword123!");
			await userManager.AddToRoleAsync(adminUser, "Admin");
		}
	}
}
