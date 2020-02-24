using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Tämä on luokka pelin kartoille, joilla pelaaja liikkuu

namespace Ohjelmointiprojekti {
    public class GameMap : Map {
        public void PiirraKartta(RLConsole karttaKonsoli) {
            karttaKonsoli.Clear();
            foreach (Cell solu in GetAllCells()) {
                AsetaSymboli(karttaKonsoli, solu);
            }
        }
        private void AsetaSymboli(RLConsole karttaKonsoli, Cell solu) {
            //Älä piirrä mitään jos pelaaja ei ole nähnyt solua
            if (!solu.IsExplored) {
                return;
            }
            //Piirrä solu jonka pelaaja näkee
            if (IsInFov(solu.X, solu.Y)) {
                karttaKonsoli.Set(solu.X, solu.Y, RLColor.LightGray, RLColor.Black, '.');
            }
            //Piirrä solu jota pelaaja ei näe, mutta jonka on nähnyt aikaisemmin
            else {
                karttaKonsoli.Set(solu.X, solu.Y, RLColor.Gray, RLColor.Black, '.');
            }
        }
    }
}
