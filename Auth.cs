using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Auth
    {
        public static Boolean LOGGED_IN = false;
        public static int LoginCounter = 0;
        Utilits UserInputValidator = new Utilits();

        public Boolean startAuth()
        {
            Console.Clear();
            try
            {

                if (File.Exists("./login.txt"))
                {
                    
                    LoginTrial();

                }
                else
                {
                    signUpAdmin();
                    LoginTrial();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return LOGGED_IN;
        }
        public void display()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("=============|    Admin    |==================");
            Console.WriteLine("==============================================");

        }
        public Boolean LoginTrial()
        {
            display();
            string db_username, db_password = "";
            using (StreamReader sr = new StreamReader(File.Open("./Login.txt", FileMode.Open)))
            {
                db_username = sr.ReadLine();
                db_password = sr.ReadLine();
                sr.Close();
            }
            
            while (!LOGGED_IN && LoginCounter< 3)
            {
            Console.Write("Enter Admin User Name: ");
            var UserName = Console.ReadLine();
            UserName =UserInputValidator.InputVarAndValidate(UserName,"string","username")[2].ToString();
            Console.Write("Enter Admin Password: ");
            var Password = Console.ReadLine();
            Password = UserInputValidator.InputVarAndValidate(Password, "string", "password")[2].ToString();

                LoginCounter += 1;

            if (UserName == db_username && Password == db_password)
               {
                    LOGGED_IN = true;

                }
                else
                {
                    if (LoginCounter > 2)
                    {
                        Console.WriteLine("Please Try Later...");
                    }
                    else
                    {
                        Console.WriteLine("You Have {0} Chance Left", 3 - LoginCounter);
                    }

                }
            

            }


            return LOGGED_IN;
        }
        public Boolean signUpAdmin()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("================|  Sign Up  |=================");
            Console.WriteLine("==============================================");
            Console.Write("Create Admin User Name: ");
            var UserName = Console.ReadLine();
            UserName = UserInputValidator.InputVarAndValidate(UserName, "string", "username")[2].ToString();

            Console.Write("Create Admin Password: ");
            var Password = Console.ReadLine();
            Password = UserInputValidator.InputVarAndValidate(Password, "string", "username")[2].ToString();

            using (StreamWriter sw = new StreamWriter(File.Create("./Login.txt")))
            {
                sw.WriteLine(UserName);
                sw.WriteLine(Password);
                sw.Close();
            }

            return true;
        }

    }
}
