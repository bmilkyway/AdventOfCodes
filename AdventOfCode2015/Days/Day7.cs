using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015.Days
{
    [Description("Egy redbull-t megittam és megbasztam ezt a feladatot")]
    public class Day7
    {
        public Day7() =>
            ReadingFile();
      

        #region Objects
        /// <summary>
        /// Bitekre vonatkozó utasítás objektum
        /// </summary>
        private class Instruction
        {
            public string? instructionCommand;
            public dynamic instructionLeft;
            public dynamic? instructionRight;
            public string instructionResult;
            public Instruction(string line)
            {
                int holdValue;
                string[] result = line.Split(" -> ");
                string[] instructSplit = result[0].Split(' ');
                if (instructSplit[0].Equals(commands[0]))
                {
                    instructionCommand = instructSplit[0];

                    instructionRight = int.TryParse(instructSplit[1], out holdValue) ? holdValue : instructSplit[1];
                }
                else
                {
                    if (instructSplit.Length == 1)
                    {

                        instructionLeft = int.TryParse(instructSplit[0], out holdValue) ? holdValue: instructSplit[0];
                    }
                    else
                    {
                        instructionLeft = int.TryParse(instructSplit[0], out holdValue) ? holdValue : instructSplit[0];
                        instructionCommand = instructSplit[1];
                        
                        instructionRight = int.TryParse(instructSplit[2],out holdValue) ? holdValue: instructSplit[2];
                    }
                }
                instructionResult = result[1];
            }
        }
        /// <summary>
        /// Bitcsoport objektuma
        /// </summary>
        private class Wire
        {
            public string wire;
            public int? value;
            public Wire(string wire, int? value)
            {
                this.wire = wire;
                this.value = value;
            }

        }
        #endregion

        #region variables
        private const string Day7Task1 = "Idén a Mikulás egy kis Bobby Tables-nek egy készletet hozott drótokkal és bitenkénti logikai kapukkal! Sajnos kis Bobby kicsit alulmúlja a javasolt életkori határt, és segítségre van szüksége az áramkör összeszereléséhez.\r\n\r\nMinden drótnak van egy azonosítója (néhány kisbetű) és egy 16-bites jelzése lehet (egy szám 0 és 65535 között). Egy kapu, egy másik drót vagy egy specifikus érték biztosít jelzést minden drótnak. Minden drót csak egy forrásból kaphat jelet, de jelet szolgáltathat több helyre is. Egy kapu nem szolgáltat jelet, amíg minden bemenetére jelet nem kap.\r\n\r\nA mellékelt útmutató leírja, hogyan kell összekapcsolni az alkatrészeket: x ÉS y -> z azt jelenti, hogy a drótokat x és y össze kell kapcsolni egy ÉS kapuval, majd annak kimenetét z dróthoz kell csatlakoztatni.\r\n\r\nPéldául:\r\n\r\n123 -> x azt jelenti, hogy a jelzés 123-at biztosítjuk az x drótnak.\r\nx ÉS y -> z azt jelenti, hogy az x és y drótok bitenkénti ÉS-ének eredményét biztosítjuk a z drótnak.\r\np BALRA 2 -> q azt jelenti, hogy az érték, amit a p dróttól kapunk, balra shiftelve 2-vel, azt biztosítjuk a q drótnak.\r\nNEM e -> f azt jelenti, hogy az érték bitenkénti komplementere, amit az e dróttól kapunk, azt biztosítjuk a f drótnak.\r\nEgyéb lehetséges kapuk közé tartoznak az VAGY (bitenkénti VAGY) és JOBBLÉPÉS (jobbra shift). Ha valamiért az áramkört szeretné szimulálni, szinte az összes programozási nyelv (például C, JavaScript vagy Python) biztosít operátorokat ezekhez a kapukhoz.\r\n\r\nPéldául itt van egy egyszerű áramkör:\r\n\r\n123 -> x\r\n456 -> y\r\nx ÉS y -> d\r\nx VAGY y -> e\r\nx BALRA 2 -> f\r\ny JOBBLÉPÉS 2 -> g\r\nNEM x -> h\r\nNEM y -> i\r\nMiután lefutott, ezek a jelek vannak a drótokon:\r\n\r\nd: 72\r\ne: 507\r\nf: 492\r\ng: 114\r\nh: 65412\r\ni: 65079\r\nx: 123\r\ny: 456\r\nA kis Bobby készletének útmutatójában (amit a feladvány bemeneteként megkap) végül milyen jelzés van biztosítva az a drótnak?";
        private const string Day7Task2 = "Most vegye át a vezetéken kapott jelet a, írja felül a vezetéket barra a jelre, és állítsa vissza a többi vezetéket (beleértve a vezetéket is a). Milyen új jel érkezik végül a vezetékhez a?";
        private static  List<string> commands = new List<string>() {"NOT","OR","AND","LSHIFT","RSHIFT" };
        private List<Wire> wireValues = new List<Wire>();
        private HashSet<Wire> wires = new HashSet<Wire>();
        private HashSet<string> unknowWireStrings= new HashSet<string>();
        private List<Instruction> instructions = new List<Instruction>();
        private int valueOfA = 0;
        #endregion
    
        #region privateFunctions

        /// <summary>
        /// Beolvassa a fájlt és beállítja az értékeket
        /// </summary>
        private void ReadingFile()
        {
            instructions.Clear();
            wireValues.Clear();
            unknowWireStrings.Clear();
            wires.Clear();
            //Egy karakteres változók
            for (int i = 0; i < 26; i++)
            {
                wireValues.Add(new Wire(((char)('a' + i)).ToString(), null));
            }
            //két karakteres változók
            for(int i=0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    wireValues.Add(new Wire(String.Format("{0}{1}", (char)('a' + i), (char)('a' + j)), null));
                }
            }


            StreamReader sr = new StreamReader("InputFiles/day7.txt");
            while(!sr.EndOfStream)
            {
                instructions.Add(new Instruction(sr.ReadLine()!));
            }

            sr.Dispose();
        }

        /// <summary>
        /// A kezdésnél egyszer végigmegy az instrukciókon, majd kiveszi az egyértelmű értékpárokat.
        /// A feldolgozott instrukciókat töröljük, hogy feleslegesen ne olvassuk be még egyszer a memóriába
        /// </summary>
        [Description("Beállítja a listákat és a halmazokat a kezdéshez")]
        private void setStartValues()
        {
            List<Instruction> removeInstuction = new List<Instruction>();
            //Először végigmegyünk, hogy van e olyan adat, amit azonnal tudunk
            foreach (Instruction instruction in instructions)
            {
                ///Ha az első operandus értéke még nem ismert, hozzáadjuk az ismeretlen gyüjteményhez
                if (instruction.instructionLeft != null && instruction.instructionLeft is string && wires.FirstOrDefault(x=>x.wire==instruction.instructionLeft!.ToString())==null)
                    unknowWireStrings.Add((string)instruction.instructionLeft!);

                ///Ha a másododik operandus értéke még nem ismert, hozzáadjuk az ismeretlen gyűjteményhez
                if (instruction.instructionRight != null && instruction.instructionRight is string && wires.FirstOrDefault(x => x.wire == instruction.instructionRight!.ToString()) == null)
                    unknowWireStrings.Add((string)instruction.instructionRight!);

                ///Ha az érték nem ismert, hozzáadjuk az ismeretlen gyűjteményhez
                if (instruction.instructionResult != null && instruction.instructionResult is string && wires.FirstOrDefault(x => x.wire == instruction.instructionResult!.ToString()) == null)
                    unknowWireStrings.Add((string)instruction.instructionResult!);

                ///Ha egy szám értéket tisztán rendelünk hozzá egy stringhez, akkor kiveszük az ismeretlenek gyűjteményéből és az instrukciót is törlésre ítéljük
                if (instruction.instructionLeft != null && instruction.instructionCommand == null && instruction.instructionLeft is int)
                {
                    removeInstuction.Add(instruction);
                    wires.Add(new Wire(instruction.instructionResult!, (int)instruction.instructionLeft));
                    unknowWireStrings.Remove(instruction.instructionResult!);

                }

                //Ha egy szám értékének kell venni a komplemensét és azt kell egy stringhez adni, akkor kivesszük az eredményt az ismeretlen gyűjteményből és törlésre ítéljük az instrukciót
                if (instruction.instructionCommand != null && instruction.instructionCommand.Equals(commands[0]) && instruction.instructionRight is int)
                {
                    removeInstuction.Add(instruction);
                    wires.Add(new Wire(instruction.instructionResult!, notCommand((int)instruction.instructionRight)));
                    unknowWireStrings.Remove(instruction.instructionResult!);

                }
            }
            //Töröljük a felhasznált instrukciókat
            foreach (Instruction removeItem in removeInstuction)
            instructions.Remove(removeItem);
        }


        /// <summary>
        /// Megkeresi a bit nevéhez tartozó értéket
        /// </summary>
        /// <param name="bitWireName">A keresni kívánt bit neve</param>
        /// <param name="partNumber">feladat sorszáma</param>
        /// <returns></returns>
        private int findWireValue(string bitWireName,int partNumber)
        {
            int result = 0;

            //Ez beállítja az alap cuccokat
            setStartValues();
            //Megfejti a bitek értékét
            bitWireCoding();

            //A érték mentése
            valueOfA = (int)wires.First(x => x.wire == bitWireName)!.value!;
            if (partNumber == 2)
            {
                ReadingFile();
                setStartValues();
                wires.First(y => y.wire == "b").value = valueOfA;
                bitWireCoding();
                valueOfA = (int)wires.First(x => x.wire == bitWireName)!.value!;
            }


            return valueOfA;
        }

        /// <summary>
        /// Megfejti az összes bit értékét
        /// </summary>
        [Description("Egy ciklus, ami addig meg, míg el nem fogy az ismeretlen gyűjtemény")]
        private void bitWireCoding()
        {
            while (unknowWireStrings.Count != 0)
            {
                //A feldolgozott utasításokat összegyűjtjük, majd kidubjuk, hogy ne legyen hosszű a ciklus
                List<Instruction> removeInstructions = new List<Instruction>();


                foreach (Instruction instruction in instructions)
                {
                    //Ha az  első szám már megvan, és balra eltolás van számmal
                    if ((instruction.instructionLeft != null && wires.FirstOrDefault(x => x.wire == instruction.instructionLeft!.ToString()) != null && instruction.instructionCommand != null && instruction.instructionCommand!.Equals(commands[3])) && instruction.instructionRight is int && instruction.instructionResult is string)
                    {
                        removeInstructions.Add(instruction);
                        unknowWireStrings.Remove((string)instruction.instructionResult!);
                        wires.Add(new Wire(instruction.instructionResult, lShiftCommand((int)wires.First(x => x.wire == instruction.instructionLeft!).value!, instruction.instructionRight)));
                    }
                    //Ha az első szám már megvan és balra eltolás van számmal
                    if ((instruction.instructionLeft != null && wires.FirstOrDefault(x => x.wire == instruction.instructionLeft!.ToString()) != null && instruction.instructionCommand != null && instruction.instructionCommand!.Equals(commands[4])) && instruction.instructionRight is int && instruction.instructionResult is string)
                    {
                        removeInstructions.Add(instruction);
                        unknowWireStrings.Remove((string)instruction.instructionResult!);
                        wires.Add(new Wire(instruction.instructionResult, rShiftCommand((int)wires.First(x => x.wire == instruction.instructionLeft!).value!, instruction.instructionRight)));
                    }
                    //Ha megvan az a szám, aminek a komplemensét venni kell
                    if (instruction.instructionCommand != null && instruction.instructionCommand.Equals(commands[0]) && wires.FirstOrDefault(y => y.wire == instruction.instructionRight!.ToString()) != null)
                    {
                        removeInstructions.Add(instruction);
                        unknowWireStrings.Remove((string)instruction.instructionResult!);
                        wires.Add(new Wire(instruction.instructionResult!, notCommand((int)wires.FirstOrDefault(y => y.wire == instruction.instructionRight!.ToString())!.value!)));
                    }
                    //Ha tudunk egy és műveletnél mind a két operandust
                    if (instruction.instructionLeft != null && (wires.FirstOrDefault(x => x.wire == instruction.instructionLeft!.ToString()) != null || instruction.instructionLeft is int) && instruction.instructionCommand != null && instruction.instructionCommand!.Equals(commands[2]) && instruction.instructionRight != null && (instruction.instructionRight is int || wires.FirstOrDefault(x => x.wire == instruction.instructionRight!.ToString()) != null))
                    {
                        removeInstructions.Add(instruction);
                        unknowWireStrings.Remove((string)instruction.instructionResult!);
                        wires.Add(new Wire(instruction.instructionResult!, andCommand(instruction.instructionLeft is int ? (int)instruction.instructionLeft : (int)wires.First(y => y.wire == instruction.instructionLeft!.ToString()).value!, instruction.instructionRight is int ? (int)instruction.instructionRight : (int)wires.First(y => y.wire == instruction.instructionRight!.ToString()).value!)));
                    }
                    //Ha tudunk egy vagy műveletnél mind a két operandust
                    if (instruction.instructionLeft != null && (wires.FirstOrDefault(x => x.wire == instruction.instructionLeft!.ToString()) != null || instruction.instructionLeft is int) && instruction.instructionCommand != null && instruction.instructionCommand!.Equals(commands[1]) && instruction.instructionRight != null && (instruction.instructionRight is int || wires.FirstOrDefault(x => x.wire == instruction.instructionRight!.ToString()) != null))
                    {
                        removeInstructions.Add(instruction);
                        unknowWireStrings.Remove((string)instruction.instructionResult!);
                        wires.Add(new Wire(instruction.instructionResult!, orCommand(instruction.instructionLeft is int ? (int)instruction.instructionLeft : (int)wires.First(y => y.wire == instruction.instructionLeft!.ToString()).value!, instruction.instructionRight is int ? (int)instruction.instructionRight : (int)wires.First(y => y.wire == instruction.instructionRight!.ToString()).value!)));
                    }
                    //Ha csak egy sima értéket adunk át, amit korábban már megfejtettünk
                    if (instruction.instructionLeft != null && instruction.instructionCommand == null && instruction.instructionRight == null && instruction.instructionLeft is string && wires.FirstOrDefault(y => y.wire == instruction.instructionLeft!.ToString()) != null)
                    {
                        removeInstructions.Add(instruction);
                        unknowWireStrings.Remove((string)instruction.instructionResult!);
                        wires.Add(new Wire(instruction.instructionResult!, (int)wires.First(y => y.wire == instruction.instructionLeft!.ToString())!.value!));
                    }
                    //Ha egy számot rendelünk hozzá
                    if (instruction.instructionLeft != null && instruction.instructionCommand == null && instruction.instructionLeft is int)
                    {
                        removeInstructions.Add(instruction);
                        wires.Add(new Wire(instruction.instructionResult!, (int)instruction.instructionLeft));
                        unknowWireStrings.Remove(instruction.instructionResult!);

                    }

                    //Ha egy számnak képezzük a komplemensét
                    if (instruction.instructionCommand != null && instruction.instructionCommand.Equals(commands[0]) && instruction.instructionRight is int)
                    {
                        removeInstructions.Add(instruction);
                        wires.Add(new Wire(instruction.instructionResult!, notCommand((int)instruction.instructionRight)));
                        unknowWireStrings.Remove(instruction.instructionResult!);

                    }

                }

                //A feldolgozott instrukciókat töröljük
                foreach (Instruction removeItem in removeInstructions)
                    instructions.Remove(removeItem);


            }
        }

        [Description("A bit műveletek függvényei")]
        #region commandFunctions
        private int notCommand(int number) => ~number;
        private int lShiftCommand(int number, int shiftValue) => number<< shiftValue;
        private int rShiftCommand(int number, int shiftValue) => number >> shiftValue;
        private int andCommand(int firstNumber, int secondNumber) => firstNumber & secondNumber;
        private int orCommand(int firstNumber, int secondNumber) => firstNumber| secondNumber;

        #endregion


        #endregion


        #region Tasks

        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task1() =>
       String.Format("{0} az A értéke", findWireValue("a",1));


        /// <summary>
        /// Megadja, hogy hány szöveg felel meg a mintának
        /// </summary>
        /// <returns></returns>
        public string Task2() =>
            String.Format("{0} a A értéke", findWireValue("a", 2));

        #endregion

    }
}