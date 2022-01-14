using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace LoanManagmentSystem
{
    class Payment
    {
        public int payeeID;
        public string payeeName;
        public string payedDate;
        public string payedMonthName;
        public double payedAmount;
        public double remainingLoanAmount;
        public double remainingMonthlyPayment;
        public double penaltyPaymentAmount;
        public string nextPaymentDate;
        public double tempPenalty;
        public Boolean isCovered = false;
        public Boolean GoOut = false;



        public Payment()
        {


           
            try
            {

                MenuPoint:

                new Menu().paymentMenu();

                Console.Write("Select Service : _\b");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        makePayment();
                        break;
                    case "2":
                        ViewAllPaymentLog();
                        break;
                    case "3":
                        Console.Write("Select User [Use ID]: ");
                        int chooseUser_ = int.Parse(Console.ReadLine());
                        viewPaymentLogByUserID(chooseUser_);
                        break;
                    case "4":
                        viewPaymentActiveUsers();
                        Console.Write("Select User [Use ID]: ");
                        int choose = int.Parse(Console.ReadLine());
                        
                        
                        if (getPaymentInfoByUserID(choose) != "")
                        {
                            if (getPaymentInfoByUserID(choose).Split("|")[5][0].ToString() == "-".ToString() || getPaymentInfoByUserID(choose).Split("|")[5][0].ToString() == "0".ToString())
                            {
                                Console.WriteLine("|-----------------------------------------------------------------------|");
                                Console.WriteLine("| You Completely Covered Your Loan.");
                                Console.WriteLine("|-----------------------------------------------------------------------|");
                                isCovered = true;
                            }
                            else
                            {
                                
                                    Console.WriteLine("|-----------------------------------------------------------------------|");
                                    Console.WriteLine("| Loan is Not Completely Covered Yet. Or No Active Loan.");
                                    Console.WriteLine("|-----------------------------------------------------------------------|");
                                
                            }

                        }
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

        /// <summary>
        /// 
        /// </summary>
        public void makePayment()
        {
    
            viewPaymentActiveUsers();
            //Enter Userid
            Console.Write("Select User [Use ID]: ");
            int chooseUser = int.Parse(Console.ReadLine());
            if (getPaymentInfoByUserID(chooseUser) != "")
            {
                if (getPaymentInfoByUserID(chooseUser).Split("|")[5][0].ToString() == "-".ToString() || getPaymentInfoByUserID(chooseUser).Split("|")[5][0].ToString() == "0".ToString())
                {
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Console.WriteLine("| You Completely Covered Your Loan.");
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    isCovered = true;
                }

            }

            if (!isCovered)
            {
                if (Loan.ViewLoanDataByCustomerID(chooseUser) != "NOT_FOUND")
                {
                    DateTime date = new DateTime();
                    var Data = Loan.ViewLoanDataByCustomerID(chooseUser).Split("|");
                    var CurrentDate = DateTime.Now;
                    string CurrentMonthName = date.ToString("MMMM");
                    var current = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    var next = current.AddMonths(1);
                    //Console.WriteLine("{0} {1} {2} {3} {4} {5} ", Data[3], Data[4], Data[5], Data[6], Data[7], Data[8]);

                    double intersetRate = double.Parse(LoanPlan.getLoanPlan(int.Parse(Data[5])).Split(" ")[2]);
                    int totalMonth = int.Parse(LoanPlan.getLoanPlan(int.Parse(Data[5])).Split(" ")[1]);
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Console.WriteLine("|\t\t\t\tNew Payment\t\t\t\t|");
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    tempPenalty = double.Parse(Data[10]);

                    Console.Write("|Enter Pay Amount: ");
                    payedAmount = double.Parse(Console.ReadLine());
                    //trace day payment
                    Program.TotalReceivedLoanMoney += payedAmount;// add to the global variable

                    Console.Write("|Is There Any Penalty [Y/N ]: ");
                    string penaltys = Console.ReadLine();



                    switch (penaltys)
                    {
                        case "Y":
                        case "y":

                            penaltyPaymentAmount = tempPenalty;
                            Program.TotalReceivedLoanMoney += tempPenalty;
                            break;
                        default:
                            penaltyPaymentAmount = 0;
                            break;
                    }

                    try
                    {
                        if (getPaymentInfoByUserID(chooseUser) != "")
                        {
                            if (getPaymentInfoByUserID(chooseUser).Split("|")[5] != "")
                            {
                                remainingLoanAmount = double.Parse(getPaymentInfoByUserID(chooseUser).Split("|")[5]) - payedAmount - penaltyPaymentAmount;
                            }

                        }
                        else
                        {
                            remainingLoanAmount = double.Parse(Data[8]) - payedAmount - penaltyPaymentAmount;

                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error Occured.Press Enter to Continue...");
                    }

                    payeeID = chooseUser;
                    payeeName = Data[1] + " " + Data[2];
                    remainingMonthlyPayment = double.Parse(Data[9]) - payedAmount;
                    payedDate = CurrentDate.ToString("MM-dd-yyyy");
                    payedMonthName = CurrentMonthName;
                    nextPaymentDate = next.ToString("MM-dd-yyyy");

                    // id Payment Date	Payment	Principal	Interest	Total Interest Paid	Remaining Balance

                    paymentForm();
                    
                    savePayment(intersetRate, totalMonth);
                }
                else
                {
                    Console.WriteLine("User Not Found");
                }


            }

            Console.WriteLine("Press Enter To Continue...");
            Console.Read();
        }

        public void viewPaymentActiveUsers()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t Select Customer By ID ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-4} | {1,-15} | {2,-15} | {3}", "ID", "First Name", "Last Name", "Middle Name");
            Console.WriteLine("---------------------------------------------------------------------------------");
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

            var counter = 0;
            if (File.ReadAllText("./active_loans.txt").Length == 0)
            {
                Console.WriteLine("No Database");
            }

            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (eachLine[12] == "GRANTED")
                {
                    Console.WriteLine("| {0,-4} | {1,-15} | {2,-15} | {3}", counter, eachLine[0], eachLine[1], eachLine[2]);

                }
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
        }

        public void savePayment(double InterestRate, double totalMonth)
        {
            // id payee Payment Date	payedamount	todayInterest	remainingAmount	mothName remainingMonthly penalty  next payment Paid	Remaining Balance
            double todaysInterestInBirr = (InterestRate / totalMonth) * remainingLoanAmount - (remainingLoanAmount);
            Program.TotalReceivedLoanMoney += payedAmount;
            if (remainingLoanAmount <= 0)
            {
                var line = payeeID + "|" + payeeName + "|" + payedDate + "|" + Math.Round(payedAmount, 2) + "|" + Math.Round(todaysInterestInBirr, 2) + "|" + 0 + "|" + payedMonthName + "|" + Math.Round(remainingMonthlyPayment, 2)  + "|" + Math.Round(penaltyPaymentAmount, 2) + "|" + nextPaymentDate + "|" + "PAID";
                var change = remainingLoanAmount <0 ? "You Have a Change of " + (-1*remainingLoanAmount) : "";
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|You Completely Covered Your Loan.{0} Birr", Math.Round(double.Parse(change), 2));
                Console.WriteLine("|-----------------------------------------------------------------------|");
                File.AppendAllText(@"./payment_log.txt", line + Environment.NewLine);
            }
            else
            {
                var line = payeeID + "|" + payeeName + "|" + payedDate + "|" + Math.Round(payedAmount, 2) + "|" + Math.Round(todaysInterestInBirr, 2) + "|" + remainingLoanAmount + "|" + payedMonthName + "|" + Math.Round(remainingMonthlyPayment, 2) + "|" + Math.Round(penaltyPaymentAmount, 2) + "|" + nextPaymentDate + "|" + "PAID";
                File.AppendAllText(@"./payment_log.txt", line + Environment.NewLine);
                
            }
           

        }
        public void paymentForm()
        {
            try
            {
                
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("\n\n");

                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|\t\t\t Payment Recipt");
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.WriteLine("|{0} : {1}","Payee ID",payeeID);
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.WriteLine("|{0} : {1}","Payee Name",payeeName);
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.WriteLine("|{0} : {1}","Payment Date",payedDate);
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.WriteLine("|{0} : {1}","Payed Month", payedMonthName);
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.WriteLine("|{0} : {1} {2}","Payed Amount", Math.Round(payedAmount, 2) ,"BIRR");
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.WriteLine("|{0} : {1} {2}", "Total Remaining Payment",remainingLoanAmount<0 ? 0 : Math.Round(remainingLoanAmount), "BIRR");
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.WriteLine("|{0} : {1} {2}", "Penalty", Math.Round(penaltyPaymentAmount),"BIRR");
                Console.WriteLine("|-----------------------------------------------------------------------|");

                //payment report


                Console.WriteLine("|{0} : {1}", "Next Payment", nextPaymentDate);    
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("\n\n");

                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("| Payment Has been Made By {0} Successfully!", payeeName);
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|-----------------------------------------------------------------------|");



            }
            catch (Exception e)
            {

                Console.WriteLine("There Has Been An Error.Please Come back later...");
                Console.Read();
            }


        }

    

        public static string getPaymentInfoByUserID(int ID)
        {
            string result = "";
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

            foreach (string line in rows)
            {
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (int.Parse(eachLine[0]) == ID)
                {
                    result = eachLine[0] + "|" + eachLine[1] + "|" + eachLine[2] + "|" + eachLine[3] + "|" + eachLine[4] + "|" + eachLine[5] + "|" + eachLine[6] + "|" + eachLine[7] + "|" + eachLine[8] + "|" + eachLine[9];
                }
            }


            return result;
        }

        public void ViewAllPaymentLog()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\tAll Payment Log");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-25} | {5,-15} | {6,-20} | {7,-20} | {8}", "ID", "Payee", "Date", "Paid Amount" ,"Remaining Loan Amount", "Month","Monthly Penalty","Next Payment Date","Status");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
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

            var counter = 0;
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-25} | {5,-15} | {6,-20} | {7,-20} | {8}", eachLine[0], eachLine[1], eachLine[2], "$" + Math.Round(double.Parse(eachLine[3])), "$" + Math.Round(double.Parse(eachLine[5])), eachLine[6], "$" + Math.Round(double.Parse(eachLine[8])), eachLine[9], eachLine[10]);
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();
        }

        public static void viewPaymentLogByUserID(int ID)
        {

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t Customer Payment Log");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-25} | {5,-15} | {6,-20} | {7,-20} | {8}", "ID", "Payee", "Date", "Paid Amount", "Remaining Loan Amount", "Month", "Monthly Penalty", "Next Payment Date", "Status");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
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

            var counter = 0;
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (int.Parse(eachLine[0]) == ID)
                {
                    Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-25} | {5,-15} | {6,-20} | {7,-20} | {8}", eachLine[0], eachLine[1], eachLine[2], "$" + Math.Round(double.Parse(eachLine[3])), "$" + Math.Round(double.Parse(eachLine[5])), eachLine[6], "$" + Math.Round(double.Parse(eachLine[8])), eachLine[9], eachLine[10]);

                }
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();

        }

    }
}
