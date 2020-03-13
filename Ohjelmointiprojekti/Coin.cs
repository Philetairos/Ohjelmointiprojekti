using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp.Random;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka kolikko-esinettä varten. Kolikoita voi käyttää kaupankäynnissä, ja sen voi myös heittää.
    /// </summary>
    public class Coin : Item {
        public Coin(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Kolikko";
            Maara = maara;
            Vari = RLColor.Yellow;
            Merkki = 'o';
        }
        public override bool KaytaEsine() {
            Program.ViestiLoki.Lisaa("Heität kolikkoa.");
            DotNetRandom satunnaisluku = new DotNetRandom();
            int heitto = satunnaisluku.Next(0, 1);
            if (heitto == 0) {
                Program.ViestiLoki.Lisaa("Klaava.");
            }
            else {
                Program.ViestiLoki.Lisaa("Kruuna.");
            }
            return false;
        }
    }
}
