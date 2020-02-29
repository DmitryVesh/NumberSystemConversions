using System;
using System.Collections.Generic;

class MainClass
{
    private static void ConvertToBinary()
    {
        List<string> userInputBinaryList = new List<string>();
        Console.Write("\nEnter a decimal number : ");
        string userInputStr = Console.ReadLine();
        if (!double.TryParse(userInputStr, out double userInputInt)) { Console.WriteLine("\nError, input entered is not a number. Try again."); }
        else
        {
            if (userInputInt <= 2147483647 || userInputInt >= 0)
            { //max int variable can hold is 32 bits
                for (int powers2Loop = 31; powers2Loop >= 0; powers2Loop--)
                {
                    double currentPowers2Value = Math.Pow(2, powers2Loop);
                    if (userInputInt - currentPowers2Value >= 0)
                    {
                        userInputInt -= currentPowers2Value;
                        userInputBinaryList.Add("1");
                    }
                    else { userInputBinaryList.Add("0"); }
                }
                for (int loop4 = 28; loop4 >= 4; loop4 -= 4) { userInputBinaryList.Insert(loop4, ","); }
                Console.Write("\nThe binary 32-bit value is equal to: ");
                foreach (string item in userInputBinaryList) { Console.Write(item); }
            }
            else if (userInputInt < 0) { Console.WriteLine("\nError, the number is too small, has to be over or equal to 0. Try again."); }
            else { Console.WriteLine("\nError, the number entered is too large, can't be over 4,294,967,295. Try again."); }
        }
    }
    private static void ConvertToDecimal()
    {
        double userInputDecimalInt = 0;
        Console.Write("\nEnter a binary number : ");
        string userInputStr = Console.ReadLine();
        if (!double.TryParse(userInputStr, out double userInputInt)) { Console.WriteLine("\nError, input entered is not a number. Try again."); }
        else
        {
            bool invalidEntry = false;
            foreach (char bit in userInputStr)
            {
                if (bit != '1' && bit != '0') { invalidEntry = true; Console.WriteLine("\nError, number entered contains incorrect numbers, only base 2 is allowed, so 1s and 0s. Try again."); break; }
            }
            if (invalidEntry == false)
            {
                if (userInputStr.Length > 32) { Console.WriteLine("\nError, too many bits entered, maximum number of bits is 31. Try again."); }
                else
                {
                    int powers2Loop = 0;
                    for (int bit = userInputStr.Length - 1; bit >= 0; bit--)
                    {
                        if (userInputStr[bit] == '1') { userInputDecimalInt += Math.Pow(2, powers2Loop); }
                        powers2Loop += 1;
                    }
                    Console.WriteLine("\nThe number in decimal is equal to: {0}", userInputDecimalInt);
                }
            }
        }
    }
    private static string BaseCheck(string output)
    {
        string[] validBases = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
        bool validation = false;
        string baseInput = "";

        while (validation == false)
        {
            Console.Write(output);
            baseInput = Console.ReadLine();
            foreach (string bases in validBases)
            {
                if (bases == baseInput) { validation = true; break; }
            }
            if (validation == false) { Console.WriteLine("\nError, base entered is not supported, only bases 2 to 16 are supported... Try again."); }
        }
        return baseInput;
    }
    private static string NumberCheck(string baseNumStr)
    {
        string userInputNumStr = "";
        int baseNumInt = Int16.Parse(baseNumStr);
        char[] validCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        Array.Resize(ref validCharacters, baseNumInt);
        bool validation = false;
        while (validation == false)
        {
            Console.Write("\nThe valid character set for base {0} is:", baseNumStr);
            foreach (char character in validCharacters) { Console.Write(" {0}", character); }

            Console.Write("\nEnter a number you want to convert: ");
            userInputNumStr = Console.ReadLine();
            if (userInputNumStr.Length > 0)
            {
                int validCharacterCount = 0;
                foreach (char userCharacter in userInputNumStr)
                {
                    foreach (char validCharacter in validCharacters)
                    {
                        if (userCharacter == validCharacter) { validCharacterCount += 1; }
                    }
                }
                if (validCharacterCount == userInputNumStr.Length) { validation = true; }
                else { Console.WriteLine("\nError, 1 or more characters entered are not supported in the base selected. Try again."); }
            }
            else { Console.WriteLine("\nError, input must be entered. Try again."); }

        }
        return userInputNumStr;
    }
    private static void ConvertFromToAnyBase()
    {
        char[] validCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        string baseNumFromStr = BaseCheck("\nEnter a base number you want to enter a number in: ");
        string numberFromStr = NumberCheck(baseNumFromStr);
        int baseNumFromInt = Int16.Parse(baseNumFromStr);

        string baseNumToStr = BaseCheck("\nEnter a base number you want to convert to: ");
        int baseNumToInt = Int16.Parse(baseNumToStr);

        //Converting number entered into a decimal value
        double numberToInBase10 = 0;
        int powersBaseNumFromLoop = 0;
        int numberValue = 0;
        for (int columnCount = numberFromStr.Length - 1; columnCount >= 0; columnCount--)
        {
            if (numberFromStr[columnCount] != '0')
            {
                for (int validCharacter = 0; validCharacter < validCharacters.Length; validCharacter++)
                {
                    if (validCharacters[validCharacter] == numberFromStr[columnCount]) { numberValue = validCharacter; }
                }
                numberToInBase10 += Math.Pow(baseNumFromInt, powersBaseNumFromLoop) * numberValue;
            }
            powersBaseNumFromLoop += 1;
        }
        Console.WriteLine("\nNumber in base 10: {0}", numberToInBase10);


        //Converting from base 10 to entered base
        bool positiveValuesPresent = false;
        string numberTo = "";

        for (int powersBaseNumToLoop = 60; powersBaseNumToLoop >= 0; powersBaseNumToLoop--)
        {
            for (int maxValueInColumn = baseNumToInt - 1; maxValueInColumn >= 0; maxValueInColumn--)
            {
                if (numberToInBase10 - (Math.Pow(baseNumToInt, powersBaseNumToLoop) * maxValueInColumn) >= 0)
                {
                    if ((positiveValuesPresent == false && maxValueInColumn > 0) || positiveValuesPresent == true)
                    {
                        positiveValuesPresent = true;
                        numberTo += validCharacters[maxValueInColumn];
                        numberToInBase10 -= (Math.Pow(baseNumToInt, powersBaseNumToLoop) * maxValueInColumn);
                        break;
                    }
                }
            }
        }
        Console.WriteLine("The number {0} in base {1}, is equal to: {2} in base {3}", numberFromStr, baseNumFromStr, numberTo, baseNumToStr);
    }
    private static string DisplayMenu()
    {
        string command = "";
        while (command != "1" && command != "2" && command != "3")
        {
            Console.WriteLine(@"
--------------------------
Display menu commands:
    1 : Convert binary to decimal.
    2 : Convert decimal to binary.
    3 : Convert from any base to another base.");
            command = Console.ReadLine();
            if (command != "1" && command != "2" && command != "3") { Console.WriteLine("\nError, command not found, try again."); }
        }
        return command;
    }

    public static void Main()
    {
        string command;
        while (true)
        {
            command = DisplayMenu();
            if (command == "1") { ConvertToDecimal(); }
            else if (command == "2") { ConvertToBinary(); }
            else { ConvertFromToAnyBase(); }
        }

    }
}