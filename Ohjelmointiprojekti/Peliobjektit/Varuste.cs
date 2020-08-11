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
    /// Tekijä: Daniel Juola
    /// Luotu: 8.6.20
    /// </summary>
    public class Varuste : Esine {
        public int Puolustus { get; set; }
        public int Voimakkuus { get; set; }
        public int Alykkyys { get; set; }
        public int Napparyys { get; set; }
        public int Lokero { get; set; }
        public bool VoiAmpua { get; set; }
        public string LokeroNimi { get; set; }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public virtual void PoistaVaruste () {

        }
    }
}
