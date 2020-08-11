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
    /// Tekijä: Daniel juola
    /// Luotu: 8.6.20.
    /// </summary>
    public class Kolikko : Esine {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Kolikko(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Coin";
            Maara = maara;
            Vari = RLColor.Yellow;
            Merkki = 'o';
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää esinettä (pelaaja heittää kolikkoa)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
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
