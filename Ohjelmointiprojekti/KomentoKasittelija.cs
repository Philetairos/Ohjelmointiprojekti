using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RogueSharp.DiceNotation;
using RLNET;

namespace Ohjelmointiprojekti {
    public enum Suunta {
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
        public bool SiirraPelaaja(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            if (Ohjelma.peliKartta.AsetaSijainti(Ohjelma.Pelaaja, koord.Item1, koord.Item2)) {
                return true;
            }
            return false;
        }
        public bool Ammu(Suunta suunta, Ammus ammus)  {
            switch (suunta) {
                case Suunta.Ylos:
                    for (int y = 1; y < ammus.Kantama; y++) {
                        if (Ohjelma.peliKartta.GetCell(Ohjelma.Pelaaja.X, Ohjelma.Pelaaja.Y - y).IsWalkable) {
                            ammus.X = Ohjelma.Pelaaja.X;
                            ammus.Y = Ohjelma.Pelaaja.Y - y;
                        }
                        else {
                            Vastustaja vastustaja = Ohjelma.peliKartta.VastustajaSijainti(Ohjelma.Pelaaja.X, Ohjelma.Pelaaja.Y - y);
                            if (vastustaja != null) {
                                return KasitteleOsuma(vastustaja, ammus);
                            }
                            Ohjelma.peliKartta.Esineet.Add(ammus);
                            return false;
                        }
                    }
                    break;
                case Suunta.Alas:
                    for (int y = 1; y < ammus.Kantama; y++) {
                        if (Ohjelma.peliKartta.GetCell(Ohjelma.Pelaaja.X, Ohjelma.Pelaaja.Y + y).IsWalkable) {
                            ammus.X = Ohjelma.Pelaaja.X;
                            ammus.Y = Ohjelma.Pelaaja.Y + y;
                        }
                        else {
                            Vastustaja vastustaja = Ohjelma.peliKartta.VastustajaSijainti(Ohjelma.Pelaaja.X, Ohjelma.Pelaaja.Y + y);
                            if (vastustaja != null) {
                                return KasitteleOsuma(vastustaja, ammus);
                            }
                            Ohjelma.peliKartta.Esineet.Add(ammus);
                            return false;
                        }
                    }
                    break;
                case Suunta.Vasen:
                    for (int x = 1; x < ammus.Kantama; x++) {
                        if (Ohjelma.peliKartta.GetCell(Ohjelma.Pelaaja.X - x, Ohjelma.Pelaaja.Y).IsWalkable) {
                            ammus.X = Ohjelma.Pelaaja.X - x;
                            ammus.Y = Ohjelma.Pelaaja.Y;
                        }
                        else {
                            Vastustaja vastustaja = Ohjelma.peliKartta.VastustajaSijainti(Ohjelma.Pelaaja.X - x, Ohjelma.Pelaaja.Y);
                            if (vastustaja != null) {
                                return KasitteleOsuma(vastustaja, ammus);
                            }
                            Ohjelma.peliKartta.Esineet.Add(ammus);
                            return false;
                        }
                    }
                    break;
                case Suunta.Oikea:
                    for (int x = 1; x < ammus.Kantama; x++) {
                        if (Ohjelma.peliKartta.GetCell(Ohjelma.Pelaaja.X + x, Ohjelma.Pelaaja.Y).IsWalkable) {
                            ammus.X = Ohjelma.Pelaaja.X + x;
                            ammus.Y = Ohjelma.Pelaaja.Y;
                        }
                        else {
                            Vastustaja vastustaja = Ohjelma.peliKartta.VastustajaSijainti(Ohjelma.Pelaaja.X + x, Ohjelma.Pelaaja.Y);
                            if (vastustaja != null) {
                                return KasitteleOsuma(vastustaja, ammus);
                            }
                            Ohjelma.peliKartta.Esineet.Add(ammus);
                            return false;
                        }
                    }
                    break;
                default:
                    return false;
            }
            Ohjelma.peliKartta.Esineet.Add(ammus);
            return false;
        }
        public bool KasitteleOsuma(Vastustaja vastustaja, Ammus ammus) {
            DiceExpression noppa = new DiceExpression().Die(6);
            DiceResult noppatulos = noppa.Roll();
            if (noppatulos.Value + ammus.Vahinko - vastustaja.Puolustus >= 3) {
                vastustaja.Elama -= ammus.Vahinko;
                if (vastustaja.Elama <= 0) {
                    Ohjelma.ViestiLoki.Lisaa($"{vastustaja.Nimi} has been shot and killed.");
                    vastustaja.KasitteleKuolema(Ohjelma.peliKartta);
                }
            }
            else {
                Ohjelma.ViestiLoki.Lisaa($"{ammus.Nimi} hits {vastustaja.Nimi}, but fails to penetrate!");
            }
            return false;
        }
        public NPC GetNPC(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            NPC npc = Ohjelma.peliKartta.NPCSijainti(koord.Item1, koord.Item2);
            return npc;
        }
        public Vastustaja GetVastustaja(Suunta suunta)
        {
            Tuple<int, int> koord = GetSuunta(suunta);
            Vastustaja vihollinen = Ohjelma.peliKartta.VastustajaSijainti(koord.Item1, koord.Item2);
            return vihollinen;
        }
        public Esine GetEsine(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            Esine esine = Ohjelma.peliKartta.EsineSijainti(koord.Item1, koord.Item2);
            return esine;
        }
        public void Hyokkaa(Hahmo hyokkaaja, Hahmo puolustaja) {
            DiceExpression noppa = new DiceExpression().Die(6);
            DiceResult noppatulos = noppa.Roll();
            if (noppatulos.Value + hyokkaaja.Napparyys - puolustaja.Napparyys >= 3) {
                noppatulos = noppa.Roll();
                if (noppatulos.Value + hyokkaaja.Voimakkuus - puolustaja.Puolustus >= 3) {
                    puolustaja.Elama -= hyokkaaja.Voimakkuus;
                    Ohjelma.ViestiLoki.Lisaa($"{hyokkaaja.Nimi} hits {puolustaja.Nimi} for {hyokkaaja.Voimakkuus} damage!");
                    if (puolustaja.Elama <= 0) {
                        Ohjelma.ViestiLoki.Lisaa($"{puolustaja.Nimi} has been struck down.");
                        puolustaja.KasitteleKuolema(Ohjelma.peliKartta);
                    }
                }
                else {
                    Ohjelma.ViestiLoki.Lisaa($"{hyokkaaja.Nimi} hits {puolustaja.Nimi}, but the blow is deflected!");
                }
            }
            else {
                Ohjelma.ViestiLoki.Lisaa($"{hyokkaaja.Nimi} misses {puolustaja.Nimi}!");
            }
        }
        public Tuple<int,int> GetSuunta(Suunta suunta) {
            int x = Ohjelma.Pelaaja.X;
            int y = Ohjelma.Pelaaja.Y;
            switch (suunta) {
                case Suunta.Ylos:
                    y = Ohjelma.Pelaaja.Y - 1;
                    break;
                case Suunta.Alas:
                    y = Ohjelma.Pelaaja.Y + 1;
                    break;
                case Suunta.Vasen:
                    x = Ohjelma.Pelaaja.X - 1;
                    break;
                case Suunta.Oikea:
                    x = Ohjelma.Pelaaja.X + 1;
                    break;
                default:
                    return null;
            }
            return Tuple.Create(x, y);
        }
    }
}
