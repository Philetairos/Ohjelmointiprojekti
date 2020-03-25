using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

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
    /// <summary>
    /// Luokka pelaajan toiminnoille, kuten liikumiselle ja interaktiolle
    /// </summary>
    public class KomentoKasittelija {
        public bool SiirraPelaaja(Suunta suunta)
        {
            int x = Ohjelma.Pelaaja.X;
            int y = Ohjelma.Pelaaja.Y;
            switch (suunta) {
                case Suunta.Ylos: {
                        y = Ohjelma.Pelaaja.Y - 1;
                        break;
                    }
                case Suunta.Alas: {
                        y = Ohjelma.Pelaaja.Y + 1;
                        break;
                    }
                case Suunta.Vasen: {
                        x = Ohjelma.Pelaaja.X - 1;
                        break;
                    }
                case Suunta.Oikea: {
                        x = Ohjelma.Pelaaja.X + 1;
                        break;
                    }
                default: {
                        return false;
                    }
            }

            if (Ohjelma.peliKartta.AsetaSijainti(Ohjelma.Pelaaja, x, y)) {
                return true;
            }
            return false;
        }
        public NPC Interaktio(Suunta suunta) {
            int x = Ohjelma.Pelaaja.X;
            int y = Ohjelma.Pelaaja.Y;
            switch (suunta) {
                case Suunta.Ylos: {
                        y = Ohjelma.Pelaaja.Y - 1;
                        break;
                    }
                case Suunta.Alas: {
                        y = Ohjelma.Pelaaja.Y + 1;
                        break;
                    }
                case Suunta.Vasen: {
                        x = Ohjelma.Pelaaja.X - 1;
                        break;
                    }
                case Suunta.Oikea: {
                        x = Ohjelma.Pelaaja.X + 1;
                        break;
                    }
                default: {
                        return null;
                    }
            }
            NPC npc = Ohjelma.peliKartta.NPCSijainti(x, y);
            return npc;
        }
        public void SiirraHahmo(Hahmo hahmo, ICell solu) {
            bool siirtyma = Ohjelma.peliKartta.AsetaSijainti(hahmo, solu.X, solu.Y);
        }
        public Esine Ota(Suunta suunta) {
            int x = Ohjelma.Pelaaja.X;
            int y = Ohjelma.Pelaaja.Y;
            switch (suunta)
            {
                case Suunta.Ylos:
                    {
                        y = Ohjelma.Pelaaja.Y - 1;
                        break;
                    }
                case Suunta.Alas:
                    {
                        y = Ohjelma.Pelaaja.Y + 1;
                        break;
                    }
                case Suunta.Vasen:
                    {
                        x = Ohjelma.Pelaaja.X - 1;
                        break;
                    }
                case Suunta.Oikea:
                    {
                        x = Ohjelma.Pelaaja.X + 1;
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }
            Esine esine = Ohjelma.peliKartta.EsineSijainti(x, y);
            return esine;
        }
    }
}
