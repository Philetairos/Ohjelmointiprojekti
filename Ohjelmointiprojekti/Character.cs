using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Hahmo-luokka, jota kaikki pelin hahmot käyttävät. Hyödyntää hahmo- ja piirrä-rajapintoja

namespace Ohjelmointiprojekti {
    public class Hahmo : IHahmo, IPiirra {
        public string Nimi {
            get;
            set;
        }
        public int Nakoetaisyys {
            get;
            set;
        }
        public RLColor Vari {
            get;
            set;
        }
        public char Merkki {
            get;
            set;
        }
        public int X {
            get;
            set;
        }
        public int Y {
            get;
            set;
        }
        public void Piirra(RLConsole konsoli, IMap kartta) {
            if (!kartta.GetCell(X, Y).IsExplored) {
                return;
            }
            if (kartta.IsInFov(X, Y)) {
                konsoli.Set(X, Y, Vari, RLColor.Black, Merkki);
            }
            else {
                konsoli.Set(X, Y, RLColor.LightGray, RLColor.Black, '.');
            }
        }
    }
}
