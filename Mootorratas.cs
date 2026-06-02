using System;
using System.Collections.Generic;
using System.Text;

namespace Soidukite_liidese_rakendamine_C__keeles
{
    public class Mootorratas : ISõiduk
    {
        public string Mudel { get; set; }
        public double Kütusekulu100km { get; set; }
        public double Kilomeetrid { get; set; }

        public Mootorratas(string mudel, double kulu, double km)
        {
            Mudel = mudel;
            Kütusekulu100km = kulu;
            Kilomeetrid = km;
        }

        public double ArvutaKulu()
        {
            return (Kilomeetrid / 100) * Kütusekulu100km;
        }

        public double ArvutaVahemaa()
        {
            return Kilomeetrid;
        }

        public override string ToString()
        {
            return $"Mootorratas: {Mudel}, Vahemaa: {ArvutaVahemaa()} km, Kulu: {ArvutaKulu():F2} L";
        }
    }
}
