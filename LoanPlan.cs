using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class LoanPlan
    {
        public int loanPlanInMonth;
        public double loanInterestInPercent;
        public double loanOverDuePenalty;
        public Boolean GoOut = false;

        public LoanPlan()
        {
            try
            {
                MenuPoint:

                new Menu().loanPlanMenu();

                Console.Write("Select Service : _\b");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        createLoanPlans();
                        break;
                    case "2":
                        viewLoanPlan();
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

                Console.WriteLine("Database Empty or Error May Occured .Hit Enter to Continue...");
                Console.Read();
                Console.Read();

            }
        }

        public void createLoanPlans() {

            Console.WriteLine("==================Create Loan Plan============================");
            Console.Write("Enter Plan in Month :    Month\b\b\b\b\b\b\b\b\b");
            var plan = Console.ReadLine();
            loanPlanInMonth = int.Parse(plan);
            Console.WriteLine();
            Console.Write("Enter Loan Interset Percent(%):      %\b\b\b\b\b\b");
            var interest = Console.ReadLine();
            loanInterestInPercent = double.Parse(interest);
            Console.WriteLine();
            Console.Write("Enter Monthly Over Due's Penalty:      %\b\b\b\b\b\b");
            var OverDuePenalty = Console.ReadLine();
            loanOverDuePenalty = double.Parse(OverDuePenalty);

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("\t\t\t Loan Plan ");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Plan: {0} Month",plan);
            Console.WriteLine("Interest: {0} %",interest);
            Console.WriteLine("Monthly Over Due's Penalty: {0} %",OverDuePenalty);
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("Are You Sure You Want To Save?[Y/N] : ");
            string Save = Console.ReadLine();
          
            switch (Save)
            {
                case "Y":
                case "y":

                    //create a file named loanPlans
                    //save data as a row
                    var line = loanPlanInMonth + " " + loanInterestInPercent + " " + loanOverDuePenalty;
                    File.AppendAllText(@"./loan_plan.txt", line + Environment.NewLine);


                    Console.WriteLine("Loan Plan Has Been Saved.Please Hit Enter To Continue...");
                    break;
                default:
                    Console.WriteLine("Loan Plan Has Been Discarded.Please Hit Enter To Continue...");
                    break;
            }

            Console.Read();
        }

        public static void viewLoanPlan()
        {
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-4} | {1,-15} | {2,-15} | {3}", "ID", "Plan [Month]", "Interest[%]", "Over Due's Penalty[%]");
            Console.WriteLine("---------------------------------------------------------------------------------");
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./loan_plan.txt"))
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
            if (File.ReadAllText("./loan_plan.txt").Length == 0)
            {
                Console.WriteLine("No Database");
            }

            var counter = 0;
            foreach(string line in rows)
            {
                counter++;
                var eachLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("| {0,-4} | {1,-15} | {2,-15} | {3}",counter, eachLine[0], eachLine[1], eachLine[2] );
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            
        }

        public static string getLoanPlan(int ID)
        {
            string result = "";
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./loan_plan.txt"))
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

            var counter = 0;
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (counter == ID)
                {
                  result = counter+" "+eachLine[0]+" "+eachLine[1]+" "+eachLine[2];

                }
               
            }
            

            return result;

        }
    }
}
