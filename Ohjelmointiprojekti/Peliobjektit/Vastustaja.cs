using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka vihollishahmoja varten, jotka hyökkäävät pelaajaa vastaan
    /// Tekijä: Daniel Juola
    /// Luotu: 6.8.20
    /// </summary>
    public class Vastustaja : Hahmo {
        public bool Liikkuu;

        /// <summary>
        /// Tyhjä konstruktori
        /// </summary>
        public Vastustaja() {

        }

        /// <summary>
        /// Vastustajan konstruktori
        /// </summary>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        /// <param name="nimi">Vastustajan nimi</param>
        /// <param name="merkki">Vastustajan symboli</param>
        /// <param name="vari">Symbolin väri</param>
        /// <param name="elama">Kuinka paljon elämää vastustajalla on</param>
        /// <param name="voimakkuus">Kuinka voimakas vastustaja on</param>
        /// <param name="napparyys">Kuinka näppärä vastustaja on</param>
        /// <param name="puolustus">Kuinka hyvä puolustus vastustajalla on</param>
        /// <param name="liikkuukko">Liikkuuko vastustaja satunnaisesti</param>
        public Vastustaja(int x, int y, string nimi, char merkki, RLColor vari, int elama, int voimakkuus, int napparyys, int puolustus, bool liikkuukko) {
            Nimi = nimi;
            Nakoetaisyys = 15;
            Vari = vari;
            Merkki = merkki;
            X = x;
            Y = y;
            Liikkuu = liikkuukko;
            Elama = elama;
            Voimakkuus = voimakkuus;
            Napparyys = napparyys;
            Puolustus = puolustus;
            Inventaario = new List<Esine> {
                Capacity = 1
            };
        }

        /// <summary>
        /// Käsittely vastustajan kuolemalle
        /// </summary>
        /// <param name="kartta">Millä kartalla vastustaja kuolee</param>
        public override void KasitteleKuolema() {
            Ohjelma.peliKartta.Vastustajat.Remove(this);
            Ohjelma.peliKartta.AsetaWalkable(X, Y, true);
            Ohjelma.peliKartta.Solut.Add(new Solu(X, Y, true, true, true, RLColor.Black, RLColor.Red, Merkki));
            foreach (Esine esine in Inventaario) {
                Ohjelma.peliKartta.Esineet.Add(esine);
            }
        }
    }
}
