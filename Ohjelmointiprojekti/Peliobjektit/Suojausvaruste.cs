using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Puettavat varusteet jotka lisäävät pelaajan puolustusta
    /// Tekijä: Daniel Juola
    /// Luotu: 11.8.20
    /// </summary>
    public class Suojausvaruste : Varuste {
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <param name="nimi">Varusteen nimi</param>
        /// <param name="vari">Varusteen väri</param>
        /// <param name="merkki">Varusteen merkki</param>
        /// <param name="puolustus">Kuinka paljon varuste lisää pelaajan puolustusta</param>
        /// <param name="lokero">Mihin varustelokeroon varuste kuuluu</param>
        public Suojausvaruste(int maara, int x, int y, string nimi, RLColor vari, char merkki, int puolustus, int lokero) {
            X = x;
            Y = y;
            Nimi = nimi;
            Maara = maara;
            Vari = vari;
            Merkki = merkki;
            Puolustus = puolustus;
            Lokero = lokero;
            switch(lokero) {
                case 0:
                    LokeroNimi = "1. Head";
                    break;
                case 1:
                    LokeroNimi = "2. Body";
                    break;
                case 2:
                    LokeroNimi = "3. Legs";
                    break;
                case 3:
                    LokeroNimi = "4. Right Hand";
                    break;
                case 4:
                    LokeroNimi = "5. Left Hand";
                    break;
            }
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää suojausvarustetta (pelaaja pukee sen)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            if (Ohjelma.Pelaaja.Varusteet[Lokero] != null) {
                Ohjelma.Pelaaja.PoistaVaruste(Lokero+1);
            }
            Ohjelma.ViestiLoki.Lisaa($"You wear the {Nimi}");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Puolustus += Puolustus;
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa ($"You take off the {Nimi}");
            Ohjelma.Pelaaja.Puolustus -= Puolustus;
        }
    }
}
