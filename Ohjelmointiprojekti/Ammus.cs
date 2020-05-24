﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka ammuttavia esineitä varten, jotka vahingoittavat kohdetta
    /// </summary>
    public class Ammus : Esine {
        public int Vahinko { get; set; }

        public Ammus(int maara, int x, int y, int vahinko, string nimi, RLColor vari, char merkki) {
            X = x;
            Y = y;
            Nimi = nimi;
            Maara = maara;
            Vari = vari;
            Merkki = merkki;
            Vahinko = vahinko;
        }
    }
}
