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
    /// Luokka pelaajan hahmolle, jota hän hallitsee
    /// </summary>
    public class Pelaaja : Hahmo {
        public List<Esine> Inventaario { get; set; }
        public Esine Paahine { get; set; }
        public int Nalka;
        public int Alykkyys;

        public Pelaaja(int x, int y) {
            Nimi = "Player";
            Nakoetaisyys = 25;
            Vari = RLColor.White;
            Merkki = '@';
            X = x;
            Y = y;
            Nalka = 100;
            Elama = 20;
            Voimakkuus = 1;
            Napparyys = 1;
            Alykkyys = 1;
            Puolustus = 1;
            Inventaario = new List<Esine> {
                Capacity = 4
            };
        }
        public void PiirraStatsit(RLConsole statsiKonsoli) {
            statsiKonsoli.Clear();
            statsiKonsoli.Print(1, 1, $"Name:         {Nimi}", RLColor.White);
            statsiKonsoli.Print(1, 2, $"Health:       {Elama}", RLColor.White);
            statsiKonsoli.Print(1, 3, $"Hunger:       {Nalka}", RLColor.White);
            statsiKonsoli.Print(1, 4, $"Defence:      {Puolustus}", RLColor.White);
            statsiKonsoli.Print(1, 5, $"Strength:     {Voimakkuus}", RLColor.White);
            statsiKonsoli.Print(1, 6, $"Intelligence: {Alykkyys}", RLColor.White);
            statsiKonsoli.Print(1, 7, $"Dexterity:    {Napparyys}", RLColor.White);
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
        public override void KasitteleKuolema(PeliKartta kartta) {
            Ohjelma.ViestiLoki.Lisaa("You have died!");
            kartta = Ohjelma.karttaGeneroija.TyhjaKartta();
        }
    }
}
