using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2015.Days
{
    public class Day9
    {
        private class TravelWay
        {
            public string startPosition { get; set; }

            public string endPosition { get; set; }
            public int wayLength { get;set; }

            public TravelWay(string positionWay) 
            {
                string[]splitHolder= positionWay.Split(' ');
                startPosition= splitHolder[0];
                endPosition= splitHolder[2];
                wayLength = int.Parse(splitHolder.Last());
            }
        }


        public Day9() =>
            ReadingFile();

        #region variables
        private const string Day9Task1 = "Minden évben a Mikulásnak sikerül az összes ajándékot egyetlen éjszaka alatt kiszállítania.\r\n\r\nIdén azonban új helyszínekre kell látogatnia; az aprónépek megadták neki az összes helyszín közötti távolságokat. Bármely két (különböző) helyszínen elkezdheti és be is fejezheti az utat, de minden helyszínt pontosan egyszer kell meglátogatnia. Mi a legrövidebb távolság, amit megtehet ennek eléréséhez?\r\n\r\nPéldául az alábbi távolságokkal rendelkezve:\r\n\r\nLondon - Dublin = 464\r\nLondon - Belfast = 518\r\nDublin - Belfast = 141\r\nEzért lehetséges útvonalak:\r\n\r\nDublin -> London -> Belfast = 982\r\nLondon -> Dublin -> Belfast = 605\r\nLondon -> Belfast -> Dublin = 659\r\nDublin -> Belfast -> London = 659\r\nBelfast -> Dublin -> London = 605\r\nBelfast -> London -> Dublin = 982\r\nEzek közül a legrövidebb az London -> Dublin -> Belfast = 605, így a válasz 605 ebben a példában.\r\n\r\nMi a legrövidebb útvonal távolsága?\r\n\r\nKezdetnek szükséged lesz a feladvány bemenetére.";
        private const string Day9Task2 = "Most menjünk a másik irányba. Azon kívül, hogy megtaláljuk a karakterek kódjának számát, most minden kód reprezentációt új sztringként kell kódolnod, majd megtalálnod az újonnan kódolt reprezentációban lévő karakterek számát, beleértve a körülvevő dupla idézőjeleket.\r\n\r\nPéldául:\r\n\r\n\"\" kódja \"\"\"\" kódolódik, ami növekedés 2 karakterről 6-ra.\r\n\"abc\" kódja \"\"abc\"\" kódolódik, ami növekedés 5 karakterről 9-re.\r\n\"aaa\"aaa\" kódja \"\"aaa\\\"aaa\"\" kódolódik, ami növekedés 10 karakterről 16-ra.\r\n\"\\x27\" kódja \"\"\\x27\"\" kódolódik, ami növekedés 6 karakterről 11-re.\r\nA feladatod az, hogy megtaláld az újonnan kódolt sztringeket reprezentáló karakterek összességét mínusz az eredeti sztring literál kódjában lévő karakterek számát. Például az adott sztringek esetében az összes kódolt hossz (6 + 9 + 16 + 11 = 42) mínusz az eredeti kód reprezentációban lévő karakterek (23, éppen úgy, mint ennek a feladványnak az első részében) 42 - 23 = 19.";
        private List<TravelWay> locationWays = new List<TravelWay>();

        #endregion


        #region privateFunctions

        /// <summary>
        /// Beolvassa a fájlt és beállítja az értékeket
        /// </summary>
        private void ReadingFile()
        {
            if (locationWays.Count == 0)
            {
                StreamReader sr = new StreamReader("InputFiles/day9.txt");
                while (!sr.EndOfStream)
                {
                    locationWays.Add(new TravelWay(sr.ReadLine()!));
                }

                sr.Dispose();

            }
        }
       
        private int shortestWay()
        {
            HashSet<string> ways = new HashSet<string>();
            foreach (TravelWay way in locationWays)
            {
                ways.Add(way.startPosition);
            }
            int wayLenght = 0;
            int minWayLength =int.MaxValue;
           

            return ways.Count;
        }

        #endregion




        #region Tasks

        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task1() =>
       String.Format("{0} az A értéke", shortestWay());


        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task2() =>
            String.Format("{0} a B értéke", shortestWay());

        #endregion

    }
}
