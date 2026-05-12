using System;
using System.Collections.Generic;
using System.Text;

namespace Soidukite_liidese_rakendamine_C__keeles
{
    public class Buss : ISõiduk
    {
        public string Liin { get; set; }
        public double Kütusekulu100km { get; set; }
        public double Kilomeetrid { get; set; }
        public int ReisijateArv { get; set; }

        public Buss(string liin, double kulu, double km, int reisijad)
        {
            Liin = liin;
            Kütusekulu100km = kulu;
            Kilomeetrid = km;
            // Väldime nulliga jagamist, kui buss on tühi
            ReisijateArv = reisijad > 0 ? reisijad : 1;
        }

        // Arvutab kütusekulu ja jagab selle reisijate vahel
        public double ArvutaKulu()
        {
            double üldKulu = (Kilomeetrid / 100) * Kütusekulu100km;
            return üldKulu / ReisijateArv;
        }

        public double ArvutaVahemaa()
        {
            return Kilomeetrid;
        }

        public override string ToString()
        {
            return $"Buss ({Liin}), Vahemaa: {ArvutaVahemaa()} km, Kulu reisija kohta: {ArvutaKulu():F2} L";
        }
    }
}
