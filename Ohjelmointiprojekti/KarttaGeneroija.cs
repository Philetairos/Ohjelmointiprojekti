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
        //Aloituskartta
        public PeliKartta TestiKartta() {
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
            DialogiNoodi testidialogi1 = new DialogiNoodi("Hello how are you", "1. I am fine 2. I am not fine", new int[] { 1,2 });
            DialogiNoodi testidialogi2 = new DialogiNoodi("Well good.", "1. Good bye.", new int[] { -1 });
            DialogiNoodi testidialogi3 = new DialogiNoodi("Well that's unfortunate.", "1. Good bye", new int[] { -1 });
            DialogiNoodi[] testidialogitaulukko = new DialogiNoodi [] { testidialogi1, testidialogi2, testidialogi3 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) - 3, ((karttaKorkeus / 2) +2), "test", 'T', RLColor.Blue, testidialogitaulukko, true));
            kartta.LisaaVastustaja(new Vastustaja((karttaLeveys / 2) + 3, ((karttaKorkeus / 2) +4), "enemy", 'E', RLColor.Red, 1,1,1,1,true));
            return kartta;
        }
        //Kartta jos pelaaja kuolee
        public PeliKartta TyhjaKartta() {
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells())
            {
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            return kartta;
        }
    }
}
