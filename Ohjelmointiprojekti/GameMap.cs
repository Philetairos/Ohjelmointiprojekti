using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Tämä on luokka pelin karttojen luomiseen, joilla pelaaja liikkuu

namespace Ohjelmointiprojekti {
    public class GameMap : Map {
        private readonly List<NPC> NPCs;
        public GameMap() {
            NPCs = new List<NPC>();
        }
        public void LisaaNPC(NPC npc) {
            NPCs.Add(npc);
            AsetaWalkable(npc.X, npc.Y, false);
        }
        public NPC NPCSijainti(int x, int y) {
            return NPCs.FirstOrDefault(m => m.X == x && m.Y == y);
        }
        public void PiirraKartta(RLConsole karttaKonsoli, RLConsole statsiKonsoli) {
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
        }
        private void AsetaSymboli(RLConsole karttaKonsoli, Cell solu) {
            //Älä piirrä mitään jos pelaaja ei ole nähnyt solua
            if (!solu.IsExplored) {
                return;
            }
            //Piirrä solu jonka pelaaja näkee
            if (IsInFov(solu.X, solu.Y)) {
                //Lattia
                if (solu.IsWalkable)
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.LightGray, RLColor.Black, '.');
                }
                //Seinä
                else
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.LightGray, RLColor.Black, '#');
                }
                
            }
            //Piirrä solu jota pelaaja ei näe, mutta jonka on nähnyt aikaisemmin
            else {
                //Lattia
                if (solu.IsWalkable)
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.Gray, RLColor.Black, '.');
                }
                //Seinä
                else
                {
                    karttaKonsoli.Set(solu.X, solu.Y, RLColor.Gray, RLColor.Black, '#');
                }
                
            }
        }
        //Päivitä, mitä tiileja pelaaja näkee
        public void PaivitaNakoKentta() {
            Player pelaaja = Program.Pelaaja;
            ComputeFov(pelaaja.X, pelaaja.Y, pelaaja.Nakoetaisyys, true);
            // Mark all cells in field-of-view as having been explored
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
                if (hahmo is Player) {
                    PaivitaNakoKentta();
                }
                return true;
            }
            return false;
        }
        //
        public void AsetaWalkable(int x, int y, bool isWalkable) {
            ICell solu = GetCell(x, y);
            SetCellProperties(solu.X, solu.Y, solu.IsTransparent, isWalkable, solu.IsExplored);
        }
    }
}
