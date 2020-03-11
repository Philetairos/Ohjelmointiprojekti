using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Luokka pelaajan toiminnoille, kuten liikumiselle ja interaktiolle

namespace Ohjelmointiprojekti {
    public enum Suunta
    {
        Null = 0,
        AlaVasen = 1,
        Alas = 2,
        AlaOikea = 3,
        Vasen = 4,
        Keski = 5,
        Oikea = 6,
        YlaVasen = 7,
        Ylos = 8,
        YlaOikea = 9
    }
    public class CommandSystem {
        public bool SiirraPelaaja(Suunta suunta)
        {
            int x = Program.Pelaaja.X;
            int y = Program.Pelaaja.Y;
            switch (suunta) {
                case Suunta.Ylos:{
                    y = Program.Pelaaja.Y - 1;
                    break;
                }
                case Suunta.Alas:{
                    y = Program.Pelaaja.Y + 1;
                    break;
                }
                case Suunta.Vasen:{
                    x = Program.Pelaaja.X - 1;
                    break;
                }
                case Suunta.Oikea:{
                    x = Program.Pelaaja.X + 1;
                    break;
                }
                default:{
                    return false;
                }
            }

            if (Program.peliKartta.AsetaSijainti(Program.Pelaaja, x, y)) {
                return true;
            }
            return false;
        }
        public NPC Interaktio(Suunta suunta, MessageLog viestiloki) {
            int x = Program.Pelaaja.X;
            int y = Program.Pelaaja.Y;
            switch (suunta) {
                case Suunta.Ylos:{
                    y = Program.Pelaaja.Y - 1;
                    break;
                }
                case Suunta.Alas:{
                    y = Program.Pelaaja.Y + 1;
                    break;
                }
                case Suunta.Vasen:{
                    x = Program.Pelaaja.X - 1;
                    break;
                }
                case Suunta.Oikea:{
                    x = Program.Pelaaja.X + 1;
                    break;
                }
                default:{
                    return null;
                }
            }
            NPC npc = Program.peliKartta.NPCSijainti(x, y);
            return npc;
        }
    }
}
