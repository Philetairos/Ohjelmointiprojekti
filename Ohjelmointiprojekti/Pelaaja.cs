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
        public Varuste[] Varusteet { get; set; }
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
            Varusteet = new Varuste[5];
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
        public bool PoistaEsine(int num) {
            Inventaario[num].X = X;
            Inventaario[num].Y = Y;
            Ohjelma.peliKartta.Esineet.Add(Inventaario[num]);
            Ohjelma.ViestiLoki.Lisaa("Dropped " + Inventaario[num].Nimi);
            Inventaario.RemoveAt(num);
            return false;
        }
        public bool PoistaVaruste(int lokero)
        {
            if (Varusteet[lokero - 1] != null) {
                if (Inventaario.Count < Inventaario.Capacity) {
                    Inventaario.Add(Varusteet[lokero - 1]);
                    Varusteet[lokero - 1].PoistaVaruste();
                    Varusteet[lokero - 1] = null;
                }
                else {
                    Ohjelma.ViestiLoki.Lisaa("Inventory full!");
                }
            }
            else {
                Ohjelma.ViestiLoki.Lisaa("No item in slot!");
            }
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
            for (; i < Varusteet.Length; i++) {
                if (!(Varusteet[i] is null)) {
                    inventaarioKonsoli.Print(3, 4 + i, Varusteet[i].LokeroNimi, RLColor.White);
                    inventaarioKonsoli.Print(8, 5 + i, Varusteet[i].Merkki.ToString(), Varusteet[i].Vari);
                    inventaarioKonsoli.Print(10, 5 + i, $"{Varusteet[i].Nimi}  {Varusteet[i].Maara}", RLColor.White);
                }
            }
        }
        public override void KasitteleKuolema(PeliKartta kartta) {
            Ohjelma.ViestiLoki.Lisaa("You have died!");
            kartta = Ohjelma.karttaGeneroija.TyhjaKartta();
        }
    }
}
