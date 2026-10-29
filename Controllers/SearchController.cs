using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using Umbraco.Cms.Infrastructure.Examine;
using Examine;

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
    private readonly IExamineManager _examineManager;

    public SearchController(
        IUmbracoContextFactory umbracoContextFactory,
        IPublishedContentQuery contentQuery,
        IExamineManager examineManager
    )
    {
        _umbracoContextFactory = umbracoContextFactory;
        _contentQuery = contentQuery;
        _examineManager = examineManager;
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

        if(!_examineManager.TryGetIndex(Umbraco.Cms.Core.Constants.UmbracoIndexes.ExternalIndexName, out var index))
        {
            return Ok(Array.Empty<SearchResultItem>());
        }

        var t = term.ToLowerInvariant();

        var queryExecutor = index.Searcher.CreateQuery(category: IndexTypes.Content)
            .GroupedOr(new[] { "nodeName" },
                t.MultipleCharacterWildcard(),
                t.Fuzzy(2f));

        var results = _contentQuery
            .Search(queryExecutor)
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
