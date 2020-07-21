using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Tämä luokka luo erilaisia karttoja, käyttäen GameMap-luokkaa
    /// </summary>
    public class KarttaGeneroija {
        private readonly int karttaLeveys;
        private readonly int karttaKorkeus;
        private PeliKartta kartta;

        //konstruktori
        public KarttaGeneroija(int leveys, int korkeus) {
            karttaLeveys = leveys;
            karttaKorkeus = korkeus;
            kartta = new PeliKartta();
        }
        //Testikartta testausta varten
        public PeliKartta TestiKartta() {
            kartta = new PeliKartta {
                id = 0
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys/2, karttaKorkeus/2, 12)) {
                kartta.SetCellProperties(solu.X, solu.Y, false, false, true);
            }
            foreach (Cell solu in kartta.GetCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2, 11)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.LightGray, RLColor.Black, '.'));
            }
            kartta.SetCellProperties(karttaLeveys / 2, karttaKorkeus-14, false, false, true);
            kartta.Ovet.Add(new Ovi { X = karttaLeveys / 2 , Y = ((karttaKorkeus / 2) - 12), Auki = false});
            kartta.Esineet.Add(new Kolikko(1, (karttaLeveys / 2)-3, (karttaKorkeus/2)-3));
            kartta.Esineet.Add(new Huppu(1, (karttaLeveys / 2) + 3, (karttaKorkeus / 2) - 7));
            kartta.Esineet.Add(new Leipa(1, (karttaLeveys / 2) + 5, (karttaKorkeus / 2) - 5));
            kartta.Esineet.Add(new Kuusieni(2, (karttaLeveys / 2) + 7, ((karttaKorkeus / 2) + 10)));
            kartta.Esineet.Add(new Jousi(1, (karttaLeveys / 2) - 5, (karttaKorkeus / 2) - 8));
            kartta.Esineet.Add(new Nuoli(1, (karttaLeveys / 2) - 2, (karttaKorkeus / 2) -10));
            DialogiNoodi testidialogi1 = new DialogiNoodi("Hello how are you", "1. I am fine 2. I am not fine", new (int, Action)[] { (1, null), (2, null) });
            DialogiNoodi testidialogi2 = new DialogiNoodi("Well good.", "1. Good bye.", new (int, Action)[] { (-1, null) });
            DialogiNoodi testidialogi3 = new DialogiNoodi("Well that's unfortunate.", "1. Good bye", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] testidialogitaulukko = new DialogiNoodi [] { testidialogi1, testidialogi2, testidialogi3 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) - 3, ((karttaKorkeus / 2) +2), "test", 'T', RLColor.Blue, testidialogitaulukko, true));
            kartta.LisaaVastustaja(new Vastustaja((karttaLeveys / 2) + 3, ((karttaKorkeus / 2) +4), "enemy", 'E', RLColor.Red, 5,1,1,1,true));
            return kartta;
        }
        //Kartta jos pelaaja kuolee
        public PeliKartta TyhjaKartta() {
            kartta = new PeliKartta {
                id = 1
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            return kartta;
        }
        public PeliKartta AloitusKartta() {
            kartta = new PeliKartta {
                id = 2
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            foreach (Cell solu in kartta.GetCellsInRectangle(karttaKorkeus/2, karttaLeveys / 2-2, 5, karttaKorkeus / 2)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, true, true, RLColor.LightRed, RLColor.Black, '.'));
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2-4, 3)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Magenta, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2 - 4, 2)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Brown, RLColor.Black, '|'));
            }
            kartta.Ovet.Add(new Ovi { X = karttaLeveys / 2, Y = ((karttaKorkeus / 2) - 1), Auki = false });
            DialogiNoodi ennustajaDialogi0 = new DialogiNoodi("Greetings. I have awaited your arrival. You may not know why you came here, but fate guided your steps.", "1. Who are you? 2. What do you want from me?", new (int, Action)[] { (1, null), (2, null) });
            DialogiNoodi ennustajaDialogi1 = new DialogiNoodi("I am a gypsy and a fortune teller. My name matters not.", "1. What do you want from me?", new (int, Action)[] { (2, null) });
            DialogiNoodi ennustajaDialogi2 = new DialogiNoodi("I shall read the path of your future. Let us begin the casting...", "1. ...", new (int, Action)[] { (3, null) });
            string napparyysVsVoimakkuus1 = "While visiting a castle, you see your host abusing his servant. What do you do?";
            string napparyysVsVoimakkuus2 = "1. I control my feelings and don't interfere rashly. 2. I kindly step in and defend the servant in the name of justice.";
            string alykkyysVsVoimakkuus1 = "On the road, you meet a beggar who smells of alcohol, begging you to give him coin. What do you do?";
            string alykkyysVsVoimakkuus2 = "1. I wisely refuse, knowing he will spend it all on drink. 2. I kindly give him my coin, for he needs it more than me.";
            DialogiNoodi ennustajaDialogi3 = new DialogiNoodi("While wandering in the woods, you find a cave with a sleeping dragon inside. What do you do?", "1. I wisely move on, knowing I cannot defeat the dragon. 2. I control my fears and sneak inside to kill the beast.", new (int, Action)[] { (4, Ohjelma.Pelaaja.LisaaAlykkyys), (5, Ohjelma.Pelaaja.LisaaNapparyys) });
            DialogiNoodi ennustajaDialogi4 = new DialogiNoodi(alykkyysVsVoimakkuus1, alykkyysVsVoimakkuus2, new (int, Action)[] { (6, Ohjelma.Pelaaja.LisaaAlykkyys), (6, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi ennustajaDialogi5 = new DialogiNoodi(napparyysVsVoimakkuus1, napparyysVsVoimakkuus2, new (int, Action)[] { (8, Ohjelma.Pelaaja.LisaaNapparyys), (8, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi ennustajaDialogi6 = new DialogiNoodi(napparyysVsVoimakkuus1, napparyysVsVoimakkuus2, new (int, Action)[] { (7, Ohjelma.Pelaaja.LisaaNapparyys), (7, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi ennustajaDialogi7 = new DialogiNoodi("Good. Now I can see your path clearly. Close your eyes, and awaken.", "1. I feel dizzy...", new (int, Action)[] { (-1, LataaLinna) });
            DialogiNoodi ennustajaDialogi8 = new DialogiNoodi(alykkyysVsVoimakkuus1, alykkyysVsVoimakkuus2, new (int, Action)[] { (7, Ohjelma.Pelaaja.LisaaAlykkyys), (7, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi[] ennustajadialogitaulukko = new DialogiNoodi[] { ennustajaDialogi0, ennustajaDialogi1, ennustajaDialogi2, ennustajaDialogi3, ennustajaDialogi4, ennustajaDialogi5, ennustajaDialogi6, ennustajaDialogi7, ennustajaDialogi8};
            kartta.LisaaNPC(new NPC((karttaLeveys / 2), ((karttaKorkeus / 2) -5), "The Fortuneteller", 'f', RLColor.Blue, ennustajadialogitaulukko, false));
            for (int x = 0; x < karttaLeveys; x+=8) {
               for (int y = 0; y < karttaKorkeus; y += 3) {
                    kartta.Solut.Add(new Solu(x, y, false, false, true, RLColor.Green, RLColor.Black, 'T'));
                    kartta.SetCellProperties(x, y, false, false, true);
                }
            }
            
            return kartta;
        }
        public PeliKartta LinnaKartta() {
            kartta = new PeliKartta {
                id = 3
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2, karttaKorkeus - 40)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2 - 40, karttaKorkeus / 2, karttaKorkeus - 48)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2 +40, karttaKorkeus / 2, karttaKorkeus - 48)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            kartta.Ovet.Add(new Ovi { X = ((karttaLeveys / 2) - 1), Y = karttaKorkeus - 8, Auki = false });
            kartta.Ovet.Add(new Ovi { X = ((karttaLeveys / 2) + 1), Y = karttaKorkeus - 8, Auki = false });
            kartta.Ovet.Add(new Ovi { X = ((karttaLeveys / 2) -24), Y = karttaKorkeus/2, Auki = false });
            kartta.Ovet.Add(new Ovi { X = ((karttaLeveys / 2) + 24), Y = karttaKorkeus / 2, Auki = false });
            return kartta;
        }
        public void LataaLinna() {
            Ohjelma.ViestiLoki.Lisaa("You feel strange as your surroundings fade temporarily...");
            Ohjelma.peliKartta = Ohjelma.karttaGeneroija.LinnaKartta();
            Ohjelma.Pelaaja.X = karttaLeveys / 2;
            Ohjelma.Pelaaja.Y = karttaKorkeus- 1;
            Ohjelma.peliKartta.PaivitaNakoKentta();
            Ohjelma.ViestiLoki.Lisaa("...Until you find yourself at an oddly familiar location.");
        }
    }
}
