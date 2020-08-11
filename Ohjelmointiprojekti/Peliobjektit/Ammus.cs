using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka ammuttavia esineitä varten, jotka vahingoittavat kohdetta
    /// Tekijä: Daniel Juola
    /// Luotu: 8.6.20
    /// </summary>
    public class Ammus : Esine {
        public int Vahinko { get; set; }
        public int Kantama { get; set; }
        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Ammus(int maara, int x, int y, string nimi, RLColor vari, char merkki, int vahinko, int kantama) {
            X = x;
            Y = y;
            Nimi = nimi;
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = merkki;
            Vahinko = vahinko;
            Kantama = kantama;
        }
    }
}
