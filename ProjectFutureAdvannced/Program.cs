using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectFutureAdvannced.Data;
using ProjectFutureAdvannced.Models.IRepository;
using ProjectFutureAdvannced.Models.Model;
using ProjectFutureAdvannced.Models.Model.AccountUser;
using ProjectFutureAdvannced.Models.SqlRepository;

var builder = WebApplication.CreateBuilder(args);



#region ConfigureService
/**********add mvc*********/
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    var Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(Policy));
}
).AddXmlSerializerFormatters();
/*******************ConnectionString*********************/
builder.Services.AddDbContextPool<AppDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnectionStrings"));
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});
/********* Add Identity**********/
builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
/*********Dependency Injection**********/
builder.Services.AddScoped<IAdminRepository, SqlAdminRepository>();
builder.Services.AddScoped<IShopRepository, SqlShopRepository>();
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<ICategoryRepository, SqlCategoryRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped< IWishlistRRepository, WishlistRepository>();

#endregion
/***************************************/
var app = builder.Build();
/***************************************/


//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;

//try
//    {
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    await SeedRolesAsync(roleManager);
//    }
//catch (Exception ex)
//    {
//    throw new Exception();
//    }
/***************************************/


#region ConfigureService
if (app.Environment.IsDevelopment())
    {
    app.UseDeveloperExceptionPage();
    }
app.UseStaticFiles();
app.UseAuthentication();   
app.UseAuthorization();
app.UseMvcWithDefaultRoute();
#endregion

app.Run();
