# Ohjelmointiprojekti
Kurssin TIEA306 ohjelmointityö, kevät 2020

Paragon of Virtue-peli

#Käyttöohje

Kuinka käynnistää peli:
1. lataa repo ZIP-tiedostona tai kloonaa se (https://github.com/Philetairos/Ohjelmointiprojekti)
2. avaa Ohjelmointiprojekti\Ohjelmointiprojekti.csproj tiedosto Visual Studio-ohjelmalla
3. Paina "Build Solution" tai "Start"
4. Jos painoit "Build Solution" niin pelin .exe löytyy oletuksena Ohjelmointiprojekti\bin kansiosta
5. Käynnistä .exe

Kuinka pelata peliä:
Alussa ilmestyy aloitusvalikko. Valitse "Start new journey" aloittaaksesi uuden pelin. 
"Continue your journey" lataa tallennuksen jos sellainen on olemassa jotta voit jatkaa peliä. Huomioi kuitenkin että tallennetun pelin jatkaminen ei toimi täydellisesti ja joitain ongelmia voi esiintyä etenkin inventaariossa olleiden esineiden kanssa.
"End your journey" sammuttaa pelin.

Kun aloitat uuden pelin, näyttöön ilmestyy pelikartta. Näytön alaosassa on viestikonsoli, jossa näkyy pelin kontrollit. Voit nähdä kontrollit milloin tahansa painamalla C-näppäintä. Oikealla puolella näkyy pelihahmosi tiedot. Ne tarkoittavat seuraavaa:
Name: Pelihahmon nimi
Health: Elämäpisteet. Jos ne putoavat nollaan, pelihahmo kuolee.
Hunger: Nälkämittari. Putoaa hitaasti pelihahmon liikkuessa. Jos nälkämittari menee alle nollan, pelihahmon elämäpisteet alkavat pudota.
Level: Pelihahmon taso. Pelin tavoitteena on saada tason 3.
Defence: Pelihahmon puolutus. Korkeampi arvo lisää mahdollisuutta torjua vastustajan hyökkäys.
Strength: Pelihahmon voimakkuus, eli kuinka paljon vahinkoa pelihahmo voi aiheuttaa vastustajalle.
Dexterity: Näppäryys. Korkeampi arvo lisää mahdollisuutta että vastustajan hyökkäys ei osu pelihahmoon.
Intelligence: Älykkyys. Korkeampi arvo mahdollistaa uusien loitsujen käyttämisen.

Peliä ohjataan yksistään näppäimistöllä. Kun komentoa painetaan, niin viestikonsoli selittää miten sitä käytetään. Tärkeintä on tietää, että Get-komennolla otetaan esineitä ja Use-komennolla käytetään niitä. Use-komennon avulla voi laittaa varusteita pelihahmon päälle, syödä ruokaa jne.

NPC-hahmot opastavat pelaajaa. Talk-komennolla heidän kanssaan voi keskustella ja selvittää miten pelin voi päihittää.

# Ohjeita kehittäjille
Tiedostot on jaoteltu kahteen osaan: Ydintiedostoihin jotka hallinnoivat pelin toimintaa, ja peliobjekteihin jotka ovat pelikartalla olevia asioita joiden välillä on interaktiota. Pelin tärkein tiedosto on Ohjelma.cs, joka pyörittää konsoleja ja tulkkaa pelaajan komentoja. KomentoKasittelija.cs on toinen tärkeä tiedosto joka suorittaa pelaajan komentona ja toimii siltana pelihahmon ja muiden peliobjektien välillä, kun taas Pelikartta.cs on silta peliobjektien ja pelikartan välillä. KarttaGeneroija luo pelin eri kartat ja sisältää kaiken siihen tarvittavan datan, joten jos karttoja haluaa muokata niin pitää muuttaa tätä tiedostoa.

Peli käyttää laajasti RogueSharp- ja RLNET-kirjastoja, joten pelin muokaamista varten voi lukea näiden kirjastojen käyttöohjeita.
https://github.com/FaronBracy/RogueSharp
https://bitbucket.org/clarktravism/rlnet/src/master/
