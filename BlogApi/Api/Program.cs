using Blog.Api.Configurations.ColumnWriters;
using Blog.Api.Extensions;
using Blog.Api.Filters;
using Blog.Application;
using Blog.Domain.Entities.Identity;
using Blog.Infrustructure;
using Blog.Infrustructure.Services.Storage.Local;
using Blog.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using SignalR;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options
    => options.AddPolicy("policy", policy =>
policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(origin => true)));

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341/#/events?range=1d")
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL("User ID=admin;Password=123;Host=localhost;Port=5432;Database=Blog","logs",needAutoCreateTable:true,
    columnOptions:new Dictionary<string,ColumnWriterBase>{
        {"message", new RenderedMessageColumnWriter() },
        {"message_template", new MessageTemplateColumnWriter() },
        {"level", new LevelColumnWriter() },
        {"time_stamp", new TimestampColumnWriter() },
        {"exception", new ExceptionColumnWriter() },
        {"log_event", new LogEventSerializedColumnWriter() },
        {"user_name", new UserNameColumnWriter()}
    })
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

builder.Services.addApplicationServices();
builder.Services.addPersistenceServices();
builder.Services.addInfrustructureServices();
builder.Services.addSignalRServices();
builder.Services.addStorage<LocalStorage>();



builder.Services.AddControllers(options => 
    {
        options.Filters.Add<RolePermissionFilter>();
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("user", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["token:audience"],
            ValidIssuer = builder.Configuration["token:issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["token:securityKey"])),

            LifetimeValidator = (notBefore,expires,securityToken,validationParameters) => expires != null ? expires > DateTime.UtcNow : false,


            NameClaimType = ClaimTypes.Name
        };
    });

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.configureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseCors("policy");

app.UseAuthorization();
app.UseAuthentication();


app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name",username);
    await next();
});

app.MapControllers();

app.mapHubs();

app.Run();
