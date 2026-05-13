using System.Text;

namespace Soidukite_liidese_rakendamine_C__keeles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            List<ISõiduk> sõidukid = new List<ISõiduk>();
            LoeFailist(sõidukid);

            // Ask for gas price once at startup
            Console.CursorVisible = true;
            double kütuseHind = KüsiArv("Sisesta hetke kütuseühiku hind (€/L): ");
            Console.CursorVisible = false;

            string[] menüüValikud = {
        "Lisa Auto",
        "Lisa Jalgratas",
        "Lisa Buss",
        "Loe andmed failist",
        "Kuva tulemused ja kogukulu",
        "Salvesta andmed",
        "Välju"
    };

            int valitudIndeks = 0;
            bool programmJookseb = true;

            while (programmJookseb)
            {
                KuvaMenüü(menüüValikud, valitudIndeks);
                ConsoleKeyInfo klahv = Console.ReadKey(true);
                if (klahv.Key == ConsoleKey.UpArrow)
                {
                    valitudIndeks = (valitudIndeks == 0) ? menüüValikud.Length - 1 : valitudIndeks - 1;
                }
                else if (klahv.Key == ConsoleKey.DownArrow)
                {
                    valitudIndeks = (valitudIndeks == menüüValikud.Length - 1) ? 0 : valitudIndeks + 1;
                }
                else if (klahv.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.CursorVisible = true;

                    switch (valitudIndeks)
                    {
                        case 0: LisaAuto(sõidukid); break;
                        case 1: LisaJalgratas(sõidukid); break;
                        case 2: LisaBuss(sõidukid); break;
                        case 3: LoeFailist(sõidukid); break;
                        case 4: KuvaAndmed(sõidukid, kütuseHind); break;  // pass price
                        case 5:
                            SalvestaFaili(sõidukid);
                            Console.WriteLine("Andmed on salvestatud!");
                            break;
                        case 6:
                            SalvestaFaili(sõidukid);
                            Console.WriteLine("Andmed salvestatud. Nägemist!");
                            programmJookseb = false;
                            break;
                    }

                    if (programmJookseb)
                    {
                        Console.ReadKey(true);
                        Console.CursorVisible = false;
                    }
                }
            }
        }
        static void KuvaMenüü(string[] valikud, int valitudIndeks)
        {
            Console.Clear();
            Console.WriteLine("=== SÕIDUKITE HALDUSSÜSTEEM ===");

            for (int i = 0; i < valikud.Length; i++)
            {
                if (i == valitudIndeks)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {valikud[i]} <");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {valikud[i]}  ");
                }
            }
        }
        static void LisaAuto(List<ISõiduk> nimekiri)
        {
            Console.Write("Sisesta auto mudel: ");
            string mudel = Console.ReadLine();
            double kulu = KüsiArv("Sisesta keskmine kütusekulu (L/100km): ");
            double km = KüsiArv("Sisesta läbitud kilomeetrid: ");
            nimekiri.Add(new Auto(mudel, kulu, km));
            SalvestaFaili(nimekiri);
            Console.WriteLine("Auto on lisatud ja andmed on salvestatud!");
        }
        static void LisaJalgratas(List<ISõiduk> nimekiri)
        {
            Console.WriteLine("Vali ratta tüüp: 0 - Maantee, 1 - Maastik, 2 - Linn");
            int valik = (int)KüsiArv("Sisesta tüübi number: ");

            // Teisendame numbri enumiks
            Rattatüüp tüüp = (Rattatüüp)valik;

            double km = KüsiArv("Sisesta läbitud kilomeetrid: ");

            nimekiri.Add(new Jalgratas(tüüp, km));
            SalvestaFaili(nimekiri);
            Console.WriteLine("Jalgratas on lisatud ja andmed on salvestatud!");
        }
        static void LisaBuss(List<ISõiduk> nimekiri)
        {
            Console.Write("Sisesta bussi liin: ");
            string liin = Console.ReadLine();
            double kulu = KüsiArv("Sisesta bussi kütusekulu (L/100km): ");
            double km = KüsiArv("Sisesta läbitud kilomeetrid: ");
            int reisijad = (int)KüsiArv("Sisesta reisijate arv: ");

            nimekiri.Add(new Buss(liin, kulu, km, reisijad));
            SalvestaFaili(nimekiri);
            Console.WriteLine("Buss on lisatud ja andmed on salvestatud!");
        }
        static double KüsiArv(string küsimus)
        {
            while (true)
            {
                Console.Write(küsimus);
                if (double.TryParse(Console.ReadLine(), out double tulemus) && tulemus >= 0)
                {
                    return tulemus;
                }
                Console.WriteLine("Viga! Palun sisesta positiivne arv.");
            }
        }
        static void KuvaAndmed(List<ISõiduk> nimekiri, double kütuseHind)
        {
            if (nimekiri.Count == 0)
            {
                Console.WriteLine("\nNimekiri on tühi.");
                return;
            }

            Console.WriteLine("\n--- SÕIDUKITE TULEMUSED ---");
            Console.WriteLine($"Kütuseühiku hind: {kütuseHind:F2} €/L\n");
            double koguKulu = 0;

            foreach (var sõiduk in nimekiri)
            {
                Console.WriteLine(sõiduk.ToString());

                if (sõiduk is Auto auto)
                {
                    double liitrid = auto.ArvutaKulu();
                    double eurodes = liitrid * kütuseHind;
                    Console.WriteLine($"  Kütusekulu: {liitrid:F2} L  →  {eurodes:F2} €");
                }
                else if (sõiduk is Buss buss)
                {
                    double liitrid = buss.ArvutaKulu();
                    double eurodes = liitrid * kütuseHind;
                    double reisijaKoht = buss.ReisijateArv > 0 ? eurodes / buss.ReisijateArv : 0;
                    Console.WriteLine($"  Kütusekulu: {liitrid:F2} L  →  {eurodes:F2} €");
                    Console.WriteLine($"  Kulu reisija kohta: {reisijaKoht:F2} €  ({buss.ReisijateArv} reisijat)");
                }
                else if (sõiduk is Jalgratas)
                {
                    Console.WriteLine("  Kütusekulu puudub (jalgratas)");
                }

                Console.WriteLine();
                koguKulu += sõiduk.ArvutaKulu();
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine($"Kõikide sõidukite kütusekulu kokku: {koguKulu:F2} L");
            Console.WriteLine($"Kogumaksumus kütusele: {koguKulu * kütuseHind:F2} €");
        }
        static void LoeFailist(List<ISõiduk> nimekiri)
        {
            string failitee = "andmed.txt";
            if (!File.Exists(failitee)) return;

            try
            {
                string[] read = File.ReadAllLines(failitee);
                nimekiri.Clear(); // Tühjendame nimekirja, et ei tekiks duplikaate

                foreach (string rida in read)
                {
                    string[] osad = rida.Split(',');
                    string tüüp = osad[0].Trim();

                    if (tüüp == "Auto")
                        nimekiri.Add(new Auto(osad[1], double.Parse(osad[2]), double.Parse(osad[3])));
                    else if (tüüp == "Buss")
                        nimekiri.Add(new Buss(osad[1], double.Parse(osad[2]), double.Parse(osad[3]), int.Parse(osad[4])));
                    else if (tüüp == "Jalgratas")
                    {
                        // Konverteerime teksti tagasi enumiks
                        Rattatüüp rTüüp = (Rattatüüp)Enum.Parse(typeof(Rattatüüp), osad[1]);
                        nimekiri.Add(new Jalgratas(rTüüp, double.Parse(osad[2])));
                    }
                }
                Console.WriteLine("Andmed failist imporditud!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Viga faili lugemisel: " + e.Message);
            }
        }
        static void SalvestaFaili(List<ISõiduk> nimekiri)
        {
            List<string> read = new List<string>();

            foreach (var s in nimekiri)
            {
                // Kontrollime, mis tüüpi objektiga on tegu, et õige formaat salvestada
                if (s is Auto a)
                    read.Add($"Auto,{a.Mudel},{a.Kütusekulu100km},{a.Kilomeetrid}");
                else if (s is Buss b)
                    read.Add($"Buss,{b.Liin},{b.Kütusekulu100km},{b.Kilomeetrid},{b.ReisijateArv}");
                else if (s is Jalgratas j)
                    read.Add($"Jalgratas,{j.Tüüp},{j.Kilomeetrid}");
            }

            File.WriteAllLines("andmed.txt", read);
        }
    }
}
