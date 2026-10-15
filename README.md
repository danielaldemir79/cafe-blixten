# ☕ Café Blixten

Gruppprojekt i kursen Publiceringsverktyg och UX på TUC Yrkeshögskola.

Vi bygger en modern, responsiv och tillgänglig webbplats för ett fiktivt café. Webbplatsen utvecklas med Umbraco och ska göra det enkelt för personalen att hantera meny, nyheter, erbjudanden och annat innehåll.

**Projektperiod:** 15 oktober till 13 november 2026  
**Teknik:** Umbraco 17, .NET 10, Razor, Sass, GitHub och uSync 17  
**Kontaktperson:** Jenny Khranovska

## 1. Kom igång

Alla utvecklar lokalt och har sin egen SQLite-databas och sitt eget administratörskonto.

1. Installera .NET 10 SDK och Git.
2. Klona projektet:

   ```bash
   git clone https://github.com/danielaldemir79/cafe-blixten.git
   cd cafe-blixten
   ```

3. Kör `dotnet restore` och därefter `dotnet run`.
4. Följ Umbracos installationsguide och välj SQLite om installationen inte redan är genomförd.
5. Kontrollera att uSync finns under Settings → uSync.
6. Importera gemensam CMS-struktur via uSync → Settings efter att ha granskat importens ändringar.

**Lokala inställningar:** Databasanslutning och HMAC-nyckel ska förvaras i `appsettings.Local.json`, som inte skickas till GitHub. Filen laddas i Debug-läge genom `Program.cs`. Vid behov skapas den lokalt enligt projektets inställningsstruktur. Använd aldrig en exempelnyckel i en verklig installation.

## 2. Gemensam checklista

### Förberedelser

- [x] Caféets namn bestämt
- [x] Kontaktperson och ansvarsområden utsedda
- [x] Privat GitHub-repository skapat
- [x] Umbraco och uSync installerade på första datorn
- [x] Grundprojektet pushat till GitHub
- [x] GitHub Projects och Scrum-tavla skapade
- [x] Arbetsuppgifter skapade och fördelade
- [x] Gruppmedlemmarna inbjudna
- [ ] Alla har verifierat att de kan starta projektet lokalt

### Utveckling

- [ ] Sidträd med minst fem sidor
- [ ] Minst två egna innehållstyper
- [ ] Minst en publicerad instans per innehållstyp
- [ ] Razor-mallar och responsiv design med Sass
- [ ] Roller och behörigheter konfigurerade och testade
- [ ] SEO-fält, rubrikstruktur och interna länkar

### UX och redovisning

- [ ] Nielsens tio heuristiker granskade
- [ ] Användartester genomförda
- [ ] Tangentbord och tillgänglighet testade
- [ ] Problem och förbättringar dokumenterade
- [ ] Alla kan demonstrera och motivera sin del

## 3. Ansvarsfördelning

| Person | Ansvarsområde | Uppgifter |
|---|---|---|
| Felix Cervin | Frontend | Startsida, layout, navigation, Sass och mobilanpassning |
| Jenny Khranovska | CMS | Menysida, Document Types, produktfält och innehåll |
| Daniel Aldemir | Behörigheter och SEO | Om oss, Aktuellt, roller, SEO och rubrikstruktur |
| Aygen Ömur Mehmethan | UX och kvalitet | Kontakt, användartester, tillgänglighet och heuristisk granskning |

Alla ansvarar för att testa och dokumentera sina ändringar. Vid behov hjälper vi varandra över ansvarsområdena.

## 4. GitHub och uSync: vårt dagliga arbetssätt

**GitHub delar koden. uSync delar Umbracos CMS-struktur.**

### När du börjar arbeta

1. Kontrollera att du har sparat eller committat dina egna ändringar.
2. Hämta senaste ändringarna från `main` och uppdatera din arbetsbranch.
3. Starta projektet med `dotnet run`.
4. Öppna Settings → uSync → Settings → Report.
5. Om rapporten visar nya ändringar, granska dem och importera vid behov. Om automatisk import är aktiverad kontrollerar du i stället att den lyckats.

### När du ändrar något

1. Arbeta i en egen branch, exempelvis `feature/menysida`.
2. Gör ändringarna lokalt i Umbraco eller VS Code.
3. Har du ändrat Document Types, Data Types eller annan CMS-struktur? Kör uSync → Settings → Export.
4. Har du endast ändrat Razor, Sass, CSS eller C# behövs ingen uSync-export.
5. Kontrollera filerna med `git status`.
6. Gör commit och push till din branch.
7. Skapa en Pull Request och be en annan gruppmedlem granska ändringarna innan de förs in i `main`.

### Viktiga uSync-regler

- Importera inte ändringar utan att först kontrollera rapporten.
- Exportera ändrad CMS-struktur före commit.
- Undvik att två personer ändrar samma Document Type samtidigt.
- uSync-filerna ska versionshanteras i GitHub.
- Använd i första hand Settings, inte Everything, för vårt vanliga strukturflöde.
- Lokala databaser, användarkonton, media och vanligt sidinnehåll delas inte automatiskt genom detta arbetsflöde.

