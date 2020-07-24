using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka nuoli-ammukselle, jota pelaaja voi ampua jousella
    /// </summary>
    public class Nuoli : Ammus {
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Nuoli(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Arrow";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = 'I';
            Vahinko = 1;
            Kantama = 15;
        }

    }
}
