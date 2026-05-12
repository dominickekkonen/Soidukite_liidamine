using System;
using System.Collections.Generic;
using System.Text;
using static Soidukite_liidese_rakendamine_C__keeles.Program;

namespace Soidukite_liidese_rakendamine_C__keeles
{
    public enum Rattatüüp
    {
        Maantee,
        Maastik,
        Linn,
        Hübriid,
        Elektriline
    }
    public class Jalgratas : ISõiduk
    {
        public Rattatüüp Tüüp { get; set; } // Use the Enum instead of string
        public double Kilomeetrid { get; set; }

        public Jalgratas(Rattatüüp tüüp, double km)
        {
            Tüüp = tüüp;
            Kilomeetrid = km;
        }

        // Jalgratas kütust ei kuluta
        public double ArvutaKulu()
        {
            return 0;
        }

        public double ArvutaVahemaa()
        {
            return Kilomeetrid;
        }

        public override string ToString()
        {
            return $"Jalgratas: {Tüüp}, Vahemaa: {Kilomeetrid} km";
        }
    }
}
