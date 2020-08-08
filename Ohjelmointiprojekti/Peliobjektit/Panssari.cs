﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Luokka panssari-esinettä varten, jonka voi pukea
    /// </summary>
    public class Panssari : Varuste {

        /// <summary>
        /// Konstruktori
        /// </summary>
        /// <param name="maara">Kuinka monta esinettä</param>
        /// <param name="x">Sijainti kartan x-akselilla</param>
        /// <param name="y">Sijainti kartan y-akselilla</param>
        public Panssari(int maara, int x, int y) {
            X = x;
            Y = y;
            Nimi = "Chainmail";
            Maara = maara;
            Vari = RLColor.LightGray;
            Merkki = 'H';
            Puolustus = 2;
            Lokero = 1;
            LokeroNimi = "2. Body";
        }

        /// <summary>
        /// Mitä tapahtuu kun pelaaja käyttää varustetta (pelaaja pukee sen)
        /// </summary>
        /// <returns>Palauttaa aina false</returns>
        public override bool KaytaEsine() {
            Ohjelma.Pelaaja.Inventaario.Remove(this);
            if (Ohjelma.Pelaaja.Varusteet[Lokero] != null) {
                Ohjelma.Pelaaja.PoistaVaruste(Lokero+1);
            }
            Ohjelma.ViestiLoki.Lisaa("You wear the chainmail.");
            Ohjelma.Pelaaja.Varusteet[Lokero] = this;
            Ohjelma.Pelaaja.Puolustus += Puolustus;
            return false;
        }

        /// <summary>
        /// Metodi sille, mitä tapahtuu kun pelaaja poistaa varusteen
        /// </summary>
        public override void PoistaVaruste() {
            Ohjelma.ViestiLoki.Lisaa("You take off the chainmail.");
            Ohjelma.Pelaaja.Puolustus -= Puolustus;
        }
    }
}