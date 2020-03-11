using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

//Ohjelman päätiedosto, Daniel Juola 
//Perustuu Faron Bracyn esimerkkikoodiin
//Projektissa käytetyt kirjastot: RLNet (Travis M. Clark, 2014) ja RogueSharp (Faron Bracy, 2014-2019), MIT-lisenssi

namespace Ohjelmointiprojekti {
    class Program {
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
        private static bool talkmoodi = false;
        private static NPC dialoginpc = new NPC();
        private static bool dialogi = false;

        public static GameMap peliKartta;
        public static Player Pelaaja {
            get;
            set;
        }
        public static CommandSystem KomentoKasittelija {
            get;
            private set;
        }
        public static MessageLog ViestiLoki {
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
            karttaKonsoli.SetBackColor(0, 0, karttaleveys, karttakorkeus, RLColor.Black);
            //Luo dialogikonsoli joka näyttää pelin viestit ja keskustelun dialogin
            dialogiKonsoli = new RLConsole(karttaleveys, dialogikonsolikorkeus);
            //Luo inventaatiokonsoli joka näyttää pelaajan esineet
            inventaarioKonsoli = new RLConsole(sivukonsolileveys, konsolikorkeuspuolet);
            inventaarioKonsoli.SetBackColor(0, 0, sivukonsolileveys, konsolikorkeuspuolet, RLColor.Blue);
            //Luo statistiikkakonsoli joka näyttää pelaajan hahmo(je)n tilan
            statsiKonsoli = new RLConsole(sivukonsolileveys, konsolikorkeuspuolet);
            MapGenerator karttaGeneroija = new MapGenerator(karttaleveys,karttakorkeus);
            //Luo pelaajan hahmo
            Pelaaja = new Player(karttaleveys/2, karttakorkeus-6);
            KomentoKasittelija = new CommandSystem();
            //Luo viestiloki
            ViestiLoki = new MessageLog();
            ViestiLoki.Lisaa("Nappaimet: T - Puhu, Nuolinappaimet - Liiku, Esc - Sulje");
            //Luo aloituskartta
            peliKartta = karttaGeneroija.TestiKartta();
            peliKartta.PaivitaNakoKentta();
            paaKonsoli.Update += PaivitaKonsoli;
            paaKonsoli.Render += PiirraKonsoli;
            paaKonsoli.Run();
        }
        //Käsittele syöte
        private static void PaivitaKonsoli(object sender, UpdateEventArgs e) {
            RLKeyPress nappain = paaKonsoli.Keyboard.GetKeyPress();
            if (nappain != null && dialogi == false) {
                if (nappain.Key == RLKey.Up) {
                    if (talkmoodi == true) {
                        dialoginpc = KomentoKasittelija.Interaktio(Suunta.Ylos, ViestiLoki);
                        if (dialoginpc != null) {
                            dialogi = true;
                            dialoginpc.Dialogi(ViestiLoki, 0);
                        }
                        talkmoodi = false;
                    }
                    else {
                        bool siirtyma = KomentoKasittelija.SiirraPelaaja(Suunta.Ylos);
                    }
                }
                else if (nappain.Key == RLKey.Down) {
                    if (talkmoodi == true) {
                        dialoginpc = KomentoKasittelija.Interaktio(Suunta.Alas, ViestiLoki);
                        if (dialoginpc != null) {
                            dialogi = true;
                            dialoginpc.Dialogi(ViestiLoki, 0);
                        }
                        talkmoodi = false;
                    }
                    else {
                        bool siirtyma = KomentoKasittelija.SiirraPelaaja(Suunta.Alas);
                    }
                }
                else if (nappain.Key == RLKey.Left) {
                    if (talkmoodi == true) {
                        dialoginpc = KomentoKasittelija.Interaktio(Suunta.Vasen, ViestiLoki);
                        if (dialoginpc != null) {
                            dialogi = true;
                            dialoginpc.Dialogi(ViestiLoki, 0);
                        }
                        talkmoodi = false;
                    }
                    else {
                        bool siirtyma = KomentoKasittelija.SiirraPelaaja(Suunta.Vasen);
                    }
                }
                else if (nappain.Key == RLKey.Right) {
                    if (talkmoodi == true) {
                        dialoginpc = KomentoKasittelija.Interaktio(Suunta.Oikea, ViestiLoki);
                        if (dialoginpc != null) {
                            dialogi = true;
                            dialoginpc.Dialogi(ViestiLoki, 0);
                        }
                        talkmoodi = false;
                    }
                    else {
                        bool siirtyma = KomentoKasittelija.SiirraPelaaja(Suunta.Oikea);
                    }
                }
                else if (nappain.Key == RLKey.T) {
                    talkmoodi = true;
                    ViestiLoki.Lisaa("Puhu: Paina nuolinäppäintä");
                }
                else if (nappain.Key == RLKey.Escape) {
                    paaKonsoli.Close();
                }
            }
            else if (nappain != null && dialogi == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4) {
                    dialogi = dialoginpc.Dialogi(ViestiLoki, Int32.Parse(nappain.Char.ToString()));
                }
            }
        }
        //Piirrä kaikki ruudulle
        private static void PiirraKonsoli(object sender, UpdateEventArgs e) {
            RLConsole.Blit(karttaKonsoli, 0, 0, karttaleveys, karttakorkeus, paaKonsoli, 0, 0);
            RLConsole.Blit(dialogiKonsoli, 0, 0, karttaleveys, dialogikonsolikorkeus, paaKonsoli, 0, karttakorkeus);
            RLConsole.Blit(inventaarioKonsoli, 0, 0, sivukonsolileveys, konsolikorkeuspuolet, paaKonsoli, karttaleveys, konsolikorkeuspuolet);
            RLConsole.Blit(statsiKonsoli, 0, 0, sivukonsolileveys, konsolikorkeuspuolet, paaKonsoli, karttaleveys, 0);
            peliKartta.PiirraKartta(karttaKonsoli,statsiKonsoli);
            ViestiLoki.Piirra(dialogiKonsoli);
            Pelaaja.Piirra(karttaKonsoli, peliKartta);
            Pelaaja.PiirraStatsit(statsiKonsoli);
            paaKonsoli.Draw();
        }
    }
}
