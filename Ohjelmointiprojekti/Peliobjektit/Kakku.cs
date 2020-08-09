using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka kakku-esineelle, jonka pelaaja voi syödä
    /// </summary>
    public class Kakku : Esine {
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Kakku(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Cake";
            Maara = maara;
            Vari = RLColor.White;
            Merkki = 'c';
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää esinettä (pelaaja syö leivän)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You eat the cake.");
            Ohjelma.Pelaaja.Nalka += 50;
            if (Maara == 1) {
                Ohjelma.Pelaaja.Inventaario.Remove(this);
            }
            else {
                Maara--;
            }
            return false;
        }
    }
}