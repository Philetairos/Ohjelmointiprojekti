using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti
{
    /// <summary>
    /// Luokka oville, joita hahmot voivat aukaista
    /// TODO: lukitut ovet
    /// </summary>
    public class Door : IPiirra {
        public bool Auki;
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
        public Door() {
            Merkki = '|';
            Vari = RLColor.LightGray;
        }
        public void Piirra(RLConsole konsoli, IMap kartta) {
            if (!kartta.GetCell(X, Y).IsExplored) {
                return;
            }
            Merkki = Auki ? '.' : '|';
            if (kartta.IsInFov(X, Y)) {
                konsoli.Set(X, Y, Vari, RLColor.Black, Merkki);
            }
            else
            {
                konsoli.Set(X, Y, RLColor.Gray, RLColor.Black, '.');
            }
        }
    }
}
