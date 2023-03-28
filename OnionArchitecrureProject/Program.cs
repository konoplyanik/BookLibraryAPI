using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer.Service.Implementation;
using System.Reflection;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using WebAPI_Layer;
using WebAPI_Layer.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    string connection = builder.Configuration.GetConnectionString("DefaultConnection");

    // Add services to the container.
    builder.Services.AddDbContext<AppDbContext>(con => con.UseSqlServer(connection))
        .AddUnitOfWork()
        .AddCustomRepository<Book, BookService>()
        .AddCustomRepository<ApplicationUser, UserService>()
        .AddCustomRepository<Order, OrderService>();

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config => 
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.Password.RequireLowercase = false;    
    })       
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

    var assembly = Assembly.GetAssembly(typeof(MappingProfile));
    builder.Services.AddAutoMapper(assembly);

    builder.Services.AddIdentityServer()
        .AddAspNetIdentity<ApplicationUser>()
        .AddInMemoryClients(Configuration.Clients)
        .AddInMemoryIdentityResources(Configuration.IdentityResources)
        .AddInMemoryApiResources(Configuration.ApiResources)
        .AddInMemoryApiScopes(Configuration.ApiScopes)
        .AddDeveloperSigningCredential();

    builder.Services.ConfigureApplicationCookie(config =>
    {
        config.Cookie.Name = "BookLibrary.Identity.Cookie";
        config.LoginPath = "/Auth/Login";
        config.LogoutPath = "/Auth/Logout";
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
               ValidAudience = builder.Configuration["JWT:ValidAudience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
            };                       
        });            

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "OkBlog API",
            Version = "v1",
            Description = "API for OkBlog service"
        });
        c.ExampleFilters();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter `Bearer` [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer aksjakjsakjskajsakjsakjskajsk\""
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }
                        , new string[]{}
                    }
                });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();


    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseIdentityServer();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseAuthorization();

    app.MapControllers();

    // При старте проверяем создана ли база.
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Произошла ошибка при инициализации приложения");
        }
    }

    app.Run();
}
catch (Exception e)
{
    logger.Error(e);
    throw (e);
}
finally
{
    NLog.LogManager.Shutdown();
}

