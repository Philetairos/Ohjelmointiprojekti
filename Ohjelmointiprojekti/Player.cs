using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Luokka pelaajan hahmolle, jonka kautta pelaaja pelaa peliä

namespace Ohjelmointiprojekti
{
    public class Player : Hahmo {
        public Player(int x, int y) {
            Nimi = "Pelaaja";
            Nakoetaisyys = 100;
            Vari = RLColor.White;
            Merkki = '@';
            X = x;
            Y = y;
        }
        public void PiirraStatsit(RLConsole statsiKonsoli) {
            statsiKonsoli.Print(1, 1, $"Nimi:    {Nimi}", RLColor.White);
        }
    }
}
