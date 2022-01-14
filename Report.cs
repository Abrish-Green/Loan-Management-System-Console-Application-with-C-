using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace LoanManagmentSystem
{
    class Report
    {
        public double MOnthlyGivenLoanInMoney = 0.0;
        public double MonthlyPaidLoanInMoney = 0.0;
        public double AnnuallyGivenOutLoan = 0.0;
        public double AnnuallyPaidLoanMoney = 0.0;
        public Boolean GoOut = false;

        public Report()
        {
            try
            {

                MenuPoint:

                new Menu().reportMenu();

                Console.Write("Select Service : _\b");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        monthlyPayOut();
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;
                    case "2":
                        mothlyPayIn();
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;
                    case "3":
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
        
        
        public void monthlyPayOut()
        {
           

            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./active_loans.txt"))
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
            if (File.ReadAllText("./active_loans.txt").Length == 0)
            {
                Console.WriteLine("No Database");
            }

            var counter = 0;
            double sum = 0;
            foreach (string line in rows)
            {
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (eachLine[12] == "GRANTED")
                {
                    counter++;
                    sum += double.Parse(eachLine[8]);
                    
                }
            }
            MOnthlyGivenLoanInMoney = sum;
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\t\tView Monthly Given Loans");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\tTotal Loans Given this Month [{0}] : {1} Birr",DateTime.Now.ToString("MMMM"), Math.Round(MOnthlyGivenLoanInMoney, 2));
            Console.WriteLine("-----------------------------------------------------------------------------------------------");

        }

        public void mothlyPayIn()
        {

               
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./payment_log.txt"))
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

            if (File.ReadAllText("./payment_log.txt").Length == 0)
            {
                Console.WriteLine("No Database");
            }
            double sum = 0;
            foreach (string line in rows)
            {
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                if (eachLine[6] == DateTime.Now.ToString("MMMM"))
                {
                    sum += double.Parse(eachLine[3]);

                }



            }
            MonthlyPaidLoanInMoney = sum;
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\t\tView Recently Paid Loan in {0}",DateTime.Now.ToString("MMMM"));
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("|\t\tTotal Loan Requests Recently : {0} Birr", Math.Round(MonthlyPaidLoanInMoney, 2));
            Console.WriteLine("-----------------------------------------------------------------------------------------------");

        }

    }
}
