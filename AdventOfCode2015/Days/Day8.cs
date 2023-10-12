using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2015.Days
{
    public class Day8
    {

        public Day8() =>
            ReadingFile();

        #region variables
        private const string Day8Task1 = "A szánon idén korlátozott a hely, ezért Mikulás digitális másolataként hozza magával a listát. Tudnia kell, hogy mekkora helyet foglal el tároláskor.\r\n\r\nSok programozási nyelvben általánosan elterjedt a speciális karakterek szövegekben való kezelésének módja. Például a C, JavaScript, Perl, Python és még a PHP is nagyon hasonló módon kezelik ezeket a karaktereket.\r\n\r\nAzonban fontos tudni a különbséget a karakterek számának között a sztring literál kód reprezentációjában és a ténylegesen a memóriában tárolt sztring karaktereinek száma között.\r\n\r\nPéldául:\r\n\r\n\"\" 2 karakter kód (a két idézőjel), de maga a sztring nem tartalmaz karaktert.\r\n\"abc\" 5 karakter kódban, de a sztringben 3 karakter van.\r\n\"aaa\"aaa\" 10 karakter kódban, de a sztring maga hat \"a\" karaktert és egyetlen menekített idézőjelet tartalmaz, tehát összesen 7 karaktert a sztringben.\r\n\"\\x27\" 6 karakter kódban, de a sztringben csak egy van - egy apostrof (') a hexadecimális jelölés segítségével áthidalva.\r\nMikulás listája egy fájl, amely sokszoros idézőjelbe tett sztring literált tartalmaz, mindegyik egy-egy sorban. Az egyetlen menekítési sorozatok, amelyeket használnak, a \\ (ami egyetlen backslash karaktert jelent), \" (ami egyetlen idézőjel karaktert jelent), és \\x plusz két hexadecimális karakter (ami egyetlen karaktert jelent az adott ASCII kódban).\r\n\r\nA fájlban lévő whitespace karakterek figyelmen kívül hagyásával, mennyi a karakterek száma a sztring literálok kódjában mínusz a sztringek tényleges memóriában elfoglalt karaktereinek száma az egész fájlban?\r\n\r\nPéldául, az adott négy sztring esetében a sztring kód összes karakterének száma (2 + 5 + 10 + 6 = 23) mínusz a sztringértékek memóriában elfoglalt összes karakterének száma (0 + 3 + 7 + 1 = 11) 23 - 11 = 12.";
        private const string Day8Task2 = "Most menjünk a másik irányba. Azon kívül, hogy megtaláljuk a karakterek kódjának számát, most minden kód reprezentációt új sztringként kell kódolnod, majd megtalálnod az újonnan kódolt reprezentációban lévő karakterek számát, beleértve a körülvevő dupla idézőjeleket.\r\n\r\nPéldául:\r\n\r\n\"\" kódja \"\"\"\" kódolódik, ami növekedés 2 karakterről 6-ra.\r\n\"abc\" kódja \"\"abc\"\" kódolódik, ami növekedés 5 karakterről 9-re.\r\n\"aaa\"aaa\" kódja \"\"aaa\\\"aaa\"\" kódolódik, ami növekedés 10 karakterről 16-ra.\r\n\"\\x27\" kódja \"\"\\x27\"\" kódolódik, ami növekedés 6 karakterről 11-re.\r\nA feladatod az, hogy megtaláld az újonnan kódolt sztringeket reprezentáló karakterek összességét mínusz az eredeti sztring literál kódjában lévő karakterek számát. Például az adott sztringek esetében az összes kódolt hossz (6 + 9 + 16 + 11 = 42) mínusz az eredeti kód reprezentációban lévő karakterek (23, éppen úgy, mint ennek a feladványnak az első részében) 42 - 23 = 19.";
        private List<string> giftList = new List<string>();

        #endregion


        #region privateFunctions

        /// <summary>
        /// Beolvassa a fájlt és beállítja az értékeket
        /// </summary>
        private void ReadingFile()
        {
            if (giftList.Count == 0) giftList.AddRange(File.ReadAllLines("InputFiles/day8.txt"));
        }
        private int valueOfStrings()
        {
            int stringValue = 0;
         

            foreach (string line in giftList)
            {
                stringValue += (line.Length - (Regex.Unescape(line).Length - 2));
            }
            return stringValue;
        }

        private int valueOfStringsPart2()
        {
            int stringValue = 0;
          
            foreach (string line in giftList)
            {
                var str =+'\"'+ line.Replace("\\", "\\\\").Replace("\"", "\\\"");
                stringValue += (str.Length-line.Length);
            }
            return stringValue;
        }

        #endregion




        #region Tasks

        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task1() =>
       String.Format("{0} Memóriastring értéke", valueOfStrings());


        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task2() =>
            String.Format("{0} Memóriastring értéke", valueOfStringsPart2());

        #endregion

    }
}
