using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka huppu-esinettä varten, jonka voi pukea
    /// </summary>
    public class KangasHousut : Varuste {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public KangasHousut(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Woolen Pants";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = 'M';
            Puolustus = 1;
            Lokero = 2;
            LokeroNimi = "3. Legs";
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää huppua (pelaaja pukee sen)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You wear the pants.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Puolustus += Puolustus;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You take off the pants.");
            Ohjelma.Pelaaja.Puolustus -= Puolustus;
        }
    }
}
