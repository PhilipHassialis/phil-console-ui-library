using System;

namespace phil_console_ui_ibrary
{

    public class Menu
    {
        public static int Display(string[] options, ConsoleColor activeColor = ConsoleColor.Blue,
            ConsoleColor activeBgColor = ConsoleColor.Black, bool fixSelectorLength = false)
        {
            int currentOption = 0;
            int currentYPos = Console.GetCursorPosition().Top;
            int selectedOption = -1;
            int maxOptionLength = 0;
            if (fixSelectorLength)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    options[i] = $" {options[i]} ";
                    if (maxOptionLength < options[i].Length) maxOptionLength = options[i].Length;
                }
            }

            foreach (var o in options) Console.WriteLine(o);
            Console.CursorVisible = false;
            WriteActiveOption(options[currentOption], currentYPos + currentOption, activeColor, activeBgColor, maxOptionLength);
            do
            {
                var ck = Console.ReadKey(true);
                switch (ck.Key)
                {
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        Console.ResetColor();
                        Console.SetCursorPosition(0, options.Length);
                        selectedOption = currentOption;
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentOption > 0)
                        {
                            WriteClearedActiveOption(options[currentOption], currentYPos + currentOption, maxOptionLength);
                            currentOption--;
                            WriteActiveOption(options[currentOption], currentYPos + currentOption, activeColor, activeBgColor, maxOptionLength);
                        }
                        else
                        {
                            Console.Beep();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentOption < options.Length - 1)
                        {
                            WriteClearedActiveOption(options[currentOption], currentYPos + currentOption, maxOptionLength);
                            currentOption++;
                            WriteActiveOption(options[currentOption], currentYPos + currentOption, activeColor, activeBgColor, maxOptionLength);
                        }
                        else
                        {
                            Console.Beep();
                        }
                        break;
                    default:
                        Console.Beep();
                        break;

                }


            } while (selectedOption == -1);
            Console.CursorVisible = true;
            return selectedOption;
        }
        private static void WriteClearedActiveOption(string option, int cursorYPos, int maxOptionLength = 0)
        {
            Console.SetCursorPosition(0, cursorYPos); Console.ResetColor();
            Console.Write(option.PadRight(maxOptionLength));
        }
        private static void WriteActiveOption(string option, int cursorYPos, ConsoleColor activeColor, ConsoleColor activeBgColor, int maxOptionLength = 0)
        {
            Console.SetCursorPosition(0, cursorYPos);
            Console.ForegroundColor = activeColor; Console.BackgroundColor = activeBgColor;
            Console.Write(option.PadRight(maxOptionLength));
        }
    }

}