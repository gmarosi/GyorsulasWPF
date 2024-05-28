# Gyorsulás WPF

Ez a projekt beadandóként készült az ELTE programtervező informatikus képzésének Eseményvezérelt alkalmazások tárgyához.  
Készítette: Marosi Gergely

## Feladatleírás

Készítsünk programot, amellyel az alábbi motoros játékot játszhatjuk.  
- A feladatunk, hogy egy gyorsuló motorral minél tovább tudjunk haladni.
- A gyorsuláshoz a motor üzemanyagot fogyaszt, egyre többet.
  - Adott egy kezdeti mennyiség, amelyet a játék során üzemanyagcellák felvételével tudunk növelni.
- A motorral a képernyő alsó sorában tudunk balra, illetve jobbra navigálni.
- A képernyő felső sorában meghatározott időközönként véletlenszerű pozícióban jelennek meg üzemanyagcellák, amelyek folyamatosan közelednek a képernyő alja felé.
  - Mivel a motor gyorsul, ezért a cellák egyre gyorsabban fognak közeledni,
  és mivel a motor oldalazó sebessége nem változik, idővel egyre nehezebb lesz
  felvenni őket, így egyszer biztosan kifogyunk üzemanyagból.
- A játék célja az, hogy a kifogyás minél később következzen be.
- A program biztosítson lehetőséget új játék kezdésére, valamint játék
  szüneteltetésére (ekkor nem telik az idő, és nem mozog semmi a játékban).
- Ismerje fel, ha vége a játéknak, és jelenítse meg, mennyi volt a játékidő.
- Ezen felül szüneteltetés alatt legyen lehetőség a játék elmentésére, valamint betöltésére.
