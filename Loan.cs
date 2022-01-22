using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Loan
    {

        public string borrowerFirstName;
        public string borrowerLastName;
        public string borrowerMiddleName;
        public string borrowerSex;
        public string borrowerAddres;
        public string borrowerPhone;
        public int loanPlan;
        public double borrowerSalary;
        public string loanType;
        public string loanPurpose;
        public double loanAmount;
        public string LoanGrant;
        public string loanDate = DateTime.Now.ToString();

        double TotalPayableAmount;
        double MonthlyPayableAmount;
        double MonthlyPenalty;
        string RejectionReason;
        public Boolean GoOut = false;

        Utilits utilits = new Utilits();
        public Loan()
        {
            try {

                MenuPoint:

                new Menu().GetMenu("LOAN");

                Console.Write("Select Service : _\b");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (File.ReadAllText("./loan_type.txt").Length == 0 || File.ReadAllText("./loan_plan.txt").Length == 0)
                        {
                            if(File.ReadAllText("./loan_type.txt").Length == 0)
                            {
                                Console.WriteLine("Please Create Loan Type First.");
                            }
                            if(File.ReadAllText("./loan_plan.txt").Length == 0)
                            {
                                Console.WriteLine("Please Create Loan Plan First.");
                            }
                            
                            GoOut = true;
                            Console.WriteLine("Press Enter to Continue...");
                            Console.Read();
                            Console.Read();

                        }
                        else
                        {
                            AnswerLoanRequest();
                            Console.WriteLine("Press Enter To Continue...");
                            Console.Read();
                        }

                        
                       
                        break;
                    case "2":
                        viewLoanList("ALL");
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;

                    case "3":
                        viewLoanList("ACCEPTED");
                        Console.WriteLine("Press Enter To Continue...");
                        Console.Read();
                        break;
                    case "4":
                        viewLoanList("REJECTED");
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

            } catch (Exception e) {
                Console.WriteLine(e);

                Console.WriteLine("Database Empty.Hit Enter to Continue...");
                Console.Read();
                Console.Read();

            }
           
            
        }

       
        public void AnswerLoanRequest()
        {

           

            //select user
            if (viewLoanList("PENDING"))
            {
                Console.WriteLine("Select User To Give Response[Use User ID]: ");
                string User_id = Console.ReadLine();
                User_id = utilits.InputVarAndValidate(User_id, "int", "UserID")[2].ToString();

                Console.WriteLine("Do You Accept the Loan?[Y/N]:");
                string AcceptOrReject = Console.ReadLine();
                AcceptOrReject = utilits.InputVarAndValidate(AcceptOrReject, "string", "Y/y or N/n")[2].ToString();

                switch (AcceptOrReject)
                {
                    case "Y":
                    case "y":
                        //save to file
                        LoanGrant = "ACCEPTED";
                        UpdateLoanStatus(int.Parse(User_id), "ACCEPTED", null);

                        //AddLoanInformation(null); //null - for no rejection reason
                        //trace customer info
                        Customers.AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);

                        Console.WriteLine("|--------------------------------------------------------------------------------------------------|");
                        Console.WriteLine("| Loan Has be Accepted.");
                        Console.WriteLine("|--------------------------------------------------------------------------------------------------|");
                        Home.TotalWithdrawalMoneyTodayInBirr += loanAmount;
                        break;
                    default:
                        LoanGrant = "REJECTED";
                        Console.WriteLine("|-----------------------------------------------------------------------|");
                        Console.WriteLine("| Please Provide a Loan REJECTION reason : ");
                        RejectionReason = Console.ReadLine();
                        RejectionReason = utilits.InputVarAndValidate(RejectionReason, "string", "Rejection Reason")[2].ToString();
                        //AddLoanInformation(RejectionReason);
                        UpdateLoanStatus(int.Parse(User_id), "REJECTED", RejectionReason);
                        Customers.AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                        Console.WriteLine("|-----------------------------------------------------------------------|");
                        Console.WriteLine("|Rejection Reason Has been  Saved.");
                        break;
                }
            }

        }
        
        public void AddLoanInformation(string RejectReason)
        {
            //Loan Information
            string line = "";
            if (RejectReason != null)
            {
                line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerPhone + "|" + borrowerSalary + "|" + loanType + "|" + loanPlan + "|" + loanPurpose + "|" + loanAmount + "|" + TotalPayableAmount + "|" + MonthlyPayableAmount + "|" + MonthlyPenalty + "|" + LoanGrant + "|" + RejectReason + "|" + loanDate;
                File.AppendAllText(@"./active_loans.txt", line + Environment.NewLine);
            }
            else
            {
                line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerPhone + "|" + borrowerSalary + "|" + loanType + "|" + loanPlan + "|" + loanPurpose + "|" + loanAmount + "|" + TotalPayableAmount + "|" + MonthlyPayableAmount + "|" + MonthlyPenalty + "|" + LoanGrant + "|None" + "|" + loanDate;
                File.AppendAllText(@"./active_loans.txt", line + Environment.NewLine);

            }
        }

        public Boolean viewLoanList(string DisplayType)
        {
           
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t List of {0} Loans ",DisplayType);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            List<string> rows = new List<string>();
            var tempoLine = "";
            bool pending = true;
            bool accepted = true;
            bool rejected = true;
            bool result = true;

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
           

            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                if (eachLine.Length == 0)
                {
                    Console.WriteLine("\t\t\tNO DATABASE");

                }

                if (eachLine[12] == DisplayType)
                {
                    pending = false;
                    accepted = false;
                    rejected = false;

                    Console.WriteLine("----[ {0} ]-----------------------------------------------------------------------------------", eachLine[0] + " " + eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "ID", counter);
                    Console.WriteLine("{0,-5}: {1} ", "First Name", eachLine[0]);
                    Console.WriteLine("{0,-5}: {1} ", "Last Name", eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "Middle Name", eachLine[2]);
                    Console.WriteLine("{0,-5}: {1} ", "Phone", eachLine[3]);
                    Console.WriteLine("{0,-5}: {1} ", "Salary", eachLine[4]);
                    Console.WriteLine("{0,-5}: {1} ", "Date", eachLine[14]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Type", eachLine[5]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Plan", eachLine[6]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Purpose", eachLine[7]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Amount", eachLine[8]);
                    Console.WriteLine("{0,-5}: {1} ", "Total Loan Payment", Math.Round(double.Parse(eachLine[9]), 2));
                    Console.WriteLine("{0,-5}: {1} ", "Monthly Payment", Math.Round(double.Parse(eachLine[10]), 2));
                    Console.WriteLine("{0,-5}: {1} ", "Over Due's Penalty", Math.Round(double.Parse(eachLine[11]), 2));
                    Console.WriteLine("{0,-10}: {1} ", "Loan Status", eachLine[12]);
                    if (eachLine[12] == "REJECTED")
                    {
                        Console.WriteLine("{0,-15}: {1} ", "Rejection Reason", eachLine[13]);

                    }
                    Console.WriteLine("---------------------------------------------------------------------------------------------");

                }


                if (DisplayType == "ALL")
                {
                    pending = false;
                    accepted = false;
                    rejected = false;
                    Console.WriteLine("----[ {0} ]-----------------------------------------------------------------------------------", eachLine[0] + " " + eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "ID", counter);
                    Console.WriteLine("{0,-5}: {1} ", "First Name", eachLine[0]);
                    Console.WriteLine("{0,-5}: {1} ", "Last Name", eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "Middle Name", eachLine[2]);
                    Console.WriteLine("{0,-5}: {1} ", "Phone", eachLine[3]);
                    Console.WriteLine("{0,-5}: {1} ", "Salary", eachLine[4]);
                    Console.WriteLine("{0,-5}: {1} ", "Date", eachLine[14]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Type", eachLine[5]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Plan", eachLine[6]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Purpose", eachLine[7]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Amount", eachLine[8]);
                    Console.WriteLine("{0,-5}: {1} ", "Total Loan Payment", Math.Round(double.Parse(eachLine[9]), 2));
                    Console.WriteLine("{0,-5}: {1} ", "Monthly Payment", Math.Round(double.Parse(eachLine[10]), 2));
                    Console.WriteLine("{0,-5}: {1} ", "Over Due's Penalty", Math.Round(double.Parse(eachLine[11]), 2));
                    Console.WriteLine("{0,-10}: {1} ", "Loan Status", eachLine[12]);
                    if (eachLine[12] == "REJECTED")
                    {
                        Console.WriteLine("{0,-15}: {1} ", "Rejection Reason", eachLine[13]);

                    }
                    Console.WriteLine("---------------------------------------------------------------------------------------------");

                }


            }


            if (pending || accepted || rejected )
            {
                Console.WriteLine("\t\t\tNO DATABASE");
                result = false;
            }
          


            //Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            //Console.WriteLine("Press Enter To Continue...");
            //Console.Read();

            return result;
        }

        public Boolean UpdateLoanStatus(int userId,string newStatus,string rejectionReason)
        {

            string result = "";
            List<string> rows = new List<string>();
            List<string> newCopy = new List<string>();
            var tempoLine = "";

            if (rejectionReason == null)
            {
                rejectionReason = "None";
            }

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
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                
                if (counter == userId)
                {
                    result = eachLine[0] + "|" + eachLine[1] + "|" + eachLine[2] + "|" + eachLine[3] + "|" + eachLine[4] + "|" + eachLine[5] + "|" + eachLine[6] + "|" + eachLine[7] + "|" + eachLine[8] + "|" + eachLine[9] + "|" + eachLine[10] + "|" + eachLine[11] + "|" + newStatus + "|" + rejectionReason + "|" + eachLine[14] + "|";
                    
                    newCopy.Add(result);
                }
                else
                {
                    newCopy.Add(line);
                }
            }

            using (TextWriter tw = new StreamWriter("active_loans.txt"))
            {
                foreach (String s in newCopy)
                    tw.WriteLine(s);
            }
            



            return true;
        }
        public static string ViewLoanDataByCustomerID(int ID)
        {
            string result = "";
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
            Boolean userFound = false;
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (counter == ID)
                {
                    result = ID + "|" + eachLine[0] + "|" + eachLine[1] + "|" + eachLine[2] + "|" + eachLine[5] + "|" + eachLine[6] + "|" + eachLine[7] + "|" + eachLine[8] + "|" + eachLine[9] + "|" + eachLine[10] + "|" + eachLine[11] + "|" + eachLine[12] + "|" + eachLine[13];
                    userFound = true;
                }
            }

            if (userFound)
            {
                return result; 
            }
            else
            {
                return "NOT_FOUND";
            }


            
        }
    }
}
