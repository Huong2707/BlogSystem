using BlogSystem.API.Middleware;
using BlogSystem.Application.Mapping;
using BlogSystem.Application.Services;
using BlogSystem.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Cryptography.Xml;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//logs
Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/Log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
builder.Host.UseSerilog();

//config controller
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
});

//config Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(cf => cf.AddProfile<MappingProfile>());
builder.Services.AddServiceApplication();

var jwtSetting = builder.Configuration.GetSection("Jwt");
var key = jwtSetting["Key"];
if(string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("JWT not key config");
}

var byteKey=Encoding.ASCII.GetBytes(key);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(byteKey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidIssuer = jwtSetting["Issuer"],
        ValidAudience = jwtSetting["Audience"],
        ClockSkew = TimeSpan.Zero
    };
    // for debbug
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole( "Admin"));
    options.AddPolicy("EditorOnly", policy => policy.RequireRole("Editor"));
});

//cau hinh swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BlogSystem API",
        Version = "v1",
        Description = "BlogSystem API with JWT Authentication"
    });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
            },
            new string[] {}
        }
    });
});
builder.Services.AddCors(option =>
   { 
    option.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod()
               .AllowAnyHeader();
    });
});



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
   app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","BlogSystem API V1");
        c.RoutePrefix = "swagger";
    });
}
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();




app.Run();

