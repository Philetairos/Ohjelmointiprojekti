using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Hahmo-luokka, jota kaikki pelin hahmot käyttävät. Hyödyntää hahmo- ja piirrä-rajapintoja
    /// </summary>
    public class Hahmo : IHahmo, IPiirra {
        public string Nimi { get; set; }
        public int Nakoetaisyys { get; set; }
        public RLColor Vari { get; set; }
        public char Merkki { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Napparyys { get; set; }
        public int Voimakkuus { get; set; }
        public int Puolustus { get; set; }
        public int Elama { get; set; }
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
        public void PiirraStatsit(RLConsole statsiKonsoli, int sijainti) {
            statsiKonsoli.Print(1, 13 + (sijainti * 2), Merkki.ToString(), Vari);
            statsiKonsoli.Print(2, 13 + (sijainti * 2), $": {Nimi}", RLColor.White);
        }
        public virtual void KasitteleKuolema(PeliKartta kartta) {
            //erillinen toteutus luokan periville
        }
    }
}
