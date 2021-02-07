using System;

namespace phil_console_ui_ibrary
{
    public class ConsoleReaders
    {
        public static int ReadPositiveInteger(int maxValue)
        {
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
    }
}