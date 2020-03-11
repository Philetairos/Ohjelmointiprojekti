using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Dialogin osio, hahmon dialogi koostuu näistä

namespace Ohjelmointiprojekti
{
    public class DialogueNode {
        public readonly string dialogi;
        public readonly string vastaukset;
        public readonly int[] linkit;

        public DialogueNode(string d, string v, int[] l) {
            dialogi = d;
            vastaukset = v;
            linkit = l;
        }
    }
}
