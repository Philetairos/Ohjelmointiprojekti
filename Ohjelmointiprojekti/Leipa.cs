using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti
{
    public class Leipa : Esine
    {
        public Leipa (int maara, int x, int y)
        {
            X = x;
            Y = y;
            Nimi = "Loaf of bread";
            Maara = maara;
            Vari = RLColor.Brown;
            Merkki = 'b';
        }
        public override bool KaytaEsine()
        {
            Ohjelma.ViestiLoki.Lisaa("You eat the bread.");
            Ohjelma.Pelaaja.Nalka += 25;
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            return false;
        }
    }
}