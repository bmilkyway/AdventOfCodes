using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Days
{
    public class Day1
    {
        public const string Day1Task1 = "Mikulás egy fehér karácsonyt remélt, de időjárási gépének a \"havazás\" funkciója csillagokkal működik, és most elfogyott! Hogy megmentse a karácsonyt, neked kell összegyűjtened ötven csillagot december 25-ig.\r\n\r\nGyűjtsd a csillagokat azzal, hogy segíted Mikulást megoldani a feladványokat. Két feladvány lesz elérhető minden nap az adventi naptárban; a második feladvány akkor lesz feloldva, amikor az elsőt befejezted. Minden feladvány egy csillagot ad. Sok szerencsét!\r\n\r\nItt van egy könnyű feladvány, hogy felmelegítsd.\r\n\r\nMikulás megpróbál ajándékokat kiszállítani egy nagy lakóépületben, de nem találja meg a megfelelő emeletet - az utasítások egy kicsit zavarosak. A földszintről (0. emelet) indul, majd az utasításokat karakterről karakterre követi.\r\n\r\nEgy nyitó zárójel, (, azt jelenti, hogy egy emelettel feljebb kell mennie, és egy záró zárójel, ), azt jelenti, hogy egy emelettel lefelé kell mennie.\r\n\r\nAz apartmanépület nagyon magas, és a pince nagyon mély; sosem fogja megtalálni a legfelső vagy legalsó emeletet.\r\n\r\nPéldául:\r\n\r\n(()) és ()() mindkettő eredményez a földszinten való maradást (0. emelet).\r\n((( és (()(()( mindkettő eredményez a 3. emeleten való tartózkodást.\r\n))((((( szintén a 3. emeleten való tartózkodást eredményezi.\r\n()) és ))( mindkettő eredményez a -1. emeleten (az első pince szintjén) való tartózkodást.\r\n))) és )())()) mindkettő eredményez a -3. emeleten való tartózkodást.\r\n\r\nHányadik emeleten tartózkodik a Mikulás az utasítások szerint?";
        public const string Day1Task2 = "Most, ugyanazokat az utasításokat figyelembe véve, keressd meg azt a karakter pozíciót, amely először arra készteti őt, hogy a pincébe lépjen (-1. emelet). Az utasításokban az első karakter pozíciójának az 1. pozíciót tekintjük, a második karakter pozíciójának a 2. pozíciót, és így tovább.\r\n\r\nPéldául:\r\n\r\n) arra készteti őt, hogy a pincébe lépjen az 1. karakter pozíciójában.\r\n()()) arra készteti őt, hogy a pincébe lépjen az 5. karakter pozíciójában.\r\nMi az a karakter pozíciója, ami először arra készteti Mikulást, hogy a pincébe lépjen?";

        private int floor = 0;
        private string instructions = string.Empty;
        public void ReadingText()
        {
            if(instructions == string.Empty)
                instructions = new StreamReader("InputFiles/day1.txt").ReadToEnd();
            if (floor != 0) floor = 0;
        }

        public string Task1()
        {
            ReadingText();
            foreach(char c in instructions)
            {
                if(c.Equals('(')) floor++;
                else if(c.Equals(')')) floor--;
            }
            return String.Format("A mikulásnak a {0} emeletre kell vinnie a szajrét", floor);
        }

        public string Task2()
        {
            ReadingText() ;
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].Equals('('))floor++;
                else if (instructions[i].Equals(')'))floor--;
                if (floor == -1) return String.Format("Az első alkalom amikor a pincébe ment : {0}", i + 1);

            }
            return String.Format("Ez a fránya mikulás nem is ment a pincébe sose...");
        
        }
    }
}
