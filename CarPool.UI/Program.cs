using CarPool.Services;
using System;

namespace CarPool.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select\n1. SignUp\n2. SignIn\n3. Exit");
                UserView _userView = new UserView();
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            string userID = _userView.SignUp();
                            if (!string.IsNullOrEmpty(userID))
                                _userView.UserInterface(userID);
                            else
                                Console.WriteLine("Sign-Up failed!");
                        }
                        break;
                    case "2":
                        {
                            string userID = _userView.SignIn();
                            if (!string.IsNullOrEmpty(userID))
                                _userView.UserInterface(userID);
                            else
                                Console.WriteLine("Sign-In failed!");
                        }
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Select valid option");
                        break;
                }
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
            }
        }
    }
}
