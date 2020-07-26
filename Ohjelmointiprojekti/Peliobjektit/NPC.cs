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
    /// </summary>
    public class NPC : Hahmo {
        public DialogiNoodi[] HahmonDialogi;
        private int DialogiID;
        public readonly bool Liikkuu;

        /// <summary>
        /// Tyhjä konstruktori
        /// </summary>
        public NPC() {

        }

        /// <summary>
        /// Konstruktori joka luo NPC-hahmon
        /// </summary>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <param name="nimi">NPC-hahmon nimi</param>
        /// <param name="merkki">NPC-hahmon symboli</param>
        /// <param name="vari">NPC-hahmon väri kartalla</param>
        /// <param name="dialogi">NPC-hahmon dialogi</param>
        /// <param name="liikkuukko">Liikkuuko NPC-hahmo satunnaisesti?</param>
        public NPC(int x, int y, string nimi, char merkki, RLColor vari, DialogiNoodi[] dialogi, bool liikkuukko) {
            Nimi = nimi;
            Nakoetaisyys = 15;
            Vari = vari;
            Merkki = merkki;
            X = x;
            Y = y;
            HahmonDialogi = dialogi;
            DialogiID = 0;
            Liikkuu = liikkuukko;
        }

        /// <summary>
        /// Pelaajan ja NPC-hahmon välisen dialogin käsittelijä
        /// </summary>
        /// <param name="syote">Valittu dialogivaihtoehto</param>
        /// <returns>True jos keskustelu jatkuu, false jos se loppuu</returns>
        public bool Dialogi(int syote) {
            if (syote <= HahmonDialogi[DialogiID].linkit.Length) {
                HahmonDialogi[DialogiID].linkit[syote - 1].Item2?.Invoke();
                DialogiID = HahmonDialogi[DialogiID].linkit[syote - 1].Item1;
                if (DialogiID != -1){
                    Ohjelma.ViestiLoki.Lisaa(HahmonDialogi[DialogiID].dialogi);
                    Ohjelma.ViestiLoki.Lisaa(HahmonDialogi[DialogiID].vastaukset);
                    return true;
                }
            }
            DialogiID = 0;
            Ohjelma.ViestiLoki.Lisaa("You end the conversation.");
            return false;
        }

        /// <summary>
        /// Yksinkertainen kaupankäyntimetodi, NPC-hahmo myy esineen inventaariosta
        /// </summary>
        public void MyyEsine() {
            if (Inventaario[0] != null) {
                if (Ohjelma.Pelaaja.MaksaRahat(1)) {
                    Ohjelma.Pelaaja.Inventaario.Add(Inventaario[0]);
                    if (Inventaario[0].Maara == 1) {
                        Inventaario.RemoveAt(0);
                    }
                    else {
                        Inventaario[0].Maara--;
                    }
                }
            }
        }
    }
}
