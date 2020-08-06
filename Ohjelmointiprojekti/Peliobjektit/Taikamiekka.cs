using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Taikamiekka jonka pelaaja voi luoda
    /// </summary>
    public class Taikamiekka : Varuste {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Taikamiekka(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Spellsword";
            Maara = maara;
            Vari = RLColor.Cyan;
            Merkki = 'l';
            Lokero = 3;
            VoiAmpua = false;
            LokeroNimi = "4. Right Hand";
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää esinettä (pelaaja asettaa sen varusteeksi)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            if (Ohjelma.Pelaaja.Varusteet[Lokero] != null) {
                Ohjelma.Pelaaja.PoistaVaruste(Lokero+1);
            }
            Ohjelma.ViestiLoki.Lisaa("You equip the Spellsword.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Voimakkuus += 3;
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You unequip the Spellsword.");
            Ohjelma.Pelaaja.Voimakkuus-=3;
        }
    }
}
