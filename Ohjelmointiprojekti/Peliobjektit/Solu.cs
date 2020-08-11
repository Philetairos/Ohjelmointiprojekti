using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Cell-luokan laajennos joka sisältää värin ja merkin. Käytetään kartan osina
    /// Tekijä: Daniel Juola
    /// Luotu: 21.6.20
    /// </summary>
    public class Solu : Cell, IPiirra {
        public RLColor Vari { get; set; }
        public RLColor Taustavari { get; set; }
        public char Merkki { get; set; }

        /// <summary>
        /// Tyhjä konstruktori
        /// </summary>
        public Solu() {

        }

        /// <summary>
        /// Solun (kartan tiilen) konstruktori
        /// </summary>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <param name="isTransparent">Onko tiili läpinävykä</param>
        /// <param name="isWalkable">Voiko tiilen päällä kävellä</param>
        /// <param name="isInFov">Näkeekö pelaaja tiilen</param>
        /// <param name="vari">Tiilen symbolin väri</param>
        /// <param name="taustavari">Tiilen taustaväri</param>
        /// <param name="merkki">Tiilen symboli</param>
        public Solu(int x, int y, bool isTransparent, bool isWalkable, bool isInFov, RLColor vari, RLColor taustavari, char merkki) : base(x,y,isTransparent,isWalkable,isInFov) {
            Vari = vari;
            Merkki = merkki;
            Taustavari = taustavari;
        }

        /// <summary>
        /// Luokka tiilen piirtämiseen
        /// </summary>
        /// <param name="konsoli">Konsoli johon piirretään</param>
        /// <param name="kartta">Kartta josta haetaan tietoja</param>
        public void Piirra(RLConsole konsoli, IMap kartta) {
            if (!kartta.GetCell(X, Y).IsExplored) {
                return;
            }
            if (kartta.IsInFov(X, Y)) {
                konsoli.Set(X, Y, Vari, Taustavari, Merkki);
            }
            else {
                konsoli.Set(X, Y, RLColor.Gray, Taustavari, '.');
            }
        }
    }
}
