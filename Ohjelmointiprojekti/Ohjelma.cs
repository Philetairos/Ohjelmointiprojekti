using System;
using System.IO;
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
        private static bool render = true;

        //Luo kaikki konsolit
        private static RLRootConsole paaKonsoli;
        private static RLConsole karttaKonsoli, dialogiKonsoli, inventaarioKonsoli, statsiKonsoli, valikkoKonsoli;

        //Dialogia varten
        private static bool talkMoodi = false;
        private static NPC dialogiNPC = new NPC();
        private static Vastustaja vihollinen = new Vastustaja();
        private static bool dialogi = false;

        //Kontrolleja varten
        private static bool getMoodi = false;
        private static bool kaytaEsine = false;
        private static bool poistaVaruste = false;
        private static bool poistaEsine = false;
        private static bool attackMoodi = false;
        private static bool lookMoodi = false;
        private static bool shootMoodi = false;
        private static bool magicMoodi = false;
        private static bool circleOne = false;

        //Vain aloitusvalikkoa varten
        private static int valittuVaihtoehto = 1;
        private static int valittuTallennus = 1;
        private static string[] tiedostot;

        readonly static SiirraHahmo liikuttaja = new SiirraHahmo();
        private static int liikkumislaskuri = 0;

        public static KarttaGeneroija karttaGeneroija;
        public static PeliKartta peliKartta;
        public static Pelaaja Pelaaja { get; set; }
        public static KomentoKasittelija KomentoKasittelija { get; private set; }
        public static Viestiloki ViestiLoki { get; private set; }

        private readonly static string Kontrollit1 = "Controls: Arrow Keys - Move, T - Talk, G - Get, A - Attack, S - Shoot, U - Use, L - Look, M - Use Magic,";
        private readonly static string Kontrollit2 = "R - Remove equipment, D - Drop Item, C - Controls, Esc - Exit game";

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
            //Luo konsoli aloitusvalikolle
            valikkoKonsoli = new RLConsole(sivukonsolileveys, konsolikorkeuspuolet);
            //luo karttageneroija joka luo datan pelikartoille
            karttaGeneroija = new KarttaGeneroija(karttaleveys,karttakorkeus);
            //Luo pelaajan hahmo
            Pelaaja = new Pelaaja(karttaleveys/2, karttakorkeus/2);
            //Luo käsittelijä pelaajan komennoille
            KomentoKasittelija = new KomentoKasittelija();
            //Luo viestiloki
            ViestiLoki = new Viestiloki();

            paaKonsoli.Update += PaivitaValikko;
            paaKonsoli.Render += PiirraValikko;
            paaKonsoli.Run();
        }
        //Suorita suunnasta riippuvat komennot
        private static void Suorita(Suunta suunta) {
            if (talkMoodi == true) {
                dialogiNPC = KomentoKasittelija.GetNPC(suunta);
                if (dialogiNPC != null) {
                    dialogi = true;
                    ViestiLoki.Lisaa(dialogiNPC.HahmonDialogi[0].dialogi);
                    ViestiLoki.Lisaa(dialogiNPC.HahmonDialogi[0].vastaukset);
                }
                talkMoodi = false;
            }
            else if (attackMoodi == true) {
                vihollinen = KomentoKasittelija.GetVastustaja(suunta);
                if (vihollinen != null) {
                    KomentoKasittelija.Hyokkaa(Pelaaja, vihollinen);
                }
                attackMoodi = false;
            }
            else if (getMoodi == true) {
                Esine otettavaEsine = KomentoKasittelija.GetEsine(suunta);
                if (otettavaEsine != null) {
                    Pelaaja.LisaaEsine(otettavaEsine);
                }
                getMoodi = false;
            }
            else if (shootMoodi == true) {
                if(Pelaaja.Varusteet[4] != null && Pelaaja.Varusteet[4].VoiAmpua == true) {
                    foreach(Ammus ammus in Pelaaja.Inventaario) {
                        if (ammus.Maara == 1) {
                            Pelaaja.Inventaario.Remove(ammus);
                        }
                        else {
                            ammus.Maara--;
                        }
                        shootMoodi = KomentoKasittelija.Ammu(suunta, ammus);
                        return;
                    }
                    ViestiLoki.Lisaa("You need ammunition to shoot.");
                }
                else {
                    ViestiLoki.Lisaa("You need to wield a ranged weapon to shoot.");
                }
                shootMoodi = false;
            }
            else if (lookMoodi == true) {
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
            else {
                bool siirtyma = KomentoKasittelija.SiirraPelaaja(suunta);
            }
        }
        //Käsittele pelaajan syöte päävalikossa
        private static void PaivitaValikko(object sender, UpdateEventArgs e) {
            RLKeyPress nappain = paaKonsoli.Keyboard.GetKeyPress();
            if (nappain == null) {
                return;
            }
            else {
                render = true;
            }
            if (valittuVaihtoehto == 1) {
                switch (nappain.Key) {
                    case RLKey.Down:
                        valittuVaihtoehto = 2;
                        break;
                    case RLKey.Enter:
                        paaKonsoli.Update -= PaivitaValikko;
                        paaKonsoli.Render -= PiirraValikko;

                        //Luo aloituskartta
                        ViestiLoki.Lisaa(Kontrollit1);
                        ViestiLoki.Lisaa(Kontrollit2);
                        peliKartta = karttaGeneroija.TestiKartta();
                        peliKartta.PaivitaNakoKentta();
                        paaKonsoli.Update += PaivitaKonsoli;
                        paaKonsoli.Render += PiirraKonsoli;
                        break;
                }
            }
            else if (valittuVaihtoehto == 2) {
                switch (nappain.Key) {
                    case RLKey.Down:
                        valittuVaihtoehto = 3;
                        break;
                    case RLKey.Up:
                        valittuVaihtoehto = 1;
                        break;
                    case RLKey.Enter:
                        valittuVaihtoehto = 4;
                        tiedostot = Directory.GetFiles("C:\\Users\\Daniel Juola\\Documents\\Yliopistotavaraa\\kurssit\\TIEA306\\Git\\Ohjelmointiprojekti\\Tallennukset");
                        if (tiedostot.Length == 0) {
                            tiedostot = new string[] { "No journeys started yet!" };
                        }
                        break;
                }
            }
            else if (valittuVaihtoehto == 3) {
                switch (nappain.Key)
                {
                    case RLKey.Up:
                        valittuVaihtoehto = 2;
                        break;
                    case RLKey.Enter:
                        paaKonsoli.Close();
                        break;
                }
            }
            else if (valittuVaihtoehto == 4) {
                switch (nappain.Key) {
                    case RLKey.Up:
                        if (valittuTallennus > 1) {
                            valittuTallennus--;
                        }
                        break;
                    case RLKey.Down:
                        valittuTallennus++;
                        if (valittuTallennus > tiedostot.Length) {
                            valittuVaihtoehto = 5;
                        }
                        break;
                    case RLKey.Enter:
                        
                        break;
                }
            }
            else if (valittuVaihtoehto == 5) {
                switch (nappain.Key) {
                    case RLKey.Up:
                        valittuVaihtoehto = 4;
                        valittuTallennus = tiedostot.Length;
                        break;
                    case RLKey.Enter:
                        valittuVaihtoehto = 1;
                        break;
                }
            }
        }
        
        //Käsittele pelaajan syöte muulloin
        private static void PaivitaKonsoli(object sender, UpdateEventArgs e) {
            RLKeyPress nappain = paaKonsoli.Keyboard.GetKeyPress();
            if (nappain == null) {
                return;
            }
            else {
                render = true;
            }
            if (dialogi == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4) {
                    dialogi = dialogiNPC.Dialogi(Int32.Parse(nappain.Char.ToString()));
                }
            }
            else if (kaytaEsine == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4) {
                    int num = Int32.Parse(nappain.Char.ToString());
                    if (num > Pelaaja.Inventaario.Count) {
                        ViestiLoki.Lisaa("No item in that slot!");
                    }
                    else {
                        kaytaEsine = Pelaaja.Inventaario[num - 1].KaytaEsine();
                    }
                }
                kaytaEsine = false;
            }
            else if (magicMoodi == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3) {
                    int num = Int32.Parse(nappain.Char.ToString());
                    if (num > Pelaaja.Taso) {
                        ViestiLoki.Lisaa("You need a higher level for this Circle!");
                        magicMoodi = false;
                    }
                    else {
                        ViestiLoki.Lisaa("Which spell do you want to cast?");
                        ViestiLoki.Lisaa("1. Heal (Moon mushroom)");
                        magicMoodi = false;
                        circleOne = true;
                    }
                }
            }
            else if (circleOne == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3) {
                    int num = Int32.Parse(nappain.Char.ToString());
                    switch (num) {
                        case 1:
                            foreach (Reagenssi reagenssi in Pelaaja.Inventaario) {
                                if(reagenssi.Nimi == "Moon Mushroom") {
                                    if (reagenssi.Maara == 1) {
                                        Pelaaja.Inventaario.Remove(reagenssi);
                                    }
                                    else {
                                        reagenssi.Maara--;
                                    }
                                    Pelaaja.Elama += 10;
                                    if (Pelaaja.Elama > 10+Pelaaja.Taso * 10) {
                                        Pelaaja.Elama = 10 + Pelaaja.Taso * 10;
                                    }
                                    ViestiLoki.Lisaa("MANI!");
                                    ViestiLoki.Lisaa("You cast Heal.");
                                    circleOne = false;
                                    return;
                                }
                            }
                            ViestiLoki.Lisaa("You need 1 Moon Mushroom to cast this!");
                            break;
                        case 2:
                            
                            break;
                        case 3:
                            
                            break;
                    }
                    circleOne = false;
                }
            }
            else if (poistaVaruste == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4 || nappain.Key == RLKey.Number5) {
                    int num = Int32.Parse(nappain.Char.ToString());
                    poistaEsine = Pelaaja.PoistaVaruste(num);
                }
            }
            else if (poistaEsine == true) {
                if (nappain.Key == RLKey.Number1 || nappain.Key == RLKey.Number2 || nappain.Key == RLKey.Number3 || nappain.Key == RLKey.Number4) {
                    int num = Int32.Parse(nappain.Char.ToString());
                    if (num > Pelaaja.Inventaario.Count) {
                        ViestiLoki.Lisaa("No item in that slot!");
                        poistaEsine = false;
                    }
                    else {
                        poistaEsine = Pelaaja.PoistaEsine(num - 1);
                    }
                }
            }
            else {
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
                        ViestiLoki.Lisaa("Look: Press an arrow key to choose direction");
                        return;
                    case RLKey.G:
                        getMoodi = true;
                        ViestiLoki.Lisaa("Get: Press an arrow key to choose direction");
                        return;
                    case RLKey.T:
                        talkMoodi = true;
                        ViestiLoki.Lisaa("Talk: Press an arrow key to choose direction");
                        return;
                    case RLKey.A:
                        attackMoodi = true;
                        ViestiLoki.Lisaa("Attack: Press an arrow key to choose direction");
                        return;
                    case RLKey.S:
                        shootMoodi = true;
                        ViestiLoki.Lisaa("Shoot: Press an arrow key to choose direction");
                        return;
                    case RLKey.U:
                        kaytaEsine = true;
                        ViestiLoki.Lisaa("Use: Press a number key to choose item");
                        return;
                    case RLKey.M:
                        magicMoodi = true;
                        ViestiLoki.Lisaa("Magic: Press a number key to choose Circle (1-3)");
                        return;
                    case RLKey.R:
                        poistaVaruste = true;
                        ViestiLoki.Lisaa("Remove Equipment: Press a number key to choose item");
                        return;
                    case RLKey.D:
                        poistaEsine = true;
                        ViestiLoki.Lisaa("Drop: Press a number key to choose item");
                        return;
                    case RLKey.C:
                        ViestiLoki.Lisaa(Kontrollit1);
                        ViestiLoki.Lisaa(Kontrollit2);
                        return;
                    case RLKey.Escape:
                        paaKonsoli.Close();
                        break;
                    default:
                        break;
                }
                foreach (Vastustaja vastustaja in peliKartta.Vastustajat) {
                    if (vastustaja.Liikkuu == true) {
                        liikuttaja.LiikuKohtiPelaajaa(vastustaja);
                    }
                }
                liikkumislaskuri++;
                if (liikkumislaskuri%3 == 0) {
                    foreach (NPC hahmo in peliKartta.NPCs) {
                        if (hahmo.Liikkuu == true && dialogi == false) {
                            liikuttaja.LiikuRandom(hahmo);
                        }
                    }
                }
                else if (liikkumislaskuri == 25) {
                    Pelaaja.Nalka--;
                    if (Pelaaja.Nalka <= 0) {
                        ViestiLoki.Lisaa("You are starving!");
                        Pelaaja.Elama -= 1;
                        if (Pelaaja.Elama <= 0) {
                            Pelaaja.KasitteleKuolema(peliKartta);
                        }
                    }
                    liikkumislaskuri = 0;
                }
            }
            
        }
        //Piirtometodi aloitusvalikolle
        private static void PiirraValikko(object sender, UpdateEventArgs e) {
            RLConsole.Blit(valikkoKonsoli, 0, 0, konsolileveys / 2, konsolikorkeuspuolet, paaKonsoli, konsolileveys / 2-10, konsolikorkeuspuolet-10);
            paaKonsoli.Draw();
            if (render) {
                valikkoKonsoli.Clear();
                valikkoKonsoli.Print(0,0, "Paragon of Virtue 0.1", RLColor.LightBlue);
                if (valittuVaihtoehto == 1) {
                    valikkoKonsoli.Print(0, 5, "Start your journey", RLColor.LightCyan);
                    valikkoKonsoli.Print(0, 7, "Continue your journey", RLColor.Cyan);
                    valikkoKonsoli.Print(0, 9, "End your journey... for now", RLColor.Cyan);
                }
                else if (valittuVaihtoehto == 2) {
                    valikkoKonsoli.Print(0, 5, "Start your journey", RLColor.Cyan);
                    valikkoKonsoli.Print(0, 7, "Continue your journey", RLColor.LightCyan);
                    valikkoKonsoli.Print(0, 9, "End your journey... for now", RLColor.Cyan);
                }
                else if (valittuVaihtoehto == 3) {
                    valikkoKonsoli.Print(0, 5, "Start your journey", RLColor.Cyan);
                    valikkoKonsoli.Print(0, 7, "Continue your journey", RLColor.Cyan);
                    valikkoKonsoli.Print(0, 9, "End your journey... for now", RLColor.LightCyan);
                }
                else if (valittuVaihtoehto == 4) {
                    valikkoKonsoli.Print(0, 5, "Unfinished Journeys:", RLColor.Cyan);
                    int i = 7;
                    foreach (string tiedosto in tiedostot) {
                        if ((i/2)-2 == valittuTallennus) {
                            valikkoKonsoli.Print(0, i, System.IO.Path.GetFileNameWithoutExtension(tiedosto), RLColor.LightCyan);
                        }
                        else {
                            valikkoKonsoli.Print(0, i, System.IO.Path.GetFileNameWithoutExtension(tiedosto), RLColor.Cyan);
                        }
                        i += 2;
                    }
                    valikkoKonsoli.Print(0, i, "Return", RLColor.Cyan);
                }
                else if (valittuVaihtoehto == 5) {
                    valikkoKonsoli.Print(0, 5, "Unfinished Journeys:", RLColor.Cyan);
                    int i = 7;
                    foreach (string tiedosto in tiedostot) {
                        valikkoKonsoli.Print(0, i, System.IO.Path.GetFileNameWithoutExtension(tiedosto), RLColor.Cyan);
                        i += 2;
                    }
                    valikkoKonsoli.Print(0, i, "Return", RLColor.LightCyan);
                }
            }
        }
        //Piirometodi pelille
        private static void PiirraKonsoli(object sender, UpdateEventArgs e) {
            RLConsole.Blit(karttaKonsoli, 0, 0, karttaleveys, karttakorkeus, paaKonsoli, 0, 0);
            RLConsole.Blit(dialogiKonsoli, 0, 0, karttaleveys, dialogikonsolikorkeus, paaKonsoli, 0, karttakorkeus);
            RLConsole.Blit(inventaarioKonsoli, 0, 0, sivukonsolileveys, konsolikorkeuspuolet, paaKonsoli, karttaleveys, konsolikorkeuspuolet);
            RLConsole.Blit(statsiKonsoli, 0, 0, sivukonsolileveys, konsolikorkeuspuolet, paaKonsoli, karttaleveys, 0);

            paaKonsoli.Draw();
            if (render) {
                Pelaaja.PiirraStatsit(statsiKonsoli);
                peliKartta.PiirraKartta(karttaKonsoli, statsiKonsoli, inventaarioKonsoli);
                ViestiLoki.Piirra(dialogiKonsoli);
                Pelaaja.Piirra(karttaKonsoli, peliKartta);
                render = false;
            }
        }
    }
}
