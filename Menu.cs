using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManagmentSystem
{
    class Menu
    {
        public void GetMenu(string menuType)
        {

            switch (menuType)
            {
                case "MAIN_MENU":
                    MAIN_MENU();
                    break;
                case "HOME":
                    HOME();
                    break;

                case "LOAN":
                    LOAN();
                    break;

                case "LOAN_PLAN":
                    PLAN();
                    break;

                case "LOAN_TYPE":
                    LOAN_TYPE();
                    break;

                case "PAYMENT":
                    PAYMENT();
                    break;

                case "CUSTOMERS":
                    CUSTOMERS();
                    break;

                case "REPORTS":
                    REPORTS();
                    break;
                case "CUSTOMER_MAIN_MENU":
                    CUSTOMER_MAIN_MENU();
                    break;
                default:
                    break;
            }


        }
        
        public void MAIN_MENU()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t=================| WELCOME TO |===============");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t===============|   ADMIN   |==================");
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
        public void CUSTOMER_MAIN_MENU()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t==============================================");
            Console.WriteLine("\t\t\t=================| WELCOME TO |===============");
            Console.WriteLine("\t\t\t========|  LOAN MANAGEMENT SYSTEM  |==========");
            Console.WriteLine("\t\t\t===============| CUSTOMER |==================");
            Console.WriteLine("=============================================================================================================");
            Console.Write("| 1.Request a Loan | ");
            Console.Write(" 2.Check Loan Status |");
            Console.Write(" 3.View Loan Payment History |");
            Console.WriteLine(" 4.Exit |");
            Console.WriteLine("=============================================================================================================");


            //3.1.2.1	
            //3.1.2.2	
            //3.1.2.3	
            //3.1.2.4	
        }
        public void HOME()
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

        public void CUSTOMERS()
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
        public void LOAN()
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
        public void PLAN()
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
        public void LOAN_TYPE()
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
        public void PAYMENT()
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

        public void REPORTS()
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
