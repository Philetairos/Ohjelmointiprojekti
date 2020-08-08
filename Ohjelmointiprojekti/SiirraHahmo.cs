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
        /// <summary>
        /// Hahmon liikkuminen satunnaiseen suuntaan
        /// </summary>
        /// <param name="hahmo">Hahmo jota siirretään</param>
        /// <returns>Palauttaa aina true</returns>
        public bool LiikuRandom(Hahmo hahmo) {
            PeliKartta kartta = Ohjelma.peliKartta;
            DotNetRandom satunnaisluku = new DotNetRandom();
            kartta.AsetaWalkable(hahmo.X, hahmo.Y, true); 
            int suunta1 = satunnaisluku.Next(-1,1);
            int suunta2 = satunnaisluku.Next(-1, 1);
            kartta.AsetaSijainti(hahmo, hahmo.X + suunta1, hahmo.Y + suunta2);
            return true;
        }

        /// <summary>
        /// Hahmon siirtäminen tiettyyn tiileen
        /// </summary>
        /// <param name="kohde">Kohde jota kohti siirretään</param>
        /// <param name="hahmo">Hahmo jota siirretään</param>
        /// <returns>Palauttaa aina true</returns>
        public bool LiikuKohteeseen(ICell kohde, Hahmo hahmo) {
            PeliKartta kartta = Ohjelma.peliKartta;
            kartta.AsetaWalkable(hahmo.X, hahmo.Y, true);
            kartta.AsetaWalkable(kohde.X, kohde.Y, true);
            PathFinder polkuEtsija = new PathFinder(kartta);
            Path polku = null;
            try {
                polku = polkuEtsija.ShortestPath(kartta.GetCell(hahmo.X, hahmo.Y), kohde);
            }
            catch (PathNotFoundException) {
                
            }
            kartta.AsetaWalkable(hahmo.X, hahmo.Y, false);
            kartta.AsetaWalkable(kohde.X, kohde.Y, false);
            if (polku != null) {
                try {
                    LiikutaHahmo(hahmo, polku.StepForward());
                }
                catch (NoMoreStepsException) {

                }
            }
            return true;
        }

        /// <summary>
        /// Siirrä hahmoa kohti pelaajan sijaintia
        /// </summary>
        /// <param name="hahmo">Hahmo jota siirretään</param>
        /// <returns>Palauttaa aina true</returns>
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

        /// <summary>
        /// Siirrä hahmo tiettyyn tiileen, jos hahmo on vastustaja ja pelaaja on vieressä niin hyökkää
        /// </summary>
        /// <param name="hahmo">Hahmo jota siirretään</param>
        /// <param name="solu">Tiili johon siirrytään</param>
        public void LiikutaHahmo(Hahmo hahmo, ICell solu) {
            if (!Ohjelma.peliKartta.AsetaSijainti(hahmo, solu.X, solu.Y)) {
                if (hahmo.GetType() == typeof(Vastustaja) && Ohjelma.Pelaaja.X == solu.X && Ohjelma.Pelaaja.Y == solu.Y) {
                    Ohjelma.KomentoKasittelija.Hyokkaa(hahmo, Ohjelma.Pelaaja);
                }
            }
        }
    }
}
