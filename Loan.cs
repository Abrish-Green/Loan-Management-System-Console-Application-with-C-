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
        public string loanDate;

        double TotalPayableAmount;
        double MonthlyPayableAmount;
        double MonthlyPenalty;
        string RejectionReason;
        public Boolean GoOut = false;


        public Loan()
        {
            try {

                MenuPoint:

                new Menu().loanMenu();

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
                            requestLoan();
                        }

                        
                       
                        break;
                    case "2":
                        viewLoanList();
                        break;
                    case "3":
                        viewApprovedLoan();
                        break;
                    case "4":
                        viewRejectedLoan();
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

        public void requestLoan()
        {
            //username
            //job position
            //loan plan
            //loan type
            //purpose
            //provide salary and document

            //Enter Personal Information
           Program.TotalLoanRequestToday += 1;
           try {
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|\t\t\t\tRequest Loan \t\t\t\t|");
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.Write("Enter Borrower's First Name: ");
                borrowerFirstName = Console.ReadLine();

                Console.Write("Enter Borrower's Last Name: ");
                borrowerLastName = Console.ReadLine();

                Console.Write("Enter Borrower's Middle Name: ");
                borrowerMiddleName = Console.ReadLine();

                Console.Write("Enter Sex: [ Male | Female ]:");
                borrowerSex = Console.ReadLine();

                Console.Write("Enter Address: ");
                borrowerAddres = Console.ReadLine();

                Console.Write("Enter Phone: ");
                borrowerPhone = Console.ReadLine();


                //Enter Loan Information

                Console.Write("Enter Monthly Salary: ");
                var borrowerSalaryTemp = Console.ReadLine();
                double.TryParse(borrowerSalaryTemp, out borrowerSalary);



                Console.WriteLine("Enter Loan Type [Notice: Use Their ID For Selection]: ");
                LoanType.listLoanType();
                loanType = Console.ReadLine();


                Console.WriteLine("Enter Loan Plan [Notice: Use Their ID For Selection]: ");
                LoanPlan.viewLoanPlan();
                loanPlan = int.Parse(Console.ReadLine());
                //Console.WriteLine(loanPlan);
               
                Console.Write("Enter Loan Amount: ");
                double.TryParse(Console.ReadLine(), out loanAmount);


                Console.Write("Enter Loan Purpose: ");
                loanPurpose = Console.ReadLine();

                //Console.Read();
                //Calculate for Grant

                //Console.WriteLine(LoanPlan.getLoanPlan(int.Parse(loanPlan)));
                string response = LoanPlan.getLoanPlan(loanPlan);
                //Console.WriteLine(response);
               
                Console.WriteLine("To Calculate Loan Details [HIT Enter Key]");
                Console.Read();
                

                calculateLoanGrant(loanAmount, Convert.ToDouble(response.Split(" ")[2]), Convert.ToInt32(response.Split(" ")[1]), Convert.ToDouble(response.Split(" ")[3]));
                policyAndConditions(loanAmount, Convert.ToDouble(response.Split(" ")[2]), Convert.ToInt32(response.Split(" ")[1]), Convert.ToDouble(response.Split(" ")[3]));
                Console.Read();

            }
            catch (Exception e)
            {
                //Console.WriteLine(e);

                Console.WriteLine("There Has Been An Error.Please Come back later...");
                Console.Read();
            }
            


        }

        public void calculateLoanGrant(double loanAmount, double interestRate, int totalMonth, double OverDuesPenaltyRate)
        {

            //Console.WriteLine("{0} {1} {2} {3}",interestRate,totalMonth,OverDuesPenaltyRate,loanAmount);
            //formula A = P (r (1+r)^n) / ( (1+r)^n -1 )
            //formula monthly = P*r(1+r)^n
            //                  (1+r)^n - 1

            double p = loanAmount;
            double r = (interestRate/100)/12; 
            int n = totalMonth;
            double monthlyUpper = p * (r) * Math.Pow(1 + (r),n);
            double monthlyLower = Math.Pow(1 + r, n) - 1;

            double monthlyRate = (interestRate / 12)/100;
            MonthlyPayableAmount = monthlyUpper/monthlyLower;
            OverDuesPenaltyRate = OverDuesPenaltyRate / 12;
            OverDuesPenaltyRate = OverDuesPenaltyRate / 100;
            TotalPayableAmount = MonthlyPayableAmount*totalMonth;
            MonthlyPenalty = loanAmount * OverDuesPenaltyRate;
            
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("\t\t\tLoan Detail");
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("|\t Total Payable Amount : {0} BIRR",Math.Round(TotalPayableAmount, 2) );
            Console.WriteLine("|\t Monthly Payable Amount: {0} BIRR", Math.Round(MonthlyPayableAmount,2));
            Console.WriteLine("|\t Over Due's Penalty Amount: {0} BIRR", Math.Round(MonthlyPenalty,2));
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.Read();
           
        }

        public void policyAndConditions(double loanAmount, double interestRate, int totalMonth, double OverDuesPenaltyRate)
        {
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("|\t\t\t\t Terms And Policy \t\t\t\t|");
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("| According to the Terms and Policy Mr/Ms {0} requested a loan and he/she Must agree to the terms and policy to get this Loan.",borrowerFirstName+" "+borrowerLastName);
            Console.WriteLine("| 1.The Borrower Should Receive the Loan according to the date defined by the Loaner");
            Console.WriteLine("| 2.The Borrower Requested a loan to {0} BIRR",loanAmount);
            Console.WriteLine("| 3.The Borrower Should Agree to {0} % Interest Rate.",interestRate);
            Console.WriteLine("| 4.The Borrower Must repay the loan in {0} Month or Before otherwise There Would be Penalty's.",totalMonth);
            Console.WriteLine("| 5.The Borrower Should Pay Monthly Payment of {0} BIRR.", Math.Round(MonthlyPayableAmount,2));
            Console.WriteLine("| 6.If Borrower Didn't Pay his/her Monthly Payment in Time they Must pay an Penalty of {0} BIRR.", Math.Round(MonthlyPenalty,2));
            Console.WriteLine("| 7.By the End of the Term the Borrower Should pay Total Money of {0} BIRR", Math.Round(TotalPayableAmount,2));
            Console.WriteLine("|-----------------------------------------------------------------------|");

            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.Write("\n|\tDo The Borrower Agree To The Terms And Conditions ?[Y/N]: ");
            var Choice = Console.ReadLine();


            switch (Choice)
            {
                case "Y":
                case "y":

                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Console.Write("|\t Do You[Administrator] Approve The Loan ?[Y/N]: ");

                    var LoanerChoice = Console.ReadLine();
                    switch (LoanerChoice)
                    {
                        case "Y":
                        case "y":
                            //save to file
                            LoanGrant = "GRANTED";
                            loanDate = DateTime.Now.ToString();
                            AddLoanInformation(null); //null - for no rejection reason
                            Customers.AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                            Console.WriteLine("|--------------------------------------------------------------------------------------------------|");
                            Console.WriteLine("| Loan Has be GRANTED SUCCESSFULLY.Congradulations Mr/Ms {0}...!", borrowerFirstName + " " + borrowerLastName);
                            Console.WriteLine("|--------------------------------------------------------------------------------------------------|");
                            Program.TotalWithdrawalMoneyToday += loanAmount;
                            break;
                        default:
                            LoanGrant = "REJECTED";
                            Console.WriteLine("|-----------------------------------------------------------------------|");
                            Console.WriteLine("\t Please Provide a Loan REJECTION reason : ");
                            RejectionReason = Console.ReadLine();
                            AddLoanInformation(RejectionReason);
                            Customers.AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                            Console.WriteLine("|-----------------------------------------------------------------------|");
                            Console.WriteLine("\t Rejection Reason Has been  Saved.");
                            break;
                    }
                    break;
                default:
                    LoanGrant = "REJECTED";
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Console.WriteLine("\t Please Provide a Loan REJECTION reason : ");
                    RejectionReason = Console.ReadLine();
                    AddLoanInformation(RejectionReason);
                    Customers.AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Console.WriteLine("\t The Loan Has Been REJECTED.");
                    break;
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
                line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerPhone + "|" + borrowerSalary + "|" + loanType + "|" + loanPlan + "|" + loanPurpose + "|" + loanAmount + "|" + TotalPayableAmount + "|" + MonthlyPayableAmount + "|" + MonthlyPenalty + "|" + LoanGrant + "| None" + "|" + loanDate;
                File.AppendAllText(@"./active_loans.txt", line + Environment.NewLine);

            }
        }

        public void viewLoanList()
        {
            //Loan granted date
            //loan type
            //loan plan
            //interest
            //monthly payment
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t List of Loans ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
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
            if(File.ReadAllText("./active_loans.txt").Length == 0)
            {
                Console.WriteLine("No Database");
            }

            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine("----[ {0} ]-----------------------------------------------------------------------------------", eachLine[0]+" "+ eachLine[1]);
                Console.WriteLine("{0,-5}: {1} ", "ID", counter);
                Console.WriteLine("{0,-5}: {1} ", "First Name", eachLine[0]);
                Console.WriteLine("{0,-5}: {1} ", "Last Name", eachLine[1]);
                Console.WriteLine("{0,-5}: {1} ", "Middle Name", eachLine[2]);
                Console.WriteLine("{0,-5}: {1} ", "Phone", eachLine[3]);
                Console.WriteLine("{0,-5}: {1} ", "Salary", eachLine[4]);
                Console.WriteLine("{0,-5}: {1} ", " Date", eachLine[14]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Type", eachLine[5]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Plan", eachLine[6]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Purpose", eachLine[7]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Amount", eachLine[8]);
                Console.WriteLine("{0,-5}: {1} ", "Total Loan Payment", Math.Round(double.Parse(eachLine[9]),2));
                Console.WriteLine("{0,-5}: {1} ", "Monthly Payment", Math.Round(double.Parse(eachLine[10]), 2));
                Console.WriteLine("{0,-5}: {1} ", "Over Due's Penalty", Math.Round(double.Parse(eachLine[11]), 2));


                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();


        }

        public void viewApprovedLoan()
        {

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t List of Granted Loans ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
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
            foreach (string line in rows)
            {
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (eachLine[12] == "GRANTED")
                {
                    counter++;
                    Console.WriteLine("----[ {0} ]-----------------------------------------------------------------------------------", eachLine[0] + " " + eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "ID", counter);
                    Console.WriteLine("{0,-5}: {1} ", "First Name", eachLine[0]);
                    Console.WriteLine("{0,-5}: {1} ", "Last Name", eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "Middle Name", eachLine[2]);
                    Console.WriteLine("{0,-5}: {1} ", " Date", eachLine[14]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Type", eachLine[5]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Plan", eachLine[6]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Purpose", eachLine[7]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Amount", eachLine[8]);
                    Console.WriteLine("{0,-5}: {1} ", "Total Loan Payment", Math.Round(double.Parse(eachLine[9]), 2));
                    Console.WriteLine("{0,-5}: {1} ", "Monthly Payment", Math.Round(double.Parse(eachLine[10]), 2));
                    Console.WriteLine("{0,-5}: {1} ", "Over Due's Penalty", Math.Round(double.Parse(eachLine[11]), 2));
                    Console.WriteLine("{0,-10}: {1} ", "Loan Status", eachLine[12]);
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");


                }
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();

        }

        public void viewRejectedLoan()
        {
            //username
            //loan type
            //rejection reason

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t List of Rejected Loans ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
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
            foreach (string line in rows)
            {
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (eachLine[12] == "REJECTED")
                {
                    counter++;
                    Console.WriteLine("----[ {0} ]-----------------------------------------------------------------------------------", eachLine[0] + " " + eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "ID", counter);
                    Console.WriteLine("{0,-5}: {1} ", "First Name", eachLine[0]);
                    Console.WriteLine("{0,-5}: {1} ", "Last Name", eachLine[1]);
                    Console.WriteLine("{0,-5}: {1} ", "Middle Name", eachLine[2]);
                    Console.WriteLine("{0,-5}: {1} ", " Date", eachLine[14]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Type", eachLine[5]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Plan", eachLine[6]);
                    Console.WriteLine("{0,-5}: {1} ", "Loan Purpose", eachLine[7]);
                    Console.WriteLine("{0,-5}: {1} ", "Over Due's Penalty", Math.Round(double.Parse(eachLine[11]),2));
                    Console.WriteLine("{0,-5}: {1} ", "Loan Status", eachLine[12]);
                    Console.WriteLine("{0,-15}: {1} ", "|Rejection Reason", eachLine[13]);
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");


                }
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();


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
