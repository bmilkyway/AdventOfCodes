using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2015.Days
{
    public class Day3
    {

        #region variables

        private const string Day3Task1 = "Mikulás ajándékokat szállít egy végtelen két dimenziós házak hálózatában.\r\n\r\nAzzal kezdi, hogy egy ajándékot szállít a kiindulási helyen lévő házhoz, majd egy manó a Északi-sarkról rádióval hívja és elmondja neki, hogy hová kell következőnek mennie. A lépések mindig pontosan egy házat jelentenek északra (^), délre (v), keletre (>) vagy nyugatra (<). Minden lépés után új helyen szállít egy másik ajándékot.\r\n\r\nAzonban az északi-sarkon lévő manó kicsit túlzásba vitte az eggnogot, és útmutatásai nem teljesen pontosak, ezért Mikulás néhány házat többször is meglátogat. Hány ház kap legalább egy ajándékot?\r\n\r\nPéldául:\r\n\r\n2 házhoz szállít ajándékot: az egyik a kiindulási helyen, a másik pedig keletre.\r\n^>v< 4 házhoz szállít ajándékot egy négyzet alakban, beleértve a kiindulási / célhelyen lévő házat is kétszer.\r\n^v^v^v^v^v néhány szerencsés gyermeknek csak 2 házhoz szállít ajándékot.";
        private const string Day3Task2 = "A következő évben, hogy felgyorsítsa a folyamatot, Mikulás létrehoz egy robot változatot magából, a Robo-Santa-t, hogy vele együtt szállítsák az ajándékokat.\r\n\r\nMikulás és Robo-Santa ugyanazon a helyen kezdenek (két ajándékot szállítva ugyanahhoz a kezdő házhoz), majd az elf útmutatása alapján váltva mozognak, aki ugyanabból a forgatókönyvből olvas, mint az előző évben.\r\n\r\nEbben az évben hány ház kap legalább egy ajándékot?\r\n\r\nPéldául:\r\n\r\n^v 3 házhoz szállít ajándékot, mert Mikulás északra megy, majd Robo-Santa délre megy.\r\n^>v< most 3 házhoz szállít ajándékot, és Mikulás és Robo-Santa végül ott vannak, ahol elkezdték.\r\n^v^v^v^v^v most 11 házhoz szállít ajándékot, Mikulás egy irányba megy, Robo-Santa pedig a másikba.";
        private string way=String.Empty;
        private HashSet<(int,int)> deliverHashSet = new HashSet<(int,int)>();
        private int x=0, y = 0;
        private int roboX=0,roboY=0;

        #endregion

        #region privateFunctions

        /// <summary>
        /// Beolvassa a fájlt és beállítja az értékeket
        /// </summary>
        private void ReadingFile()
        {
            if(way == String.Empty)
            {
               way = new StreamReader("InputFiles/day3.txt").ReadToEnd();
            }
            deliverHashSet.Clear();
        }
        private int deliverWithAlone()
        {
                foreach (var direction in way)
                {
                    if (direction.Equals('^')) y++;
                    else if (direction.Equals('>')) x++;
                    else if (direction.Equals('<')) x--;
                    else y--;
                    deliverHashSet.Add((x, y));
                }
            return deliverHashSet.Count;
        }
        private int deliverWithRoboSanta()
        {
            for (int i = 0; i < way.Length; i++)
            {

                if (i % 2 == 0)
                {
                    switch (way[i])
                    {
                        case '^':
                            y++;
                            break;
                        case '>':
                            x++;
                            break;
                        case '<':
                            x--;
                            break;
                        default:
                            y--;
                            break;
                    }
                    deliverHashSet.Add((x, y));
                }
                else
                {
                    switch (way[i])
                    {
                        case '^':
                            roboY++;
                            break;
                        case '>':
                            roboX++;
                            break;
                        case '<':
                            roboX--;
                            break;
                        default:
                            roboY--;
                            break;
                    }
                    deliverHashSet.Add((roboX, roboY));
                }
            }
            return deliverHashSet.Count;
        }
        #endregion




        #region Tasks

        /// <summary>
        /// Hány háznál volt Miki
        /// </summary>
        /// <returns></returns>
        public string Task1()
        {
            ReadingFile();
            return String.Format("A mikulás {0} helyre vitt ajándékot", deliverWithAlone());
        }

        /// <summary>
        /// Hány háznál volt robo-Santa és Miki
        /// </summary>
        /// <returns></returns>
        public string Task2()
        {
            ReadingFile();
            return String.Format("A mikulás most {0} helyre vitte már ki az ajit", deliverWithRoboSanta());

        }
        #endregion
    }
}
