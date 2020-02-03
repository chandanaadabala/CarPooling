using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.UI
{
    static class PasswordMasker
    {
        /// <summary>
        /// This method masks the password when user entering the password in terminal
        /// </summary>
        /// <returns></returns>
        internal static string GetPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while(keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Escape && (!char.IsControl(keyInfo.KeyChar)))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
                else if(keyInfo.Key == ConsoleKey.Backspace && !string.IsNullOrEmpty(password))
                {
                    password = password.Substring(0, password.Length - 1);
                    int curPos = Console.CursorLeft;
                    Console.SetCursorPosition(curPos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(curPos - 1, Console.CursorTop);
                }
                else if(keyInfo.Key == ConsoleKey.Escape && !string.IsNullOrEmpty(password))
                {
                    
                    int curPos = Console.CursorLeft;
                    Console.SetCursorPosition(curPos - password.Length, Console.CursorTop);
                    for(int i = 0; i < password.Length; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition(curPos - password.Length, Console.CursorTop);
                    password = "";
                }
                keyInfo = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }
    }
}
