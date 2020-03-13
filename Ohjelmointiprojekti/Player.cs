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
    /// Luokka pelaajan hahmolle, jonka kautta pelaaja pelaa peliä
    /// </summary>
    public class Player : Hahmo {
        public Item[] Inventaario {
            get;
            set;
        }
        public Player(int x, int y) {
            Nimi = "Pelaaja";
            Nakoetaisyys = 100;
            Vari = RLColor.White;
            Merkki = '@';
            X = x;
            Y = y;
            Inventaario = new Item[4];
        }
        public void PiirraStatsit(RLConsole statsiKonsoli) {
            statsiKonsoli.Print(1, 1, $"Nimi:    {Nimi}", RLColor.White);
        }
        public bool LisaaEsine(Item esine) {
            for (int i = 0; i < Inventaario.Length; i++) {
                if (Inventaario[i] is null) {
                    Inventaario[i] = esine;
                    Program.peliKartta.Esineet.Remove(esine);
                    return true;
                }
            }
            Program.ViestiLoki.Lisaa("Inventaario on täynnä!");
            return false;
        }
        public void PiirraInventaario(RLConsole inventaarioKonsoli) {
            inventaarioKonsoli.Print(1,1, "Inventaario:", RLColor.White);
            for (int i = 0; i < Inventaario.Length; i++)  {
                if (!(Inventaario[i] is null)) {
                    inventaarioKonsoli.Print(1, 2+i, Inventaario[i].Merkki.ToString(), Inventaario[i].Vari);
                    inventaarioKonsoli.Print(3, 2+i, $"{Inventaario[i].Nimi}  {Inventaario[i].Maara}", RLColor.White);
                }
            }
        }
    }
}
