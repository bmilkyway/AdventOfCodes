﻿using AdventOfCode2015;

namespace AdventOfCodeTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AdventOfCode2015.Results resultOf2015= new AdventOfCode2015.Results();
            Console.WriteLine(resultOf2015.Day1Part1());
            Console.WriteLine(resultOf2015.Day1Part2());
            Console.WriteLine(resultOf2015.Day2Part1());
            Console.WriteLine(resultOf2015.Day2Part2());
            Console.WriteLine(resultOf2015.Day3Part1());
            Console.WriteLine(resultOf2015.Day3Part2());
            Console.WriteLine(resultOf2015.Day4Part1());
            Console.WriteLine(resultOf2015.Day4Part2());
            Console.WriteLine(resultOf2015.Day5Part1());
            Console.ReadLine();
        }
    }
}