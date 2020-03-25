using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti
{
    /// <summary>
    /// Dialogin osio, hahmon dialogi koostuu näistä
    /// </summary>
    public class DialogiNoodi {
        public readonly string dialogi;
        public readonly string vastaukset;
        public readonly int[] linkit;

        public DialogiNoodi(string d, string v, int[] l) {
            dialogi = d;
            vastaukset = v;
            linkit = l;
        }
    }
}
