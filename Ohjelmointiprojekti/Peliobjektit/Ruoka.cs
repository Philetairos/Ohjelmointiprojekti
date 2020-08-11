using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka ruoka-esineelle, jonka pelaaja voi syödä
    /// Tekijä: Daniel Juola
    /// Luotu: 11.8.20
    /// </summary>
    public class Ruoka : Esine {
        public int Ravinto { get; set; }
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <param name="nimi">Esineen nimi</param>
        /// <param name="vari">Esineen väri</param>
        /// <param name="merkki">Esineen merkki</param>
        /// <param name="ravinto">Kuinka paljon pelaaja saa ravinteita syömällä ruoka-esineen</param>
        public Ruoka(int maara, int x, int y, string nimi, RLColor vari, char merkki, int ravinto) {
            X = x;
            Y = y;
            Nimi = nimi;
            Maara = maara;
            Vari = vari;
            Merkki = merkki;
            Ravinto = ravinto;
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää esinettä (pelaaja syö ruoan)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa($"You eat the {Nimi}.");
            Ohjelma.Pelaaja.Nalka += Ravinto;
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