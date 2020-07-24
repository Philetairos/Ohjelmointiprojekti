using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka kuusieni-esinettä varten jota pelaaja käyttää taikaloitsuissa
    /// </summary>
    public class Kuusieni : Esine {
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Kuusieni(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Moon Mushroom";
            Maara = maara;
            Vari = RLColor.LightBlue;
            Merkki = 't';
        }
    }
}
