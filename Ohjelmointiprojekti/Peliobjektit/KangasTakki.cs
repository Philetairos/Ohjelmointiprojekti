using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka takki-esinettä varten, jonka voi pukea
    /// </summary>
    public class KangasTakki : Varuste {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public KangasTakki(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Gambeson";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = 'H';
            Puolustus = 1;
            Lokero = 1;
            LokeroNimi = "2. Body";
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää huppua (pelaaja pukee sen)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You wear the gambeson.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Puolustus += Puolustus;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You take off the gambeson.");
            Ohjelma.Pelaaja.Puolustus -= Puolustus;
        }
    }
}
