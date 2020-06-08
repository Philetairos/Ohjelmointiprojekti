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
    public class Huppu : Varuste {
        public Huppu(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Woolen hood";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = 'n';
            Puolustus = 1;
            Lokero = 0;
            LokeroNimi = "1. Head";
        }
        public override bool KaytaEsine() {
            Ohjelma.ViestiLoki.Lisaa("You wear the hood.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Puolustus += Puolustus;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You take off the hood.");
            Ohjelma.Pelaaja.Puolustus -= Puolustus;
        }
    }
}
