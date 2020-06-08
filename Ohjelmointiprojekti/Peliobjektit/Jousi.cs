using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    public class Jousi : Varuste {
        public Jousi(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Bow";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = ')';
            Lokero = 4;
            VoiAmpua = true;
            LokeroNimi = "4. Right Hand";
        }
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You equip the bow.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You unequip the bow.");
        }
    }
}
