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
    public class Pelaaja : Hahmo {
        public List<Esine> Inventaario {
            get;
            set;
        }
        public Esine Paahine {
            get;
            set;
        }
        public Pelaaja(int x, int y) {
            Nimi = "Player";
            Nakoetaisyys = 100;
            Vari = RLColor.White;
            Merkki = '@';
            X = x;
            Y = y;
            Inventaario = new List<Esine> {
                Capacity = 4
            };
        }
        public void PiirraStatsit(RLConsole statsiKonsoli) {
            statsiKonsoli.Print(1, 1, $"Name:    {Nimi}", RLColor.White);
        }
        public bool LisaaEsine(Esine esine) {
            if (Inventaario.Count < Inventaario.Capacity) {
                Inventaario.Add(esine);
                Ohjelma.peliKartta.Esineet.Remove(esine);
                return true;
            }
            Ohjelma.ViestiLoki.Lisaa("Inventory is full!");
            return false;
        }
        public void PiirraInventaario(RLConsole inventaarioKonsoli) {
            inventaarioKonsoli.Clear();
            inventaarioKonsoli.Print(1,1, "Inventory:", RLColor.White);
            int i;
            for (i = 0; i < Inventaario.Count; i++)  {
                if (!(Inventaario[i] is null)) {
                    inventaarioKonsoli.Print(1, 2+i, Inventaario[i].Merkki.ToString(), Inventaario[i].Vari);
                    inventaarioKonsoli.Print(3, 2+i, $"{Inventaario[i].Nimi}  {Inventaario[i].Maara}", RLColor.White);
                }
            }
            inventaarioKonsoli.Print(1, 3+i, "Equipment:", RLColor.White);
            inventaarioKonsoli.Print(3, 4+i, "Head:", RLColor.White);
            if (!(Paahine is null)) {
                inventaarioKonsoli.Print(8, 4+i, Paahine.Merkki.ToString(), Paahine.Vari);
                inventaarioKonsoli.Print(9, 4 + i, Paahine.Nimi, RLColor.White);
            }
        }
    }
}
