using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka taikanuoli-ammukselle
    /// </summary>
    public class Taikanuoli : Ammus {
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Taikanuoli(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Firebolt";
            Maara = maara;
            Vari = RLColor.Red;
            Merkki = '>';
            Vahinko = 5;
            Kantama = 10;
        }

    }
}
