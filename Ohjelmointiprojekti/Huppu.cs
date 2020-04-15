using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka huppu-esinettä varten, jonka voi pukea
    /// </summary>
    public class Huppu : Esine {
        public Huppu(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Woolen hood";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = 'n';
        }
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You wear the hood.");
            Ohjelma.Pelaaja.Paahine = this;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }
    }
}
