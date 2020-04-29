using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RogueSharp.DiceNotation;
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
            Tuple<int, int> koord = GetSuunta(suunta);
            if (Ohjelma.peliKartta.AsetaSijainti(Ohjelma.Pelaaja, koord.Item1, koord.Item2)) {
                return true;
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
                    if (puolustaja.Elama == 0) {
                        Ohjelma.ViestiLoki.Lisaa($"{puolustaja.Nimi} has been struck down.");
                        //Lisää kunnon käsittely kuolemalle
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
