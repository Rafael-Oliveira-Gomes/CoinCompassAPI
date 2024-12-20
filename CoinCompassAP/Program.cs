using System.Text;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Application.Service;
using CoinCompassAPI.Application.Util;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;
using CoinCompassAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the database context to use SQL Server.
var connectionString = builder.Configuration.GetConnectionString("ConnectionDB");

// Configuração do DbContext
builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseSqlServer(connectionString));

// Configuração do Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// Configuração da Autenticação JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateLifetime = true
    };
});

// Configuração do Swagger
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinCompassAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IInvestimentoRepository, InvestmentRepository>();
builder.Services.AddScoped<ISavingsGoalRepository, SavingsGoalRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransactionRepository>();
builder.Services.AddScoped<IOutgoingRepository, OutgoingsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddScoped<ISavingsGoalService, SavingsGoalService>();
builder.Services.AddScoped<ITransactionService, TransasctionService>();
builder.Services.AddScoped<IOutgoingsService, OutgoingsService>();
builder.Services.AddScoped<ValidacoesFinanceiras>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoinPass V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(p => {
    p.AllowAnyMethod();
    p.AllowAnyHeader();
    p.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
