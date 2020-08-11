using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Dialogin osio, hahmon dialogi koostuu näistä
    /// Tekijä: Daniel Juola
    /// Luotu: 11.3.20.
    /// </summary>
    public class DialogiNoodi {
        public readonly string dialogi;
        public readonly string vastaukset;
        public readonly (int, Action)[] linkit;

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="d">Dialogin puhe tekstimuodossa</param>
        /// <param name="v">Vastausvaihtoehdot listattuna tekstimuodossa</param>
        /// <param name="l">Vastausvaihtoehdot linkkeinä muihin vastauksiin, voivat suorittaa koodia Actionin kautta</param>
        public DialogiNoodi(string d, string v, (int, Action)[] l) {
            dialogi = d;
            vastaukset = v;
            linkit = l;
        }
    }
}
