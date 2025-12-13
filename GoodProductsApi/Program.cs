using GoodProductsApi.BusinessLogic.Extensions;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("GoodProductsApi is starting");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddGoodProductsApiBusinessLogic(builder.Configuration.GetConnectionString("ApiDb") ?? string.Empty)
        .AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console(new ExpressionTemplate(
                "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}",
                theme: TemplateTheme.Code)));

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "GoodProductsApi stopped unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
