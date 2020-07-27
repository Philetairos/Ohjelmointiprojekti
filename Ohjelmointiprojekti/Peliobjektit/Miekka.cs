using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Miekkavaruste joka lisää pelaajan aiheuttamaa vahinkoa
    /// </summary>
    public class Miekka : Varuste {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Miekka(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Sword";
            Maara = maara;
            Vari = RLColor.LightGray;
            Merkki = 'l';
            Lokero = 3;
            VoiAmpua = true;
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
            Ohjelma.ViestiLoki.Lisaa("You equip the sword.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.LisaaVoimakkuus();
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You unequip the sword.");
            Ohjelma.Pelaaja.Voimakkuus--;
        }
    }
}
