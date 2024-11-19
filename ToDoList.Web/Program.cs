using Microsoft.AspNetCore.Diagnostics;
using ToDoList.DAL.Base;
using ToDoList.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

ServiceRegistrator.RegisterRequiredServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(e =>
    {
        e.Run(async context =>
        {
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

            context.Response.Redirect($"/Error?errorMessage={exceptionHandlerFeature?.Error.Message ?? string.Empty}");
        });
    });

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
