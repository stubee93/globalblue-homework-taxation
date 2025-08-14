using Taxation.Api.Extensions;
using Taxation.Application.Extensions;
using Taxation.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app
    .ConfigureMiddleware(builder.Configuration)
    .Run();