using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka leipä-esineelle, jonka pelaaja voi syödä
    /// </summary>
    public class Leipa : Esine {
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Leipa (int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Loaf of bread";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = 'b';
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää esinettä (pelaaja syö leivän)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You eat the bread.");
            Ohjelma.Pelaaja.Nalka += 25;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }
    }
}