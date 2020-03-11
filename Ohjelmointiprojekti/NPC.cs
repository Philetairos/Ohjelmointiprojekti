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
        public DialogueNode[] hahmonDialogi;
        private int dialogiID;
        public NPC() {

        }
        public NPC(int x, int y, string nimi, char merkki, RLColor vari, DialogueNode[] dialogi) {
            Nimi = nimi;
            Nakoetaisyys = 100;
            Vari = vari;
            Merkki = merkki;
            X = x;
            Y = y;
            hahmonDialogi = dialogi;
            dialogiID = 0;
        }
        public void PiirraStatsit(RLConsole statsiKonsoli, int sijainti) {
            statsiKonsoli.Print(1, 13+(sijainti*2), Merkki.ToString(), Vari);
            statsiKonsoli.Print(2, 13 + (sijainti * 2), $": {Nimi}", RLColor.White);
        }
        public bool Dialogi(MessageLog viestiloki, int syote) {
            foreach (int i in hahmonDialogi[dialogiID].linkit) {
                if(i == syote)
                {
                    dialogiID = i;
                    viestiloki.Lisaa(hahmonDialogi[dialogiID].dialogi);
                    viestiloki.Lisaa(hahmonDialogi[dialogiID].vastaukset);
                    return true;
                }
            }
            dialogiID = 0;
            return false;
        }
    }
}
