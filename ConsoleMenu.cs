using System;

namespace phil_console_ui_ibrary
{

    /// <summary>
    /// A class that helps with Menu creations
    /// </summary>
    public class Menu
    {
        private enum YesNoOption { Yes, No };

        /// <summary>
        /// Displays a menu and returns a value representing the index of the option selected
        /// </summary>
        /// <param name="options">A string array of options - user is expected to select one of them</param>
        /// <param name="activeColor">The color that the active option is highlighted with</param>
        /// <param name="activeBgColor">The background color for the active option</param>
        /// <param name="fixSelectorLength">If true then the selection indicator is padded with spaces for a better menu experience</param>
        /// <returns></returns>
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

        /// <summary>
        /// Displays a Yes / No option
        /// </summary>
        /// <param name="activeYesColor"></param>
        /// <param name="activeNoColor"></param>
        /// <param name="activeBgColor">The background color for the active option</param>
        /// <returns></returns>
        public static Boolean YesNo(ConsoleColor activeYesColor = ConsoleColor.Green, ConsoleColor activeNoColor = ConsoleColor.Red, ConsoleColor activeBgColor = ConsoleColor.Black)
        {
            int currentYPos = Console.GetCursorPosition().Top;
            var currentOption = YesNoOption.Yes;
            string selectedOption = null;
            Console.CursorVisible = false;
            WriteYesNoOption(currentOption, currentYPos, activeYesColor, activeNoColor, activeBgColor);
            do
            {
                var ck = Console.ReadKey();
                switch (ck.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (currentOption==YesNoOption.No)
                        {
                            currentOption = YesNoOption.Yes;
                        }
                        WriteYesNoOption(currentOption, currentYPos, activeYesColor, activeNoColor, activeBgColor);
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentOption == YesNoOption.Yes)
                        {
                            currentOption = YesNoOption.No;
                        }
                        WriteYesNoOption(currentOption, currentYPos, activeYesColor, activeNoColor, activeBgColor);
                        break;
                    case ConsoleKey.Y:
                        currentOption = YesNoOption.Yes;
                        WriteYesNoOption(currentOption, currentYPos, activeYesColor, activeNoColor, activeBgColor);
                        selectedOption = YesNoOption.Yes.ToString();
                        break;
                    case ConsoleKey.N:
                        currentOption = YesNoOption.No;
                        WriteYesNoOption(currentOption, currentYPos, activeYesColor, activeNoColor, activeBgColor);
                        selectedOption = YesNoOption.No.ToString();
                        break;
                    case ConsoleKey.Enter:
                        selectedOption = currentOption.ToString();
                        break;
                    default:
                        Console.Beep();
                        break;
                }

            } while (selectedOption == null);


            Console.CursorVisible = true;
            return selectedOption == YesNoOption.Yes.ToString();

        }
        private static void WriteYesNoOption(YesNoOption activeOption, int cursorYPos, ConsoleColor activeYesColor, ConsoleColor activeNoColor, ConsoleColor activeBgColor)
        {
            Console.SetCursorPosition(0, cursorYPos); Console.ResetColor();
            if (activeOption==YesNoOption.Yes)
            {
                Console.ForegroundColor = activeYesColor; Console.BackgroundColor = activeBgColor;
            }
            Console.Write("[Yes]"); Console.ResetColor(); Console.Write(" ");
            if (activeOption==YesNoOption.No)
            {
                Console.ForegroundColor = activeNoColor; Console.BackgroundColor = activeBgColor;
            }
            Console.Write("[No]"); Console.ResetColor();
        }

        private static void WriteClearedActiveOption(string option, int cursorYPos, int maxOptionLength = 0, int cursorXPos = 0)
        {
            Console.SetCursorPosition(cursorXPos, cursorYPos); Console.ResetColor();
            Console.Write(option.PadRight(maxOptionLength));
        }
        private static void WriteActiveOption(string option, int cursorYPos, ConsoleColor activeColor, ConsoleColor activeBgColor, int maxOptionLength = 0, int cursorXPos = 0)
        {
            Console.SetCursorPosition(cursorXPos, cursorYPos);
            Console.ForegroundColor = activeColor; Console.BackgroundColor = activeBgColor;
            Console.Write(option.PadRight(maxOptionLength));
        }
    }



}
