namespace Taxation.Api.Extensions;


public static class WebApplicationExtensions
{
    public static WebApplication ConfigureMiddleware(this WebApplication app, IConfiguration configuration)
    {
        app.UseExceptionHandler();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        });
        
        return app;
    }
}