using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using RogueSharp.Random;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Tämä luokka siirtää hahmoa, välttäen seiniin törmäämistä
    /// </summary>
    public class SiirraHahmo {
        public bool LiikuRandom(Hahmo hahmo) {
            PeliKartta kartta = Ohjelma.peliKartta;
            DotNetRandom satunnaisluku = new DotNetRandom();
            kartta.AsetaWalkable(hahmo.X, hahmo.Y, true); 
            int suunta1 = satunnaisluku.Next(-1,1);
            int suunta2 = satunnaisluku.Next(-1, 1);
            if (kartta.IsWalkable(hahmo.X+suunta1, hahmo.Y+suunta2)) {
                kartta.AsetaSijainti(hahmo, hahmo.X + suunta1, hahmo.Y + suunta2);
            }
            return true;
        }
        public bool LiikuKohteeseen(ICell kohde, Hahmo hahmo) {
            PeliKartta kartta = Ohjelma.peliKartta;
            kartta.AsetaWalkable(hahmo.X, hahmo.Y, true);
            PathFinder polkuEtsija = new PathFinder(kartta);
            Path polku = null;
            try {
                polku = polkuEtsija.ShortestPath(kartta.GetCell(hahmo.X, hahmo.Y), kohde);
            }
            catch (PathNotFoundException) {
                
            }
            kartta.AsetaWalkable(hahmo.X, hahmo.Y, false);
            if (polku != null) {
                try {
                    LiikutaHahmo(hahmo, polku.StepForward());
                }
                catch (NoMoreStepsException) {

                }
            }
            return true;
        }
        public bool LiikuKohtiPelaajaa(Hahmo hahmo) {
            Pelaaja pelaaja = Ohjelma.Pelaaja;
            PeliKartta kartta = Ohjelma.peliKartta;
            FieldOfView hahmoFoV = new FieldOfView(kartta);
            hahmoFoV.ComputeFov(hahmo.X, hahmo.Y,hahmo.Nakoetaisyys, true);
            if (hahmoFoV.IsInFov(pelaaja.X, pelaaja.Y)) {
                LiikuKohteeseen(kartta.GetCell(pelaaja.X, pelaaja.Y), hahmo);
            }
            return true;
        }
        public void LiikutaHahmo(Hahmo hahmo, ICell solu) {
            if (!Ohjelma.peliKartta.AsetaSijainti(hahmo, solu.X, solu.Y)) {
                if (hahmo.GetType() == typeof(Vastustaja) && Ohjelma.Pelaaja.X == solu.X && Ohjelma.Pelaaja.Y == solu.Y) {
                    Ohjelma.KomentoKasittelija.Hyokkaa(hahmo, Ohjelma.Pelaaja);
                }
            }
        }
    }
}
