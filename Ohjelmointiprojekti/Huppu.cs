using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    public class Huppu : Esine {
        public Huppu(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Woolen Hood";
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
