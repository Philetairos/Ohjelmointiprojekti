using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka taikakilpi-varustetta varten jonka pelaaja voi luoda
    /// </summary>
    public class Taikakilpi : Varuste {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Taikakilpi(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Spellshield";
            Maara = maara;
            Vari = RLColor.Cyan;
            Merkki = '0';
            Puolustus = 2;
            Lokero = 4;
            LokeroNimi = "5. Left Hand";
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää huppua (pelaaja pukee sen)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            if (Ohjelma.Pelaaja.Varusteet[Lokero] != null) {
                Ohjelma.Pelaaja.PoistaVaruste(Lokero+1);
            }
            Ohjelma.ViestiLoki.Lisaa("You equip the spellshield.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Puolustus += Puolustus;
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You remove the spellshield.");
            Ohjelma.Pelaaja.Puolustus -= Puolustus;
        }
    }
}
