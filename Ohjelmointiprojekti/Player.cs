using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Luokka pelaajan hahmolle, jota hän kontrolloi

namespace Ohjelmointiprojekti
{
    public class Player : Hahmo {
        public Player() {
            Nimi = "Pelaaja";
            Nakoetaisyys = 100;
            Vari = RLColor.White;
            Merkki = '@';
            X = 0;
            Y = 0;
        }
        public Player(int x, int y)
        {
            Nimi = "Pelaaja";
            Nakoetaisyys = 100;
            Vari = RLColor.White;
            Merkki = '@';
            X = x;
            Y = y;
        }
    }
}
