# Git gyorstalpaló

![https://xkcd.com/1597/](xkcd_git.png)

Forrás: https://xkcd.com/1597/

## Ami kelleni fog

1. Git verziókezelő kliens a gépedre. Windows esetén a https://gitforwindows.org/ címről letölthető. Linux esetén disztribúció függő a telepítése, de debian alapú rendszereknél ez `apt-get install git` bonyolultságú.

2. Github regisztráció.

## Előkészületek

1. Lépés: A fork gombra kell kattintani.

    ![1. lépés](tutorial01.png)

2. Lépés: Ez felfog hozni egy új oldat, ahol a repó kódját át tudod másolni a saját profilod alá. Itt csak a `Create fork` gombra kell kattintani.

    ![2. lépés](tutorial02.png)

3. Lépés: navigálj a profilod alatt létrejött másolathoz. A nagy zöld code gombra kattintva nyílik egy lenyíló menü. Itt tudod majd leklónozni, leszedni a gépedre a kódot. A HTTPS fülön ki tudod másolni a Repó url címét.

    ![2. lépés](tutorial03.png)

4. Lépés: Nyisd meg a gépeden a GIT Bash programot.

## Házi előkészületek, klónozás

A Git bash-en belül a legtöbb Linux alatt ismert parancs működik. Windows esetén, például, ha a `d:\git` mappába szeretnél navigálni akkor az alábbi formában kell megadni az elérési útvonalat: `/d/git`.

A korábban kimásolt URL a klónozáshoz kell. A klónozás lemásolja a repository-t a gépedre. Ehhez ezt a parancsot kell kiadnod:

```bash
git clone [url]
```

Ahol az `[url]` helyére a korábban kimásolt url-t kell írnod. Authetikáció után, ha minden jól ment, akkor az aktuális mappában ahol kiadtad a parancsot létre fog jönni egy `egyetemikurzus-2023-tavasz` mappa.

Ezen a mappán belül hozz létre egy új mappát, ami a neptun kódod, majd ezen belül egy konzol alkalmazást. Ezt a `dotnet` parancs segítségével az alábbi kiadásával tudod könnyen elvégezni

```bash
dotnet new console -n [neptunkodod]
```

Ezt követően a `hazifeladatok.sln.sln` fájlhoz hozzá kell adnod a létrejött projektet. Ezt visal studio segítségével is meg tudod tenni, vagy parancssorból:

```bash
dotnet sln add [neptunkodod]
```

## Kommitolás

Ha idáig megvagy, akkor a Visual Studio-ban megnyitva az SLN fájlodat meg kellene jelennie a projektedned. De még mielőtt el kezdenél dolgozni a házi feladaton, ez a pont lenne az ahol érdemes lenne elsőként kommitolnod.

Ez Visual Studio-ból is megtehető, de parancssrorból a parancs:

```bash
git commit -m "[leírás]"
```

A `"[leírás]"` helyére a kommitod leírása írandó. Fontos, hogy idézőjelekben legyen, illetve jó lenne, ha a leírásnak valami értelmes lenne és az elvégzett munkára utalna, nem csak egy semmit mondó `added files` vagy `asd` lenne.

A kommitok lokálisan jönnek létre a GIT elosztott mivoltából. A szerverre a `git push` parancs segítségével tudod feltenni.

Érdemes sűrűn kommitolni, mert minden kommit egy snapshot, amire bármikor visszaállíthatod a kódodat.

## Beadás

Ha készen vagy a házival, akkor egy merge request-et kell nyitnod. `git push` segítségével tedd fel az összes kommitot, ha még nem történt volna meg, majd navigálj el a githubra.

Itt a `Pull requests` tabon a `New pull request` gombra kel kattintani.

![Pull request gomb](tutorial04.png)

Itt feljön egy ablak, ami összehasonítja a saját forkod az eredeti repóval. Itt a lapon valahol lesz egy gomb, amivel ténylegesen létrehozod a Pull requestet.

![Pull request létrehozás](tutorial05.png)

Ha ideáig eljutottál, akkor gratulálunk, sikeresen beadtad a házi feladatod.

## További hasznos olvasnivalók

* https://git-scm.com/docs/gittutorial
* https://www.udemy.com/course/git-complete/
