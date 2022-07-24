global using Microsoft.EntityFrameworkCore;
global using GMS.Core.Interfaces;
global using GMS.Core.Repositories;
global using Microsoft.AspNetCore.Mvc;
global using GMS.Data.Models;
global using GMS.Data.Context;
global using System.ComponentModel.DataAnnotations;
global using System.Text;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//AutoMapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var jwt = builder.Configuration.GetSection(nameof(GMS.Api.JwtConfig)).Get<GMS.Api.JwtConfig>().Key;
var audience = builder.Configuration.GetSection(nameof(GMS.Api.JwtConfig)).Get<GMS.Api.JwtConfig>().ValidAudience;
var issuer = builder.Configuration.GetSection(nameof(GMS.Api.JwtConfig)).Get<GMS.Api.JwtConfig>().ValidIssuer;


var connectionString = builder.Configuration.GetConnectionString("LaptopConnection");
builder.Services.AddDbContext<GMSAppContext>(options=>options.UseSqlServer(connectionString));
builder.Services.AddIdentity<GMSUser, IdentityRole>(options=>options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<GMSAppContext>().AddDefaultTokenProviders();

// DI's
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IManagerTypeRepository, ManagerTypeRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
builder.Services.AddScoped<ICoachRepository, CoachRepository>();
builder.Services.AddScoped<ICoachTypeRepository, CoachTypeRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IGymRepository, GymRepository>();


builder.Services.AddCors(options=>options.AddPolicy("MyPolicies", builder=>{
    builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(origin=>true);
}));
builder.Services.AddOptions();
builder.Services.AddMvc(options =>
{
   options.SuppressAsyncSuffixInActionNames = false;
});

var key = Encoding.ASCII.GetBytes(jwt);
builder.Services.AddAuthentication(options =>  
            {  
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            })  
  
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>  
            {  
                options.SaveToken = true;  
                options.RequireHttpsMetadata = false;  
                options.TokenValidationParameters = new TokenValidationParameters()  
                {  
                    ValidateIssuer = true,  
                    ValidateAudience = true,  
                    ValidAudience = audience,  
                    ValidIssuer = issuer,  
                    IssuerSigningKey = new SymmetricSecurityKey(key)  
                };  
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();
app.MapControllers();
app.UseCors("MyPolicies");

app.Run();
