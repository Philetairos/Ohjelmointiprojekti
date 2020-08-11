using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka reagentti-esinettä varten jota pelaaja käyttää taikaloitsuissa
    /// Tekijä: Daniel Juola
    /// Luotu: 11.8.20
    /// </summary>
    public class Reagentti : Esine {
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <param name="nimi">Esineen nimi</param>
        /// <param name="vari">Esineen väri</param>
        /// <param name="merkki">Esineen merkki</param>
        public Reagentti(int maara, int x, int y, string nimi, RLColor vari, char merkki) {
            X = x;
            Y = y;
            Nimi = nimi;
            Maara = maara;
            Vari = vari;
            Merkki = merkki;
        }
    }
}
