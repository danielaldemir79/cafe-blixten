using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace CafeBlixten.Controllers;

/// <summary>
/// Live-sök för sök-overlayen. Anropas från wwwroot/js/main.js:
/// GET /api/search?q=kaffe
/// </summary>
[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private const int MaxResults = 8;

    private readonly IUmbracoContextFactory _umbracoContextFactory;
    private readonly IPublishedContentQuery _contentQuery;

    public SearchController(
        IUmbracoContextFactory umbracoContextFactory,
        IPublishedContentQuery contentQuery)
    {
        _umbracoContextFactory = umbracoContextFactory;
        _contentQuery = contentQuery;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? q)
    {
        var term = q?.Trim();

        if (string.IsNullOrEmpty(term) || term.Length < 2)
        {
            return Ok(Array.Empty<SearchResultItem>());
        }

        // Säkerställer att Umbraco-innehållet går att läsa utanför vanlig sidrouting
        using var context = _umbracoContextFactory.EnsureUmbracoContext();

        var results = _contentQuery
            .Search(term)
            .Select(result => result.Content)
            .Where(content => content.ContentType.Alias != "sokresultat")
            .Take(MaxResults)
            .Select(content => new SearchResultItem(
                content.Name ?? string.Empty,
                content.Url(),
                content.ContentType.Alias))
            .ToList();

        return Ok(results);
    }

    public record SearchResultItem(string Name, string Url, string Type);
}
