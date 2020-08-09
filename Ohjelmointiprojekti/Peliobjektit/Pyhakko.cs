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
    /// Luokka pyhäkölle, josta pelaaja saa tason
    /// </summary>
    public class Pyhakko : IPiirra {
        public bool Auki;
        public RLColor Vari { get; set; }
        public char Merkki { get; set; }
        public string Nimi { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Konstruktori
        /// </summary>
        public Pyhakko(string nimi) {
            Merkki = '§';
            Vari = RLColor.White;
            Nimi = nimi;
        }

        /// <summary>
        /// Piirtometodi pyhäkölle
        /// </summary>
        /// <param name="konsoli">Konsoli johon piirretään</param>
        /// <param name="kartta">Kartta jonka tietoja tarkastellaan</param>
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
