using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Auth
    {
       
        
        public Auth() {
            try
            {

                if (File.Exists("./login.txt"))
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine("=============| Admin Login |==================");
                    Console.WriteLine("==============================================");
                   
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


        }

        public void LoginTrial()
        {
           
            Console.Write("Enter Admin User Name: ");
            var UserName = Console.ReadLine();
            Console.Write("Enter Admin Password: ");
            var Password = Console.ReadLine();
            string db_username, db_password = "";

            // VERIFY USER

            using (StreamReader sr = new StreamReader(File.Open("./Login.txt", FileMode.Open)))
            {
                db_username = sr.ReadLine();
                db_password = sr.ReadLine();
                sr.Close();
            }
            

            if (UserName == db_username && Password == db_password)
            {
                Program.LOGGED_IN = true;
            }
            else
            {
               // Console.WriteLine("Not Logged...Try Another Time");
            }

        }

        public void signUpAdmin()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("=================| WELCOME TO |===============");
            Console.WriteLine("========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("==============================================");

            Console.Write("Create Admin User Name: ");
            var UserName = Console.ReadLine();
            Console.Write("Create Admin Password: ");
            var Password = Console.ReadLine();


            using (StreamWriter sw = new StreamWriter(File.Create("./Login.txt")))
            {
                sw.WriteLine(UserName);
                sw.WriteLine(Password);
                sw.Close();
            }
            



        }

    }
}
