using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Days
{
    public class Day2
    {
        #region variables

        private const string Day2Task1 = "Az manóknak elfogyott a csomagolópapír, és szükségük van több rendelésre. Van egy lista, amely tartalmazza az ajándékok méreteit (hosszúság l, szélesség w és magasság h), és csak annyit akarnak rendelni, amennyire szükségük van.\r\n\r\nSzerencsére minden ajándék egy doboz (egy tökéletes derékszögű henger alakú), ami megkönnyíti az ajándékhoz szükséges csomagolópapír kiszámítását: találd meg a doboz felületét, ami 2lw + 2wh + 2hl. Az manóknak továbbá szükségük van egy kis plusz papírra minden ajándékhoz: a legkisebb oldal területére.\r\n\r\nPéldául:\r\n\r\nEgy olyan ajándék, amelynek a méretei 2x3x4, szükségessé teszik 26 + 212 + 28 = 52 négyzetlábnyi csomagolópapírt, plusz 6 négyzetlábnyi tartalékot, összesen 58 négyzetlábnyi csomagolópapírt.\r\nEgy olyan ajándék, amelynek a méretei 1x1x10, szükségessé teszik 21 + 210 + 210 = 42 négyzetlábnyi csomagolópapírt, plusz 1 négyzetlábnyi tartalékot, összesen 43 négyzetlábnyi csomagolópapírt.\r\nAz összes szám az manók listájában lábokban van kifejezve. Mennyi összesen csomagolópapír négyzetlábnyi szükséges nekik rendelniük?";
        private const string Day2Task2 = "Az manóknak kezd elfogyni a szalag is. A szalag mindenhol ugyanolyan széles, tehát csak a hosszúságra kell figyelniük, amit pontosan meg szeretnének rendelni.\r\n\r\nAz ajándék becsomagolásához szükséges szalag a legrövidebb távolság a oldalain körbe, vagyis a legkisebb kerülete az egyik oldalnak. Minden ajándékhoz szalagból készült masnit is készítenek; a tökéletes masnihoz szükséges szalag hossza megegyezik az ajándék térfogatával láb lábon mérve. Ne kérdezd, hogyan kötik a masnit, sosem árulják el.\r\n\r\nPéldául:\r\n\r\nEgy olyan ajándék, amelynek a méretei 2x3x4, szükségessé teszik a becsomagoláshoz 2+2+3+3 = 10 lábnyi szalagot, plusz 234 = 24 lábnyi szalagot a masnihoz, összesen 34 lábnyi szalagot.\r\nEgy olyan ajándék, amelynek a méretei 1x1x10, szükségessé teszik a becsomagoláshoz 1+1+1+1 = 4 lábnyi szalagot, plusz 1110 = 10 lábnyi szalagot a masnihoz, összesen 14 lábnyi szalagot.\r\nHány összesen lábnyi szalagot kellene rendelniük?\r\n\r\n\r\n\r\n\r\n\r\n";
        private List<int[]> wrappingSizes= new List<int[]>();
        private int squareFeetOfWrappingPaper= 0;
        private int feetOfRibbon = 0;
        #endregion

        #region privateFunctions

        /// <summary>
        /// Beolvassa a fájlt és beállítja az értékeket
        /// </summary>
        private void ReadingFile()
        {
            if (wrappingSizes.Count == 0)
            {
                StreamReader sr = new StreamReader("InputFiles/day2.txt");
                while (!sr.EndOfStream)
                {
                    int[] wrappSize = sr.ReadLine()!.Split("x").Select(int.Parse).ToArray();
                    wrappingSizes.Add(wrappSize);
                }
            }
        }

        /// <summary>
        /// Kiszámolja egy ajándéknak a felszinét
        /// </summary>
        /// <param name="wrappingSize">Egy ajándéknak a méreteit tartalmazó tömb</param>
        /// <returns></returns>
        private int giftSurface(int[] wrappingSize)=>
            2*wrappingSize[0]*wrappingSize[1] + 2*wrappingSize[1]*wrappingSize[2] + 2*wrappingSize[0]*wrappingSize[2];
       

        /// <summary>
        /// Megadja a legkisebb oldal területének nagyságát
        /// </summary>
        /// <param name="wrappingSize">Egy ajándéknak a méreteit tartalmazó tömb</param>
        /// <returns></returns>
        private int areaOfTheSmallestSize(List<int> wrappingSize)
        {
            wrappingSize.Sort();
            return wrappingSize[0] * wrappingSize[1];
        }

        /// <summary>
        /// Kiszámolja a legkisebb oldal kerületét
        /// </summary>
        /// <param name="wrappingSize">Egy ajándék métereit tartalmazó lista</param>
        /// <returns></returns>
        private int perimeterOfSide(List<int> wrappingSize)
        {
            wrappingSize.Sort();
            return wrappingSize[0] * 2 + wrappingSize[1]*2;
        }

        /// <summary>
        /// Egy ajándék térfogatát adja vissza
        /// </summary>
        /// <param name="wrappingSize">Egy ajándék méreteit tartalmazó tömb</param>
        /// <returns></returns>
        private int volumeOfGift(int[] wrappingSize) => wrappingSize[0] * wrappingSize[1]*wrappingSize[2];

        #endregion




        #region Tasks

        /// <summary>
        /// Kiszámolja, hogy mennyi csomagolóanyag kell összesen az ajándékokhoz
        /// </summary>
        /// <returns></returns>
        public string Task1()
        {
            ReadingFile();
            wrappingSizes.ForEach(wrappingSize => { squareFeetOfWrappingPaper += giftSurface(wrappingSize) + areaOfTheSmallestSize(wrappingSize.ToList()); });
            return String.Format("A manóknak  {0} négyzetméternyi csomagolópapír kellene...",squareFeetOfWrappingPaper);
        }

        /// <summary>
        /// Kiszámolja, hogy mennyi szalag kell az ajándékok becsomagolásához
        /// </summary>
        /// <returns></returns>
        public string Task2()
        {
            ReadingFile();
            wrappingSizes.ForEach(wrappingSize => { feetOfRibbon += volumeOfGift(wrappingSize) + perimeterOfSide(wrappingSize.ToList()); });
            return String.Format("A manóknak a sok csomagoló cucc mellé még {0} láb szalag is kell",feetOfRibbon);

        }
        #endregion

    }
}
