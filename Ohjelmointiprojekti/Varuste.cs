using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka pelin varusteita varten, joilla pelaaja voi varustautua
    /// </summary>
    public class Varuste : Esine {
        public int Puolustus { get; set; }
        public int Voimakkuus { get; set; }
        public int Alykkyys { get; set; }
        public int Napparyys { get; set; }
        public int Lokero { get; set; }
        public string LokeroNimi { get; set; }

        public virtual void PoistaVaruste () {

        }
    }
}
