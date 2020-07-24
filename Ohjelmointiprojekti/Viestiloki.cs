using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// //Luokka viestien lisäämiselle ja piirtämiselle dialogikonsoliin
    /// </summary>
    public class Viestiloki {
        private readonly Queue<string> rivit;

        /// <summary>
        /// Konstruktori
        /// </summary>
        public Viestiloki() {
            rivit = new Queue<string>();
        }

        /// <summary>
        /// Lisää viesti lokiin
        /// </summary>
        /// <param name="viesti">Viesti jota lisätään</param>
        public void Lisaa(string viesti) {
            rivit.Enqueue(viesti);
            if (rivit.Count > 10) {
                rivit.Dequeue();
            }
        }

        /// <summary>
        /// Piirrä viestiloki konsoliin
        /// </summary>
        /// <param name="konsoli">Konsoli johon piirretään</param>
        public void Piirra(RLConsole konsoli) {
            konsoli.Clear();
            string[] rivitaulukko = rivit.ToArray();
            for (int i = 0; i < rivitaulukko.Length; i++) {
                konsoli.Print(1, i + 1, rivitaulukko[i], RLColor.White);
            }
        }
    }
}
