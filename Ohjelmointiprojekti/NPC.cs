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
        public DialogiNoodi[] HahmonDialogi;
        private int DialogiID;
        public readonly bool Liikkuu;
        public NPC() {

        }
        public NPC(int x, int y, string nimi, char merkki, RLColor vari, DialogiNoodi[] dialogi, bool liikkuukko) {
            Nimi = nimi;
            Nakoetaisyys = 100;
            Vari = vari;
            Merkki = merkki;
            X = x;
            Y = y;
            HahmonDialogi = dialogi;
            DialogiID = 0;
            Liikkuu = liikkuukko;
        }
        public bool Dialogi(int syote) {
            foreach (int i in HahmonDialogi[DialogiID].linkit) {
                if(i == syote)
                {
                    DialogiID = i;
                    Ohjelma.ViestiLoki.Lisaa(HahmonDialogi[DialogiID].dialogi);
                    Ohjelma.ViestiLoki.Lisaa(HahmonDialogi[DialogiID].vastaukset);
                    return true;
                }
            }
            DialogiID = 0;
            return false;
        }
    }
}
