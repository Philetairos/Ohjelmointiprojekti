using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Cell-luokan laajennos joka sisältää värin ja merkin
    /// </summary>
    public class Solu : Cell, IPiirra {
        public RLColor Vari { get; set; }
        public char Merkki { get; set; }

        public Solu() {

        }

        public Solu(int x, int y, bool isTransparent, bool isWalkable, bool isInFov, RLColor vari, char merkki) : base(x,y,isTransparent,isWalkable,isInFov) {
            Vari = vari;
            Merkki = merkki;
        }

        public void Piirra(RLConsole konsoli, IMap kartta)
        {
            if (!kartta.GetCell(X, Y).IsExplored) {
                return;
            }
            if (kartta.IsInFov(X, Y)) {
                konsoli.Set(X, Y, Vari, RLColor.Black, Merkki);
            }
            else {
                konsoli.Set(X, Y, RLColor.Gray, RLColor.Black, '.');
            }
        }
    }
}
