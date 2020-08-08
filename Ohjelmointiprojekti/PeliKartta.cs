using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Tämä on luokka pelin karttojen luomiseen, joilla pelaaja liikkuu
    /// </summary>
    public class PeliKartta : Map {
        public List<NPC> NPCs { get; set; }
        public List<Vastustaja> Vastustajat { get; set; }
        public List<Ovi> Ovet { get; set; }
        public List<Esine> Esineet { get; set; }
        public List<Solu> Solut { get; set; }
        public Pyhakko Pyhakko { get; set; }
        public int id;

        /// <summary>
        /// Konstruktori
        /// </summary>
        public PeliKartta() {
            NPCs = new List<NPC>();
            Vastustajat = new List<Vastustaja>();
            Ovet = new List<Ovi>();
            Esineet = new List<Esine>();
            Solut = new List<Solu>();
        }

        /// <summary>
        /// Metodi NPC-hahmon lisäämiseen kartalle
        /// </summary>
        /// <param name="npc">NPC-hahmo joka lisätään</param>
        public void LisaaNPC(NPC npc) {
            NPCs.Add(npc);
            AsetaWalkable(npc.X, npc.Y, false);
        }

        /// <summary>
        /// Metodi vastustajahahmon lisäämiseen kartalle
        /// </summary>
        /// <param name="npc">Vastustajahahmo joka lisätään</param>
        public void LisaaVastustaja(Vastustaja vastustaja) {
            Vastustajat.Add(vastustaja);
            AsetaWalkable(vastustaja.X, vastustaja.Y, false);
        }

        /// <summary>
        /// Tarkista, sijaitseeko tiilellä NPC-hahmo
        /// </summary>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <returns>NPC-hahmo joka sijaitsee tiilellä, jos ei ole niin palauttaa null</returns>
        public NPC NPCSijainti(int x, int y) {
            return NPCs.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        /// <summary>
        /// Tarkista, sijaitseeko tiilellä vastustajahahmo
        /// </summary>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <returns>Vastustajahahmo joka sijaitsee tiilellä, jos ei ole niin palauttaa null</returns>
        public Vastustaja VastustajaSijainti(int x, int y) {
            return Vastustajat.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        /// <summary>
        /// Tarkista, sijaitseeko tiilellä esine
        /// </summary>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <returns>Esine joka sijaitsee tiilellä, jos ei ole niin palauttaa null</returns>
        public Esine EsineSijainti(int x, int y) {
            return Esineet.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        /// <summary>
        /// Tarkista, sijaitseeko tiilellä ovi
        /// </summary>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <returns>Ovi joka sijaitsee tiilellä, jos ei ole niin palauttaa null</returns>
        public Ovi PalautaOvi(int x, int y) {
            return Ovet.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        /// <summary>
        /// Päivitä konsolit ja piirrä kartta sekä siinä olevat hahmot
        /// </summary>
        /// <param name="karttaKonsoli">Konsoli johon piirretään kartan tiilet</param>
        /// <param name="statsiKonsoli">Konsoli johon piirretään kartalla olevat ja pelaajan näkemät hahmot</param>
        public void PiirraKartta(RLConsole karttaKonsoli, RLConsole statsiKonsoli) {
            karttaKonsoli.Clear();
            foreach (Cell solu in GetAllCells()) {
                AsetaSymboli(karttaKonsoli, solu);
            }
            foreach (Solu solu2 in Solut) {
                solu2.Piirra(karttaKonsoli, this);
            }
            foreach (Esine esine in Esineet) {
                esine.Piirra(karttaKonsoli, this);
            }
            foreach (Ovi ovi in Ovet) {
                ovi.Piirra(karttaKonsoli, this);
            }
            if (Pyhakko != null){
                Pyhakko.Piirra(karttaKonsoli, this);
            }
            int i = 0;
            foreach (NPC npc in NPCs) {
                npc.Piirra(karttaKonsoli, this);
                if (IsInFov(npc.X, npc.Y)) {
                    npc.PiirraStatsit(statsiKonsoli, i);
                    i++;
                }
            }
            foreach (Vastustaja vastustaja in Vastustajat) {
                vastustaja.Piirra(karttaKonsoli, this);
                if (IsInFov(vastustaja.X, vastustaja.Y)) {
                    vastustaja.PiirraStatsit(statsiKonsoli, i);
                    i++;
                }
            }
        }

        /// <summary>
        /// Aseta oletussymboli kyseiselle tiilelle
        /// </summary>
        /// <param name="karttaKonsoli">Konsoli johon piirretään</param>
        /// <param name="solu">Tiili joka piirretään</param>
        private void AsetaSymboli(RLConsole karttaKonsoli, Cell solu) {
            if (!solu.IsExplored) {
                return;
            }
            if (IsInFov(solu.X, solu.Y)) {
                if (solu.IsWalkable) {
                    karttaKonsoli.Set(solu.X, solu.Y,RLColor.Green, RLColor.Black, '\'');
                }
                else {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.LightGray, RLColor.Black, '#');
                }
            }
            else {
                if (solu.IsWalkable) {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.Gray, RLColor.Black, '\'');
                }
                else {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.Gray, RLColor.Black, '#');
                }
            }
        }

        /// <summary>
        /// Päivitä, mitä tiilejä pelaaja näkee
        /// </summary>
        public void PaivitaNakoKentta() {
            Pelaaja pelaaja = Ohjelma.Pelaaja;
            ComputeFov(pelaaja.X, pelaaja.Y, pelaaja.Nakoetaisyys, true);
            foreach (Cell solu in GetCellsInDiamond(pelaaja.X, pelaaja.Y, pelaaja.Nakoetaisyys)) {
                if (IsInFov(solu.X, solu.Y)) {
                    SetCellProperties(solu.X, solu.Y, solu.IsTransparent, solu.IsWalkable, true);
                }
            }
        }

        /// <summary>
        /// Aseta hahmon sijainti, jos sijantiin voi liikkua (isWalkable)
        /// </summary>
        /// <param name="hahmo">Hahmo jota siirretään</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <returns>Onnistuiko siirtäminen? True jos kyllä, false jos ei</returns>
        public bool AsetaSijainti(Hahmo hahmo, int x, int y) {
            if (x >= Width || y >= Height || x < 0 || y < 0) {
                if (hahmo is Pelaaja) {
                    Ohjelma.karttaGeneroija.VaihdaKarttaa();
                }
                return false;
            }
            if (id == 4) {
                Ohjelma.karttaGeneroija.SiirrySaarella(x, y);
            }
            else if (Pyhakko != null && hahmo is Pelaaja) {
                KaytaPyhakko(x, y);
            }
            if (GetCell(x, y).IsWalkable) {
                AsetaWalkable(hahmo.X, hahmo.Y, true);
                hahmo.X = x;
                hahmo.Y = y;
                AsetaWalkable(hahmo.X, hahmo.Y, false);
                if (hahmo is Pelaaja) {
                    PaivitaNakoKentta();
                }
                return true;
            }
            AvaaOvi(hahmo, x, y);
            return false;
        }

        /// <summary>
        /// Aseta tiilen isWalkable-muuttuja
        /// </summary>
        /// <param name="x">Tiilen sijainti x-akselilla</param>
        /// <param name="y">Tiilen sijainti y-akselilla</param>
        /// <param name="isWalkable">Arvo joka halutaan asettaa</param>
        public void AsetaWalkable(int x, int y, bool isWalkable) {
            ICell solu = GetCell(x, y);
            SetCellProperties(solu.X, solu.Y, solu.IsTransparent, isWalkable, solu.IsExplored);
        }

        /// <summary>
        /// Avaa ovi jos sellainen on hahmon tiellä
        /// </summary>
        /// <param name="hahmo">Hahmo joka avaa ovea</param>
        /// <param name="x">Avattavan oven sijainti kartan x-akselilla</param>
        /// <param name="y">Avattavan oven sijainti kartan y-akselilla</param>
        private void AvaaOvi (Hahmo hahmo, int x, int y) {
            Ovi door = PalautaOvi(x, y);
            if (door != null && !door.Auki) {
                door.Auki = true;
                var solu = GetCell(x, y);
                SetCellProperties(x, y, true, true, solu.IsExplored);
                Ohjelma.ViestiLoki.Lisaa($"{hahmo.Nimi} opened a door.");
                PaivitaNakoKentta();
            }
        }

        /// <summary>
        /// Käytä pyhäkköä jos sellainen on pelaajan tiellä
        /// </summary>
        /// <param name="x">Avattavan oven sijainti kartan x-akselilla</param>
        /// <param name="y">Avattavan oven sijainti kartan y-akselilla</param>
        private void KaytaPyhakko(int x, int y) {
            if (Pyhakko.X == x && Pyhakko.Y == y){
                switch (id) {
                    case 5:
                        if (Ohjelma.Pelaaja.ViisausKarma > 10) {
                            Ohjelma.ViestiLoki.Lisaa("You mediate on the shrine and learn the secrets of Wisdom.");
                            Ohjelma.ViestiLoki.Lisaa("Level up!");
                            Ohjelma.Pelaaja.LisaaTaso();
                            Ohjelma.Pelaaja.LisaaAlykkyys();
                            Ohjelma.Pelaaja.ViisausKarma = -1;
                        }
                        else if (Ohjelma.Pelaaja.ViisausKarma == -1) {
                            Ohjelma.ViestiLoki.Lisaa("You have already mastered the virtue of Wisdom.");
                        }
                        else {
                            Ohjelma.Pelaaja.LisaaViisausKarma();
                            Ohjelma.ViestiLoki.Lisaa("You meditate on the shrine for a long time and feel wiser.");
                            Ohjelma.ViestiLoki.Lisaa("You also feel very hungry...");
                            if (Ohjelma.Pelaaja.Nalka > 0) {
                                Ohjelma.Pelaaja.Nalka = 0;
                            }
                            else {
                                Ohjelma.Pelaaja.Elama -= 10;
                                if (Ohjelma.Pelaaja.Elama <= 0) {
                                    Ohjelma.Pelaaja.KasitteleKuolema();
                                }
                            }
                        }
                        break;
                    case 6:
                        if (Ohjelma.Pelaaja.KontrolliKarma > 10) {
                            Ohjelma.ViestiLoki.Lisaa("You mediate on the shrine and learn the secrets of Control.");
                            Ohjelma.ViestiLoki.Lisaa("Level up!");
                            Ohjelma.Pelaaja.LisaaTaso();
                            Ohjelma.Pelaaja.LisaaNapparyys();
                            Ohjelma.Pelaaja.KontrolliKarma = -1;
                        }
                        else if (Ohjelma.Pelaaja.KontrolliKarma == -1) {
                            Ohjelma.ViestiLoki.Lisaa("You have already mastered the virtue of Control.");
                        }
                        else {
                            Ohjelma.Pelaaja.LisaaKontrolliKarma();
                            Ohjelma.ViestiLoki.Lisaa("You meditate on the shrine for a long time and control your feelings better.");
                            Ohjelma.ViestiLoki.Lisaa("You also feel very hungry...");
                            if (Ohjelma.Pelaaja.Nalka > 0) {
                                Ohjelma.Pelaaja.Nalka = 0;
                            }
                            else {
                                Ohjelma.Pelaaja.Elama -= 10;
                                if (Ohjelma.Pelaaja.Elama <= 0) {
                                    Ohjelma.Pelaaja.KasitteleKuolema();
                                }
                            }
                        }
                        break;
                    case 7:
                        if (Ohjelma.Pelaaja.SopuisuusKarma > 10) {
                            Ohjelma.ViestiLoki.Lisaa("You mediate on the shrine and learn the secrets of Amity.");
                            Ohjelma.ViestiLoki.Lisaa("Level up!");
                            Ohjelma.Pelaaja.LisaaTaso();
                            Ohjelma.Pelaaja.LisaaVoimakkuus();
                            Ohjelma.Pelaaja.SopuisuusKarma = -1;
                        }
                        else if (Ohjelma.Pelaaja.SopuisuusKarma == -1) {
                            Ohjelma.ViestiLoki.Lisaa("You have already mastered the virtue of Amity.");
                        }
                        else {
                            Ohjelma.Pelaaja.LisaaSopuisuusKarma();
                            Ohjelma.ViestiLoki.Lisaa("You meditate on the shrine for a long time and feel unity with humankind.");
                            Ohjelma.ViestiLoki.Lisaa("You also feel very hungry...");
                            if (Ohjelma.Pelaaja.Nalka > 0) {
                                Ohjelma.Pelaaja.Nalka = 0;
                            }
                            else {
                                Ohjelma.Pelaaja.Elama -= 10;
                                if (Ohjelma.Pelaaja.Elama <= 0) {
                                    Ohjelma.Pelaaja.KasitteleKuolema();
                                }
                            }
                        }
                        break;

                }
            }
        }
    }
}
