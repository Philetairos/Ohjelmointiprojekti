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
    public class Esine : IPiirra, IEsine {
        public string Nimi { get; set; }
        public char Merkki { get; set; }
        public RLColor Vari { get; set; }
        public int Maara { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Oletusmetodi sille mitä tapahtuu kun pelaaja käyttää esinettä
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public virtual bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("Nothing happens.");
            return false;
        }

        /// <summary>
        /// Metodi sille että pelaaja ottaa esineen kartalta
        /// </summary>
        /// <param name="pelaaja">Pelaaja joka ottaa esineen</param>
        /// <returns>Palauttaa aina true</returns>
        public bool OtaEsine(Pelaaja pelaaja) {
            if (pelaaja.LisaaEsine(this)) {
                Ohjelma.ViestiLoki.Lisaa($"{Nimi} ({Maara}) added to inventory.");
            }
            return true;
        }

        /// <summary>
        /// Piirtometodi objektille
        /// </summary>
        /// <param name="konsoli">Konsoli johon piirretään</param>
        /// <param name="kartta">Kartta josta haetaan tietoja</param>
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
