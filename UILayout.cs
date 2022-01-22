using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManagmentSystem
{
    class UILayout
    {

        Menu menu = new Menu();
        public void WelcomePage()
        {
            Console.WriteLine("+----------------------------------------------------------------");
            Console.WriteLine("+---------------------------| WELCOME TO |-----------------------");
            Console.WriteLine("+-------------------|  LOAN MANAGEMENT SYSTEM  |-----------------");
            Console.WriteLine("+----------------------------------------------------------------");
            Console.WriteLine("+----------------------------------------------------------------");
            Console.WriteLine("| Welcome, Please Choose Your User Type ?");
            Console.WriteLine("+----------------------------------------------------------------");
            Console.WriteLine("| 1.Customer");
            Console.WriteLine("| 2.Administrator");
            Console.WriteLine("+----------------------------------------------------------------");


        }

        public string LoginOrSignUpPage(int userType)
        {
            Console.Clear();
            string err = "";
            Console.WriteLine("+----------------------------------------------------------------");
            Console.WriteLine("+---------------------------| WELCOME TO |-----------------------");
            Console.WriteLine("+-------------------|  LOAN MANAGEMENT SYSTEM  |-----------------");
            Console.WriteLine("+----------------------------------------------------------------");

            switch (userType)
            {
                case 1:
                    Console.WriteLine("+----------------------------------------------------------------");
                    Console.WriteLine("| Welcome Customer");
                    Console.WriteLine("+----------------------------------------------------------------");
                    Console.WriteLine("| 0.<-Back");
                    Console.WriteLine("| 1.Login");
                    Console.WriteLine("| 2.Sign Up");
                    Console.WriteLine("+----------------------------------------------------------------");
                    break;
                case 2:
                    Console.WriteLine("+----------------------------------------------------------------");
                    Console.WriteLine("| Welcome Administrator");
                    Console.WriteLine("+----------------------------------------------------------------");
                    Console.WriteLine("| 0.<-Back");
                    Console.WriteLine("| 1.Login");
                    Console.WriteLine("+----------------------------------------------------------------");
                    break;
                default:
                    Console.WriteLine("Wrong Input");
                    err = "Wrong Input";
                    break;

            }
            return err;


           

        }

        public void CustomerWelcomePage()
        {
            menu.GetMenu("CUSTOMER_MAIN_MENU");
        }
    }
}
