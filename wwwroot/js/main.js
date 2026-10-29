

(function () {

  // För snabbare användning
  var ToggleSidebarButton = document.getElementById("ToggleSidebar");
  var SidebarCloseButton = document.getElementById("SidebarClose");
  var SearchClickArea = document.getElementById("SearchClickArea");
  var searchOverlay = document.getElementsByClassName("search-overlay")[0];
  var Sidebar = document.getElementById("Sidebar");

  // För att kunna scrolla till toppen
  var mainContainer = document.querySelector("#Main > .main-container");
  var backToTopLink = document.getElementsByClassName("back-to-top-link")[0];
  var prefersReducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

  // Stäng sidebaren på mobila enheter
  function closeSidebar() {
    Sidebar.classList.remove("open");
  }

  // Ifall man klickar på en navigeringslänk i sidebaren på mobila enheter, så stängs navigationen
  Sidebar.addEventListener("click", function (event) {
    if (event.target.closest("a")) {
      closeSidebar();
    }
  });

  // Om sida ändras, stäng sidebaren/navigationen
  window.addEventListener("pageshow", function (event) {
    if (event.persisted) {
      closeSidebar();
    }
  });

  // Toggla sidomenyn på mobila enheter
  ToggleSidebarButton.addEventListener("click", function (event) {
    event.preventDefault();
    document.getElementById("Sidebar").classList.toggle("open");
  });

  // Stäng sidomenyn på mobila enheter, om man klickar på X:et
  SidebarCloseButton.addEventListener("click", function (event) {
    event.preventDefault();
    document.getElementById("Sidebar").classList.remove("open");
  });

  // Stäng sidomenyn på mobila enheter om sidomenyn är öppen, och om skärmen är mindre än 900px
  window.addEventListener("resize", function (event) {
    if (window.matchMedia("(max-width: 900px)").matches
      && document.getElementById("Sidebar").classList.contains("open")) {
      document.getElementById("Sidebar").classList.remove("open");
    }
  });

  // Sökelement
  var Search = document.getElementById("Search");
  var SearchInput = document.getElementById("SearchInput");

  // Öppna sökfältet
  function openSearch() {
    Search.classList.add("open");
    SearchClickArea.setAttribute("aria-expanded", "true");
    document.body.style.overflow = "hidden"; // lås scroll bakom overlayen

    // Vänta tills overlayen är synlig innan fokus flyttas
    requestAnimationFrame(function () {
      SearchInput.focus();
    });
  }

  // Stäng sökfältet
  function closeSearch() {
    if (!Search.classList.contains("open")) return;
    Search.classList.remove("open");
    SearchClickArea.setAttribute("aria-expanded", "false");
    document.body.style.overflow = "";
    SearchClickArea.focus(); // tillbaka till knappen som öppnade sökningen
  }

  // Öppna sökfältet, om knappen är tryckt
  SearchClickArea.addEventListener("click", function (event) {
    event.preventDefault();
    openSearch();
  });

  // Stäng sökfältet, om knappen är tryckt
  document.getElementById("CloseSearch").addEventListener("click", function (event) {
    event.preventDefault();
    closeSearch();
  });

  // Stäng sökfältet, om man trycker på overlayen
  searchOverlay.addEventListener("click", function (event) {
    event.preventDefault();
    closeSearch();
  });

  // Scrolla till toppen
  backToTopLink.addEventListener("click", function (event) {
    event.preventDefault();

    mainContainer.scrollTo({
      top: 0,
      behavior: prefersReducedMotion ? "auto" : "smooth",
    });

    var main = document.getElementById("Main");
    main.setAttribute("tabindex", "-1");
    main.focus({ preventScroll: true });
  });

  // ---------- Live-sök ----------
  var SearchForm = Search.querySelector(".search-form");
  var SearchResults = document.getElementById("SearchResults");
  var SearchStatus = document.getElementById("SearchStatus");
  var searchTimer = null; // Söktimer
  var searchController = null;

  // Rensa sök
  function clearResults(message) {
    SearchResults.innerHTML = "";
    SearchStatus.textContent = message || "";
    Search.classList.remove("has-results");
  }

  function renderResults(items, term) {
    SearchResults.innerHTML = "";

    // Ifall inget hittas
    if (items.length === 0) {
      SearchStatus.textContent = "Inga träffar för \u201d" + term + "\u201d";
      Search.classList.add("has-results");
      return;
    }

    // Visa hur många träffar som har hittats
    SearchStatus.textContent = items.length === 1 ? "1 träff" : items.length + " träffar";

    // För varje träff, visa en länk
    items.forEach(function (item) {
      var li = document.createElement("li");
      li.className = "search-result";

      var link = document.createElement("a");
      link.className = "search-result-link";
      link.href = item.url;
      link.textContent = item.name; // textContent = ingen risk för XSS

      li.appendChild(link);
      SearchResults.appendChild(li);
    });

    Search.classList.add("has-results");
  }

  // Kör och sök baserat på sökterm
  function runSearch(term) {
    if (searchController) searchController.abort(); // avbryt äldre sökning
    searchController = new AbortController();

    SearchStatus.textContent = "Söker…";

    // Hämta från API:n ("Controllers/SearchController.cs")
    fetch("/api/search?q=" + encodeURIComponent(term), { signal: searchController.signal })
      .then(function (response) {
        if (!response.ok) throw new Error("HTTP " + response.status);
        return response.json();
      })
      .then(function (items) {
        renderResults(items, term);
      })
      .catch(function (error) {
        if (error.name === "AbortError") return;
        clearResults("Sökningen misslyckades. Försök igen.");
      });
  }

  // NÄR man söker/skriver i input-fältet
  SearchInput.addEventListener("input", function () {
    var term = SearchInput.value.trim();
    clearTimeout(searchTimer);

    // Om man bara angett 1 karaktär i sök-fältet, sök inte
    if (term.length < 2) {
      if (searchController) searchController.abort();
      clearResults();
      return;
    }

    // Vänta 250 ms efter senaste tangenttryckning innan vi söker
    searchTimer = setTimeout(function () {
      runSearch(term);
    }, 250);
  });

  // Enter-knappen: gå till första träffen i stället för en söksida
  SearchForm.addEventListener("submit", function (event) {
    var first = SearchResults.querySelector("a");
    event.preventDefault();
    if (first) window.location.href = first.href;
  });

  // Piltangenter: flytta mellan fältet och träffarna
  Search.addEventListener("keydown", function (event) {
    if (event.key !== "ArrowDown" && event.key !== "ArrowUp") return;

    var links = Array.prototype.slice.call(SearchResults.querySelectorAll("a"));
    if (links.length === 0) return;

    var index = links.indexOf(document.activeElement);
    event.preventDefault();

    if (event.key === "ArrowDown") {
      links[index < links.length - 1 ? index + 1 : 0].focus();
    } else if (index <= 0) {
      SearchInput.focus();
    } else {
      links[index - 1].focus();
    }
  });

  document.addEventListener("keydown", function (event) {
    // Esc stänger sökningen
    if (event.key === "Escape") {
      closeSearch();
    }

    // Ctrl+K / Cmd+K öppnar sökningen
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "k") {
      event.preventDefault();
      openSearch();
    }

    // Håll Tab-fokus inne i sökdialogen medan den är öppen
    if (event.key === "Tab" && Search.classList.contains("open")) {
      var focusable = Search.querySelectorAll("button, input, a[href]");
      var first = focusable[0];
      var last = focusable[focusable.length - 1];

      if (event.shiftKey && document.activeElement === first) {
        event.preventDefault();
        last.focus();
      } else if (!event.shiftKey && document.activeElement === last) {
        event.preventDefault();
        first.focus();
      }
    }
  });

  // ---------- Cookie-information ----------
  var CookieBanner = document.getElementById("CookieBanner");
  var CookieBannerAccept = document.getElementById("CookieBannerAccept");
  var cookieKey = "cookieInfoAccepted";

  function hasAcceptedCookies() {
    try {
      return localStorage.getItem(cookieKey) === "true";
    } catch (e) {
      return false; // t.ex. privat läge där lagring är blockerad
    }
  }

  if (CookieBanner && CookieBannerAccept && !hasAcceptedCookies()) {
    CookieBanner.hidden = false;

    CookieBannerAccept.addEventListener("click", function () {
      try {
        localStorage.setItem(cookieKey, "true");
      } catch (e) {
        // går inte att spara – rutan döljs ändå för den här sidvisningen
      }
      CookieBanner.hidden = true;
    });
  }

})();