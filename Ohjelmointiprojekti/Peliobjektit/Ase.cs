using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Asevaruste joka lisää pelaajan aiheuttamaa vahinkoa, ja jota voi mahdollisesti käyttää nuolien ampumiseen
    /// Tekijä: Daniel Juola
    /// Luotu: 11.8.20
    /// </summary>
    public class Ase : Varuste {
        public int Vahinko { get; set; }
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <param name="nimi">Varusteen nimi</param>
        /// <param name="vari">Varusteen väri</param>
        /// <param name="merkki">Varusteen merkki</param>
        /// <param name="vahinko">Kuinka paljon varuste lisää pelaajan voimakkuutta</param>
        /// <param name="ampumaase">Voiko aseella ampua ammuksia</param>
        public Ase(int maara, int x, int y, string nimi, RLColor vari, char merkki, int vahinko, bool ampumaase) {
            X = x;
            Y = y;
            Nimi = nimi;
            Maara = maara;
            Vari = vari;
            Merkki = merkki;
            Lokero = 3;
            Vahinko = vahinko;
            VoiAmpua = ampumaase;
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
            Ohjelma.ViestiLoki.Lisaa($"You equip the {Nimi}");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Voimakkuus += Vahinko;
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa($"You unequip the {Nimi}.");
            Ohjelma.Pelaaja.Voimakkuus -= Vahinko;
        }
    }
}
