# ROVData - Vizualizare Date pentru România

>**ROVData** este o aplicație prin care oricine poate introduce un set de date pentru un anumit județ, și poate vizualiza date și statistici la o apăsare de buton. Interfața este facută într-un mod simplu și ușor de ințeles, fiind în același timp și eficientă.

## Cuprins

>1. [Despre mine](#despremine)
>2. [Scop](#scop)
>3. [Instalare](#instalare)
>4. [Folosire](#folosire)
>5. [Obiective](#obiective)
>6. [Credite](#credite)
## Despre mine <a name = "despremine"></a>

Berechet Tudor:
>- clasa a 11-a Mate-Info Intensiv, Colegiul Național "Vasile Alecsandri"
>- Olimpic național la matematicp și informatică
>- Limbaje: C++, C#, Python, Java, Arduino
>- Alte proiecte sunt pe [profilul meu de Github](https://github.com/dulap16)

## Scop <a name = "scop"></a>

>Acest proiect este dedicat ocaziilor la care trebuie afișate date, statistici sau procente pe județe ale Romaniei.
>Această afișare a datelor este facută foarte simplu prin această aplicație, folosindu-se de o **interfața ajutătoare**, **un mod de a insera date rapid**, și diferite **moduri de prezentare** a datelor.

## Instalare <a name = "instalare"></a>

>Pentru a accesa ultima versiune, descărcați ZIP-ul cu ultimul [Release](https://github.com/dulap16/Proiect-Galati/releases) pentru sistemul vostru de operare(Windows, Max, Linux).
>Apoi, deschideți folderul *Executables*, și apăsați pe fișierul executabil.
## Folosire <a name = "folosire"></a>

**ROVData** are multiple funcționalități: 
- *inserarea* unui set de date propriu, cât timp este în formatul potrivit;
- *accesarea* seturilor de date inserate în alte sesiuni;
- *prezentarea* datelor in 3 moduri diferite și reprezentative;
- *selectarea* a două valori "limite" care acționează ca două margini pentru valorile afișate, astfel încât doar regiunile cu valoarea între cele două limite vor fi vizibile.

Aici este un ghid pentru folosirea tuturor acestor funcționalități:

### Inserarea datelor
> Se face prin copierea și alipirea unui set de date în secțiunea *Inserarea valorilor în masă*.
**SETUL DE DATE TREBUIE SĂ FIE ÎN FORMATUL URMĂTOR: LOC VAL**
>Spre exemplu, pentru Galați un set de date corect și potrivit ar fi: 
```
Priponesti 5355 
Balasesti 1757 
Beresti 7987 
Balabanesti 7219 
Beresti-meria 7346 
Cavadinesti 2612 
Brahasesti 5165 
Gohor 4614 
Ghidigeni 3206 
Certesti 9304 
Buciumeni 6479 
Draguseni 3342 
Jorasti 4934 
Suceveni 3174 
Tepu 9283 
Nicoresti 8165 
Baneasa 3616 
Corod 3485 
Munteni 36 
Varlezi 4155 
Oancea 693 
Smulti 1793 
Targu Bujor 3355 
Valea Marului 3312 
Corni 1093 
Cosmesti 3631 
Matca 4684 
Tecuci 2819 
Vladesti 6497 
Fartanesti 2238 
Baleni 4250 
Draganesti 521 
Mastacani 8291 
Cudalbi 369 
Movileni 7602 
Barcea 4943 
Cuca 2361 
Foltesti 2989 
Rediu 4566 
Umbraresti 5662 
Costache Negri 3325 
Scanteiesti 7264 
Grivita 3105 
Frumusita 3623 
Pechea 2441 
Liesti 2209 
Tulucesti 2055 
Slobozia Conachi 8242 
Smardan 21 
Fundenii Noi 8846 
Tudor Vladimirescu 4532 
Piscu 2027 
Vanatori 2235 
Schela 34 
Namoloasa 4616 
Galati 8711 
Independenta 4726 
Sendreni 5396 
Braniste 6882 

```

### Accesarea și salvarea datelor
>Când setul de date dorit este inserat, tot ce rămâne de făcut este alegerea unui titlu pentru setul de date, și apăsarea pe butonul *Salvare*.

>Apoi, setul de date poate fi selectat din lista de deasupra acestui buton, atâta timp cât sunteți pe județul potrivit

### Moduri de afisare
1. Varietate de culori
2. Mărimea punctelor
3. Densitatea simbolurilor

>Fiecare mod în parte poate fi selectat foarte ușor în secțiunea *Prezentare*.

### Limite
>Tot in secțiunea *Prezentare*, se află și limitele. Manipulând sliderele, pe hartă vor apărea doar localitățile care au valori între cele 2 limite selectate. Această funcționalitate este perfectă atunci când trebuie să scoatem în evidență valorile de o anumită magnitudine.


## Obiective <a name = "obiective"></a>

>Misiunea finală și ideală este de a face acest proiect să funcționeze pentru orice regiune din orice țară din toată lumea. Datele acestea sunt la dispoziția publicului prin [GADM](https://gadm.org/). De asemenea, acest proces ar trebui să fie automat.

>Însă, deocamdată **ROVDATA** poate afișa doar statistici pe România. Prin urmare, un alt obiectiv ar fi răspândirea și implementarea aplicației în toată România, astfel încât oricine are nevoie de o interfață pentru afișat date pe țara noastră, să știe de ce să se folosească. 
## Credite <a name = "credite"></a>
Shapefile-urile sunt luate de pe site-ul GADM, iar PoissonDiscSampler.cs a fost luat de le [Gregory Schlomoff](http://gregschlom.com/devlog/2014/06/29/Poisson-disc-sampling-Unity.html).
Pozele din fundalul aplicatiei, cu geografia fiecarui judet in parte, au fost luate de pe Google Earth Pro.
In rest, tot codul a fost făcut de Berechet Tudor.
