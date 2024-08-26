using Ligl.LegalManagement.Api.Extensions;
using Ligl.LegalManagement.Repository;
using FluentValidation;
using Ligl.LegalManagement.Api.Model;
using Ligl.LegalManagement.Business.Query;


using Ligl.Core.Sdk.Shared.Repository.Master;
using Ligl.Core.Sdk.Shared.Repository.Region;
using Ligl.Core.Sdk.Shared.Repository.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LiglMaster");
var logConnectionString = builder.Configuration.GetConnectionString("LogDb");

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers()
    .AddOData(
        opt => opt.AddRouteComponents("v1", EdmModelBuilder.GetModels())
            .EnableQueryFeatures(maxTopValue: 2)
            .SetMaxTop(100)
            .SkipToken()
            .Select()
            .Filter()
            .OrderBy()
            .Count()
            .Expand()
    );


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:5001",
        ValidAudience = "https://localhost:5001",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    gen =>
    {
        gen.IgnoreObsoleteProperties();
        gen.DescribeAllParametersInCamelCase();
        gen.CustomSchemaIds(type => type.FullName);
        gen.SwaggerDoc("v1", new OpenApiInfo { Title = "Ligl.LegalManagement.Api", Version = "v1" });
    });

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssemblyContaining(typeof(CaseCustodianDetailQueryHandler));
});
builder.Services.AddDependency();


builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins((builder.Configuration["Origin"] == null ||
                            string.IsNullOrEmpty(builder.Configuration["Origin"])
                ? builder.Configuration["Origin"]
                : "*") ?? "*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<StoreBaseContext>(options =>
{
    if (logConnectionString != null)
        options.UseSqlServer(logConnectionString,
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 1,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });
});

builder.Services.AddDbContext<MasterBaseContext>(options =>
{
    if (connectionString != null)
        options.UseSqlServer(connectionString,
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 1,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });
});

builder.Services.AddDbContext<RegionBaseContext>();
builder.Services.AddDbContext<RegionContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ligl.LegalManagement.Api v1"); });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
