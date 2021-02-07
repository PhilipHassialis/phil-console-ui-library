using System;
using System.Linq;

namespace phil_console_ui_ibrary
{
    /// <summary>
    /// Reads from console. Contains several functions to facilitate specific read scenarios.
    /// </summary>
    public class ConsoleReaders
    {
        /// <summary>
        /// Reads a positive integer from the console. Expects a maxValue of a positive integer. If amaxValue is less or equal than 0 an ArgumentException is thrown.
        /// </summary>
        /// <param name="maxValue">The maximum value allowed to be typed by the user.</param>
        public static int ReadPositiveInteger(int maxValue)
        {
            if (maxValue <= 0) throw new ArgumentException("maxValue must be a positive integer");
            ConsoleKey? consoleKey;
            string strNumber = "";
            string potentialNewNumber = "";
            int currNumber = 0;
            var cursorPos = Console.GetCursorPosition();
            do
            {
                Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
                Console.Write(strNumber);
                ConsoleKeyInfo ckInfo = Console.ReadKey(true);
                consoleKey = ckInfo.Key;
                if (ckInfo.KeyChar < '0' && ckInfo.KeyChar > '9' && ckInfo.Key != ConsoleKey.Enter && ckInfo.Key != ConsoleKey.Backspace)
                {
                    Console.Beep();
                    continue;
                }

                if (ckInfo.KeyChar >= '0' && ckInfo.KeyChar <= '9')
                {
                    potentialNewNumber = strNumber + ckInfo.KeyChar;
                    if (int.Parse(potentialNewNumber) <= maxValue)
                    {
                        strNumber = potentialNewNumber;
                        currNumber = int.Parse(strNumber);
                    }

                }
                else
                {
                    if (ckInfo.Key == ConsoleKey.Backspace)
                    {
                        if (strNumber.Length > 0)
                        {
                            strNumber = strNumber.Substring(0, strNumber.Length - 1);
                            if (strNumber.Length > 0)
                                currNumber = int.Parse(strNumber);
                            else
                                currNumber = 0;
                            Console.SetCursorPosition(cursorPos.Left + strNumber.Length, cursorPos.Top); Console.Write(" ");
                        }

                    }
                }

            } while (consoleKey != ConsoleKey.Enter);
            Console.WriteLine();
            return currNumber;
        }

        /// <summary>
        /// Reads an integer from the console.
        /// </summary>
        /// <param name="minValue">The minimum acceptable input value</param>
        /// <param name="maxValue">The maximum acceptable input value</param>
        /// <returns></returns>
        public static int ReadInteger(int minValue, int maxValue)
        {
            ConsoleKey? consoleKeyRead;
            string strNumber = "";
            string potentialNewNumber = "";
            int currNumber = 0;
            var cursorPos = Console.GetCursorPosition();
            int readNumber;
            do
            {
                Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
                Console.Write(strNumber);
                ConsoleKeyInfo ckInfo = Console.ReadKey(true);
                consoleKeyRead = ckInfo.Key;
                ConsoleKey[] otherKeys = { ConsoleKey.Enter, ConsoleKey.Backspace, ConsoleKey.OemMinus };

                if (ckInfo.KeyChar < '0' && ckInfo.KeyChar > '9' && otherKeys.Contains(ckInfo.Key))
                {
                    Console.Beep();
                    continue;
                }

                if ((ckInfo.KeyChar >= '0' && ckInfo.KeyChar <= '9') || ckInfo.Key == ConsoleKey.OemMinus)
                {
                    potentialNewNumber = strNumber + ckInfo.KeyChar;
                    readNumber = int.Parse(potentialNewNumber);
                    if (readNumber >= minValue && readNumber <= maxValue)
                    {
                        strNumber = potentialNewNumber;
                        currNumber = readNumber;
                    }
                }
                else
                {
                    if (ckInfo.Key == ConsoleKey.Backspace)
                    {
                        if (strNumber.Length > 0)
                        {
                            strNumber = strNumber.Substring(0, strNumber.Length - 1);
                            if (strNumber.Length > 0)
                                currNumber = int.Parse(strNumber);
                            else
                                currNumber = 0;
                            Console.SetCursorPosition(cursorPos.Left + strNumber.Length, cursorPos.Top); Console.Write(" ");
                        }

                    }
                }

            } while (consoleKeyRead != ConsoleKey.Enter);
            Console.WriteLine();
            return currNumber;
        }

    }
}