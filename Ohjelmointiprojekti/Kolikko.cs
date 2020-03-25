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
    public class Kolikko : Esine {
        public Kolikko(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Coin";
            Maara = maara;
            Vari = RLColor.Yellow;
            Merkki = 'o';
        }
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You flip the coin.");
            DotNetRandom satunnaisluku = new DotNetRandom();
            int heitto = satunnaisluku.Next(0, 1);
            if (heitto == 0) {
                Ohjelma.ViestiLoki.Lisaa("Heads.");
            }
            else {
                Ohjelma.ViestiLoki.Lisaa("Tails.");
            }
            return false;
        }
    }
}
