using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Jousivaruste, jota pelaaja voi käyttää ampumiseen
    /// </summary>
    public class Jousi : Varuste {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Jousi(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Bow";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = ')';
            Lokero = 4;
            VoiAmpua = true;
            LokeroNimi = "4. Right Hand";
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää esinettä (pelaaja asettaa sen varusteeksi)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You equip the bow.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You unequip the bow.");
        }
    }
}
