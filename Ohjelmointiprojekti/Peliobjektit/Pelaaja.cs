using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Taso = 0;
            Inventaario = new List<Esine> {
                Capacity = 5
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
                        inv.Maara += esine.Maara;
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
        public override void KasitteleKuolema() {
            Ohjelma.ViestiLoki.Lisaa("You have died!");
            Ohjelma.peliKartta = Ohjelma.karttaGeneroija.TyhjaKartta();
            Ohjelma.ViestiLoki.Lisaa("A dark cold void surrounds you. It's over, you feel.");
            Ohjelma.ViestiLoki.Lisaa("...Or is it?");
            Ohjelma.ViestiLoki.Lisaa("(Press Enter to respawn)");
        }

        /// <summary>
        /// Vähentää tietyn määrän kolikoita pelaajalta, jos niitä on tarpeeksi
        /// </summary>
        /// <param name="maara">Kuinka paljon pitää maksaa</param>
        /// <returns>Onnistuiko maksaminen, true jos kyllä, false jos ei</returns>
        public bool MaksaRahat(int maara) {
            foreach (var kolikko in Inventaario.OfType<Kolikko>()) {
                if (kolikko is Kolikko) {
                    if (kolikko.Maara < maara) {
                        Ohjelma.ViestiLoki.Lisaa("You don't have enough money!");
                        return false;
                    }
                    else if (kolikko.Maara == 1) {
                        Inventaario.Remove(kolikko);
                        return true;
                    }
                    else {
                        kolikko.Maara--;
                        return true;
                    }
                }
            }
            Ohjelma.ViestiLoki.Lisaa("You don't have enough money!");
            return false;
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
        /// Metodi joka lisää pelaajan viisauskarmaa. Jos karma on -1, pelaaja on jo meditoinut eikä tarvitse enää karmaa
        /// </summary>
        public void LisaaViisausKarma() {
            if (ViisausKarma >= 0) {
                ViisausKarma++;
            }
        }

        /// <summary>
        /// Metodi joka lisää pelaajan kontrollikarmaa. Jos karma on -1, pelaaja on jo meditoinut eikä tarvitse enää karmaa
        /// </summary>
        public void LisaaKontrolliKarma() {
            if (KontrolliKarma >= 0) {
                KontrolliKarma++;
            }
        }

        /// <summary>
        /// Metodi joka lisää pelaajan sopuisuuskarmaa. Jos karma on -1, pelaaja on jo meditoinut eikä tarvitse enää karmaa
        /// </summary>
        public void LisaaSopuisuusKarma() {
            if (SopuisuusKarma >= 0) {
                SopuisuusKarma++;
            }
        }

        /// <summary>
        /// Metodi joka vähentää pelaajan viisauskarmaa. Jos pelaaja on jo saavuttanut tason meditoimalla, se vähennetään
        /// </summary>
        public void VahennaViisausKarma() {
            if (ViisausKarma > 0) {
                ViisausKarma--;
            }
            else if (ViisausKarma < 0) {
                Ohjelma.ViestiLoki.Lisaa("You have lost your Wisdom!");
                ViisausKarma = 0;
                Taso--;
                Alykkyys--;
            }
        }

        /// <summary>
        /// Metodi joka vähentää pelaajan kontrollikarmaa. Jos pelaaja on jo saavuttanut tason meditoimalla, se vähennetään
        /// </summary>
        public void VahennaKontrolliKarma() {
            if (KontrolliKarma > 0) {
                KontrolliKarma--;
            }
            else if (KontrolliKarma < 0) {
                Ohjelma.ViestiLoki.Lisaa("You have lost your Control!");
                KontrolliKarma = 0;
                Taso--;
                Napparyys--;
            }
        }

        /// <summary>
        /// Metodi joka vähentää pelaajan sopuisuuskarmaa. Jos pelaaja on jo saavuttanut tason meditoimalla, se vähennetään
        /// </summary>
        public void VahennaSopuisuusKarma() {
            if (SopuisuusKarma > 0) {
                SopuisuusKarma--;
            }
            else if (SopuisuusKarma < 0) {
                Ohjelma.ViestiLoki.Lisaa("You have lost your Amity!");
                SopuisuusKarma = 0;
                Taso--;
                Voimakkuus--;
            }
        }
        /// <summary>
        /// Metodi veren luovuttamista varten
        /// </summary>
        public void LuovutaVerta() {
            if (Elama > 10) {
                Elama -= 5;
            }
            else {
                Ohjelma.ViestiLoki.Lisaa("You don't have enough blood to donate!");
                VahennaSopuisuusKarma();
            }
        }
        /// <summary>
        /// Metodi tason lisäämistä varten
        /// </summary>
        public void LisaaTaso() {
            Taso++;
            if (Taso >= 3) {
                Ohjelma.peliKartta = Ohjelma.karttaGeneroija.TyhjaKartta();
                Ohjelma.ViestiLoki.Lisaa("You have mastered all three virtues and become a Paragon of Virtue!");
                Ohjelma.ViestiLoki.Lisaa("Your quest is complete, but your journey may still continue if you wish.");
                Ohjelma.ViestiLoki.Lisaa("Press Enter to keep playing, press Escape to quit the game...");
            }
        }
    }
}
