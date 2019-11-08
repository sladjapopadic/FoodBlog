//Vorraussetzungen:
// - jQuery Touchwipe Plugin http://www.codingjack.com/playground/swipe/
// - modernizr mit Touch Events Check

//Benutzung: Damit der Slider richtig erkannt wird, muss folgendes HTML Grundgerüst verwendet werden:
//<div class="mainSlider">
//    <ul class="slider">
//      <li><div class="slItem">IHR HTML was angeziegt werden soll für Slide 1</div></li>
//      <li><div class="slItem">IHR HTML was angeziegt werden soll für Slide 2</div></li>
//      <li><div class="slItem">IHR HTML was angeziegt werden soll für Slide ...</div></li>
//     </ul>
//</div>

//Die Vor und Zurück buttons müssen nur die übergebene Klasse besitzten und am besten auf display none stehen, diese werden
//automatisch sichtbar sobald JavaScript aktiv ist und der Slider angezeigt wird.
//<a class="SlideBack" style="display: none" href="prev">Zurück</a>
//<a class="SlideNext" style="display: none" href="next">Nächstes</a>
//<a class="SlidePlay" style="display: none" href="play">Play/Stop</a>

//Aufgerufen wird das ganz dann z.B.: $('.mainSlider').swSlider();


(function ($, window, document, undefiend) {
    $.fn.swSlider = function (opt) {
        var defaultsOptions = {
            slideSpeed: 600, //SlideSpeed
            animationSpeed: 2000, //Speed für die autoamtische Animation
            useCarousel: true, //Karussell Funktion nutzen
            startSlideShow: true, //gibt an ob die Slideshow automatisch gestartet werden soll
            nextItemClass: 'SlideNext', //Die Klasse für den NextLink/Button
            backItemClass: 'SlideBack', //Die Klasse für den BackLink/Button
            playItemClass: 'SlidePlay'  //Die Klasse für den Play Button
        },
            options = $.extend(defaultsOptions, opt),
            self = $(this),
            sliderWidth = self.width(), //Die Breite des Sliders ermitteln
            slideCounter = $('.slider > li', self).length, //Die Anzahl der Slides ermitteln
            slideContainer = $('ul:first', self), //den UL Container ermitteln, da bei diesem margin-left gesetzt wird
            currentSlide = 0, //Aktuell angezeigte Slide
            intervall = undefiend,
            hoverCheck = true,
            wasClicked = false, //gibt an ob aktuell ein "event" ausgeführt wird und ob schon auf die nächste Slide gewechselt werden kann

        //Binden der Events die ausgeführt werden sollen
        bindEvents = function () {
            //die Touch Events werden nur geladen, wenn diese auch untersüzt werden.
            if (Modernizr.touch) {
                self.touchSwipeLeft(slideNextEvent);
                self.touchSwipeRight(slideBackEvent);
            }

            //Die buttons werden immer angezeigt, hier die Klasse verwenden die übergeben wurde.
            $('.' + options.backItemClass).show().on('click', slideBackEvent);
            $('.' + options.nextItemClass).show().on('click', slideNextEvent);
            $('.' + options.playItemClass).show().on('click', playSlidesEvent);

            //Hover Event festlegen, wenn der Slider noch auf Play ist, dann kurz stoppen und beim Verlassen wieder weiter machen.
            $(self).hover(hoverInEvent, hoverOutEvent);
        },

        //Intervallabspielen starten für die Slides
            playSlides = function () {
                //Nur wenn das Intervall Undefined ist, damit der Slider nicht mehrmals gestartet werden kann.
                if (intervall === undefiend) {
                    //Die Bilder automatisch durchsliden nach x Sekunden 
                    //und der intervall Variablen zuordnen, damit das Intervall auch wieder gestoppt werden kann
                    intervall = setInterval(slideNext, options.animationSpeed);
                } else {
                    //Stoppen des Sliders
                    window.clearInterval(intervall);
                    intervall = undefiend;
                }
            },
        //das erste und letzte li Element der Liste noch einmal hinzufügen, damit ein natloser Caroussel Slider gebaut werden kann
            addCaroussellItems = function () {
                //nur wenn sich mehr wie 1 Item in der Sliderliste befinden lohnt sich das Caroussell
                if (slideCounter > 1) {
                    //als erstes unsere beiden Knoten heraussuchen
                    var first = $('> li:first', slideContainer),
                        last = $('> li:last', slideContainer);

                    //dem Dom das letzte li vorn anfügen und das erste li hinten anfügen.
                    $(slideContainer).append(first.clone()).prepend(last.clone());
                }
            },
        //Eine Slide zurück "sliden"
            slideBack = function () {
                //Prüfen welche Funktion zum Sliden verwendet werden soll
                if (options.useCarousel && !wasClicked) {
                    wasClicked = true;
                    currentSlide = --currentSlide % slideCounter;

                    slideContainer.animate({
                        marginLeft: -1 * currentSlide * sliderWidth
                    }, options.slideSpeed, function () {
                        //Prüfen ob es sich bei der aktuellen Slide um die Letzte anzuzeigende handelt
                        if (currentSlide === 0) {
                            //den Margin dann direkt auf das vorletzte anzuzeigende Slide setzten um einen natlosen übergang zu gewährleisten
                            slideContainer.attr('style', 'margin-left:' + (-1 * (slideCounter - 2) * sliderWidth) + 'px;');
                            currentSlide = (slideCounter - 2);
                        }
                        wasClicked = false;
                    });
                }

                //Normale Slider Funktion ohne Karoussell
                if (!options.useCarousel) {
                    //Beim rückwärts sliden muss aufgepasst werden, das bei 0 das "Letzte" element aus der Liste genommen wird.
                    if (currentSlide === 0) {
                        currentSlide = slideCounter - 1;
                    } else {
                        currentSlide = --currentSlide % slideCounter;
                    }

                    //Die Animation durchführen und zur nächsten Slide gehen
                    //Um einen Slide auszuführen, muss man den Margin des ul Elements auf margin-left: -600px*x; setzen
                    slideContainer.animate({
                        marginLeft: -1 * currentSlide * sliderWidth
                    }, options.slideSpeed);
                }
            },
        //Eine Slide vorwärts "sliden"
            slideNext = function () {
                //Prüfen welche Funktion zum Sliden verwendet werden soll und ob bereits geklickt wurde
                //wasklicked prüft, ob das animation Event bereits beendet wurde und nur dann kann die nächste Slide angewählt werden
                if (options.useCarousel && !wasClicked) {
                    wasClicked = true;
                    currentSlide = ++currentSlide % slideCounter;

                    slideContainer.animate({
                        marginLeft: -1 * currentSlide * sliderWidth
                    }, options.slideSpeed, function () {
                        //Prüfen ob es sich bei der aktuellen Slide um die Letzte anzuzeigende handelt
                        if (currentSlide === (slideCounter - 1)) {
                            //den Margin dann direkt auf die 2te anzuzeigende Slide setzten um einen natlosen übergang zu gewährleisten
                            slideContainer.attr('style', 'margin-left:' + (-1 * sliderWidth) + 'px;');
                            currentSlide = 1;
                        }
                        //das event ist Fertig und was Clicked kann wieder "freigegeben" werden.
                        wasClicked = false;
                    });
                }

                //Normale Slider Funktion ohne Karoussell
                if (!options.useCarousel) {
                    currentSlide = ++currentSlide % slideCounter;
                    //Die Animation durchführen und zur nächsten Slide gehen
                    slideContainer.animate({
                        marginLeft: -1 * currentSlide * sliderWidth
                    }, options.slideSpeed);
                }
            },
            playSlidesEvent = function (e) {
                playSlides();
                e.preventDefault();
            },
            slideBackEvent = function (e) {
                window.clearInterval(intervall);
                intervall = undefiend;
                slideBack();
                e.preventDefault();
            },
            slideNextEvent = function (e) {
                //Die Animation stoppen sobald die Buttons zum Wechseln benutzt wurden
                window.clearInterval(intervall);
                intervall = undefiend;
                //Die nächste Slide auswählen
                slideNext();
                //Verhindern, das der Next button Link ausgeführt wird.
                e.preventDefault();
            },

        //Wenn es noch ein Intervall gibt, dann dieses stoppen
            hoverInEvent = function () {
                //Wenn es noch ein automatisches Intervall gibt, dann dieses stoppen
                if (intervall) {
                    hoverCheck = true;
                    window.clearInterval(intervall);
                    intervall = undefiend;
                } else {
                    //Es gibt kein Intervall mehr, dann darf beim Verlassen
                    //aber auch kein Intervall erstellt werden
                    hoverCheck = false;
                }
            },

        //Starten des Intervalls wenn es vorher noch gelaufen ist.
            hoverOutEvent = function () {
                //Wenn das Intervall wieder eingeschalten werden darf.
                if (hoverCheck) {
                    intervall = setInterval(slideNext, options.animationSpeed);
                }
            };

        (function init() {
            //Verhindern, das der Slider zwei mal mit dem gleichen Selector gebunden wird.
            if (self.attr('Data-Bound')) {
                console.log('Found');
                return self;
            }
            //Sobald der Slider das erste mal aufgerugen wird, für dieses Element festlegen, das der Slider bereits in Benutzung ist.
            self.attr('Data-Bound', 'true');

            //Wenn der Slider als Carroussel genutzt werden soll, die passenden Items hinzufügen
            if (options.useCarousel) {
                addCaroussellItems();
                //Die Anzahl der Slides ermitteln, die sich geändert haben
                slideCounter = $('.slider > li', self).length;
                //den UL Container ermitteln, da bei diesem margin-left gesetzt wird und sich dieser geändert hat
                slideContainer = $('ul:first', self);
                //das erste Bild "auslassen", das es sich hier um das letzte handelt.
                slideContainer.attr('style', 'margin-left:' + (-1 * sliderWidth) + 'px;');
                currentSlide = 1;
            }

            //Wenn die Slideshow direkt gestartet werden soll hier prüfen
            if (options.startSlideShow) {
                playSlides();
            }

            bindEvents();
        })();
    };
})(jQuery, window, document);