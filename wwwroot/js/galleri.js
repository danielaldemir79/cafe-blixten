// "Lightbox" (image-modal) javascript

(function () {
  var dialog = document.querySelector(".lightbox");
  var links = Array.prototype.slice.call(document.querySelectorAll(".galleri__lank"));

  // "Guard clause" - Om webbläsaren är för gammal (inte stödjer <dialog></dialog> elemenetet)
  if (!dialog || !links.length || typeof dialog.showModal !== "function") {
    return;
  }

  var image = dialog.querySelector(".lightbox__bild");
  var caption = dialog.querySelector(".lightbox__text");
  var currentIndex = 0; // Vilken bild är den aktuella

  function showImage(index) {

    // Gå runt i en loop; sätt korrekt index av bild
    currentIndex = (index + links.length) % links.length;
    var link = links[currentIndex];
    image.src = link.getAttribute("href");
    image.alt = link.querySelector("img").alt;
    caption.textContent = link.dataset.bildtext || "";
  }

  // För varje länk, lägg till en event-lyssnare
  links.forEach(function (link, index) {
    link.addEventListener("click", function (event) {
      event.preventDefault();
      showImage(index);
      dialog.showModal();
    });
  });

  // Om man klickar på någon av knapparna i "lightbox:en"
  dialog.querySelector(".lightbox__knapp--forra").addEventListener("click", function () { showImage(currentIndex - 1); });
  dialog.querySelector(".lightbox__knapp--nasta").addEventListener("click", function () { showImage(currentIndex + 1); });
  dialog.querySelector(".lightbox__knapp--stang").addEventListener("click", function () { dialog.close(); });

  // Stäng om man klickar utanför bilden
  dialog.addEventListener("click", function (event) {
    if (event.target === dialog) {
      dialog.close();
    }
  });

  // Piltangenter för att bläddra (Esc stänger automatiskt)
  dialog.addEventListener("keydown", function (event) {
    if (event.key === "ArrowLeft") { showImage(currentIndex - 1); }
    if (event.key === "ArrowRight") { showImage(currentIndex + 1); }
  });

  // När dialogen stängs: ge fokus tillbaka till bilden man klickade på
  dialog.addEventListener("close", function () {
    links[currentIndex].focus();
  });
})();
