# Heuristisk granskning – Café Blixten

Vi har granskat webbplatsen utifrån Nielsens tio heuristiker. Kopplat till #26.

**Datum:** 25 oktober 2026
**Uppdaterad:** 8 november 2026

Nya sidor som läggs till efter detta datum kontrolleras i vår egen kontroll före presentationen.

## Allvarlighetsgrad

Vi har bedömt varje fynd enligt Nielsens skala:

- **0:** Inget användbarhetsproblem
- **1:** Kosmetiskt problem, åtgärdas om tid finns
- **2:** Mindre problem, låg prioritet
- **3:** Stort problem, hög prioritet
- **4:** Katastrof, måste åtgärdas

## Problem vi hittade

### Fynd 1: Länkar som inte fungerar
- **Var:** Sidfoten (alla sidor)
- **Heuristik:** 5. Förebygga fel
- **Problem:** Tillgänglighet, Integritetspolicy och Cookies i footern leder ingenstans.
- **Allvarlighetsgrad:** 2
- **Förslag:** Skapa sidorna.
- **Status:** Åtgärdat i #75 (Cookies) och #77 (Tillgänglighet och Integritetspolicy).

### Fynd 2: Ingen sida om tillgänglighet
- **Var:** Sidfoten (alla sidor)
- **Heuristik:** 10. Hjälp och dokumentation
- **Problem:** Det finns ingen sida som berättar hur tillgänglig webbplatsen är.
- **Allvarlighetsgrad:** 2
- **Förslag:** Skapa sidan Tillgänglighet.
- **Status:** Åtgärdat i #77.

