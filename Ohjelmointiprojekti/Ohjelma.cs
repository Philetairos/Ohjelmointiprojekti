using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    ///  Ohjelman päätiedosto. Ohjelman tekijä: Daniel Juola 
    ///  Perustuu Faron Bracyn esimerkkikoodiin
    ///  Projektissa käytetyt kirjastot: RLNet (Travis M. Clark, 2014) ja RogueSharp (Faron Bracy, 2014-2019), MIT-lisenssi
    /// </summary>
    class Ohjelma {
        //Pääkonsolin koko tiileinä, ei pikseleinä
        private static readonly int konsolileveys = 160;
        private static readonly int konsolikorkeus = 80;
        //Muiden konsolien koko perustuu sitten pääkonsolin kokoon
        private static readonly int karttaleveys = Convert.ToInt32(konsolileveys *0.75);
        private static readonly int karttakorkeus = Convert.ToInt32(konsolikorkeus * 0.8);
        private static readonly int sivukonsolileveys = Convert.ToInt32(konsolileveys * 0.25);
        private static readonly int dialogikonsolikorkeus = Convert.ToInt32(konsolikorkeus * 0.2);
        private static readonly int konsolikorkeuspuolet = Convert.ToInt32(konsolikorkeus * 0.5);

        //Luo kaikki konsolit
        private static RLRootConsole paaKonsoli;
        private static RLConsole karttaKonsoli;
        private static RLConsole dialogiKonsoli;
        private static RLConsole inventaarioKonsoli;
        private static RLConsole statsiKonsoli;

        //Dialogia varten
        private static bool talkMoodi = false;
        private static NPC dialogiNPC = new NPC();
        private static Vastustaja vihollinen = new Vastustaja();
        private static bool dialogi = false;

        private static bool getMoodi = false;
        private static bool kaytaEsine = false;
        private static bool attackMoodi = false;
        private static bool lookMoodi = false;

        readonly static SiirraHahmo liikuttaja = new SiirraHahmo();
        private static int liikkumislaskuri = 0;

        public static KarttaGeneroija karttaGeneroija;
        public static PeliKartta peliKartta;
        public static Pelaaja Pelaaja {
            get;
            set;
        }
        public static KomentoKasittelija KomentoKasittelija {
            get;
            private set;
        }
        public static Viestiloki ViestiLoki {
            get;
            private set;
        }

        public static void Main() {
            //Fontti jota tiilit ja teksti käyttävät
            string fonttiTiedosto = "terminal8x8.png";
            string konsoliNimi = "Paragon of Virtue";
            //Luo pääkonsoli joka toimii pelin perustana
            paaKonsoli = new RLRootConsole(fonttiTiedosto, konsolileveys, konsolikorkeus, 8, 8, 1, konsoliNimi);
            //Luo karttakonsoli joka toimii pelimaailman näkymänä
            karttaKonsoli = new RLConsole(karttaleveys, karttakorkeus);
            //Luo dialogikonsoli joka näyttää pelin viestit ja keskustelun dialogin
            dialogiKonsoli = new RLConsole(karttaleveys, dialogikonsolikorkeus);
            //Luo inventaatiokonsoli joka näyttää pelaajan esineet
            inventaarioKonsoli = new RLConsole(sivukonsolileveys, konsolikorkeuspuolet);
            //Luo statistiikkakonsoli joka näyttää pelaajan hahmo(je)n tilan
            statsiKonsoli = new RLConsole(sivukonsolileveys, konsolikorkeuspuolet);
            karttaGeneroija = new KarttaGeneroija(karttaleveys,karttakorkeus);
            //Luo pelaajan hahmo
            Pelaaja = new Pelaaja(karttaleveys/2, karttakorkeus-6);
            KomentoKasittelija = new KomentoKasittelija();
            //Luo viestiloki
            ViestiLoki = new Viestiloki();
            ViestiLoki.Lisaa("Controls: Arrow Keys - Move, T - Talk, G - Get, A - Attack, U - Use, L - Look, Esc - Exit game");
            //Luo aloituskartta
            peliKartta = karttaGeneroija.TestiKartta();
            peliKartta.PaivitaNakoKentta();
            paaKonsoli.Update += PaivitaKonsoli;
            paaKonsoli.Render += PiirraKonsoli;
            paaKonsoli.Run();
        }
        //Suorita suunnasta riippuvat komennot
        private static void Suorita(Suunta suunta) {
            if (talkMoodi == true)
            {
                dialogiNPC = KomentoKasittelija.GetNPC(suunta);
                if (dialogiNPC != null)
                {
                    dialogi = true;
                    ViestiLoki.Lisaa(dialogiNPC.HahmonDialogi[0].dialogi);
                    ViestiLoki.Lisaa(dialogiNPC.HahmonDialogi[0].vastaukset);
                }
                talkMoodi = false;
            }
            else if (attackMoodi == true)
            {
                vihollinen = KomentoKasittelija.GetVastustaja(suunta);
                KomentoKasittelija.Hyokkaa(Pelaaja, vihollinen);
                attackMoodi = false;
            }
            else if (getMoodi == true)
            {
                Esine otettavaEsine = KomentoKasittelija.GetEsine(suunta);
                if (otettavaEsine != null)
                {
                    Pelaaja.LisaaEsine(otettavaEsine);
                }
                getMoodi = false;
            }
            else if (lookMoodi == true)
            {
                Esine katsottavaEsine = KomentoKasittelija.GetEsine(suunta);
                if (katsottavaEsine != null) {
                    ViestiLoki.Lisaa($"You see a {katsottavaEsine.Nimi}.");
                }
                else if (KomentoKasittelija.GetNPC(suunta) != null) {
                    ViestiLoki.Lisaa($"You see {KomentoKasittelija.GetNPC(suunta).Nimi}.");
                }
                else if (KomentoKasittelija.GetVastustaja(suunta) != null) {
                    ViestiLoki.Lisaa($"You see a {KomentoKasittelija.GetVastustaja(suunta).Nimi}.");
                }
                else {
                    ViestiLoki.Lisaa($"You see nothing of note.");
                }
                lookMoodi = false;
            }
            else
            {
                bool siirtyma = KomentoKasittelija.SiirraPelaaja(suunta);
            }
        }
        //Käsittele syöte
        private static void PaivitaKonsoli(object sender, UpdateEventArgs e) {
            RLKeyPress nappain = paaKonsoli.Keyboard.GetKeyPress();
            if (nappain != null && dialogi == false && kaytaEsine == false) {
                switch (nappain.Key) {
                    case RLKey.Up:
                        Suorita(Suunta.Ylos);
                        break;
                    case RLKey.Down:
                        Suorita(Suunta.Alas);
                        break;
                    case RLKey.Left:
                        Suorita(Suunta.Vasen);
                        break;
                    case RLKey.Right:
                        Suorita(Suunta.Oikea);
                        break;
                    case RLKey.L:
                        lookMoodi = true;
                        ViestiLoki.Lisaa("Look: Press an arrow key");
                        break;
                    case RLKey.G:
                        getMoodi = true;
                        ViestiLoki.Lisaa("Get: Press an arrow key");
                        break;
                    case RLKey.T:
                        talkMoodi = true;
                        ViestiLoki.Lisaa("Talk: Press an arrow key");
                        break;
                    case RLKey.A:
                        attackMoodi = true;
                        ViestiLoki.Lisaa("Attack: Press an arrow key");
                        break;
                    case RLKey.U:
                        kaytaEsine = true;
                        ViestiLoki.Lisaa("Use: Press a number key");
                        break;
                    case RLKey.Escape:
                        paaKonsoli.Close();
                        break;
                    default:
                        break;
                }
                liikkumislaskuri++;
                if (liikkumislaskuri%3 == 0) {
                    foreach (NPC hahmo in peliKartta.NPCs) {
                        if (hahmo.Liikkuu == true && dialogi == false) {
                            liikuttaja.LiikuRandom(hahmo);
                        }
                    }
                    foreach (Vastustaja vastustaja in peliKartta.Vastustajat)
                    {
                        if (vastustaja.Liikkuu == true)
                        {
                            liikuttaja.LiikuKohtiPelaajaa(vastustaja);
                        }
                    }
                }
                else if (liikkumislaskuri == 25) {
                    Pelaaja.Nalka--;
                    if (Pelaaja.Nalka <= 0)
                    {
                        ViestiLoki.Lisaa("You are starving!");
                        Pelaaja.Elama -= 5;
                        if (Pelaaja.Elama <= 0)
                        {
                            ViestiLoki.Lisaa("You have died!");
                            peliKartta = karttaGeneroija.TyhjaKartta();
                            //Lisää kunnon käsittely kuolemalle
                            paaKonsoli.Update -= PaivitaKonsoli;
                        }
                    }
                    liikkumislaskuri = 0;
                }
            }
            else if (nappain != null && dialogi == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4) {
                    dialogi = dialogiNPC.Dialogi(Int32.Parse(nappain.Char.ToString()));
                }
            }
            else if (nappain != null && kaytaEsine == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4) {
                    int num = Int32.Parse(nappain.Char.ToString());
                    if (num > Pelaaja.Inventaario.Count) {
                        ViestiLoki.Lisaa("No item in that slot!");
                    }
                    else {
                        kaytaEsine = Pelaaja.Inventaario[num].KaytaEsine();
                    }
                }
                kaytaEsine = false;
            }
        }
        //Piirrä kaikki ruudulle
        private static void PiirraKonsoli(object sender, UpdateEventArgs e) {
            RLConsole.Blit(karttaKonsoli, 0, 0, karttaleveys, karttakorkeus, paaKonsoli, 0, 0);
            RLConsole.Blit(dialogiKonsoli, 0, 0, karttaleveys, dialogikonsolikorkeus, paaKonsoli, 0, karttakorkeus);
            RLConsole.Blit(inventaarioKonsoli, 0, 0, sivukonsolileveys, konsolikorkeuspuolet, paaKonsoli, karttaleveys, konsolikorkeuspuolet);
            RLConsole.Blit(statsiKonsoli, 0, 0, sivukonsolileveys, konsolikorkeuspuolet, paaKonsoli, karttaleveys, 0);
            peliKartta.PiirraKartta(karttaKonsoli,statsiKonsoli,inventaarioKonsoli);
            ViestiLoki.Piirra(dialogiKonsoli);
            Pelaaja.Piirra(karttaKonsoli, peliKartta);
            Pelaaja.PiirraStatsit(statsiKonsoli);
            paaKonsoli.Draw();
        }
    }
}
