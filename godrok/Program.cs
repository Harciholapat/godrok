using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        StreamReader bemenet = new StreamReader("melyseg.txt");
        var mélységek = bemenet.ReadToEnd().Split('\n').Select(int.Parse).ToList();

        Console.WriteLine("1. feladat");
        Console.WriteLine($"A fájl adatainak száma: {mélységek.Count}");

        Console.WriteLine("2. feladat");
        Console.Write("Adjon meg egy távolságértéket! ");
        int hely = int.Parse(Console.ReadLine());
        Console.WriteLine($"Ezen a helyen a felszín {mélységek[hely - 1]} méter mélyen van.");

        Console.WriteLine("3. feladat");
        int érintetlen = mélységek.Count(x => x == 0);
        Console.WriteLine($"Az érintetlen terület aránya {100.0 * érintetlen / mélységek.Count:4.2f}%.");

        StreamWriter kimenet = new StreamWriter("godrok.txt");
        int előző = 0;
        var egysor = new System.Collections.Generic.List<string>();
        var sorok = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
        foreach (var érték in mélységek)
        {
            if (érték > 0)
            {
                egysor.Add(érték.ToString());
            }
            if (érték == 0 && előző > 0)
            {
                sorok.Add(egysor);
                egysor = new System.Collections.Generic.List<string>();
            }
            előző = érték;
        }
        foreach (var egysorok in sorok)
        {
            kimenet.WriteLine(string.Join(" ", egysorok));
        }
        kimenet.Close();

        Console.WriteLine("5. feladat");
        Console.WriteLine($"A gödrök száma: {sorok.Count}");

        Console.WriteLine("6. feladat");
        if (mélységek[hely - 1] > 0)
        {
            Console.WriteLine("a)");
            int poz = hely - 1;
            while (mélységek[poz] > 0)
            {
                poz--;
            }
            int kezdő = poz + 2;
            poz = hely;
            while (mélységek[poz] > 0)
            {
                poz++;
            }
            int záró = poz;
            Console.WriteLine($"A gödör kezdete: {kezdő} méter, a gödör vége: {záró} méter.");

            Console.WriteLine("b)");
            int mélypont = 0;
            poz = kezdő;
            while (mélységek[poz] >= mélységek[poz - 1] && poz <= záró)
            {
                poz++;
            }
            while (mélységek[poz] <= mélységek[poz - 1] && poz <= záró)
            {
                poz++;
            }
            if (poz > záró)
            {
                Console.WriteLine("Folyamatosan mélyül.");
            }
            else
            {
                Console.WriteLine("Nem mélyül folyamatosan.");
            }

            Console.WriteLine("c)");
            Console.WriteLine($"A legnagyobb mélysége {mélységek.GetRange(kezdő - 1, záró - kezdő + 1).Max()} méter.");

            Console.WriteLine("d)");
            int térfogat = 10 * mélységek.GetRange(kezdő - 1, záró - kezdő + 1).Sum();
            Console.WriteLine($"A térfogata {térfogat} m^3.");

            Console.WriteLine("e)");
            int biztonságos = térfogat - 10 * (záró - kezdő + 1);
            Console.WriteLine($"A vízmennyiség {biztonságos} m^3.");
        }
        else
        {
            Console.WriteLine("Az adott helyen nincs gödör.");
        }
    }
}