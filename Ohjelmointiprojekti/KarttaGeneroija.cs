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
        private readonly PeliKartta kartta;

        //konstruktori
        public KarttaGeneroija(int leveys, int korkeus) {
            karttaLeveys = leveys;
            karttaKorkeus = korkeus;
            kartta = new PeliKartta();
        }
        //Testikartta testausta varten
        public PeliKartta TestiKartta() {
            kartta.id = 0;
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
            DialogiNoodi testidialogi1 = new DialogiNoodi("Hello how are you", "1. I am fine 2. I am not fine", new int[] { 1,2 });
            DialogiNoodi testidialogi2 = new DialogiNoodi("Well good.", "1. Good bye.", new int[] { -1 });
            DialogiNoodi testidialogi3 = new DialogiNoodi("Well that's unfortunate.", "1. Good bye", new int[] { -1 });
            DialogiNoodi[] testidialogitaulukko = new DialogiNoodi [] { testidialogi1, testidialogi2, testidialogi3 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) - 3, ((karttaKorkeus / 2) +2), "test", 'T', RLColor.Blue, testidialogitaulukko, true));
            kartta.LisaaVastustaja(new Vastustaja((karttaLeveys / 2) + 3, ((karttaKorkeus / 2) +4), "enemy", 'E', RLColor.Red, 5,1,1,1,true));
            return kartta;
        }
        //Kartta jos pelaaja kuolee
        public PeliKartta TyhjaKartta() {
            kartta.id = 1;
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            return kartta;
        }
        public PeliKartta AloitusKartta() {
            kartta.id = 2;
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
            DialogiNoodi ennustajaDialogi1 = new DialogiNoodi("Greetings. I have awaited your arrival. You may not know why you came here, but fate guided your steps.", "1. Who are you? 2. What do you want from me?", new int[] { 1, 2 });
            DialogiNoodi ennustajaDialogi2 = new DialogiNoodi("I am a gypsy and a fortune teller. My name matters not.", "1. What do you want from me?", new int[] { 2 });
            DialogiNoodi ennustajaDialogi3 = new DialogiNoodi("I shall read the path of your future. Let us begin the casting...", "1. ...", new int[] { 3 });
            DialogiNoodi ennustajaDialogi4 = new DialogiNoodi("", "1. ...", new int[] { -1 }) {
                Koodi = Ohjelma.Pelaaja.LisaaAlykkyys()
            };
            DialogiNoodi[] ennustajadialogitaulukko = new DialogiNoodi[] { ennustajaDialogi1, ennustajaDialogi2, ennustajaDialogi3, ennustajaDialogi4 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2), ((karttaKorkeus / 2) -5), "The Fortuneteller", 'f', RLColor.Blue, ennustajadialogitaulukko, false));
            for (int x = 0; x < karttaLeveys; x+=8) {
               for (int y = 0; y < karttaKorkeus; y += 3) {
                    kartta.Solut.Add(new Solu(x, y, false, false, true, RLColor.Green, RLColor.Black, 'T'));
                    kartta.SetCellProperties(x, y, false, false, true);
                }
            }
            
            return kartta;
        }
    }
}
