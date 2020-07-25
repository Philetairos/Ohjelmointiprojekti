using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka pelaajan hahmolle, jota hän hallitsee
    /// </summary>
    public class Pelaaja : Hahmo {
        public Varuste[] Varusteet { get; set; }
        public int Nalka { get; set; }
        public int Alykkyys { get; set; }
        public int ViisausKarma { get; set; }
        public int KontrolliKarma { get; set; }
        public int SopuisuusKarma { get; set; }
        public int Taso { get; set; }

        /// <summary>
        /// Konstruktori pelaajalle
        /// </summary>
        /// <param name="x">Sijanti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
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
            ViisausKarma = 0;
            KontrolliKarma = 0;
            SopuisuusKarma = 0;
            Taso = 1;
            Inventaario = new List<Esine> {
                Capacity = 4
            };
            Varusteet = new Varuste[5];
        }

        /// <summary>
        /// Piirtää pelaajan tilan konsoliin
        /// </summary>
        /// <param name="statsiKonsoli">Mihin konsoliin piirretään</param>
        public void PiirraStatsit(RLConsole statsiKonsoli) {
            statsiKonsoli.Clear();
            statsiKonsoli.Print(1, 1, $"Name:         {Nimi}", RLColor.White);
            statsiKonsoli.Print(1, 2, $"Health:       {Elama}", RLColor.White);
            statsiKonsoli.Print(1, 3, $"Hunger:       {Nalka}", RLColor.White);
            statsiKonsoli.Print(1, 4, $"Level:        {Taso}", RLColor.White);
            statsiKonsoli.Print(1, 5, $"Defence:      {Puolustus}", RLColor.White);
            statsiKonsoli.Print(1, 6, $"Strength:     {Voimakkuus}", RLColor.White);
            statsiKonsoli.Print(1, 7, $"Dexterity:    {Napparyys}", RLColor.White);
            statsiKonsoli.Print(1, 8, $"Intelligence: {Alykkyys}", RLColor.White);
        }

        /// <summary>
        /// Lisää esine pelaajan inventaarioon
        /// </summary>
        /// <param name="esine">Esine joka lisätään</param>
        /// <returns>Palauttaa aina false</returns>
        public bool LisaaEsine(Esine esine) {
            if (Inventaario.Count < Inventaario.Capacity) {
                foreach (Esine inv in Inventaario) {
                    if (inv.Nimi == esine.Nimi) {
                        inv.Maara++;
                        Ohjelma.peliKartta.Esineet.Remove(esine);
                        return true;
                    }
                }
                Inventaario.Add(esine);
                Ohjelma.peliKartta.Esineet.Remove(esine);
                return true;
            }
            Ohjelma.ViestiLoki.Lisaa("Inventory is full!");
            return false;
        }

        /// <summary>
        /// Poista tietty esine inventaariosta 
        /// </summary>
        /// <param name="num">Esineen sijainti taulukossa</param>
        /// <returns>Palauttaa aina false</returns>
        public bool PoistaEsine(int num) {
            Inventaario[num].X = X;
            Inventaario[num].Y = Y;
            Ohjelma.peliKartta.Esineet.Add(Inventaario[num]);
            Ohjelma.ViestiLoki.Lisaa("Dropped " + Inventaario[num].Nimi);
            Inventaario.RemoveAt(num);
            return false;
        }

        /// <summary>
        /// Poista tietty esine pelaajan varusteista
        /// </summary>
        /// <param name="lokero">Varusteen sijainti</param>
        /// <returns>Palauttaa aina false</returns>
        public bool PoistaVaruste(int lokero) {
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

        /// <summary>
        /// Piirrä pelaajan inventaarion sisältö konsoliin
        /// </summary>
        /// <param name="inventaarioKonsoli">Mihin konsoliin piiretään</param>
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
            for (int j = 0; j < Varusteet.Length; j++, i += 2) {
                if (!(Varusteet[j] is null)) {
                    inventaarioKonsoli.Print(3, 4 + i, Varusteet[j].LokeroNimi, RLColor.White);
                    inventaarioKonsoli.Print(8, 5 + i, Varusteet[j].Merkki.ToString(), Varusteet[j].Vari);
                    inventaarioKonsoli.Print(10, 5 + i, $"{Varusteet[j].Nimi}  {Varusteet[j].Maara}", RLColor.White);
                }
            }
        }

        /// <summary>
        /// Metodi pelaajan kuolemiselle (kun elämämittari on 0), lataa tyhjän kartan
        /// </summary>
        /// <param name="kartta">Kartta joka korvataan</param>
        public override void KasitteleKuolema(PeliKartta kartta) {
            Ohjelma.ViestiLoki.Lisaa("You have died!");
            kartta = Ohjelma.karttaGeneroija.TyhjaKartta();

        }

        /// <summary>
        /// Metodi joka lisää pelaajan tasoa, kutsutaan kun kokemusta on tarpeeksi
        /// </summary>
        public void LisaaTaso() {
            if (Taso < 3) {
                Taso += 1;
                Ohjelma.ViestiLoki.Lisaa("Level up!");
                Random rand = new Random();
                int result = rand.Next(1,3);
                switch (result) {
                    case 1:
                        Voimakkuus++;
                        Ohjelma.ViestiLoki.Lisaa("Your Strength has improved!");
                        break;
                    case 2:
                        Napparyys++;
                        Ohjelma.ViestiLoki.Lisaa("Your Dexterity has improved!");
                        break;
                    case 3:
                        Alykkyys++;
                        Ohjelma.ViestiLoki.Lisaa("Your Intelligence has improved!");
                        break;
                }
            }
        }

        /// <summary>
        /// Metodi joka lisää pelaajan älykkyyttä
        /// </summary>
        public void LisaaAlykkyys() {
            Alykkyys++;
        }

        /// <summary>
        /// Metodi joka lisää pelaajan voimakkuutta
        /// </summary>
        public void LisaaVoimakkuus() {
            Voimakkuus++;
        }

        /// <summary>
        /// Metodi joka lisää pelaajan näppäryyttä
        /// </summary>
        public void LisaaNapparyys() {
            Napparyys++;
        }

        /// <summary>
        /// Metodi joka lisää pelaajan viisauskarmaa
        /// </summary>
        public void LisaaViisausKarma() {
            ViisausKarma++;
        }

        /// <summary>
        /// Metodi joka lisää pelaajan kontrollikarmaa
        /// </summary>
        public void LisaaKontrolliKarma() {
            KontrolliKarma++;
        }

        /// <summary>
        /// Metodi joka lisää pelaajan sopuisuuskarmaa
        /// </summary>
        public void LisaaSopuisuusKarma() {
            SopuisuusKarma++;
        }

        /// <summary>
        /// Metodi joka vähentää pelaajan viisauskarmaa
        /// </summary>
        public void VahennaViisausKarma() {
            ViisausKarma--;
        }

        /// <summary>
        /// Metodi joka vähentää pelaajan kontrollikarmaa
        /// </summary>
        public void VahennaKontrolliKarma() {
            KontrolliKarma++;
        }

        /// <summary>
        /// Metodi joka lisää pelaajan näppäryyttä
        /// </summary>
        public void VahennaSopuisuusKarma() {
            SopuisuusKarma++;
        }
    }
}
