using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Days
{
    public class Day6
    {
        public Day6() =>
            ReadingFile();

        private class Instruction
        {
            public int startX,startY;
            public int endX,endY;   
            public string command;
            public Instruction(string instructionLine)
            {
                string[] instructionSplit= instructionLine.Split(' ');

                command = instructionSplit[0] == "turn" ? String.Format("{0} {1}", instructionSplit[0], instructionSplit[1]) : instructionSplit[0];
                startX = instructionSplit[0]=="turn"? int.Parse(instructionSplit[2].Split(',').First()) : int.Parse(instructionSplit[2].Split(',').First());
                startY = instructionSplit[0]=="turn"? int.Parse(instructionSplit[2].Split(',')[1]) : int.Parse(instructionSplit[2].Split(',')[1]);
                endX = int.Parse(instructionSplit.Last().Split(',').First());
                endY = int.Parse(instructionSplit.Last().Split(',')[1]);
            }
        }



        #region variables
        private const string Day6Task1 = "Mikulásnak segítségre van szüksége, hogy kiderítse, melyik sztringek a szövegfájljában szép vagy csúnya szavak.\r\n\r\nEgy szép sztring az alábbi tulajdonságokkal rendelkezik:\r\n\r\nLegalább három magánhangzót tartalmaz (csak aeiou karaktereket), például: aei, xazegov, vagy aeiouaeiouaeiou.\r\nTartalmaz legalább egy betűt, ami egymás után kétszer szerepel, például: xx, abcdde (dd), vagy aabbccdd (aa, bb, cc, vagy dd).\r\nNem tartalmazza az ab, cd, pq, vagy xy sztringeket, még akkor sem, ha azok más követelmények részei.\r\nPéldául:\r\n\r\nugknbfddgicrmopn szép, mert legalább három magánhangzót tartalmaz (u...i...o...), egy dupla betűt (...dd...), és egyik tiltott részsztringet sem tartalmaz.\r\naaa szép, mert legalább három magánhangzót és egy dupla betűt tartalmaz, még akkor is, ha a különböző szabályok szerinti karakterek átfednek.\r\njchzalrnumimnmhp csúnya, mert nincs benne dupla betű.\r\nhaegwjzuvuyypxyu csúnya, mert tartalmazza a xy sztringet.\r\ndvszwmarrgswjxmb csúnya, mert csak egy magánhangzót tartalmaz.\r\nHány olyan sztring van, ami szép?";
        private const string Day6Task2 = "Mikulás belátva tévedését, egy jobb modellt választott annak eldöntésére, hogy egy sztring csúnya vagy szép-e. Az összes régi szabály, amint egyértelműen értelmetlenek.\r\n\r\nMost egy szép sztring az alábbi tulajdonságokkal rendelkezik:\r\n\r\nTartalmaz egy olyan karakterpárt, amely két különböző karakterből áll, és legalább kétszer szerepel a sztringben anélkül, hogy egymásra lennének helyezve, például: xyxy (xy) vagy aabcdefgaa (aa), de nem például aaa (aa, de ezek átfednek).\r\nTartalmaz legalább egy olyan karaktert, amely pontosan egy karakterrel ismétlődik, például: xyx, abcdefeghi (efe), vagy akár aaa is.\r\nPéldául:\r\n\r\nqjhvhtzxzqqjkmpb szép, mert van benne egy páros, amely kétszer szerepel (qj), és van benne egy karakter, amely pontosan egy karakterrel ismétlődik (zxz).\r\nxxyxx szép, mert van benne egy páros, amely kétszer szerepel, és van benne egy karakter, amely egy karakterrel ismétlődik, annak ellenére, hogy az egyes szabályok szerinti karakterek átfednek.\r\nuurcxstgmygtbstg csúnya, mert van benne egy páros (tg), de nincs ismétlődő karakter egy karakterrel közöttük.\r\nieodomkazucvgmuy csúnya, mert van benne ismétlődő karakter egy karakterrel közöttük (odo), de nincs olyan páros, amely kétszer szerepel.\r\nHány sztring van szép ezeknek az új szabályoknak megfelelően?";
        private List<string> instructions = new List<string>();
        private bool[,] flashState = new bool[1000, 1000];
        private string[] commands = new string[] { "Turn On", "Turn Of", "toggle", "through" };
        
        #endregion


        #region privateFunctions

        /// <summary>
        /// Beolvassa a fájlt és beállítja az értékeket
        /// </summary>
        private void ReadingFile()
        {
            if (instructions.Count == 0)
                instructions.AddRange(File.ReadAllLines("InputFiles/day6.txt"));
        }





        #endregion




        #region Tasks

        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task1()
        {
            ReadingFile();
            return String.Format("{0} szép szöveg van a bevitelben",instructions.Count);
        }

        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task2() =>
            String.Format("{0} szép szöveg van a bevitelben");

        #endregion

    }
}
