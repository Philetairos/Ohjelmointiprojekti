using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka pelin hahmoille, joita vastaan pelaaja ei voi taistella, mutta joiden kanssa pelaaja voi keskustella
    /// TODO: kaupankäynti
    /// </summary>
    public class NPC : Hahmo {
        public DialogiNoodi[] hahmonDialogi;
        private int dialogiID;
        public readonly bool liikkuu;
        public NPC() {

        }
        public NPC(int x, int y, string nimi, char merkki, RLColor vari, DialogiNoodi[] dialogi, bool liikkuukko) {
            Nimi = nimi;
            Nakoetaisyys = 100;
            Vari = vari;
            Merkki = merkki;
            X = x;
            Y = y;
            hahmonDialogi = dialogi;
            dialogiID = 0;
            liikkuu = liikkuukko;
        }
        public void PiirraStatsit(RLConsole statsiKonsoli, int sijainti) {
            statsiKonsoli.Print(1, 13+(sijainti*2), Merkki.ToString(), Vari);
            statsiKonsoli.Print(2, 13 + (sijainti * 2), $": {Nimi}", RLColor.White);
        }
        public bool Dialogi(int syote) {
            foreach (int i in hahmonDialogi[dialogiID].linkit) {
                if(i == syote)
                {
                    dialogiID = i;
                    Ohjelma.ViestiLoki.Lisaa(hahmonDialogi[dialogiID].dialogi);
                    Ohjelma.ViestiLoki.Lisaa(hahmonDialogi[dialogiID].vastaukset);
                    return true;
                }
            }
            dialogiID = 0;
            return false;
        }
    }
}
