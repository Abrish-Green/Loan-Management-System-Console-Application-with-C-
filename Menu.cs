using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManagmentSystem
{
    class Menu
    {
        
        public void menu1()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t=================| WELCOME TO |===============");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("===================================================================================================");
            Console.Write("| 1.Home | ");
            Console.Write(" 2.Loans |");
            Console.Write(" 3.Payments |");
            Console.Write(" 4.Loan Plans |");
            Console.Write(" 5.Loan types |");
            Console.Write(" 6.Customers |");
            Console.Write(" 7.Report |");
            Console.WriteLine(" 8.Exit |");
            Console.WriteLine("===================================================================================================");
        }
        public void HomeMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t========|          Home        |==============");
            Console.WriteLine("======================================================================================================================================");
            Console.Write("| 1.View Total WithDrawal Money Today | ");
            Console.Write("2.Total Loan Requests  | ");
            Console.Write("3.Total Recived Money Today | ");
            Console.Write("4.Active Customers | ");
            Console.WriteLine("5.Return To Menu |");
            Console.WriteLine("======================================================================================================================================");

        }

        public void customerMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t========|          Customer        |==========");
            Console.WriteLine("===================================================================================================");
            Console.Write("| 1.View Customers | ");
            Console.WriteLine("2.Return To Menu |");
            Console.WriteLine("===================================================================================================");

        }
        public void loanMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t========|           Loan           |==========");
            Console.WriteLine("================================================================================================================");
            Console.Write("| 1.New Loan Application | ");
            Console.Write("2.View Loan History |");
            Console.Write("3.View Approved Loans |");
            Console.Write("4.View Rejected Loans |");
            Console.WriteLine("5.Return To Menu |");
            Console.WriteLine("================================================================================================================");

        }
        public void loanPlanMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t========|        Loan Plan         |==========");
            Console.WriteLine("===================================================================================================");
            Console.Write("| 1.Create New Plan | ");
            Console.Write("2.View Loan Plans |");           
            Console.WriteLine("3.Return To Menu |");
            Console.WriteLine("===================================================================================================");
            
        }
        public void loanTypeMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t========|        Loan Type         |==========");
            Console.WriteLine("===================================================================================================");
            Console.Write("| 1.Create New Loan Type | ");
            Console.Write("2.View Loan Types |");
            Console.WriteLine("3.Return To Menu |");
            Console.WriteLine("===================================================================================================");

        }
        public void paymentMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t========|         Payment          |==========");
            Console.WriteLine("==================================================================================================================================");
            Console.Write("| 1.Make Payment | ");
            Console.Write("2.View All Payment Transaction |");
            Console.Write("3.View Users Loan History |");
            Console.Write("4.Check Customer Loan Clearance |");
            Console.WriteLine("5.Returen To Menu |");
            Console.WriteLine("==================================================================================================================================");

        }

        public void reportMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t========|         Report           |==========");
            Console.WriteLine("==================================================================================================================================");
            Console.Write("| 1.View Monthly Withdrawn Loan | ");
            Console.Write("2.View Monthly Paid Loan |");
            Console.WriteLine("3.Returen To Menu |");
            Console.WriteLine("==================================================================================================================================");

        }
    }
}
