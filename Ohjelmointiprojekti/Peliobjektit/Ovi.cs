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
    /// TODO: lukitut ovet?
    /// Tekijä: Daniel Juola (Perustuu Faron Bracyn esimerkkikoodiin)
    /// Luotu: 8.6.20
    /// </summary>
    public class Ovi : IPiirra {
        public bool Auki;
        public RLColor Vari { get; set; }
        public char Merkki { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Konstruktori
        /// </summary>
        public Ovi() {
            Merkki = '|';
            Vari = RLColor.LightGray;
        }

        /// <summary>
        /// Uniikki piirtometodi ovelle, tarkistaa onko ovi auki vai kiinni
        /// </summary>
        /// <param name="konsoli">Konsoli johon piirretään</param>
        /// <param name="kartta">Kartta jonka tietoja tarkastellaan</param>
        public void Piirra(RLConsole konsoli, IMap kartta) {
            if (!kartta.GetCell(X, Y).IsExplored) {
                return;
            }
            Merkki = Auki ? '.' : '|';
            if (kartta.IsInFov(X, Y)) {
                konsoli.Set(X, Y, Vari, RLColor.Black, Merkki);
            }
            else {
                konsoli.Set(X, Y, RLColor.Gray, RLColor.Black, '.');
            }
        }
    }
}
