using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka vihollishahmoja varten, jotka hyökkäävät pelaajaa vastaan
    /// </summary>
    public class Vastustaja : Hahmo {
        public readonly bool Liikkuu;
        public int Elama;
        public int Voimakkuus;
        public int Napparyys;
        public int Alykkyys;
        public int Puolustus;
        public Vastustaja() {

        }
        public Vastustaja(int x, int y, string nimi, char merkki, RLColor vari, int elama, int voimakkuus, int napparyys, int alykkyys, int puolustus, bool liikkuukko) {
            Nimi = nimi;
            Nakoetaisyys = 100;
            Vari = vari;
            Merkki = merkki;
            X = x;
            Y = y;
            Liikkuu = liikkuukko;
            Elama = elama;
            Voimakkuus = voimakkuus;
            Napparyys = napparyys;
            Alykkyys = alykkyys;
            Puolustus = puolustus;
        }
    }
}
