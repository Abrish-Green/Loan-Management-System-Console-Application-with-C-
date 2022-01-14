using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Home
    {
        public Boolean GoOut = false;

        public Home()
        {
            try
            {

                MenuPoint:

                new Menu().HomeMenu();

                Console.Write("Select Service : _\b");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TotalWithdrawalMoneyToday();
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;
                    case "2":
                        TotalLoanRequestedToday();
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;
                    case "3":
                        TotalReceivedLoanMoney();
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;
                    case "4":
                        activeCustomers();
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;
                    case "5":
                        GoOut = true;
                        break;
                    default:
                        break;
                }

                if (!GoOut)
                {
                    goto MenuPoint;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Console.WriteLine("Database Empty.Hit Enter to Continue...");
                Console.Read();
                Console.Read();
            }
        }

        public void activeCustomers()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t Active Customers");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-20} | {6,-10}  ", "ID", "First Name", "Last Name", "Middle Name", "Sex", "Address","Status");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./customer.txt"))
            {

                if (letter.Equals('\n'))
                {
                    rows.Add(tempoLine);
                    tempoLine = "";
                    //Console.WriteLine();
                }
                else
                {
                    tempoLine += letter;
                    //Console.Write(letter);
                }

            }

            if (File.ReadAllText("./customer.txt").Length == 0)
            {
                Console.WriteLine("No Database");
            }

            var counter = 0;
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine("| {0, -5} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-20} | {6,-20} ", counter, eachLine[0], eachLine[1], eachLine[2], eachLine[3], eachLine[4],"Active");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
          

        }

        public void TotalWithdrawalMoneyToday()
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\t\tView Recently WithDrawal Money  ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\tTotal Loan Requests Recently : {0} Birr", Math.Round(Program.TotalWithdrawalMoneyToday, 2));
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
        }
        public void TotalLoanRequestedToday()
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\t\tRecently Loan Requests ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\tTotal Loan Requests Recently : {0} Requests", Program.TotalLoanRequestToday);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");

        }

        public void TotalReceivedLoanMoney()
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\t\tRecently Recived Money Today ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\tTotal Recived Money Today[{0}] : {1} Birr", DateTime.Now, Math.Round(Program.TotalReceivedLoanMoney, 2));
            Console.WriteLine("-----------------------------------------------------------------------------------------------");

        }
    }
}
