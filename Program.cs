using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LoanManagmentSystem
{

    class Program
    {
        public static Boolean LOGGED_IN = false;
        public static Boolean isAdminCreated = false;
        public static Boolean EXIT = false;
        public static int TotalLoanRequestToday = 0;
        public static double TotalWithdrawalMoneyToday = 0.0;
        public static double TotalReceivedLoanMoney = 0.0;

        //to make console full screen 
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;
     

        static void Main(string[] args)
        {
            //make console fullscreen
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
          
            Menu Menus = new Menu();
            Home home;
            Loan loan;
            LoanType loanType;
            LoanPlan loanPlan;
            Payment payment;
            Customers customers;
            Report report;
            //Create Files
            File.AppendAllText(@"./customer.txt", "");
            File.AppendAllText(@"./active_loans.txt", "");
            File.AppendAllText(@"./loan_plan.txt", "");
            File.AppendAllText(@"./loan_type.txt", "");
            File.AppendAllText(@"./payment_log.txt", "");

            
            //Setup Administrator
            Auth Administartor = new Auth();
            loginSuccess:
            //Verify Administrator  
            if (LOGGED_IN)
            {
                MenuselectionPoint:      //used for looping back to this point from the goto point
                Menus.menu1();
                Console.Write("Select Service : _\b");
                string choice = Console.ReadLine();
                
                try
                {
                    switch (int.Parse(choice))
                    {
                        case 1:
                            home = new Home();
                            break;
                        case 2:
                            loan = new Loan();
                            break;
                        case 3:
                            payment = new Payment();
                            break;
                        case 4:
                            loanPlan = new LoanPlan();
                            break;
                        case 5:
                            loanType = new LoanType();
                            break;
                        case 6:
                            customers = new Customers();
                            break;
                        case 7:
                            report = new Report();
                            break;
                        case 8:
                            EXIT = true;
                            break;
                        default:
                            Console.WriteLine("No Service For This Selection.Please Use Numbers Between 1 - 6 only!");
                            goto MenuselectionPoint;

                    }
                    if (!EXIT){
                        goto MenuselectionPoint;
                    }
                    else{
                        Console.WriteLine("Press Enter To EXIT....");
                    }
                }
                catch (System.FormatException e) {
                    //Console.WriteLine(e);
                    Console.WriteLine("Wrong Input..Please Use Numbers only.Press Any Key To continue.For Exit Press 0");
                    var back = Console.ReadLine();
                    if (back != "0") {
                        goto MenuselectionPoint;
                    }
                    
                
                }
                
            }
            else
            {
                
                    Console.Clear();
                    Console.WriteLine("Please Try To Login With The Correct Credential.You 1 Trial Left");
                    Administartor.LoginTrial();
                    if (LOGGED_IN)
                    {
                         goto loginSuccess;
                    }
                    Console.WriteLine("Please Try Another Time....Press Enter To Exit...");
                    
            
            }
           
            Console.Read();
            
        }


    }
    
    
}
