INSERT INTO STUDENTS (neptun_id,hash_password, first_name, last_name, user_status) VALUES
(N'B2TN3S',N'ed3022ef11740e30681340546e8613cffda2e31aa9193a01d27f065cfea1ce68', N'Mark', N'Kovacs', N'Active'),
(N'F2XVKV',N'42f71a7b742bd4807a169e87d9a29dae4e1c022db61bd399eeb292a4102514ec', N'Attila', N'Hoki', N'Active'),
(N'GYBASN',N'dfd74f685ea29d899c3c7d8efc468b5239225a3c95d69e5ae8ff23f969c19f5b', N'Armand', N'Koltai', N'Active'),
(N'teststudent',N'9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', N'Test', N'Test', N'Active'),
(N'programstudent',N'7bd9ca7a756115eabdff2ab281ee9d8c22f44b51d97a6801169d65d90ff16327', N'Programming', N'Student', N'Active');


INSERT INTO INSTRUCTORS (hash_password, first_name, last_name, department, office, user_status) VALUES
(N'e53b70490a8e07e2acb210b04723cd4dfd0248642e59aabc1a2f1ad927a064d9', N'Jakab', N'Gipsz', N'Szotverfejlesztes tanszek', N'4/111', N'Active'),
(N'27eaf211ead0de6749e671c4816218750fce631d15d10947ac3512cb966d6731', N'Bela', N'Kovacs', N'Alaptudomanyi tanszek', N'4/123', N'Active'),
(N'3834f5a9f235fd708f64544b65974e2eb6a778b89c541d3c962a0409496ee451', N'Eva', N'Szabo', N'Szoftverfejlesztes tanszek', N'4/222', N'Active');

INSERT INTO EXAMs (title, level, kredit_value, start_time, end_time, time_limit) VALUES
(N'Programozas I.', N'BsC', 4, '2023-10-15T09:00:00', '2023-10-15T11:00:00', 120),
(N'Fizika I.', N'BsC', 3, '2023-10-16T13:00:00', '2023-10-16T15:30:00', 150),
(N'Adatbázisok', N'BsC', 3, '2023-11-01T10:00:00', '2023-11-01T12:00:00', 120),
(N'Kalkulus I.', N'BsC', 3, '2023-10-18T15:00:00', '2023-10-18T17:00:00', 120),
(N'Villamossagtan', N'BsC', 4, '2023-11-05T14:00:00', '2023-11-05T16:30:00', 150);

INSERT INTO EXAMs_INSTRUCTORs (course_id, profid) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 2),
(5, 2);

INSERT INTO STUDENTs_EXAMs(neptun_id, course_id) VALUES 
('B2TN3S', 5),
('B2TN3S', 2),
('GYBASN', 1),
('GYBASN', 2),
('GYBASN', 4),
('F2XVKV', 3),
('teststudent',1),
('teststudent',2),
('teststudent',3),
('teststudent',4),
('teststudent',5),
('programstudent',1),
('programstudent',3);


INSERT INTO QUESTIONS (question, answers, solution, point_value, course_id) VALUES
('Mi a kimenete a következő C# kódnak: Console.WriteLine(5 / 2);?', '2.5;2;2.0;Hiba történik', 2, 5, 1),
('Melyik kulcsszóval jelezzük, hogy egy metódus nem tér vissza értékkel C#-ban?', 'void;null;empty;zero', 1, 5, 1),
('Hogyan hozhatunk létre egy egész számokat tartalmazó tömböt C#-ban?', 'int[] myArray = new int[5];int myArray[] = new int[5];array int[5] myArray;new array[5] int', 1, 5, 1),
('Mi a célja a "using" direktívának C#-ban?', 'Megadja az osztályok elérési útvonalát;Kódismétlés csökkentése;Dinamikus tárfoglalás;Memóriakezelés automatizálása', 1, 5, 1),
('Melyik hozzáférési szinttel jelöljük, hogy egy tag csak az adott osztály és leszármazottai számára elérhető C#-ban?', 'private;protected;public;internal', 2, 5, 1),
('Milyen típusú a "var" kulcsszó használata esetén definiált változó C#-ban?', 'Dinamikus típusú;Statikus típusú;Anonim típusú;Erősen típusos', 4, 5, 1),
('Mi a szerepe a "try-catch" szerkezetnek C#-ban?', 'Hibakezelés;Ciklusok kezelése;Memória felszabadítása;Fájlműveletek kezelése', 1, 5, 1),
('Milyen művelet eredménye az "as" operátor használata C#-ban?', 'Típuskonverzió, ha lehetséges, egyébként null;Típuskonverzió, kivétel keletkezik, ha nem lehetséges;Értékadás;Típusinformációk lekérdezése', 1, 5, 1),
('Mi jellemzi az "override" kulcsszót C#-ban?', 'Egy szülőosztály metódusának felülírása;Egy metódus túlterhelése;Egy adattag elrejtése;Egy interfész implementálása', 1, 5, 1),
('Hogyan lehet C#-ban egy osztálypéldányt explicit módon a szemétgyűjtőnek átadni?', 'Garbage.Collect();System.GC.Collect();Collect.Garbage();Dispose.Now()', 2, 5, 1);


