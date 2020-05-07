using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka pelin esineitä varten. Pelaaja voi kerätä ja käyttää esineitä.
    /// </summary>
    public class Esine : IEsine, IPiirra {
        public string Nimi { get; set; }
        public char Merkki { get; set; }
        public RLColor Vari { get; set; }
        public int Maara { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public virtual bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("Mitään ei tapahdu.");
            return false;
        }
        public bool OtaEsine(Pelaaja pelaaja) {
            if (pelaaja.LisaaEsine(this)) {
                Ohjelma.ViestiLoki.Lisaa($"{Nimi} ({Maara}) lisatty inventaarioon.");
            }
            return true;
        }
        public void Piirra(RLConsole konsoli, IMap kartta) {
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
