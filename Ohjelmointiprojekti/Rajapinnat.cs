using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace Ohjelmointiprojekti {
    /// <summary>
    /// Ohjelman eri rajapinnat
    /// Tekijä: Daniel Juola
    /// Luotu: 26.2.20
    /// </summary>

    //Hahmojen rajapinta, sisältää nimen ja näköetäisyyden (kuinka monen ruudun päähän hahmo näkee)
    public interface IHahmo {
        string Nimi { get; set; }
        int Nakoetaisyys { get; set; }
    }
    //Rajapinta joka sisältää kaiken piirtämistä varten: Värin, merkin, koordinaattisijainnin ja metodin itse piirtämiselle
    public interface IPiirra {
        RLColor Vari { get; set; }
        char Merkki { get; set; }
        int X { get; set; }
        int Y { get; set; }

        void Piirra(RLConsole konsoli, IMap kartta);
    }
    //Rajapinta esineitä varten, joita hahmot voivat kertätä ja käyttää
    public interface IEsine {
        string Nimi { get; set; }
        int Maara { get; set; }
        bool KaytaEsine();
        bool OtaEsine(Pelaaja pelaaja);
    }
}