INSERT INTO QUESTIONS (question, answers, solution, point_value, course_id) VALUES
('Mi az energia megmaradás elve?', 'Az energia nem teremthető meg és nem semmisíthető meg;Az energia csak egy formából másikba alakulhat át;Az energia csökkenhet a rendszerben;Az energia mindig növekszik', 1, 5, 2),
('Mi a newtoni dinamika első törvénye?', 'Minden test nyugalomban marad vagy egyenes vonalú egyenletes mozgást végez, amíg egy erő meg nem változtatja;Az erő egyenlő a tömeg és a gyorsulás szorzatával;Minden hatásnak van egyenlő és ellentétes irányú reakciója;Az erők mindig párosával lépnek fel', 1, 5, 2),
('Mikor mondjuk, hogy egy mozgás egyenletes körmozgás?', 'Amikor a test sebessége állandó, de az iránya folyamatosan változik;Amikor a test sebessége és iránya is állandó;Amikor a test sebessége változik, de az iránya állandó;Amikor a test gyorsulása állandó', 1, 5, 2),
('Mi a tehetetlenségi nyomaték?', 'Egy forgó test tömegének és a forgástengelytől mért távolságának szorzata;Egy test tömegközéppontjának és a forgástengelytől mért távolságának a négyzete;Egy forgó test tömegének és sebességének szorzata;Egy test tömegének és gyorsulásának szorzata', 1, 5, 2),
('Mi a fénysebesség vákuumban?', 'Körülbelül 300 000 km/s;Körülbelül 150 000 km/s;Körülbelül 200 000 km/s;Körülbelül 100 000 km/s', 1, 5, 2),
('Mi a relativitáselmélet egyik alapvető következménye?', 'Az idő és a tér abszolút fogalmak;Az idő és a tér relatív fogalmak, amelyek a megfigyelő sebességétől függően változnak;Csak az idő abszolút, a tér relatív;Csak a tér abszolút, az idő relatív', 2, 5, 2),
('Milyen egységben mérjük a nyomást?', 'Pascal;Newton;Joule;Watt', 1, 5, 2),
('Mi határozza meg egy bolygó gravitációs vonzását?', 'A bolygó tömege és sugara;Csak a bolygó tömege;Csak a bolygó sugara;A bolygó tömege és a Nap távolsága', 1, 5, 2),
('Mi a hőmérséklet szerepe az ideális gáz állapotegyenletében?', 'Arányos a gáz nyomásával;Arányos a gáz térfogatával;Fordítottan arányos a gáz nyomásával;Fordítottan arányos a gáz térfogatával', 2, 5, 2),
('Hogyan változik a hang sebessége, ha a hőmérséklet növekszik?', 'Növekszik;Csökken;Nem változik;Véletlenszerűen változik', 1, 5, 2);


INSERT INTO QUESTIONS (question, answers, solution, point_value, course_id) VALUES
('Milyen SQL parancsot használunk adatok lekérdezésére?', 'INSERT;UPDATE;SELECT;DELETE', 3, 5, 3),
('Mi a funkciója az "index"-nek egy adatbázisban?', 'Adatok frissítése;Adatok beszúrása;Keresési műveletek gyorsítása;Adatok törlése', 3, 5, 3),
('Mi jellemzi az "ER" modellt az adatbázis-tervezésben?', 'Entitások és kapcsolatok ábrázolása;Adatok normál formába hozása;Tranzakciók kezelése;Táblák és indexek létrehozása', 1, 5, 3),
('Mi a célja a "foreign key" (idegen kulcs) használatának egy adatbázisban?', 'Táblák összekapcsolása;Tranzakciók gyorsítása;Lekérdezések egyszerűsítése;Adatok biztonságának növelése', 1, 5, 3),
('Melyik SQL parancsot használjuk egy új tábla létrehozásához?', 'CREATE TABLE;SELECT TABLE;INSERT INTO;UPDATE TABLE', 1, 5, 3),
('Milyen típusú integritási kényszer biztosítja, hogy egy oszlop vagy oszlopcsoport értékei egyediek maradjanak?', 'Check;Unique;Primary Key;Not Null', 2, 5, 3),
('Hogyan nevezik az adatbázisban azokat a műveleteket, amelyek atomikusak és konzisztensek?', 'Lekérdezések;Tranzakciók;View-k;Indexek', 2, 5, 3),
('Mi a szerepe a "normalizációnak" az adatbázis-tervezésben?', 'Adatismétlés csökkentése;Lekérdezések gyorsítása;Adatbiztonság növelése;Tranzakciók kezelése', 1, 5, 3),
('Mi a különbség az "inner join" és az "outer join" között SQL-ben?', 'Csatlakoztat minden sort;Csak a metszetet csatlakoztatja;Csak a különbséget csatlakoztatja;Nem csatlakoztat sorokat', 2, 5, 3),
('Melyik SQL utasítás frissít egy meglévő rekordot az adatbázisban?', 'UPDATE;INSERT;DELETE;SELECT', 1, 5, 3);


