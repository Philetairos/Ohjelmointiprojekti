using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Hahmot, joita vastaan pelaaja ei voi taistella, mutta joiden kanssa pelaaja voi keskustella

namespace Ohjelmointiprojekti {
    public class NPC : Hahmo {
        public string[] dialogiTaulukko;
        private readonly int dialogiID;
        public NPC() {

        }
        public NPC(int x, int y, string nimi, char merkki, RLColor vari, string[] dialogi) {
            Nimi = nimi;
            Nakoetaisyys = 100;
            Vari = vari;
            Merkki = merkki;
            X = x;
            Y = y;
            dialogiTaulukko = dialogi;
            dialogiID = 0;
        }
        public void PiirraStatsit(RLConsole statsiKonsoli, int sijainti) {
            statsiKonsoli.Print(1, 13+(sijainti*2), Merkki.ToString(), Vari);
            statsiKonsoli.Print(2, 13 + (sijainti * 2), $": {Nimi}", RLColor.White);
        }
        public bool Dialogi(MessageLog viestiloki, int syote) {
            string[] viestit = dialogiTaulukko[dialogiID].Split('|');
            foreach (string viesti in viestit)
            {
                viestiloki.Lisaa(viesti);
            }
            return false;
        }
    }
}
