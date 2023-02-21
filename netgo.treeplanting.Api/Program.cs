using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using netgo.treeplanting.Application.Interfaces;
using netgo.treeplanting.Application.Services;
using netgo.treeplanting.Domain.CommandHandler;
using netgo.treeplanting.Domain.Commands.PlantingPlace;
using netgo.treeplanting.Domain.Commands.Seedling;
using netgo.treeplanting.Domain.Commands.User;
using netgo.treeplanting.Domain.Core.Bus;
using netgo.treeplanting.Domain.Core.DomainEvents;
using netgo.treeplanting.Domain.Core.Notifications;
using netgo.treeplanting.Domain.Interfaces;
using netgo.treeplanting.Domain.Interfaces.Repositories.System;
using netgo.treeplanting.Infrastructure;
using netgo.treeplanting.Infrastructure.Bus;
using netgo.treeplanting.Infrastructure.Database;
using netgo.treeplanting.Infrastructure.EventSourcing;
using netgo.treeplanting.Infrastructure.Repository;
using System.Configuration;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlConnection"]);

});

builder.Services.AddDbContext<EventStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlConnection"]);

});

builder.Services.AddDbContext<DomainNotificationStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlConnection"]);

});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IRequestHandler<CreateUserCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<UpdateUserCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<DeleteUserCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<EmailRegisterCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<PromoteAdminCommand, Unit>, UserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DemoteAdminCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<PromoteTreecoinsDeterminerCommand, Unit>, UserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DemoteTreecoinsDeterminerCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<PromotePlantingOfficerCommand, Unit>, UserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DemotePlantingOfficerCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<PromotePollManagerCommand, Unit>, UserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DemotePollManagerCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<PromoteSeedlingsManagerCommand, Unit>, UserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DemoteSeedlingsManagerCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<AddTreecoinsCommand, Unit>, UserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<RemoveTreecoinsCommand, Unit>, UserCommandHandler>();

builder.Services.AddScoped<IRequestHandler<CreateSeedlingCommand, Unit>, SeedlingCommandHandler>();
builder.Services.AddScoped<IRequestHandler<EditSeedlingCommand, Unit>, SeedlingCommandHandler>();

builder.Services.AddScoped<IRequestHandler<CreatePlantingPlaceCommand, Unit>, PlantingPlaceCommandHandler>();
builder.Services.AddScoped<IRequestHandler<EditPlantingPlaceCommand, Unit>, PlantingPlaceCommandHandler>();

builder.Services.AddScoped<IDomainEventStore, DomainEventStore>();

builder.Services.AddScoped<IMediatorHandler, InMemoryBus>();

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddAutoMapper(Assembly.Load("netgo.treeplanting.Application"));

builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISeedlingService, SeedlingService>();
builder.Services.AddScoped<IPlantingPlaceService, PlantingPlaceService>();

builder.Services.AddScoped<IIdentityMessageService, EmailService>();

builder.Services.AddScoped<IUserSystemRepository, UserSystemRepository>();
builder.Services.AddScoped<ISeedlingSystemRepository, SeedlingSystemRepository>();

builder.Services.AddScoped<IPlantingPlaceSystemRepository, PlantingPlaceSystemRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
            {
                policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
            });
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5001",
            ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["IssuerSigningKey:Key"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



