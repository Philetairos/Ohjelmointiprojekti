﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Tämä luokka luo erilaisia karttoja, käyttäen GameMap-luokkaa

namespace Ohjelmointiprojekti {
    public class MapGenerator {
        private readonly int karttaLeveys;
        private readonly int karttaKorkeus;
        private readonly GameMap kartta;

        //konstruktori
        public MapGenerator(int leveys, int korkeus) {
            karttaLeveys = leveys;
            karttaKorkeus = korkeus;
            kartta = new GameMap();
        }
        //Aloituskartta
        public GameMap TestiKartta() {
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells())
            {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys/2, karttaKorkeus-2, 12))
            {
                kartta.SetCellProperties(solu.X, solu.Y, false, false, true);
            }
            return kartta;
        }
    }
}
