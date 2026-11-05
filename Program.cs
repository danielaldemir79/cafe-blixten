
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Net.Http.Headers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
#endif

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

// ---- Prestanda (PageSpeed/Lighthouse) ----

// Komprimera HTML, CSS, JS, JSON och SVG med Brotli/Gzip
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
});

// Lång cache-tid för statiska filer på webbplatsen. CSS/JS har ?v=<hash> (asp-append-version)
// och mediafiler får en ny mapp vid varje uppladdning, så en ny version får alltid en ny adress.
// Byter man en bild i wwwroot/images bör den därför få ett nytt filnamn.
// Backofficen (/umbraco) lämnas orörd.
string[] cachadeMappar = ["/css", "/js", "/fonts", "/images", "/logo", "/media"];
builder.Services.Configure<StaticFileOptions>(options =>
{
    options.OnPrepareResponse = context =>
    {
        var path = context.Context.Request.Path;
        var cachas = cachadeMappar.Any(mapp => path.StartsWithSegments(mapp, StringComparison.OrdinalIgnoreCase))
            || path.Value?.StartsWith("/favicon", StringComparison.OrdinalIgnoreCase) == true
            || path.Value?.StartsWith("/apple-touch-icon", StringComparison.OrdinalIgnoreCase) == true;

        if (cachas)
        {
            context.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=31536000, immutable";
        }
    };
});

WebApplication app = builder.Build();


await app.BootUmbracoAsync();


app.UseResponseCompression();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
