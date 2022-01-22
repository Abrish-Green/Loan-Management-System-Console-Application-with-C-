using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Customer
    {
        //3.1.2.1	Request a Loan
        //3.1.2.2	Check Loan Status
        //3.1.2.3	View Loan Payment History
        //3.1.2.4	View Next Payment Date

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
        public string UserName;
        public string Password;

        double TotalPayableAmount;
        double MonthlyPayableAmount;
        double MonthlyPenalty;
        string RejectionReason;
        public static Boolean LOGGED_IN = false;
        public static int LoginCounter = 0;
        public int Auth_User_id = 0;

        //Import Utilits
        Utilits utilits = new Utilits();

        public Customer(int CustomerId)
        {
            Auth_User_id = CustomerId;
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------------------------------");
            Console.WriteLine("+---------------------| WELCOME TO |-----------------------------");
            Console.WriteLine("+-------------------|    Customer      |-------------------------");
            Console.WriteLine("+----------------------------------------------------------------");
        }
        
        public Boolean CustomerSignUp()
        {
            Display();

            //Accept User Input
            Console.Write("|Enter Name: ");
            UserName = Console.ReadLine();
            UserName = (utilits.InputVarAndValidate(UserName, "string", "Customer Name")[2]).ToString();
            Console.Write("|Enter Password: ");
            Password = Console.ReadLine();
            Password = (utilits.InputVarAndValidate(Password, "password", "Customer Password")[2]).ToString();
            
            //Store User Data to A file 
            if (StoreUser(UserName, Password)) {
                Console.WriteLine("User Created Successfully...Press Enter To Continue...");
                Console.Read();
                Console.Read();

            }
            else
            {
                Console.WriteLine(" User Not Created");
            }

            return true;
        }

        public int CustomerLogin() {

            string db_username = "";
            string db_password = "";

            string result = "";
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./userLogin.txt"))
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
            

            while (!LOGGED_IN && LoginCounter < 3)
            {
                Console.Write("Enter User Name: ");
                var UserName = Console.ReadLine();
                UserName = utilits.InputVarAndValidate(UserName, "string", "username")[2].ToString();
                Console.Write("Enter Password: ");
                var Password = Console.ReadLine();
                Password = utilits.InputVarAndValidate(Password, "string", "password")[2].ToString();
                foreach (string line in rows)
                {
                    counter++;
                    var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
               

                    if (eachLine[0] == UserName && eachLine[1] == Password)
                    {
                        Auth_User_id = counter;
                        db_username = eachLine[0].ToString();
                        db_password = eachLine[1].ToString();
                        LOGGED_IN = true;
                    }
                }
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


            return LOGGED_IN ? Auth_User_id : 0;
        }
        public Boolean StoreUser(string username, string password)
        {
            var line = username+"|"+password+"|";
            bool result = true;
            try
            {
                File.AppendAllText(@"./userLogin.txt", line + Environment.NewLine);
            }
            catch (Exception e)
            {
                result = false;
            }
            

            return result;
        }
        
        public void RequestLoan()
        {
            //Accept information

           
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("|\t\t\t\tRequest Loan \t\t\t\t|");
            Console.WriteLine("|-----------------------------------------------------------------------|");

            Console.Write("Enter Borrower's First Name: ");
            borrowerFirstName = Console.ReadLine();
            borrowerFirstName = utilits.InputVarAndValidate(borrowerFirstName, "string", "First Name")[2].ToString();

            Console.Write("Enter Borrower's Last Name: ");
            borrowerLastName = Console.ReadLine();
            borrowerLastName = utilits.InputVarAndValidate(borrowerLastName, "string", "Last Name")[2].ToString();

            Console.Write("Enter Borrower's Middle Name: ");
            borrowerMiddleName = Console.ReadLine();
            borrowerMiddleName = utilits.InputVarAndValidate(borrowerMiddleName, "string", "Middle Name")[2].ToString();

            Console.Write("Enter Sex: [ Male | Female ]:");
            borrowerSex = Console.ReadLine();
            borrowerSex = utilits.InputVarAndValidate(borrowerSex, "string", "Sex")[2].ToString();

            Console.Write("Enter Address[Email]: ");
            borrowerAddres = Console.ReadLine();
            borrowerAddres = borrowerAddres.Contains("@") ? borrowerAddres : "";
            borrowerAddres = utilits.InputVarAndValidate(borrowerAddres, "string", "Email")[2].ToString();

            Console.Write("Enter Phone: ");
            borrowerPhone = Console.ReadLine();
            borrowerPhone = utilits.InputVarAndValidate(borrowerPhone, "int", "Phone")[2].ToString();

            Console.Write("Enter Monthly Salary: ");
            string borrowerSalaryTemp = Console.ReadLine();
            borrowerSalaryTemp = utilits.InputVarAndValidate(borrowerSalaryTemp, "int", "Salary")[2].ToString();
            borrowerSalary = Convert.ToDouble(borrowerSalaryTemp);

            string CheckLoanPlan = LoanPlan.getLoanPlan(1);
            if (CheckLoanPlan.Length != 0)
            {

                Console.WriteLine("Enter Loan Type [Notice: Use Their ID For Selection]: ");
                LoanType.listLoanType();
                loanType = Console.ReadLine();
                loanType = utilits.InputVarAndValidate(loanType, "int", "Loan Type")[2].ToString();


                Console.WriteLine("Enter Loan Plan [Notice: Use Their ID For Selection]: ");
                LoanPlan.viewLoanPlan();
                string loanPlanTemp = Console.ReadLine();
                loanPlanTemp = utilits.InputVarAndValidate(loanPlanTemp, "int", "Loan Plan")[2].ToString();
                loanPlan = int.Parse(loanPlanTemp);

                Console.Write("Enter Loan Amount: ");
                string AmountTemp = Console.ReadLine();
                AmountTemp = utilits.InputVarAndValidate(AmountTemp, "int", "Loan Amount")[2].ToString();
                loanAmount = Convert.ToDouble(AmountTemp);


                Console.Write("Enter Loan Purpose: ");
                loanPurpose = Console.ReadLine();
                loanPurpose = utilits.InputVarAndValidate(loanPurpose, "string", "Loan Purpose")[2].ToString();


                //calculate
                string response = LoanPlan.getLoanPlan(loanPlan);
               
                //Console.WriteLine(response);

                Console.WriteLine("To Calculate Loan Details [HIT Enter Key]");
                Console.Read();


                calculateLoanGrant(loanAmount, Convert.ToDouble(response.Split(" ")[2]), Convert.ToInt32(response.Split(" ")[1]), Convert.ToDouble(response.Split(" ")[3]));
                policyAndConditions(loanAmount, Convert.ToDouble(response.Split(" ")[2]), Convert.ToInt32(response.Split(" ")[1]), Convert.ToDouble(response.Split(" ")[3]));
                Console.WriteLine("Press Enter To Continue...");
                Console.Read();
                Console.Read();


            }
            else
            {
                Console.WriteLine("Please Wait Until Loan Plan and Loan Type is Provided by the Administrator.");
                Console.Read();
                Console.Read();

            }
            //policy



        }

        public void calculateLoanGrant(double loanAmount, double interestRate, int totalMonth, double OverDuesPenaltyRate)
        {


            double p = loanAmount;
            double r = (interestRate / 100) / 12;
            int n = totalMonth;
            double monthlyUpper = p * (r) * Math.Pow(1 + (r), n);
            double monthlyLower = Math.Pow(1 + r, n) - 1;

            double monthlyRate = (interestRate / 12) / 100;
            MonthlyPayableAmount = monthlyUpper / monthlyLower;
            OverDuesPenaltyRate = OverDuesPenaltyRate / 12;
            OverDuesPenaltyRate = OverDuesPenaltyRate / 100;
            TotalPayableAmount = MonthlyPayableAmount * totalMonth;
            MonthlyPenalty = loanAmount * OverDuesPenaltyRate;

            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("\t\t\tLoan Detail");
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("|\t Total Payable Amount : {0} BIRR", Math.Round(TotalPayableAmount, 2));
            Console.WriteLine("|\t Monthly Payable Amount: {0} BIRR", Math.Round(MonthlyPayableAmount, 2));
            Console.WriteLine("|\t Over Due's Penalty Amount: {0} BIRR", Math.Round(MonthlyPenalty, 2));
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.Read();

        }

        public void policyAndConditions(double loanAmount, double interestRate, int totalMonth, double OverDuesPenaltyRate)
        {
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("|\t\t\t\t Terms And Policy \t\t\t\t|");
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("| According to the Terms and Policy Mr/Ms {0} requested a loan and \n| he/she Must agree to the terms and policy to get this Loan.", borrowerFirstName + " " + borrowerLastName);
            Console.WriteLine("| 1.The Borrower Should Receive the Loan according to the date defined \n| by the Loaner");
            Console.WriteLine("| 2.The Borrower Requested a loan to {0} BIRR", loanAmount);
            Console.WriteLine("| 3.The Borrower Should Agree to {0} % Interest Rate.", interestRate);
            Console.WriteLine("| 4.The Borrower Must repay the loan in {0} Month or Before otherwise \n| There Would be Penalty's.", totalMonth);
            Console.WriteLine("| 5.The Borrower Should Pay Monthly Payment of {0} \n| BIRR.", Math.Round(MonthlyPayableAmount, 2));
            Console.WriteLine("| 6.If Borrower Didn't Pay his/her Monthly Payment in Time they Must\n| pay an Penalty of {0} BIRR.", Math.Round(MonthlyPenalty, 2));
            Console.WriteLine("| 7.By the End of the Term the Borrower Should pay Total Money of \n| {0} BIRR", Math.Round(TotalPayableAmount, 2));
            Console.WriteLine("|-----------------------------------------------------------------------|");

            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.Write("\n|\t Do You Agree To The Terms And Conditions ?[Y/N]: ");
            var Choice = Console.ReadLine();
            Choice = utilits.InputVarAndValidate(Choice, "string", "Choice")[2].ToString();


            switch (Choice)
            {
                case "Y":
                case "y":
                    LoanGrant = "PENDING";
                    loanDate = DateTime.Now.ToString();
                    AddLoanInformation(null);
                    Customers.AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Console.WriteLine("| Your Loan Request Has be Sent.Loan Status : [Pending]");
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Home.TotalWithdrawalMoneyTodayInBirr += loanAmount;
                    break;
                 
                default:
                    LoanGrant = "REJECTED";
                    Console.WriteLine("|-----------------------------------------------------------------------|");
                    Console.WriteLine("|\tPlease Provide a Loan REJECTION reason : ");
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
                line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerPhone + "|" + borrowerSalary + "|" + loanType + "|" + loanPlan + "|" + loanPurpose + "|" + loanAmount + "|" + TotalPayableAmount + "|" + MonthlyPayableAmount + "|" + MonthlyPenalty + "|" + LoanGrant + "|" + RejectReason + "|" + loanDate + "|";
                File.AppendAllText(@"./active_loans.txt", line + Environment.NewLine);
            }
            else
            {
                line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerPhone + "|" + borrowerSalary + "|" + loanType + "|" + loanPlan + "|" + loanPurpose + "|" + loanAmount + "|" + TotalPayableAmount + "|" + MonthlyPayableAmount + "|" + MonthlyPenalty + "|" + LoanGrant + "| None" + "|" + loanDate + "|";
                File.AppendAllText(@"./active_loans.txt", line + Environment.NewLine);

            }
        }

        public void CheckLoanStatus()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t Loan Status ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            List<string> rows = new List<string>();
            var tempoLine = "";
            bool empty = true;

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

               

                if (counter == Auth_User_id)
                {

                    empty = false;
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
            if (empty)
            {
                Console.WriteLine("\t\t\tNO DATABASE");

            }
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();
            Console.Read();

        }

        public void ViewLoanPaymentHistory()
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
                    if (int.Parse(eachLine[0]) == Auth_User_id)
                    {
                        Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-25} | {5,-15} | {6,-20} | {7,-20} | {8}", eachLine[0], eachLine[1], eachLine[2], "$" + Math.Round(double.Parse(eachLine[3])), "$" + Math.Round(double.Parse(eachLine[5])), eachLine[6], "$" + Math.Round(double.Parse(eachLine[8])), eachLine[9], eachLine[10]);

                    }
                }
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Press Enter To Continue...");
                Console.Read();
                Console.Read();


        }

        public string GetUserData(int ID,int INDEX)
        {
            string result = "";
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

            var counter = 0;
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (counter == ID)
                {
                    result = ID + "|" + eachLine[0] + "|" + eachLine[1];
                }
            }


            return result.Split("|", StringSplitOptions.RemoveEmptyEntries)[INDEX];

       
        }


        

    }
}
