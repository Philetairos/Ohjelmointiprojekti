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
        /// <summary>
        /// Metodi pelaajan siirtämiselle
        /// </summary>
        /// <param name="suunta">Mihin suuntaan halutaan siirtää</param>
        /// <returns>Onnistuiko siirtäminen, true jos kyllä, false jos ei</returns>
        public bool SiirraPelaaja(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            if (Ohjelma.peliKartta.AsetaSijainti(Ohjelma.Pelaaja, koord.Item1, koord.Item2)) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodi pitkän kantaman aseella ampumiselle
        /// </summary>
        /// <param name="suunta">Mihin suuntaan ammutaan</param>
        /// <param name="ammus">Mitä ammutaan</param>
        /// <returns>Palauttaa aina false</returns>
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

        /// <summary>
        /// Käsittele ammutun ammuksen osuma vastustajaan
        /// </summary>
        /// <param name="vastustaja">Vastustaja johon ammus on osunut</param>
        /// <param name="ammus">Ammus joka osuu</param>
        /// <returns>Palauttaa aina false</returns>
        public bool KasitteleOsuma(Vastustaja vastustaja, Ammus ammus) {
            DiceExpression noppa = new DiceExpression().Die(6);
            DiceResult noppatulos = noppa.Roll();
            if (noppatulos.Value + ammus.Vahinko - vastustaja.Puolustus >= 3) {
                vastustaja.Elama -= ammus.Vahinko;
                Ohjelma.ViestiLoki.Lisaa($"{ammus.Nimi} hits {vastustaja.Nimi} for {ammus.Vahinko} damage!");
                if (vastustaja.Elama <= 0) {
                    Ohjelma.ViestiLoki.Lisaa($"{vastustaja.Nimi} has been shot and killed.");
                    vastustaja.KasitteleKuolema();
                }
            }
            else {
                Ohjelma.ViestiLoki.Lisaa($"{ammus.Nimi} hits {vastustaja.Nimi}, but fails to penetrate!");
            }
            return false;
        }

        /// <summary>
        /// Palauttaa NPC-hahmon halutusta suunnasta
        /// </summary>
        /// <param name="suunta">Suunta jota tarkastellaan</param>
        /// <returns>NPC-hahmo jos sellainen on, muutoin null</returns>
        public NPC GetNPC(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            NPC npc = Ohjelma.peliKartta.NPCSijainti(koord.Item1, koord.Item2);
            return npc;
        }

        /// <summary>
        /// Palauttaa vastustajahahmon halutusta suunnasta
        /// </summary>
        /// <param name="suunta">Suunta jota tarkastellaan</param>
        /// <returns>Vastustajahahmo jos sellainen on, muutoin null</returns>
        public Vastustaja GetVastustaja(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            Vastustaja vihollinen = Ohjelma.peliKartta.VastustajaSijainti(koord.Item1, koord.Item2);
            return vihollinen;
        }

        /// <summary>
        /// Palauttaa esineen halutusta suunnasta
        /// </summary>
        /// <param name="suunta">Suunta jota tarkastellaan</param>
        /// <returns>Esine jos sellainen on, muutoin null</returns>
        public Esine GetEsine(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            Esine esine = Ohjelma.peliKartta.EsineSijainti(koord.Item1, koord.Item2);
            return esine;
        }

        /// <summary>
        /// Palauttaa pyhäkön halutusta suunnasta
        /// </summary>
        /// <param name="suunta">Suunta jota tarkastellaan</param>
        /// <returns>Pyhäkkö jos sellainen on, muutoin null</returns>
        public Pyhakko GetPyhakko(Suunta suunta) {
            Tuple<int, int> koord = GetSuunta(suunta);
            Pyhakko esine = Ohjelma.peliKartta.PyhakkoSijainti(koord.Item1, koord.Item2);
            return esine;
        }

        /// <summary>
        /// Metodi joka käsittelee lähitaistelun pelaajan ja vastustajan välillä
        /// </summary>
        /// <param name="hyokkaaja">Hahmo joka hyökkää</param>
        /// <param name="puolustaja">Hahmo joka puolustaa</param>
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
                        puolustaja.KasitteleKuolema();
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

        /// <summary>
        /// Hae tiilen koordinaatit halutusta suunnasta
        /// </summary>
        /// <param name="suunta">Haluttu suunta</param>
        /// <returns>Naapuritiilen koordinaatit</returns>
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