INSERT INTO QUESTIONS (question, answers, solution, point_value, course_id) VALUES
('Mi a deriválás alapvető célja a matematikában?', 'Függvények közelítése;Függvények minimum és maximum pontjainak meghatározása;Függvények integrálásának megkönnyítése;Egyenletek megoldása', 2, 5, 4),
('Mi a határozott integrál fizikai értelmezése?', 'Terület meghatározása a függvény és az x-tengely között;A függvény alatti térfogat meghatározása;Az x-tengely alatti terület meghatározása;A függvény és a y-tengely közötti terület meghatározása', 1, 5, 4),
('Mikor mondjuk, hogy egy függvény folytonos egy pontban?', 'Ha létezik a határértéke és egyenlő a függvényértékkel abban a pontban;Ha a deriváltja létezik abban a pontban;Ha nincsenek szakadásai a függvénynek;Ha a függvény grafikonja sima és összefüggő', 1, 5, 4),
('Hogyan határozzuk meg egy függvény lokális maximumát vagy minimumát?', 'A derivált nulla értékeinek megtalálásával;Az integrál maximum értékeivel;A függvény értékeinek összehasonlításával;A második derivált próbájával', 1, 5, 4),
('Mi a Taylor-sor?', 'Egy függvény közelítése polinomok sorozatával egy adott pont körül;Egy sorozat összegének határértéke;Egy differenciálegyenlet megoldása;Egy integrál kiszámítása', 1, 5, 4),
('Mi a LHôpital szabály alkalmazásának feltétele?', 'A határérték számításakor 0/0 vagy ∞/∞ formában kell lennie;A deriváltak létezése;Az eredeti függvények folytonossága;Az integrálok határértékének létezése', 1, 5, 4),
('Mire használjuk a parciális deriváltakat?', 'Többváltozós függvények lokális extrémumainak megtalálására;Egyváltozós függvények extrémumainak megtalálására;Függvények egyszerűsítésére;Alapvető deriválási szabályok megértésére', 1, 5, 4),
('Mi a zérushelye egy függvénynek?', 'Az a pont, ahol a függvény értéke nulla;Az a pont, ahol a függvény deriváltja nulla;Az a pont, ahol a függvény integrálja nulla;Az a pont, ahol a függvény diszkontinuitást mutat', 1, 5, 4),
('Mi a differenciálegyenletek alapvető célja?', 'Ismeretlen függvények viselkedésének modellezése;Függvények integrálásának megkönnyítése;Egyenletek megoldásainak számítása;Adatok közelítése', 1, 5, 4),
('Milyen problémát old meg az integrálás?', 'Területek, térfogatok és egyéb mennyiségek kiszámítása;Függvények deriválása;Egyenletek lineárizálása;Adatok elemzése', 1, 5, 4);



INSERT INTO QUESTIONS (question, answers, solution, point_value, course_id) VALUES
('Mi a feszültség definíciója az elektromosság területén?', 'Az elektromos mező ereje egy adott pontban;Az elektromos mező egy adott felületén mérhető energia;Az elektromos mező egy adott pontján mérhető energia;Az elektromos mező ereje egy adott felületen', 3, 5, 5),
('Mi a definíciója az ellenállásnak az elektromosság területén?', 'Az elektromos mező ereje egy adott pontban;Az elektromos mező egy adott felületén mérhető energia;Az elektromos mező egy adott pontján mérhető energia;Az elektromos mező ereje egy adott felületen', 4, 5, 5),
('Mi a definíciója az áramerősségnek az elektromosság területén?', 'Az elektromos mező ereje egy adott pontban;Az elektromos mező egy adott felületén mérhető energia;Az elektromos mező egy adott pontján mérhető energia;Az elektromos mező ereje egy adott felületen', 2, 5, 5),
('Mi a definíciója a teljesítménynek az elektromosság területén?', 'Az elektromos mező ereje egy adott pontban;Az elektromos mező egy adott felületén mérhető energia;Az elektromos mező egy adott pontján mérhető energia;Az elektromos mező ereje egy adott felületen', 1, 5, 5),
('Mi a definíciója a kapacitásnak az elektromosság területén?', 'Az elektromos mező ereje egy adott pontban;Az elektromos mező egy adott felületén mérhető energia;Az elektromos mező egy adott pontján mérhető energia;Az elektromos mező ereje egy adott felületen', 4, 5, 5);
