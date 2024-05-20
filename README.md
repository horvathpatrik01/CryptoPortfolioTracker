# Alkalmazásfejlesztés (VIAUMA09) házi feladat

Alapadatok:
- A csapat neve: CryptoBros
- Csapattagok (név, neptun kód):
  - Berky Bence EHLXA6
  - Horváth Patrik JN50TD
- Leadáshoz videó URL: TBA

A videó későbbi evfolyamok számára bemutatható/felhasználható videó vágásra?
- [ ] Igen, névtelenül
- [x] Igen, szerzőket megnevezve
- [ ] Nem

## Pontot érő dolgok

Architektúra, magas szintű koncepciók
- [x] 10p: MVVM architektúra (legalább 3 modell és 3 view model osztállyal) (Videóban: solution explorerben megmutatva a modell és view model osztályokat)
- Többszálúság
  - [x] 8p: Task és async-await használatával. (Videóban: forráskódban kiemelve) A maximális pont akkor jár, ha egy Task példány segítségével, ténylegesen külön szálon elindítotok egy async metódust.
  - [ ] 3p: BackgroundWorker használatával progress reporttal.
- [x] 10p: Entity Framework használata
- [x] 5p: Hálózati kommunikáció HTTP felett
  - [x] +5p: HTTP feletti kommunikációban legalább 3 HTTP ige (get, put, delete, post stb.) használata, REST API kialakítása

Technológiák
- [ ] 5p: Canvas és Shape használata (Videóban: UI-on megmutatva)
- [x] 5p: Adatkötés használata (Videóban: xaml kód)
- [x] 10p: Heterogén listához adatkötés (DataTemplateSelector) (Videóban: xaml kód)
- [x] 5p: Regex használat nem triviális feladatra (pl. nem Substring helyett, hanem például URL vagy IP cím ellenérzésére) (Videóban: forráskód részlet)
- [x] 5p: IValueConverter használata (Videóban: xaml kód)
- [x] 5p: ICommand (Videóban: forráskódban az ICommandot implementáló osztály)
- [x] 5p: StaticResource használata (Videóban: xaml kód)
- [ ] 5p: Fájlba mentés és onnan betöltés (Videóban: használat közben a UI vagy forráskód részlet)
- [x] 5p: Linq használata nem triviális feladatra (query vagy method syntax is lehet) (Videóban: forráskódban kiemelve)
- [ ] 5p: Sorosítás JSON vagy XML formátumba (Videóban: generált XML/JSON felvillantása). A sorosításnál saját típust olvassatok be vagy írjatok ki, vagyis ne egy JsonObject-et töltsetek be, amiből aztán kézzel kiszeditek az adatokat. Azzal nem használjátok ki a sorosítás igazi erejét!
- [ ] 5p: Alapos öntesztelő funkció a robot számára. A tesztet futtathatja a kliens program is, de a robot firmwareje is. A lényeg, hogy van öntesztelési funkció. (Videóban: futás közben bemutatva)
- [ ] 10p: grafikon megjelenítő package (pl. oxyplot) használata nem csak alapbeállításokkal (Videóban: UI-on megmutatva)

Módszertani szempontok
- [ ] mintánként 5p: A tárgy keretében szereplő tervezési minta használata saját megvalósításban (videóban: forráskódban megmutatva). (Observer csak akkor, ha az esemény kiváltása is saját kód, pl. egy nyomógomb Click eseménykezelőjének megírása még nem elég ehhez.) A videóban minden tervezési mintára térjetek ki, hogy hol van, vagy írjátok le ide, hogy melyik és melyik fájlban található:
  - példa minta - pelda.cs
- [ ] 10p: Legalább 20% unit teszt lefedettség (Videóban: unit tesztek lefutnak és zöldek, coverage report 20% feletti számot mutat). Ha kisebb a lefedettség, arányosan kevesebb pontot ér. (UWP alkalmazásra macerás tesztet írni, a tesztelendő osztályokat egy .NET Standard 2.0 projektbe hozzátok létre és azt tudjátok hivatkozni xUnit Test projektből, ha a teszt projekt .NET Core 2.0-át céloz meg.)
- [ ] 10p: DocFX segítségével, XML kommentárokkal generált dokumentáció legalább 3 áttekintő UML diagrammal. A dokumentáció fejlesztői dokumentáció. Olyan mértékben kell, hogy tartalmazza a rendszer működését, hogy abból kiderüljön, hogy egy adott funkció hogy működik és hol található a forráskódban. A repository értelemszerűen tartalmazza a dokumentáció minden forrását is. A DocFX által generált HTML dokumentáció ZIP-elve a github.com release funkciójával letölthető formában kell, hogy elérhető legyen a leadáskor. A generált dokumentációt semmiképp ne commitoljátok be a repositoryba! https://github.com/blog/1547-release-your-software 
- [x] 3p: Határidőre leadott pull request az 1. code reviewra, szignifikáns mennyiségű fejlesztéssel.
- [x] 2p: Határidőre leadott pull request a 2. code reviewra, szignifikáns mennyiségű fejlesztéssel.

További lehetőségek, amik nem részei a tananyagnak, de pontot érnek:
- [x] 8p: Behaviour használata (nem része a tananyagnak) (Videóban: xaml kód)
- [x] 8p: Animációk használata (nem része a tananyagnak) (Videóban: UI használat közben vagy xaml kód)
- [x] 5p: Style használata (nem része a tananyagnak) (Videóban: xaml kód) Az 5 pont saját definiált stílusra vonatkozik, ami legalább 2 propertyt beállít. Előre gyártott stílus használata 1p.
- [ ] 5p: OpenCvSharp használata (Videóban: UI használat közben vagy forráskód részlet)
- [ ] 3p: Statikus kódelemző használata a fejlesztés során (Videóban: az elemző visszajelzéseinek felvillantása)
- [x] 8p: Dependency Injection keretrendszer használata

