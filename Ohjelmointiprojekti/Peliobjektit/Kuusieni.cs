using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Ohjelmointiprojekti {
    public class Kuusieni : Esine {
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
