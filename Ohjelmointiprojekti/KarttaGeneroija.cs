using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Tämä luokka luo erilaisia karttoja, käyttäen GameMap-luokkaa
    /// </summary>
    public class KarttaGeneroija {
        private readonly int karttaLeveys;
        private readonly int karttaKorkeus;
        private PeliKartta kartta;

        /// <summary>
        /// Generoijan konstruktori
        /// </summary>
        /// <param name="leveys">Kartan leveys tiileinä</param>
        /// <param name="korkeus">Kartan korkeus tiileinä</param>
        public KarttaGeneroija(int leveys, int korkeus) {
            karttaLeveys = leveys;
            karttaKorkeus = korkeus;
            kartta = new PeliKartta();
        }

        /// <summary>
        /// Testikartta testausta varten
        /// </summary>
        /// <returns>Palauttaa valmiin testikartan</returns>
        public PeliKartta TestiKartta() {
            kartta = new PeliKartta {
                id = 0
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys/2, karttaKorkeus/2, 12)) {
                kartta.SetCellProperties(solu.X, solu.Y, false, false, true);
            }
            foreach (Cell solu in kartta.GetCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2, 11)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.LightGray, RLColor.Black, '.'));
            }
            kartta.SetCellProperties(karttaLeveys / 2, karttaKorkeus-14, false, false, true);
            kartta.Ovet.Add(new Ovi { X = karttaLeveys / 2 , Y = ((karttaKorkeus / 2) - 12), Auki = false});
            kartta.Esineet.Add(new Kolikko(1, (karttaLeveys / 2)-3, (karttaKorkeus/2)-3));
            kartta.Esineet.Add(new Huppu(1, (karttaLeveys / 2) + 3, (karttaKorkeus / 2) - 7));
            kartta.Esineet.Add(new Leipa(1, (karttaLeveys / 2) + 5, (karttaKorkeus / 2) - 5));
            kartta.Esineet.Add(new Kuusieni(2, (karttaLeveys / 2) + 7, ((karttaKorkeus / 2) + 10)));
            kartta.Esineet.Add(new Jousi(1, (karttaLeveys / 2) - 5, (karttaKorkeus / 2) - 8));
            kartta.Esineet.Add(new Nuoli(1, (karttaLeveys / 2) - 2, (karttaKorkeus / 2) -10));
            DialogiNoodi testidialogi1 = new DialogiNoodi("Hello how are you", "1. I am fine 2. I am not fine", new (int, Action)[] { (1, null), (2, null) });
            DialogiNoodi testidialogi2 = new DialogiNoodi("Well good.", "1. Good bye.", new (int, Action)[] { (-1, null) });
            DialogiNoodi testidialogi3 = new DialogiNoodi("Well that's unfortunate.", "1. Good bye", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] testidialogitaulukko = new DialogiNoodi [] { testidialogi1, testidialogi2, testidialogi3 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) - 3, ((karttaKorkeus / 2) +2), "test", 'T', RLColor.Blue, testidialogitaulukko, true));
            kartta.LisaaVastustaja(new Vastustaja((karttaLeveys / 2) + 3, ((karttaKorkeus / 2) +4), "enemy", 'E', RLColor.Red, 5,1,1,1,true));
            return kartta;
        }

        /// <summary>
        /// Tyhjä kartta jos pelaaja kuolee
        /// </summary>
        /// <returns>Palauttaa tyhjän kartan</returns>
        public PeliKartta TyhjaKartta() {
            kartta = new PeliKartta {
                id = 1
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            return kartta;
        }

        /// <summary>
        /// Kartta josta peli alkaa
        /// </summary>
        /// <returns>Palauttaa valmiin aloituskartan</returns>
        public PeliKartta AloitusKartta() {
            kartta = new PeliKartta {
                id = 2
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            foreach (Cell solu in kartta.GetCellsInRectangle(karttaKorkeus/2, karttaLeveys / 2-2, 5, karttaKorkeus / 2)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, true, true, RLColor.LightRed, RLColor.Black, '.'));
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2-4, 3)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Magenta, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2 - 4, 2)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Brown, RLColor.Black, '|'));
            }
            kartta.Ovet.Add(new Ovi { X = karttaLeveys / 2, Y = ((karttaKorkeus / 2) - 1), Auki = false });
            DialogiNoodi ennustajaDialogi0 = new DialogiNoodi("Greetings. I have awaited your arrival. You may not know why you came here, but fate guided your steps.", "1. Who are you? 2. What do you want from me?", new (int, Action)[] { (1, null), (2, null) });
            DialogiNoodi ennustajaDialogi1 = new DialogiNoodi("I am a gypsy and a fortune teller. My name matters not.", "1. What do you want from me?", new (int, Action)[] { (2, null) });
            DialogiNoodi ennustajaDialogi2 = new DialogiNoodi("I shall read the path of your future. Let us begin the casting...", "1. ...", new (int, Action)[] { (3, null) });
            string napparyysVsVoimakkuus1 = "While visiting a castle, you see your host abusing his servant. What do you do?";
            string napparyysVsVoimakkuus2 = "1. I control my feelings and don't interfere rashly. 2. I kindly step in and defend the servant in the name of justice.";
            string alykkyysVsVoimakkuus1 = "On the road, you meet a beggar who smells of alcohol, begging you to give him coin. What do you do?";
            string alykkyysVsVoimakkuus2 = "1. I wisely refuse, knowing he will spend it all on drink. 2. I kindly give him my coin, for he needs it more than me.";
            DialogiNoodi ennustajaDialogi3 = new DialogiNoodi("While wandering in the woods, you find a cave with a sleeping dragon inside. What do you do?", "1. I wisely move on, knowing I cannot defeat the dragon. 2. I control my fears and sneak inside to kill the beast.", new (int, Action)[] { (4, Ohjelma.Pelaaja.LisaaAlykkyys), (5, Ohjelma.Pelaaja.LisaaNapparyys) });
            DialogiNoodi ennustajaDialogi4 = new DialogiNoodi(alykkyysVsVoimakkuus1, alykkyysVsVoimakkuus2, new (int, Action)[] { (6, Ohjelma.Pelaaja.LisaaAlykkyys), (6, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi ennustajaDialogi5 = new DialogiNoodi(napparyysVsVoimakkuus1, napparyysVsVoimakkuus2, new (int, Action)[] { (8, Ohjelma.Pelaaja.LisaaNapparyys), (8, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi ennustajaDialogi6 = new DialogiNoodi(napparyysVsVoimakkuus1, napparyysVsVoimakkuus2, new (int, Action)[] { (7, Ohjelma.Pelaaja.LisaaNapparyys), (7, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi ennustajaDialogi7 = new DialogiNoodi("Good. Now I can see your path clearly. Close your eyes, and awaken.", "1. I feel dizzy...", new (int, Action)[] { (-1, LataaLinna) });
            DialogiNoodi ennustajaDialogi8 = new DialogiNoodi(alykkyysVsVoimakkuus1, alykkyysVsVoimakkuus2, new (int, Action)[] { (7, Ohjelma.Pelaaja.LisaaAlykkyys), (7, Ohjelma.Pelaaja.LisaaVoimakkuus) });
            DialogiNoodi[] ennustajadialogitaulukko = new DialogiNoodi[] { ennustajaDialogi0, ennustajaDialogi1, ennustajaDialogi2, ennustajaDialogi3, ennustajaDialogi4, ennustajaDialogi5, ennustajaDialogi6, ennustajaDialogi7, ennustajaDialogi8};
            kartta.LisaaNPC(new NPC((karttaLeveys / 2), ((karttaKorkeus / 2) -5), "The Fortuneteller", 'f', RLColor.Blue, ennustajadialogitaulukko, false));
            for (int x = 0; x < karttaLeveys; x+=8) {
               for (int y = 0; y < karttaKorkeus; y += 3) {
                    kartta.Solut.Add(new Solu(x, y, false, false, true, RLColor.Green, RLColor.Black, 'T'));
                    kartta.SetCellProperties(x, y, false, false, true);
                }
            }
            
            return kartta;
        }

        /// <summary>
        /// Kartta linnaa varten, toimii pelaajan "tukikohtana"
        /// </summary>
        /// <returns>Palauttaa valmiin linnakartan</returns>
        public PeliKartta LinnaKartta() {
            kartta = new PeliKartta {
                id = 3
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2, karttaKorkeus - 40)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2, karttaKorkeus - 41)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.LightGray, RLColor.Black, '.'));
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2, karttaKorkeus / 2, karttaKorkeus - 55)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2 - 40, karttaKorkeus / 2, karttaKorkeus - 48)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsInSquare(karttaLeveys / 2 - 40, karttaKorkeus / 2, karttaKorkeus - 49)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.LightGray, RLColor.Black, '.'));
            }
            foreach (Cell solu in kartta.GetBorderCellsInSquare(karttaLeveys / 2 +40, karttaKorkeus / 2, karttaKorkeus - 48)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsInSquare(karttaLeveys / 2 + 40, karttaKorkeus / 2, karttaKorkeus - 49)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.LightGray, RLColor.Black, '.'));
            }
            foreach (Cell solu in kartta.GetCellsAlongLine(karttaLeveys / 2-23, karttaKorkeus / 2+9, karttaLeveys / 2+23, karttaKorkeus / 2+9)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsAlongLine(karttaLeveys / 2 - 9, karttaKorkeus / 2 - 23, karttaLeveys / 2 - 9, karttaKorkeus / 2 + 23)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsAlongLine(karttaLeveys / 2 - 23, karttaKorkeus / 2 - 9, karttaLeveys / 2 + 23, karttaKorkeus / 2 - 9)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsAlongLine(karttaLeveys / 2 + 9, karttaKorkeus / 2 - 23, karttaLeveys / 2 + 9, karttaKorkeus / 2 + 23)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsAlongLine(karttaLeveys / 2 - 40, karttaKorkeus / 2 +16, karttaLeveys / 2 - 40, karttaKorkeus / 2 -16)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsAlongLine(karttaLeveys / 2 + 40, karttaKorkeus / 2 + 16, karttaLeveys / 2 + 40, karttaKorkeus / 2 - 16)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, false, true, RLColor.Gray, RLColor.Black, '#'));
                kartta.SetCellProperties(solu.X, solu.Y, false, false, false);
            }
            foreach (Cell solu in kartta.GetCellsInRectangle(karttaKorkeus-7, karttaLeveys / 2 - 2, 5, 7)) {
                kartta.Solut.Add(new Solu(solu.X, solu.Y, false, true, true, RLColor.LightRed, RLColor.Black, '.'));
            }
            kartta.Solut.Add(new Solu(12, karttaKorkeus/2+12, false, false, true, RLColor.Red, RLColor.Black, 'X'));
            kartta.SetCellProperties(12, karttaKorkeus / 2 + 12, false, false, false);

            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) - 1, Y = karttaKorkeus - 8, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) + 1, Y = karttaKorkeus - 8, Auki = false });
            kartta.Ovet.Add(new Ovi { X = karttaLeveys / 2, Y = karttaKorkeus - 23, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) -24, Y = karttaKorkeus/2, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) + 24, Y = karttaKorkeus / 2, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) - 16, Y = karttaKorkeus - 23, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) + 16, Y = karttaKorkeus - 23, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) - 16, Y = karttaKorkeus / 2 - 9, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) + 16, Y = karttaKorkeus / 2 - 9, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) - 9, Y = karttaKorkeus / 2 +16, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) + 9, Y = karttaKorkeus / 2 + 16, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) - 9, Y = karttaKorkeus / 2 - 16, Auki = false });
            kartta.Ovet.Add(new Ovi { X = (karttaLeveys / 2) + 9, Y = karttaKorkeus / 2 - 16, Auki = false });
            kartta.Ovet.Add(new Ovi { X = karttaLeveys / 2 - 40, Y = karttaKorkeus / 2, Auki = false });
            kartta.Ovet.Add(new Ovi { X = karttaLeveys / 2 + 40, Y = karttaKorkeus / 2, Auki = false });

            kartta.Esineet.Add(new Huppu(1, (karttaLeveys / 2) -15, (karttaKorkeus / 2) +22));
            kartta.Esineet.Add(new KangasTakki(1, (karttaLeveys / 2) - 14, (karttaKorkeus / 2) + 22));
            kartta.Esineet.Add(new KangasHousut(1, (karttaLeveys / 2) - 13, (karttaKorkeus / 2) + 22));
            kartta.Esineet.Add(new Miekka(1, (karttaLeveys / 2) +25, (karttaKorkeus / 2) + 10));
            kartta.Esineet.Add(new Jousi(1, (karttaLeveys / 2) - 25, (karttaKorkeus / 2) + 10));
            kartta.Esineet.Add(new Nuoli(5, (karttaLeveys / 2) - 25, (karttaKorkeus / 2) - 10));

            DialogiNoodi narriDialogi0 = new DialogiNoodi("Ho eyo he hum, I am Chuckles! Everyone's favourite court jester! Wanna hear a joke?", "1. Yes 2. No thank you", new (int, Action)[] { (1, null), (1, Ohjelma.Pelaaja.VahennaSopuisuusKarma) });
            DialogiNoodi narriDialogi1 = new DialogiNoodi("What do you call two witches who live together? Broom mates!", "1. That was funny 2. No more jokes please", new (int, Action)[] { (2, Ohjelma.Pelaaja.LisaaSopuisuusKarma), (2, null) });
            DialogiNoodi narriDialogi2 = new DialogiNoodi("What happened to the King's seat? It was throne out!", "1. Alright, enough jokes 2. I beg you to stop", new (int, Action)[] { (3, null), (3, null) });
            DialogiNoodi narriDialogi3 = new DialogiNoodi("I tried reading a book about a castle with the drawbridge up... but it was impossible to get into!", "1. Guards! This man has gone mad!", new (int, Action)[] { (4, null) });
            DialogiNoodi narriDialogi4 = new DialogiNoodi("Ho eyo he hum! It is my job to be mad! Maddeningly funny! Haha!", "1. I think I've had enough of you.", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] narridialogitaulukko = new DialogiNoodi[] { narriDialogi0, narriDialogi1, narriDialogi2, narriDialogi3, narriDialogi4 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) -2, karttaKorkeus - 16, "Chuckles", 'c', RLColor.LightGreen, narridialogitaulukko, true));

            DialogiNoodi palvelustyttoDialogi0 = new DialogiNoodi("Hello there, I am the King's maid, Amelina. How may I serve you?", "1. Can I borrow these clothes?", new (int, Action)[] { (1, null) });
            DialogiNoodi palvelustyttoDialogi1 = new DialogiNoodi("Oh, of course. Your current clothes look very strange. Are you from another land?", "1. Yes 2. No", new (int, Action)[] { (2, Ohjelma.Pelaaja.LisaaViisausKarma), (3, Ohjelma.Pelaaja.VahennaViisausKarma) });
            DialogiNoodi palvelustyttoDialogi2 = new DialogiNoodi("Well then, you should talk to the King. He can provide you guidance.", "1. Alright, thank you", new (int, Action)[] { (-1, null) });
            DialogiNoodi palvelustyttoDialogi3 = new DialogiNoodi("Really? Then you must be a jester. Talk to Chuckles, you two should get along well.", "1. Uh, okay...", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] palvelustyttodialogitaulukko = new DialogiNoodi[] { palvelustyttoDialogi0, palvelustyttoDialogi1, palvelustyttoDialogi2, palvelustyttoDialogi3 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) - 18, karttaKorkeus - 11, "Amelina", 'a', RLColor.Yellow, palvelustyttodialogitaulukko, true));

            NPC maagi = new NPC((karttaLeveys / 2) - 18, (karttaKorkeus / 2) - 15, "Magnus", 'm', RLColor.Blue, new DialogiNoodi[] { }, true) {
                Inventaario = new List<Esine> { new Kuusieni(5, 1, 1) }
            };
            DialogiNoodi velhoDialogi0 = new DialogiNoodi("Oh, greetings, stranger! I am Magnus, the court magician in service to the King.", "1. How does magic work?", new (int, Action)[] { (1, null) });
            DialogiNoodi velhoDialogi1 = new DialogiNoodi("That is a question I hear quite often. Magic in Brythonn requires reagents, which provide power for spells.", "1. Where can I get reagents?", new (int, Action)[] { (2, null) });
            DialogiNoodi velhoDialogi2 = new DialogiNoodi("Well you can get them from me! I am selling Moon Mushrooms, one coin each.", "1. I would like to buy one 2. No thank you", new (int, Action)[] { (3, maagi.MyyEsine), (4, null) });
            DialogiNoodi velhoDialogi3 = new DialogiNoodi("These mushrooms are handy for the healing spell of the first Circle.", "1. Thank you and good bye", new (int, Action)[] { (-1, null)});
            DialogiNoodi velhoDialogi4 = new DialogiNoodi("Perhaps some other time then. Now if you excuse me, I have magical studies to conduct.", "1. Good bye", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] velhodialogitaulukko = new DialogiNoodi[] { velhoDialogi0, velhoDialogi1, velhoDialogi2, velhoDialogi3, velhoDialogi4 };
            maagi.HahmonDialogi = velhodialogitaulukko;
            kartta.LisaaNPC(maagi);

            NPC kokki = new NPC((karttaLeveys / 2) + 18, (karttaKorkeus / 2) - 15, "Elthon", 'e', RLColor.LightRed, new DialogiNoodi[] { }, true) {
                Inventaario = new List<Esine> { new Leipa(3, 1, 1) }
            };
            DialogiNoodi kokkiDialogi0 = new DialogiNoodi("Howdy, good to meet you. I am Elthon, the King's chef, responsible for feeding the court.", "1. What food do you make?", new (int, Action)[] { (1, null) });
            DialogiNoodi kokkiDialogi1 = new DialogiNoodi("Usually I bake bread, unless there is a celebration and fancier foods are needed. Would you like to buy some for a coin?", "1. Sure 2. No thanks", new (int, Action)[] { (2, kokki.MyyEsine), (2, null) });
            DialogiNoodi kokkiDialogi2 = new DialogiNoodi("It brings me great pleasure to provide sustenance for others. Now I must return to my work.", "1. Farewell", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] kokkidialogitaulukko = new DialogiNoodi[] { kokkiDialogi0, kokkiDialogi1, kokkiDialogi2 };
            kokki.HahmonDialogi = kokkidialogitaulukko;
            kartta.LisaaNPC(kokki);

            DialogiNoodi soturiDialogi0 = new DialogiNoodi("Who's this weakling? Some stranger? Haven't seen you before.", "1. Can I have a sword?", new (int, Action)[] { (1, null) });
            DialogiNoodi soturiDialogi1 = new DialogiNoodi("You're unarmed? Sure, it is dangerous to go alone. Grab one, though your arms seem too weak to lift it.", "1. How dare you! 2. Thank you, good bye", new (int, Action)[] { (2, Ohjelma.Pelaaja.VahennaKontrolliKarma), (-1, Ohjelma.Pelaaja.LisaaKontrolliKarma) });
            DialogiNoodi soturiDialogi2 = new DialogiNoodi("What, you want a fight? I am the head of the King's guard, so it's a bad idea. Besides, I was just warning you.", "1. Oh, my apologies... good bye", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] soturidialogitaulukko = new DialogiNoodi[] { soturiDialogi0, soturiDialogi1, soturiDialogi2 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) + 27, karttaKorkeus/2, "Jorg", 'j', RLColor.Red, soturidialogitaulukko, true));

            DialogiNoodi metsastajaDialogi0 = new DialogiNoodi("Hello, stranger. I am Elfkind, the King's hunter and master of the bow.", "1. Can I borrow a bow and some arrows?", new (int, Action)[] { (1, null) });
            DialogiNoodi metsastajaDialogi1 = new DialogiNoodi("Yes, of course. You may also do target practice in the room behind me if you wish.", "1. Thank you and good bye", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] metsastajadialogitaulukko = new DialogiNoodi[] { metsastajaDialogi0, metsastajaDialogi1 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2) - 27, karttaKorkeus / 2, "Elfkind", 'e', RLColor.Green, metsastajadialogitaulukko, true));

            DialogiNoodi kuningasDialogi0 = new DialogiNoodi("Ah, at last you have arrived, stranger! I have been waiting for you. Your quest shall now begin.", "1. What quest? 2. Who are you?", new (int, Action)[] { (1, null), (2, null) });
            DialogiNoodi kuningasDialogi1 = new DialogiNoodi("The Quest to become a Paragon of Virtue! The demon Ea has been slain, and a Paragon will prevent him from returning.", "1. What's a Paragon?", new (int, Action)[] { (3, null) });
            DialogiNoodi kuningasDialogi2 = new DialogiNoodi("Hahah, of course, you're from another world so you don't know me. I am King Shulong, heir of Lord British.", "1. What quest were you talking about?", new (int, Action)[] { (1, null)});
            DialogiNoodi kuningasDialogi3 = new DialogiNoodi("A Paragon has mastered all three Virtues and become an exemplar of all that is good.", "1. Virtues?", new (int, Action)[] { (4, null) });
            DialogiNoodi kuningasDialogi4 = new DialogiNoodi("The traditional three Virtues of Brythonn are Wisdom, Control and Amity.", "1. Tell me about Wisdom 2. Tell me about Control 3. Tell me about Amity", new (int, Action)[] { (5, null), (6, null) , (7, null) });
            DialogiNoodi kuningasDialogi5 = new DialogiNoodi("Wisdom represents truth, reason and transcendence. This virtue is gained by being honest and reasonable.", "1. What were the other virtues? 2. How do I become a Paragon?", new (int, Action)[] { (4, null), (8, null) });
            DialogiNoodi kuningasDialogi6 = new DialogiNoodi("Control represents temperance, discipline and courage. This virtue is gained by controlling your emotions.", "1. What were the other virtues? 2. How do I become a Paragon?", new (int, Action)[] { (4, null), (8, null) });
            DialogiNoodi kuningasDialogi7 = new DialogiNoodi("Amity represents humanity, justice and cooperation. This virtue is gained by being good to others.", "1. What were the other virtues? 2. How do I become a Paragon?", new (int, Action)[] { (4, null), (8, null) });
            DialogiNoodi kuningasDialogi8 = new DialogiNoodi("You must venture into three dungeons and find the Shrines of each Virtue. Medidate at each shrine, and you will ascend.", "1. Can you tell me about the virtues again? 2. Where are the dungeons?", new (int, Action)[] { (4, null), (9, null) });
            DialogiNoodi kuningasDialogi9 = new DialogiNoodi("You will find them in the wilds by venturing outside the castle. My courtiers will provide you with everything you need.", "1. I will not disappoint you, King. Good bye", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] kuningasadialogitaulukko = new DialogiNoodi[] { kuningasDialogi0, kuningasDialogi1, kuningasDialogi2, kuningasDialogi3, kuningasDialogi4, kuningasDialogi5, kuningasDialogi6, kuningasDialogi7, kuningasDialogi8, kuningasDialogi9 };
            kartta.LisaaNPC(new NPC(karttaLeveys / 2, karttaKorkeus / 2, "King Shulong d'Caix", 'K', RLColor.Magenta, kuningasadialogitaulukko, true));

            DialogiNoodi hevosDialogi0 = new DialogiNoodi("Neigh!", "1. What a fine steed 2. Hi, horse", new (int, Action)[] { (-1, null), (1, null) });
            DialogiNoodi hevosDialogi1 = new DialogiNoodi("\"Hi, horse?\" What is that supposed to be? Do you say \"Hi, human\" too?", "1. How can you talk?", new (int, Action)[] { (2, null)});
            DialogiNoodi hevosDialogi2 = new DialogiNoodi("How do you talk? Answer that first.", "1. Do you have a clue?", new (int, Action)[] { (3, null) });
            DialogiNoodi hevosDialogi3 = new DialogiNoodi("Clue? Oh, yes, a clue! Don't buy Ultima IX. You're welcome.", "1. Well that was over 20 years late. Bye", new (int, Action)[] { (-1, null) });
            DialogiNoodi[] hevosdialogitaulukko = new DialogiNoodi[] { hevosDialogi0, hevosDialogi1, hevosDialogi2, hevosDialogi3 };
            kartta.LisaaNPC(new NPC((karttaLeveys / 2), 3, "Smith the Horse", 'H', RLColor.Brown, hevosdialogitaulukko, false));

            return kartta;
        }

        public PeliKartta SaariKartta() {
            kartta = new PeliKartta {
                id = 4
            };
            kartta.Initialize(karttaLeveys, karttaKorkeus);
            foreach (Cell solu in kartta.GetAllCells()) {
                kartta.SetCellProperties(solu.X, solu.Y, true, true, false);
            }
            return kartta;
        }

        /// <summary>
        /// Lataa uuden kartan kun pelaaja menee tämänhetkisen kartan ulkopuolelle
        /// </summary>
        public void VaihdaKarttaa() {
            switch (Ohjelma.peliKartta.id) {
                case 3:
                    Ohjelma.peliKartta = SaariKartta();
                    Ohjelma.Pelaaja.X = karttaLeveys / 2;
                    Ohjelma.Pelaaja.Y = karttaKorkeus / 2;
                    Ohjelma.peliKartta.PaivitaNakoKentta();
                    break;
            }
        }

        /// <summary>
        /// Metodi linnakartan lataamista varten
        /// </summary>
        public void LataaLinna() {
            Ohjelma.ViestiLoki.Lisaa("You feel strange as your surroundings fade temporarily...");
            Ohjelma.peliKartta = LinnaKartta();
            Ohjelma.Pelaaja.X = karttaLeveys / 2;
            Ohjelma.Pelaaja.Y = karttaKorkeus- 1;
            Ohjelma.peliKartta.PaivitaNakoKentta();
            Ohjelma.ViestiLoki.Lisaa("...Until you find yourself at an oddly familiar location.");
        }
    }
}
