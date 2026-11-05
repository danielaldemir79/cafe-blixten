using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace CafeBlixten.Helpers;

/// <summary>
/// Färdiga värden för ett &lt;img&gt;-element: src, srcset, width och height.
/// </summary>
public record Bild(string Src, string? SrcSet, int Bredd, int? Hojd, string? Alt);

/// <summary>
/// Gör bilder snabbare att ladda (PageSpeed/Lighthouse):
/// - webp i stället för stora png/jpg,
/// - flera storlekar via srcset så att mobilen hämtar en mindre fil,
/// - width/height så att webbläsaren kan reservera plats (inget layoutskift).
/// </summary>
public static class Bilder
{
    /// <summary>
    /// Standardbilder i wwwroot/images. Varje bild finns förminskad som
    /// "namn-600.webp" och "namn-1200.webp" (originalen ligger kvar bredvid).
    /// </summary>
    public static Bild Standard(string namn, string alt)
    {
        // Herobilden är 3:2, övriga standardbilder 16:9
        var hojd = namn == "pexels-nathanjhilton-30173526" ? 800 : 675;

        return new Bild(
            $"/images/{namn}-1200.webp",
            $"/images/{namn}-600.webp 600w, /images/{namn}-1200.webp 1200w",
            1200,
            hojd,
            alt);
    }

    /// <summary>
    /// Bild från Umbracos mediabibliotek, beskuren/förminskad av ImageSharp och levererad som webp.
    /// Anges <paramref name="hojd"/> beskärs bilden till det formatet, annars behålls proportionerna.
    /// </summary>
    public static Bild? FranMedia(MediaWithCrops? media, int bredd, int? hojd = null, string? alt = null)
    {
        if (media == null)
        {
            return null;
        }

        var originalBredd = media.Value<int?>("umbracoWidth") ?? 0;
        var originalHojd = media.Value<int?>("umbracoHeight") ?? 0;

        // SVG och liknande saknar mått – skicka filen som den är
        if (originalBredd <= 0 || originalHojd <= 0)
        {
            return new Bild(media.Url(), null, bredd, hojd, alt ?? media.Name);
        }

        // Förstora aldrig en bild som är mindre än önskad bredd
        if (originalBredd < bredd)
        {
            if (hojd.HasValue)
            {
                hojd = (int)Math.Round((double)hojd.Value * originalBredd / bredd);
            }
            bredd = originalBredd;
        }

        var slutHojd = hojd ?? (int)Math.Round((double)originalHojd * bredd / originalBredd);

        string Url(int w)
        {
            int? h = hojd.HasValue ? (int)Math.Round((double)hojd.Value * w / bredd) : null;
            return media.GetCropUrl(width: w, height: h, quality: 75, furtherOptions: "&format=webp")
                ?? media.Url();
        }

        var halv = bredd / 2;

        return new Bild(
            Url(bredd),
            $"{Url(halv)} {halv}w, {Url(bredd)} {bredd}w",
            bredd,
            slutHojd,
            alt ?? media.Name);
    }
}
