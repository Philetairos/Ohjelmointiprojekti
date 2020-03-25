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
        public readonly List<NPC> NPCs;
        public List<Ovi> Ovet;
        public List<Esine> Esineet;
        public PeliKartta() {
            NPCs = new List<NPC>();
            Ovet = new List<Ovi>();
            Esineet = new List<Esine>();
        }
        public void LisaaNPC(NPC npc) {
            NPCs.Add(npc);
            AsetaWalkable(npc.X, npc.Y, false);
        }
        public NPC NPCSijainti(int x, int y) {
            return NPCs.FirstOrDefault(m => m.X == x && m.Y == y);
        }
        public Esine EsineSijainti(int x, int y)
        {
            return Esineet.FirstOrDefault(m => m.X == x && m.Y == y);
        }
        public void PiirraKartta(RLConsole karttaKonsoli, RLConsole statsiKonsoli, RLConsole inventaarioKonsoli) {
            karttaKonsoli.Clear();
            foreach (Cell solu in GetAllCells()) {
                AsetaSymboli(karttaKonsoli, solu);
            }
            int i = 0;
            foreach (NPC npc in NPCs) {
                npc.Piirra(karttaKonsoli, this);
                if (IsInFov(npc.X, npc.Y)) {
                    npc.PiirraStatsit(statsiKonsoli, i);
                    i++;
                }
            }
            foreach (Esine esine in Esineet) {
                esine.Piirra(karttaKonsoli, this);
            }
            Ohjelma.Pelaaja.PiirraInventaario(inventaarioKonsoli);
        }
        private void AsetaSymboli(RLConsole karttaKonsoli, Cell solu) {
            if (!solu.IsExplored) {
                return;
            }
            if (IsInFov(solu.X, solu.Y)) {
                if (solu.IsWalkable)
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.LightGray, RLColor.Black, '.');
                }
                else
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.LightGray, RLColor.Black, '#');
                }
                
            }
            else {
                if (solu.IsWalkable)
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.Gray, RLColor.Black, '.');
                }
                else
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.Gray, RLColor.Black, '#');
                }
            }
            foreach (Ovi ovi in Ovet) {
                ovi.Piirra(karttaKonsoli, this);
            }
            foreach (Esine esine in Esineet)
            {
                esine.Piirra(karttaKonsoli, this);
            }
        }
        //Päivitä, mitä tiilejä pelaaja näkee
        public void PaivitaNakoKentta() {
            Pelaaja pelaaja = Ohjelma.Pelaaja;
            ComputeFov(pelaaja.X, pelaaja.Y, pelaaja.Nakoetaisyys, true);
            foreach (Cell solu in GetAllCells())
            {
                if (IsInFov(solu.X, solu.Y))
                {
                    SetCellProperties(solu.X, solu.Y, solu.IsTransparent, solu.IsWalkable, true);
                }
            }
        }
        //Aseta hahmon sijainti, jos sijantiin voi liikkua (isWalkable)
        public bool AsetaSijainti(Hahmo hahmo, int x, int y) {
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
        public void AsetaWalkable(int x, int y, bool isWalkable) {
            ICell solu = GetCell(x, y);
            SetCellProperties(solu.X, solu.Y, solu.IsTransparent, isWalkable, solu.IsExplored);
        }
        public Ovi PalautaOvi(int x, int y)
        {
            return Ovet.SingleOrDefault(d => d.X == x && d.Y == y);
        }
        private void AvaaOvi (Hahmo hahmo, int x, int y)
        {
            Ovi door = PalautaOvi(x, y);
            if (door != null && !door.Auki)
            {
                door.Auki = true;
                var solu = GetCell(x, y);
                SetCellProperties(x, y, true, true, solu.IsExplored);
                Ohjelma.ViestiLoki.Lisaa($"{hahmo.Nimi} opened a door.");
            }
        }
    }
}
