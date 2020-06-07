using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

/// <summary>
/// Luokka loitsuissa käytettäviä esineitä varten
/// </summary>
namespace Ohjelmointiprojekti {
    class Reagenssi : Esine {
        public Reagenssi(int maara, int x, int y, string nimi, RLColor vari, char merkki) {
            X = x;
            Y = y;
            Nimi = nimi;
            Maara = maara;
            Vari = vari;
            Merkki = merkki;
        }
    }
}
