﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2006Feherje
{
  struct Aminosav
  {
    public string rovid;
    public char betujel;
    public int szen; // C
    public int hidrogen; // H
    public int oxigen; // O 
    public int nitrogen; // N
    public int ken; // S
    public int tomeg;
  }

  class Program
  {
    static Aminosav[] aminok = new Aminosav[20];
    static char[] bsaLanc = new char[1000];
    static int bsaHossz = 0;
                                  // C  H   O   N   S
    static int[] tomeg = new int[] {12, 1, 16, 14, 32 };

    static void Elso()
    {
      Console.WriteLine("1. feladat");
      Console.WriteLine("Adatok beolvasása");
      StreamReader file = new StreamReader("aminosav.txt");

      // beolvasás --> tudjuk, hogy 20 darab van!!!
      for (int i = 0; i < 20; i++)
      {
        
        aminok[i].rovid = file.ReadLine();
        aminok[i].betujel = Convert.ToChar(file.ReadLine());
        aminok[i].szen = Convert.ToInt32(file.ReadLine());
        aminok[i].hidrogen = Convert.ToInt32(file.ReadLine());
        aminok[i].oxigen = Convert.ToInt32(file.ReadLine());
        aminok[i].nitrogen = Convert.ToInt32(file.ReadLine());
        aminok[i].ken = Convert.ToInt32(file.ReadLine());
        // Ellenőrzés
        //Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}",
        //  aminok[i].rovid, aminok[i].betujel, aminok[i].szen.ToString().PadLeft(2),
        //  aminok[i].hidrogen.ToString().PadLeft(2), aminok[i].oxigen.ToString().PadLeft(2),
        //  aminok[i].nitrogen.ToString().PadLeft(2), aminok[i].ken.ToString().PadLeft(2));
      }

      file.Close();
    } // Elso metódus vége

    static void Masodik()
    {
      Console.WriteLine("\n2. feladat");
      Console.WriteLine("Tömegszámítás");

      for (int i = 0; i < 20; i++)
      {
        aminok[i].tomeg = aminok[i].szen * tomeg[0] + aminok[i].hidrogen * tomeg[1] +
          aminok[i].oxigen * tomeg[2] + aminok[i].nitrogen * tomeg[3] +
          aminok[i].ken * tomeg[4];
        Console.WriteLine(aminok[i].tomeg);
      }


    }

    static void Harmadik()
    {
      Console.WriteLine("\n3. feladat");

      // tömeg szerint sorrendbe
      for (int i = 0; i < aminok.Length - 1; i++)
      {
        for (int j = i+1; j < aminok.Length; j++)
        {
          if (aminok[i].tomeg > aminok[j].tomeg)
          {
            Aminosav tmp = aminok[i];
            aminok[i] = aminok[j];
            aminok[j] = tmp;
          }
        }
      }

      // kiírása
      for (int i = 0; i < 20; i++)
      {
        //Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}",
        //  aminok[i].rovid, aminok[i].betujel, aminok[i].szen.ToString().PadLeft(2),
        //  aminok[i].hidrogen.ToString().PadLeft(2), aminok[i].oxigen.ToString().PadLeft(2),
        //  aminok[i].nitrogen.ToString().PadLeft(2), aminok[i].ken.ToString().PadLeft(2), 
        //  aminok[i].tomeg);
        Console.WriteLine("{0} {1}", aminok[i].rovid, aminok[i].tomeg);
      }
    }

    static void Negyedik()
    {
      Console.WriteLine("\n4. feladat");
      // Tárolástalan fájlfeldolgozás
      // C H O N S ?????
      // ? Aminosav
      // levonni a H2O!
      int dbC = 0, dbH = 0, dbO = 0, dbN = 0, dbS = 0, dbAmino = 0;
      StreamReader bsa = new StreamReader("bsa.txt");

      while (!bsa.EndOfStream)
      {
        char mit = Convert.ToChar(bsa.ReadLine());
        bsaLanc[bsaHossz++] = mit;
        Aminosav epito = Melyik(mit);
        dbC += epito.szen;
        dbH += epito.hidrogen;
        dbN += epito.nitrogen;
        dbO += epito.oxigen;
        dbS += epito.ken;
        dbAmino++;
      }

      bsa.Close();

      // vízképződés miatt csökkenteni a H, O
      dbH -= (2 * (dbAmino - 1));
      dbO -= (dbAmino - 1);

      Console.WriteLine("C {0} H {1} O {2} N {3} S {4}",
                         dbC, dbH, dbO, dbN, dbS);
    }

    static void Otodik()
    {
      Console.WriteLine("\n5. feladat");
      int eleje = 0, vege, hossza;

      for (int i = 0; i < bsaHossz; i++)
      {
        if (bsaLanc[i] == 'Y' || bsaLanc[i] == 'W' || bsaLanc[i] == 'F')
        {
          vege = i;
          hossza = vege - eleje;
          //??? egyelőre mert nincs időnk :-(
        }
      }



    }

    static Aminosav Melyik(char ch)
    {
      int i = 0;
      while (ch != aminok[i].betujel)
      {
        i++;
      }

      return aminok[i];
    }

    static void Main(string[] args)
    {
      Elso();
      Masodik();
      Harmadik();
      Negyedik();
      Otodik();

      Console.ReadKey();
    }
  }
}