### Fynd 3: Flera H1 på varje sida
- **Var:** Alla sidor
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Varje sida har flera huvudrubriker (H1), bland annat två i footern.
- **Allvarlighetsgrad:** 2
- **Förslag:** En H1 per sida (#18).
- **Status:** Åtgärdat i #55.

### Fynd 4: Olika färg på "Stängt nu"
- **Var:** Sidfoten och sidan Öppettider & kontakt
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Pricken är röd i footern men grå på sidan Öppettider & kontakt.
- **Allvarlighetsgrad:** 1
- **Förslag:** Samma färg överallt.
- **Status:** Åtgärdat i #58.

### Fynd 5: Fel år i footern
- **Var:** Sidfoten (alla sidor)
- **Heuristik:** 2. Matcha verkligheten
- **Problem:** Det står "© Café Blixten 2022", vilket ser gammalt ut.
- **Allvarlighetsgrad:** 1
- **Förslag:** Visa året automatiskt.
- **Status:** Åtgärdat i #77.

### Fynd 6: Otydlig alt-text på loggan
- **Var:** Sidhuvudet och sidfoten (alla sidor)
- **Heuristik:** 2. Matcha verkligheten
- **Problem:** En skärmläsare säger bara "Logo".
- **Allvarlighetsgrad:** 1
- **Förslag:** Tom alt-text (alt=""), eftersom namnet redan står bredvid loggan.
- **Status:** Åtgärdat i #98.

### Fynd 7: Favicon är inte vår logga
- **Var:** Webbläsarfliken (alla sidor)
- **Heuristik:** 6. Känna igen hellre än minnas
- **Problem:** Webbläsarfliken visar inte caféets logga.
- **Allvarlighetsgrad:** 1
- **Förslag:** Gör en favicon av loggan.
- **Status:** Åtgärdat i #73.

### Fynd 8: Platshållartext på startsidan
- **Var:** Startsidan
- **Heuristik:** 8. Estetisk och minimalistisk design
- **Problem:** Under rubriken på startsidan står det bara "Test.".
- **Allvarlighetsgrad:** 3
- **Förslag:** Skriv en riktig ingress om caféet.
- **Status:** Åtgärdat i #55.

### Fynd 9: Olika stavning av caféets namn
- **Var:** Startsidan
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Rubriken på startsidan säger "Kafé Blixten", men loggan och resten av webbplatsen säger "Café Blixten".
- **Allvarlighetsgrad:** 2
- **Förslag:** Skriv "Café Blixten" överallt.
- **Status:** Åtgärdat i #55.

### Fynd 10: Låg kontrast på knappen i cookie-rutan
- **Var:** Cookie-rutan (första besöket, alla sidor)
- **Heuristik:** 8. Estetisk och minimalistisk design
- **Problem:** Orange text på grön bakgrund på OK-knappen har för låg kontrast (ca 3:1, WCAG kräver 4,5:1).
- **Allvarlighetsgrad:** 3
- **Förslag:** Mörk eller vit text på knappen.
- **Status:** Åtgärdat i #99. Knappen har nu vit text på mörkare orange.

### Fynd 11: Olika textfärger i sidfoten
- **Var:** Sidfoten (alla sidor)
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Texten i sidfoten hade olika färger. Adressen och öppettiderna var vita, men telefon, e-post och länkar var grönaktiga med för låg kontrast (ca 4:1, WCAG kräver 4,5:1).
- **Allvarlighetsgrad:** 2
- **Förslag:** Vita rubriker och en ljusare grön färg för all övrig text och alla länkar.
- **Status:** Åtgärdat i #82. Länkarna är också understrukna. Kontrasten på de orange knapparna förbättrades i #99.

### Fynd 12: Bilder i galleriet utan beskrivning
- **Var:** Galleri
- **Heuristik:** 2. Matcha verkligheten
- **Problem:** En skärmläsare läste upp bildens filnamn, till exempel "pexels-igor-starkov-233202-1055054".
- **Allvarlighetsgrad:** 2
- **Förslag:** Skriv en bildtext i Umbraco. Saknas den, läs "bild 1 av 3" i stället för filnamnet.
- **Status:** Åtgärdat i #102.

### Fynd 13: Menyknappen på mobil säger inte om menyn är öppen
- **Var:** Menyknappen på mobil (alla sidor)
- **Heuristik:** 1. Synlig systemstatus
- **Problem:** En skärmläsare säger inte om menyn är öppen eller stängd.
- **Allvarlighetsgrad:** 2
- **Förslag:** Lägg till aria-expanded på knappen.
- **Status:** Åtgärdat i #102.

### Fynd 14: Många länkar till samma sida, med olika namn
- **Var:** Startsidan och sidfoten
- **Heuristik:** 8. Estetisk och minimalistisk design, 4. Konsekvens och standard
- **Problem:** På startsidan går fem länkar till sidan Hitta oss: i hero, i rutan "Välkommen in", överst i sidfoten, i menyn och under Utforska. Rutan "Välkommen in" upprepar dessutom sidfoten (samma text, adress och länkar). Samma sida heter ibland "Hitta hit" och ibland "Hitta oss".
- **Allvarlighetsgrad:** 2
- **Förslag:** Ta bort rutan "Välkommen in" och använd namnet "Hitta oss" överallt.
- **Status:** Kvar.

Några fynd (3, 6, 10, 11, 12 och 13) är främst tillgänglighetsproblem (WCAG) och passar inte helt in i Nielsens heuristiker. Vi har placerat dem under den heuristik som ligger närmast.

## Det som fungerar bra

- **1. Synlig systemstatus:** "Öppet nu / Stängt nu" visas, och aktiv sida är markerad i menyn.
- **2. Matcha verkligheten:** Enkla ord som "Hitta hit" och "Öppettider".
- **3. Användarkontroll:** Loggan leder till startsidan, och det finns en länk "Till toppen".
- **3. Användarkontroll:** "Hoppa till innehållet" låter tangentbordsanvändare hoppa förbi menyn.
- **7. Effektivitet:** Telefonnummer och e-post går att klicka på.
- **9. Hjälp vid fel:** Om öppettider saknas står det "Öppettiderna uppdateras snart".

Heuristiken visar vilken typ av problem det är. Allvarlighetsgraden visar hur allvarligt problemet är. Två problem under samma heuristik kan ha olika grad.

Vi bedömer graden utifrån tre frågor:

1. **Frekvens:** Hur ofta uppstår problemet? På varje sida eller på ett ställe?
2. **Påverkan:** Kan användaren ta sig förbi problemet, eller fastnar hen?
3. **Beständighet:** Försvinner problemet när användaren har lärt sig, eller stör det varje gång?

Varje gruppmedlem sätter först en egen grad, sedan bestämmer vi en gemensam.
