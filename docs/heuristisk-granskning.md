# Heuristisk granskning – Café Blixten

Vi har granskat webbplatsen utifrån Nielsens tio heuristiker. Kopplat till #26.

**Datum:** 25 oktober 2026
**Uppdaterad:** 27 oktober 2026

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
- **Var:** 
- **Heuristik:** 5. Förebygga fel
- **Problem:** Tillgänglighet, Integritetspolicy och Cookies i footern leder ingenstans.
- **Allvarlighetsgrad:** 
- **Förslag:** Skapa sidorna.
- **Status:** Åtgärdat i #75 (Cookies) och #77 (Tillgänglighet och Integritetspolicy).

### Fynd 2: Ingen sida om tillgänglighet
- **Var:** 
- **Heuristik:** 10. Hjälp och dokumentation
- **Problem:** Det finns ingen sida som berättar hur tillgänglig webbplatsen är.
- **Allvarlighetsgrad:** 
- **Förslag:** Skapa sidan Tillgänglighet.
- **Status:** Åtgärdat i #77.

### Fynd 3: Flera H1 på varje sida
- **Var:** 
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Varje sida har flera huvudrubriker (H1), bland annat två i footern.
- **Allvarlighetsgrad:** 
- **Förslag:** En H1 per sida (#18).
- **Status:** Åtgärdat i #55.

### Fynd 4: Olika färg på "Stängt nu"
- **Var:** 
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Pricken är röd i footern men grå på sidan Öppettider & kontakt.
- **Allvarlighetsgrad:** 
- **Förslag:** Samma färg överallt.
- **Status:** Åtgärdat i #58.

### Fynd 5: Fel år i footern
- **Var:** 
- **Heuristik:** 2. Matcha verkligheten
- **Problem:** Det står "© Café Blixten 2022", vilket ser gammalt ut.
- **Allvarlighetsgrad:** 
- **Förslag:** Visa året automatiskt.
- **Status:** Åtgärdat i #77.

### Fynd 6: Otydlig alt-text på loggan
- **Var:** 
- **Heuristik:** 2. Matcha verkligheten
- **Problem:** En skärmläsare säger bara "Logo".
- **Allvarlighetsgrad:** 
- **Förslag:** Tom alt-text (alt=""), eftersom namnet redan står bredvid loggan.
- **Status:** Kvar.

### Fynd 7: Favicon är inte vår logga
- **Var:** 
- **Heuristik:** 6. Känna igen hellre än minnas
- **Problem:** Webbläsarfliken visar inte caféets logga.
- **Allvarlighetsgrad:** 
- **Förslag:** Gör en favicon av loggan.
- **Status:** Åtgärdat i #73.

### Fynd 8: Platshållartext på startsidan
- **Var:** 
- **Heuristik:** 8. Estetisk och minimalistisk design
- **Problem:** Under rubriken på startsidan står det bara "Test.".
- **Allvarlighetsgrad:** 
- **Förslag:** Skriv en riktig ingress om caféet.
- **Status:** Åtgärdat i #55.

### Fynd 9: Olika stavning av caféets namn
- **Var:** 
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Rubriken på startsidan säger "Kafé Blixten", men loggan och resten av webbplatsen säger "Café Blixten".
- **Allvarlighetsgrad:** 
- **Förslag:** Skriv "Café Blixten" överallt.
- **Status:** Åtgärdat i #55.

### Fynd 10: Låg kontrast på knappen i cookie-rutan
- **Var:** 
- **Heuristik:** 8. Estetisk och minimalistisk design
- **Problem:** Orange text på grön bakgrund på OK-knappen har för låg kontrast (ca 3:1, WCAG kräver 4,5:1).
- **Allvarlighetsgrad:** 
- **Förslag:** Mörk eller vit text på knappen.
- **Status:** Kvar.

### Fynd 11: Olika textfärger i sidfoten
- **Var:** Sidfoten (alla sidor)
- **Heuristik:** 4. Konsekvens och standard
- **Problem:** Texten i sidfoten hade olika färger. Adressen och öppettiderna var vita, men telefon, e-post och länkar var grönaktiga med för låg kontrast (ca 4:1, WCAG kräver 4,5:1).
- **Allvarlighetsgrad:** 
- **Förslag:** Vita rubriker och en ljusare grön färg för all övrig text och alla länkar.
- **Status:** Åtgärdat i #82. Länkarna är också understrukna. Orange text vid hover och vit text på orange knappar har fortfarande lite för låg kontrast (Kvar).

Några fynd (3, 6, 10 och 11) är främst tillgänglighetsproblem (WCAG) och passar inte helt in i Nielsens heuristiker. Vi har placerat dem under den heuristik som ligger närmast.

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
