using System;
using System.Collections.Generic;
using System.Text;

namespace Soidukite_liidese_rakendamine_C__keeles
{
    public class Auto : ISõiduk
    {
        public string Mudel { get; set; }
        public double Kütusekulu100km { get; set; }
        public double Kilomeetrid { get; set; }

        // Konstruktor parameetritega
        public Auto(string mudel, double kütusekulu, double km)
        {
            Mudel = mudel;
            Kütusekulu100km = kütusekulu;
            Kilomeetrid = km;

        }

        // Arvutab kütusekulu vastavalt distantsile
        public double ArvutaKulu()
        {
            return (Kilomeetrid / 100) * Kütusekulu100km;
        }

        public double ArvutaVahemaa()
        {
            return Kilomeetrid;
        }

        // Ülekirjutatud ToString() info kuvamiseks
        public override string ToString()
        {
            return $"Auto: {Mudel}, Vahemaa: {ArvutaVahemaa()} km, Kütusekulu: {ArvutaKulu():F2} L";
        }
    }
}
