// vas.txt http://www.infojegyzet.hu/vizsgafeladatok/okj-programozas/rendszeruzemelteto-180517/
// 1-980227-1258
// 2-990926-7743
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

class Azonosito
{
    public string sor   { get; set; }
    public int  s1      { get; set; }
    public int ev       { get; set; }
    public int honap    { get; set; }
    public int nap      { get; set; }
    public bool ferfi   { get; set; }

    public Azonosito(string sor)
    {
        var s = sor.Replace("-","");
        this.s1     = int.Parse(s.Substring(0,1));
        this.honap  = int.Parse(s.Substring(3,2));
        this.nap    = int.Parse(s.Substring(5,2));
        this.ferfi  = s1==1 || s1==3;
        int szazad  = s1==1 || s1==2 ? 1900 : 2000;
        this.ev     = szazad + int.Parse(s.Substring(1,2)); 
        this.sor    = sor;
    }
}

class Program 
{

// 3. feladat: Cdvell
    public static bool Cdvell(string sor)
    {
        var s = sor.Replace("-","");
        int szumma = 0;
        for(int i=0; i<10; i++)
        {
            szumma += (10-i) * int.Parse(s[i].ToString());
        }
        return  int.Parse(s[10].ToString()) == szumma % 11;
    }

    public static void Main (string[] args) 
    {
        //2. feladat: Adatok beolvasása, tárolása
        Console.WriteLine($"2. feladat: Adatok beolvasása, tárolása");
        var lista = new List<Azonosito>();
        var hiba_lista = new List<string>();
        
        var fr    = new StreamReader("vas.txt");
        while(!fr.EndOfStream)
        {
            var sor = fr.ReadLine().Trim();
            if (Cdvell(sor)) 
            { 
                lista.Add( new Azonosito(sor) ); 
            }
            else 
            { 
                hiba_lista.Add( sor ); 
            }
        }
        fr.Close();

        //4. feladat: Ellenőrzés
       
        Console.WriteLine(    $"4. feladat: Ellenőrzés      ");
        foreach( var sor in hiba_lista) { Console.WriteLine($"        Hibás a {sor} személyi azonosító!"); }
                
        // 5. feladat: Vas megyében a vizsgált évek alatt {} csecsemő született.
        Console.WriteLine($"5. feladat: Vas megyében a vizsgált évek alatt {lista.Count()} csecsemő született.");

        // 6. feladat: Fiúk száma: 
        var fiuk = 
        (
            from sor in lista
            where sor.ferfi
            select sor
        ); 
        Console.WriteLine($"6. feladat: Fiúk száma: {fiuk.Count()} ");
        
        // 7. feladat: Vizsgált időszak: {} - {}
        var idoszak = 
        (
            from sor in lista
            select sor.ev
        ); 
        Console.WriteLine($"7. feladat: Vizsgált időszak: {idoszak.Min()} - {idoszak.Max()}");

        //8. feladat: szökőnap 02.24  év%4
        var szokonapok = 
        (
            from sor in lista
            where sor.honap == 2
            where sor.nap == 24
            where sor.ev % 4 == 0
            select sor.sor
        );
        //foreach(var sor in szokonapok){Console.WriteLine(sor)};
        
        if (szokonapok.Any())
        {
            Console.WriteLine($"8. feladat: Szökőnapon született baba!");
        }
        else
        {
            Console.WriteLine($"8. feladat: Szökőnapon nem született baba!");
        }
        // 9. feladat: Statisztika
        var stat =
        (
            from sor in lista
            group sor by sor.ev
        );
        Console.WriteLine(    $"9. feladat: Statisztika");
        foreach(var sor in stat) { Console.WriteLine($"        {sor.Key} - {sor.Count()} fő"); }
    }
}
