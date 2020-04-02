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
        private static bool talkmoodi = false;
        private static NPC dialoginpc = new NPC();
        private static bool dialogi = false;

        private static bool getmoodi = false;
        private static bool kaytaEsine = false;

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
            ViestiLoki.Lisaa("Controls: Arrow Keys - Move, T - Talk, G - Get, U - Use, Esc - Exit game");
            //Luo aloituskartta
            peliKartta = karttaGeneroija.TestiKartta();
            peliKartta.PaivitaNakoKentta();
            paaKonsoli.Update += PaivitaKonsoli;
            paaKonsoli.Render += PiirraKonsoli;
            paaKonsoli.Run();
        }
        //Suorita suunnasta riippuvat komennot
        private static void Suorita(Suunta suunta) {
            if (talkmoodi == true)
            {
                dialoginpc = KomentoKasittelija.Interaktio(suunta);
                if (dialoginpc != null)
                {
                    dialogi = true;
                    ViestiLoki.Lisaa(dialoginpc.hahmonDialogi[0].dialogi);
                    ViestiLoki.Lisaa(dialoginpc.hahmonDialogi[0].vastaukset);
                }
                talkmoodi = false;
            }
            else if (getmoodi == true)
            {
                Esine otettavaEsine = KomentoKasittelija.Ota(suunta);
                if (otettavaEsine != null)
                {
                    Pelaaja.LisaaEsine(otettavaEsine);
                }
                getmoodi = false;
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
                if (nappain.Key == RLKey.Up) {
                    Suorita(Suunta.Ylos);
                }
                else if (nappain.Key == RLKey.Down) {
                    Suorita(Suunta.Alas);
                }
                else if (nappain.Key == RLKey.Left) {
                    Suorita(Suunta.Vasen);
                }
                else if (nappain.Key == RLKey.Right) {
                    Suorita(Suunta.Oikea);
                }
                else if (nappain.Key == RLKey.G) {
                    getmoodi = true;
                    ViestiLoki.Lisaa("Get: Press an arrow key");
                }
                else if (nappain.Key == RLKey.T) {
                    talkmoodi = true;
                    ViestiLoki.Lisaa("Talk: Press an arrow key");
                }
                else if (nappain.Key == RLKey.U)
                {
                    kaytaEsine = true;
                    ViestiLoki.Lisaa("Use: Press a number key");
                }
                else if (nappain.Key == RLKey.Escape) {
                    paaKonsoli.Close();
                }
                liikkumislaskuri++;
                if (liikkumislaskuri == 3) {
                    Pelaaja.Nalka--;
                    if (Pelaaja.Nalka <= 0) {
                        ViestiLoki.Lisaa("You are starving!");
                        Pelaaja.Elama-= 5;
                        if(Pelaaja.Elama <= 0) {
                            ViestiLoki.Lisaa("You have died!");
                            peliKartta = karttaGeneroija.TyhjaKartta();
                            //Lisää kunnon käsittely kuolemalle kun valikot lisätään
                            paaKonsoli.Update -= PaivitaKonsoli;
                        }
                    }
                    foreach (NPC hahmo in peliKartta.NPCs) {
                        if (hahmo.liikkuu == true && dialogi == false) {
                            liikuttaja.LiikuRandom(hahmo, KomentoKasittelija);
                        }
                    }
                    liikkumislaskuri = 0;
                }
            }
            else if (nappain != null && dialogi == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4) {
                    dialogi = dialoginpc.Dialogi(Int32.Parse(nappain.Char.ToString()));
                }
            }
            else if (nappain != null && kaytaEsine == true)
            {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4)
                {
                    kaytaEsine = Pelaaja.Inventaario[Int32.Parse(nappain.Char.ToString())-1].KaytaEsine();
                }
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
