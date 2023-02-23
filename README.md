# SZTE egyetemi kurzus, 2023 tavaszi félév házi feleadat & Órai munka repó

Git gyorstalpaló: https://github.com/CsharptutorialHungary/egyetemikurzus-2023-tavasz/blob/main/GitTutorial/Readme.md

## Követelmények

Írj egy tetszőleges témájú programot, ami megfelel az alábbi technológiai követelményeknek a tanultak alapján:

**Nem kihagyható elemek:**
* Legyen benne kivételkezelés (`try-catch`)
* Legalább a képenyőre írjon ki hibaüzeneteket

**Kötelezelő elemek** - Ezek közül egy kihagyható vagy cserélhető, ha Unit és/vagy Integration tesztek tartoznak a projekthez:

* adat olvasása fájlból szerializáció segítségével (pl.: Adat betöltés és/vagy mentés JSON/XML fájlból/fájlba)
* legyen benne saját immutable type (pl.: `record class`)
* legyen benne LINQ segítségével: szűrés (`where`), csoportosítás (`group by`), rendezés (`order by`), agregáció (Pl.: `Min()`, `Max()`, `First()`, `FirstOrDefault`, `Average()`, stb...) közül legalább kettő
* legyen benne generikus kollekció (pl.: `List<T>`, `Stack<T>`, stb...)
* legyen benne aszinkron rész (`async` és `Task`)

## Technikai követelmények

* .NET 6
* Konzolos alkalmazás


## Értékelés

Az értékelés utolsó órán védéssel fog zárulni.

* **Két ember dolgozhat egy alkalmazáson, de akkor a Unit tesztek megléte kötelező és nem opcionális!**
* **Kódot fogom nézni, nem a program működését főként**, de ez nem azt jelenti, hogy a kódnak nem kell fordulnia! (Unit teszt ha van, akkor az bukhat, de indokot várok ebben az esetben, hogy miért bukik a teszt.)
* **A karakterek ingyen vannak.** Legyen normálisan elnevezve minden. Nem akarok látni `asd`, `a`, `b`, `c` meg semmit mondó metódus, tulajdonság és változó neveket.
* **Folyamatos munkát várok**, nem egy giga maratonban kommitolást => másolást feltételezek

## Konzultáció

* Óra után személyesen
* Github issue formában itt.

## Beadás menete

1. Regisztrálsz github-ra, ha még nem tetted volna meg.
2. Ezen repó fork gombjával készítesz egy fork-ot erről a repóról.
3. A forkot repót checkoutolod, csinálsz egy mappát, ami a kódod tartalmazza. A mappa neve a neptun kódod legyen. Ha ketten dolgoztok, akkor a kettőtök neve `_` karakterrel elválasztva.
4. A `hazifeladatok.sln`-be vedd fel a projektedet, így a CI-CD futni fog rá.
4. Elkészítitek a beadandót, folyamatos commitokkal.
5. A végén, amikor be akarjátok adni, akkor készítetek egy pull request-et erre a repóra.

## Ajánlott olvasmányok

* [Git tutorial](https://docs.github.com/en/get-started/quickstart)
* [C# tutorial](https://csharptutorial.hu/)
