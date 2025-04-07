using CCP;
using CCP.CCPServices;
using Crayon_Service.Helpers;
using CrayonService.Command;
using CrayonService.Command.Validators;
using CrayonService.Queries;
using CrayonService.Queries.Validators;
using CrayonService.Repository;
using CrayonService.Repository.AccountRepository;
using CrayonService.Repository.Repository;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//adding active functions to Mediatr from Queries
builder.Services.AddApplicationQueries();
builder.Services.AddApplicationCommands();
//adding Mediatr
builder.Services.AddScoped<IMediator, Mediator>();

builder.Services.AddDbContext<CrayonDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySQL(connectionString);
});


builder.Services.AddScoped<IValidator<GetAllAccounts.Query>, AllAccountsValidator>();
builder.Services.AddScoped<IValidator<OrderService.Command>, OrderServiceValidator>();
builder.Services.AddScoped<IValidator<GetAllServicesForAccount.Query>, AllSubscribedServicesValidator>();
builder.Services.AddScoped<IValidator<UpdateSubscriptionQuantity.Command>, SubscriptionQuantityValidator>();
builder.Services.AddScoped<IValidator<CancelSubscription.Command>, CancelSubscriptionValidator>();
builder.Services.AddScoped<IValidator<ExtendServiceValidity.Command>, ExtendValidityValidator>();

builder.Services.AddScoped<IAccountCustomerLinkRepository, AccountCustomerLinkRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();

builder.Services.AddScoped<ICCPApi, CCPApi>();
builder.Services.AddScoped<IListOfServices, ListOfServices>();
builder.Services.AddScoped<IOrderServiceCCP, OrderServiceCCP>();
builder.Services.AddScoped<ISubscriptionEditService, SubscriptionEditService>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));


var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandling>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
