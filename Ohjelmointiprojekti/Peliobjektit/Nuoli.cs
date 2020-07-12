using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Ohjelmointiprojekti {
    public class Nuoli : Ammus {
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
