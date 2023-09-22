using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Days
{
    [Description("Ez egy nagyon buzi feladat volt, kapja be a faszom aki kitalálta")]
    public class Day4
    {
        #region variables

        private const string Day4Task1 = "Mikulásnak segítségre van szüksége AdventCoins-ok bányászatában (nagyon hasonlóan a bitcoinekhez), hogy azokat ajándékba adhassa azoknak a gazdaságilag előrelátó kisfiúknak és kislányoknak.\r\n\r\nEhhez olyan MD5 hash-eket kell találnia, amelyek hexadecimális formában legalább öt nullával kezdődnek. Az MD5 hash bemenete egy titkos kulcs (a feladványod, amit alább láthatsz), amit egy decimális szám követ. Az AdventCoins bányászatához meg kell találnod azt a legkisebb pozitív számot (nincsenek vezető nullák: 1, 2, 3, ...), amely ilyen hash-t eredményez.\r\n\r\nPéldául:\r\n\r\nHa a titkos kulcsod abcdef, a válasz 609043, mert az abcdef609043 MD5 hash-je öt nullával kezdődik (000001dbbfa...), és ez a legalacsonyabb ilyen szám.\r\nHa a titkos kulcsod pqrstuv, a legalacsonyabb szám, amivel együtt alkot egy MD5 hash-t, amely öt nullával kezdődik, az 1048970; vagyis a pqrstuv1048970 MD5 hash-je úgy néz ki, mint 000006136ef....\r\nA feladványod az: bgvyzdsv.";
        private const string Day4Task2 = "Ugyan az,csak most 6db 0-val kell kezdődnie";
        private const string inputData = "bgvyzdsv";
        #endregion

        #region privateFunctions

        /// <summary>
        /// Md5hash kódoló
        /// </summary>
        /// <param name="input">kulcs</param>
        /// <returns></returns>
        private string MdHash(string input)
        {

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
               
                return Convert.ToHexString(hashBytes);
            }
        }
        /// <summary>
        /// Megkeresi, hogy melyik szám lesz a befutó
        /// </summary>
        /// <param name="leadingZeroes"></param>
        /// <returns></returns>
        private int FindAdventCoin(string leadingZeroes)
        {
            int number = 1;
            while (true)
            {
                string input = inputData + number.ToString();
                string hash = MdHash(input);
                if(hash.StartsWith(leadingZeroes))
                {
                    return number;
                }
                number++;
            }
        }
     
        #endregion




        #region Tasks

        /// <summary>
        /// Melyik a legkisebb szám amit a kulcs mellé kell rakni, ha a hash első 5 karaktere 0
        /// </summary>
        /// <returns></returns>
        public string Task1()
        {
            return String.Format("A legkisebb szám: {0}", FindAdventCoin("00000"));
        }

        /// <summary>
        /// Melyik a legkisebb szám amit a kulcs mellé kell rakni, ha a hash első 6 karaktere 0
        /// </summary>
        /// <returns></returns>
        public string Task2()
        {
            return String.Format("A legkisebb szám: {0}", FindAdventCoin("000000"));

        }
        #endregion
    }
}
