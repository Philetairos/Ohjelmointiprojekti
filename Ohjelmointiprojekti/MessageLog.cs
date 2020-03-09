using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Luokka viestien lisäämiselle ja piirtämiselle dialogikonsoliin

namespace Ohjelmointiprojekti {
    public class MessageLog {
        private readonly Queue<string> rivit;
        public MessageLog()
        {
            rivit = new Queue<string>();
        }
        public void Lisaa(string viesti)
        {
            rivit.Enqueue(viesti);
            if (rivit.Count > 10)
            {
                rivit.Dequeue();
            }
        }
        public void Piirra(RLConsole konsoli)
        {
            konsoli.Clear();
            string[] rivitaulukko = rivit.ToArray();
            for (int i = 0; i < rivitaulukko.Length; i++)
            {
                konsoli.Print(1, i + 1, rivitaulukko[i], RLColor.White);
            }
        }
    }
}
