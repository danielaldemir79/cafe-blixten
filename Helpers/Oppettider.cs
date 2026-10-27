using System.Globalization;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace CafeBlixten.Helpers;

/// <summary>
/// Gemensam logik för öppettider från sidan "Öppettider &amp; kontakt".
/// Används av Öppettider &amp; kontakt, startsidan och footern så att alla tre
/// alltid visar samma tider och samma "Öppet nu / Stängt nu".
/// </summary>
public static class Oppettider
{
    // Veckodagar i samma ordning som DayOfWeek (söndag = 0)
    public static readonly string[] Veckodagar = { "Söndag", "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag", "Lördag" };

    public record Rad(string Dag, string? Oppnar, string? Stanger, bool Stangt);

    // Namn bara vid avvikelse
    public record Tider(bool Stangt, string? Oppnar, string? Stanger, string? Namn = null)
    {
        public string Text => Stangt ? "Stängt" : $"{Oppnar}–{Stanger}";
    }

    public record Avvikelse(string Namn, DateTime Fran, DateTime Till, bool Stangt, string? Oppnar, string? Stanger, string? Kommentar)
    {
        public bool GallerDatum(DateTime datum) => datum.Date >= Fran && datum.Date <= Till;
    }

    /// <summary>Tiden i Sverige, oavsett var servern står.</summary>
    public static DateTime NuISverige()
    {
        try
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Europe/Stockholm"));
        }
        catch (TimeZoneNotFoundException)
        {
            return DateTime.Now;
        }
    }

    public static TimeOnly? Tid(string? text) =>
        TimeOnly.TryParseExact(text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tid) ? tid : null;

    /// <summary>Veckoschemat, en rad per dag.</summary>
    public static List<Rad> Veckoschema(IPublishedContent? kontaktsida) =>
        Block(kontaktsida, "oppettider")
            .Select(e => new Rad(
                e.Value<string>("dag") ?? "",
                e.Value<string>("oppnar"),
                e.Value<string>("stanger"),
                e.Value<bool>("stangt")))
            .Where(rad => rad.Dag != "")
            .ToList();

    /// <summary>Avvikande öppettider som inte har passerat, sorterade efter datum.</summary>
    public static List<Avvikelse> KommandeAvvikelser(IPublishedContent? kontaktsida, DateTime nu) =>
        Block(kontaktsida, "avvikandeOppettider")
            .Select(e => (Element: e, Fran: LasDatum(e, "datum"), Till: LasDatum(e, "datumTill")))
            .Where(x => x.Fran.HasValue)
            .Select(x => new Avvikelse(
                x.Element.Value<string>("namn") ?? "",
                x.Fran!.Value,
                x.Till ?? x.Fran!.Value,
                x.Element.Value<bool>("stangt"),
                x.Element.Value<string>("oppnar"),
                x.Element.Value<string>("stanger"),
                x.Element.Value<string>("kommentar")))
            .Where(a => a.Till >= nu.Date)
            .OrderBy(a => a.Fran)
            .ToList();

    /// <summary>
    /// Tider för ett visst datum: avvikande tider går före veckoschemat.
    /// Om flera passar (t.ex. en röd dag mitt i ett lov) vinner den som börjar senast.
    /// </summary>
    public static Tider? TiderFor(DateTime datum, List<Rad> veckoschema, List<Avvikelse> avvikelser)
    {
        var avvikelse = avvikelser
            .Where(a => a.GallerDatum(datum))
            .OrderByDescending(a => a.Fran)
            .FirstOrDefault();

        if (avvikelse != null)
        {
            return new Tider(avvikelse.Stangt, avvikelse.Oppnar, avvikelse.Stanger, avvikelse.Namn);
        }

        var rad = veckoschema.FirstOrDefault(r => r.Dag == Veckodagar[(int)datum.DayOfWeek]);
        return rad == null ? null : new Tider(rad.Stangt, rad.Oppnar, rad.Stanger);
    }

    /// <summary>Dagens tider direkt från sidan "Öppettider &amp; kontakt".</summary>
    public static Tider? TiderIdag(IPublishedContent? kontaktsida, DateTime nu) =>
        TiderFor(nu, Veckoschema(kontaktsida), KommandeAvvikelser(kontaktsida, nu));

    /// <summary>
    /// "Öppet nu" eller "Stängt nu", och en kort text som "stänger 17:00" eller "öppnar i morgon 07:00".
    /// </summary>
    public static (bool ArOppet, string Text) Status(DateTime nu, List<Rad> veckoschema, List<Avvikelse> avvikelser)
    {
        var klockan = TimeOnly.FromDateTime(nu);

        if (TiderFor(nu, veckoschema, avvikelser) is { Stangt: false } t
            && Tid(t.Oppnar) is TimeOnly oppnar
            && Tid(t.Stanger) is TimeOnly stanger)
        {
            if (klockan >= oppnar && klockan < stanger)
            {
                return (true, $"stänger {t.Stanger}");
            }

            if (klockan < oppnar)
            {
                return (false, $"öppnar {t.Oppnar}");
            }
        }

        for (var steg = 1; steg <= 7; steg++)
        {
            var datum = nu.AddDays(steg);

            if (TiderFor(datum, veckoschema, avvikelser) is { Stangt: false } d && Tid(d.Oppnar) != null)
            {
                var dag = Veckodagar[(int)datum.DayOfWeek];
                return (false, steg == 1 ? $"öppnar i morgon {d.Oppnar}" : $"öppnar {dag.ToLower()} {d.Oppnar}");
            }
        }

        return (false, "");
    }

    /// <summary>Om caféet är öppet just nu, direkt från sidan "Öppettider &amp; kontakt".</summary>
    public static bool ArOppetNu(IPublishedContent? kontaktsida, DateTime nu) =>
        Status(nu, Veckoschema(kontaktsida), KommandeAvvikelser(kontaktsida, nu)).ArOppet;

    private static IEnumerable<IPublishedElement> Block(IPublishedContent? sida, string alias) =>
        sida?.Value<BlockListModel>(alias)?.Select(block => block.Content) ?? Enumerable.Empty<IPublishedElement>();

    // Datumfältet (Date Only) kan ge DateOnly eller DateTime
    private static DateTime? LasDatum(IPublishedElement element, string alias) =>
        element.GetProperty(alias)?.GetValue() switch
        {
            DateOnly d => d.ToDateTime(TimeOnly.MinValue),
            DateTime d when d > DateTime.MinValue => d.Date,
            _ => null
        };
}