## 5. Scrum och samarbete

Vi använder **GitHub Projects** för att planera, fördela och följa upp utvecklingen av Café Blixten.

Vår projekttavla heter **Café Blixten Scrum**. Arbetsuppgifterna skapas som GitHub Issues och tilldelas ansvariga gruppmedlemmar.

### Arbetsflöde i GitHub Projects

| Kolumn | Betydelse |
|---|---|
| Backlog | Uppgifter som ska prioriteras och planeras |
| Todo | Uppgifter som är redo att påbörjas |
| In Progress | Uppgifter som någon arbetar med |
| Review | Uppgifter som väntar på granskning eller test |
| Done | Uppgifter som är färdiga och godkända |

Varje uppgift ska ha en tydlig beskrivning, en ansvarig person och ett förväntat resultat.

Gruppmedlemmarna ansvarar för att hålla sina uppgifter uppdaterade och flytta dem till rätt kolumn när arbetets status förändras.

### Daily Scrum

Vi har en kort avstämning varje planerad arbetsdag, cirka 10 till 15 minuter.

Vi går igenom vad vi har gjort, vad vi planerar att göra och om något hindrar arbetet.

### Närvaro

Vi dokumenterar närvaro vid möten i `docs/narvaro.md`. Frånvaro meddelas gruppen och berörda uppgifter uppdateras.

### Sprintar och uppföljning

Vi arbetar med en förenklad Scrum-modell med korta sprintar.

Efter varje sprint går vi igenom vilka uppgifter som blivit färdiga, vad som återstår och vilka förbättringar vi kan göra i samarbetet.

GitHub Projects används också som underlag för att visa arbetsprocessen vid den muntliga redovisningen.

## 6. Planerat sidträd

- Startsida
- Meny: Kaffe, Lunch, Kampanjer
- Evenemang
- Om oss
- Hitta oss
- Öppettider & Kontakt
- Jobba hos oss
- Aktuellt: Nyheter, Veckans fika, Erbjudanden, Evenemang

Sidträdet kan justeras efter användartester. Undersidor ska återanvända gemensamma mallar där det är lämpligt.

## 7. Kvalitet och Definition of Done

En uppgift är färdig när den:

- Fungerar och är testad lokalt.
- Följer projektets design och struktur.
- Har relevanta uSync-filer exporterade.
- Är committad och pushad till rätt branch.
- Är granskad och godkänd genom en Pull Request när uppgiften innehåller kodändringar.
- Är sammanslagen med `main` när kodändringar ingår.
- Har uppdaterad dokumentation vid behov.
- Är markerad som Done i GitHub Projects.

Vi arbetar med responsiv design, grundläggande SEO och WCAG 2.2 som riktlinje för tillgänglighet.

Vi granskar webbplatsen med Nielsens tio heuristiker och genomför användartester. Upptäckta problem och genomförda förbättringar dokumenteras inför redovisningen.

## 8. Gemensamma regler

- Vi arbetar i egna branches och undviker direkta ändringar i `main`.
- Commits ska ha tydliga svenska beskrivningar.
- Minst en annan gruppmedlem ska granska kodändringar innan en Pull Request sammanfogas.
- Vi samordnar ändringar som påverkar gemensamma Document Types.
- Vi pushar aldrig lösenord, hemliga nycklar, lokala databaser eller personliga inställningar.
- Vi meddelar gruppen tidigt om något blockerar arbetet.
- Viktiga tekniska beslut dokumenteras.

**Branchskydd:** Eftersom projektet ligger i ett privat repository där branchskydd inte verkställs med nuvarande GitHub-abonnemang bygger arbetsflödet på gruppens gemensamma överenskommelse. Alla ansvarar för att följa reglerna för branches och Pull Requests.

### NuGet och säkerhet

Vi har granskat projektets NuGet-beroenden och genomfört en säkerhetsuppdatering.

`System.Security.Cryptography.Pkcs` uppdaterades från version 10.0.0 till 10.0.13. Därmed försvann två säkerhetsvarningar. Ändringen verifierades med `dotnet restore` och `dotnet build` och sammanfogades via Pull Request #33.

Fem säkerhetsvarningar återstår för `SixLabors.ImageSharp`, som används av Umbraco 17.7.1.

Vi testade ImageSharp 3.2.0, men samma fem säkerhetsvarningar kvarstod. Teständringen återställdes därför.

Kvarvarande ImageSharp-sårbarheter följs upp separat. En större uppgradering ska inte genomföras utan att kompatibiliteten med Umbracos bildhantering har kontrollerats och testats.

## 9. Redovisning

**13 november 2026**

Vi demonstrerar webbplatsen, förklarar våra tekniska och designmässiga val samt visar hur användartester och tillgänglighetsgranskning har förbättrat resultatet.

Vi visar även hur vi har organiserat arbetet med GitHub Projects, Issues, branches, Pull Requests och uSync.

Målet är både en fungerande caféwebbplats och ett strukturerat grupparbete där varje medlem kan visa sin egen insats.