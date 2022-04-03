using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using WebApplication2.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("version")
    );

    options.Conventions.Controller<ProductController>()
        .HasDeprecatedApiVersion(1, 0)
        .HasApiVersion(2, 0)
        .Action(typeof(ProductController).GetMethod(nameof(ProductController.GetProductV2))!)
        .MapToApiVersion(2, 0);

    options.ReportApiVersions = true;
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();