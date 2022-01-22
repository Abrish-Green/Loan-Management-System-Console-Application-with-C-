using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManagmentSystem
{
    class LogicCombiner
    {


        //import Layout
        UILayout layout = new UILayout();
        Utilits utilits = new Utilits();
        

        public string UserType;
        public LogicCombiner()
        {
            ProgramConfiguration();
            ProgramStarter();

        }
        public void ProgramStarter()
        {

            back_1:
            Console.Clear();
            layout.WelcomePage();
            //Accept User Type
            Console.Write("Enter Selection: ");
            UserType = Console.ReadLine();
            UserType = utilits.InputVarAndValidate(UserType, "int", "Selection")[2].ToString();

            //Next Page
            layout.LoginOrSignUpPage(int.Parse(UserType));

            if (layout.LoginOrSignUpPage(int.Parse(UserType)) == "Wrong Input")
            {
                Console.Clear();
                goto back_1;
            }

            //choose //1 login  //sign up
            back_2:
            Console.Write("Select: ");
            string loginOrSignUp = Console.ReadLine();
            loginOrSignUp = utilits.InputVarAndValidate(loginOrSignUp, "int", "User Type")[2].ToString();

            if (int.Parse(UserType) == 1)
            {
                if (int.Parse(loginOrSignUp) == 1)
                {

                    int Auth_user_id = new Customer(0).CustomerLogin();
                    if (Auth_user_id > 0)
                    {
                        
                        CustomerPage(Auth_user_id);
                    }


                }
                else if (int.Parse(loginOrSignUp) == 2)
                {
                  
                    new Customer(0).CustomerSignUp();
                    goto back_1;
                }
                else if (int.Parse(loginOrSignUp) == 0)
                {
                    goto back_1;
                }
                else
                {
                    Console.WriteLine("Wrong Input.Out of Range");
                    goto back_2;
                }

            }
            else if (int.Parse(UserType) == 2)
            {
                if (int.Parse(loginOrSignUp) == 1)
                {
                    
                    Auth Administrator = new Auth();
                    
                    if (Administrator.startAuth())
                    {
                      
                        AdministratorPage();
                    }
                }
                else if (int.Parse(loginOrSignUp) == 0)
                {
                    goto back_1;
                }
                else
                {
                    Console.WriteLine("Wrong Input");
                    goto back_2;
                }
            }
            else
            {
                Console.WriteLine("Out of Range...");
                goto back_2;
            }


            Console.WriteLine("Press Enter To EXIT...");
            Console.Read();

        }

        public void AdministratorPage()
        {

            Menu Menus = new Menu();
            Home home;
            Loan loan;
            LoanType loanType;
            LoanPlan loanPlan;
            Payment payment;
            Customers customers;
            Report report;
            bool EXIT = false;


            loginSuccess:

            MenuselectionPoint:      //used for looping back to this point from the goto point
            Menus.GetMenu("MAIN_MENU");
            Console.Write("Select Service : _\b");
            string choice = Console.ReadLine();
            choice = utilits.InputVarAndValidate(choice, "int", "Choice")[2].ToString();

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
                if (!EXIT)
                {
                    goto MenuselectionPoint;
                }
                else
                {
                    Console.WriteLine("Press Enter To EXIT....");
                }
            }
            catch (System.FormatException e)
            {
                //Console.WriteLine(e);
                Console.WriteLine("Wrong Input..Please Use Numbers only.Press Any Key To continue.For Exit Use 0");
                var back = Console.ReadLine();
                if (back != "0")
                {
                    goto MenuselectionPoint;
                }

            }



        }//end of function

        public void CustomerPage(int CustomerId)
        {
            bool EXIT = false;
            Customer customer = new Customer(CustomerId);
            loginSuccess:

            MenuselectionPoint:      //used for looping back to this point from the goto point
            layout.CustomerWelcomePage();
            Console.Write("Select Service : _\b");
            string choice = Console.ReadLine();


            try
            {
                switch (int.Parse(choice))
                {
                    case 1:
                        customer.RequestLoan();
                        break;
                    case 2:
                       customer.CheckLoanStatus();
                        break;
                    case 3:
                        customer.ViewLoanPaymentHistory();
                        break;
                    case 4:
                        EXIT = true;
                        break;
                    default:
                        Console.WriteLine("No Service For This Selection.Please Use Numbers Between 1 - 6 only!");
                        goto MenuselectionPoint;

                }
                if (!EXIT)
                {
                    goto MenuselectionPoint;
                }
                
            }
            catch (System.FormatException e)
            {
                //Console.WriteLine(e);
                Console.WriteLine("Wrong Input..Please Use Numbers only.Press Any Key To continue.For Exit Press 0");
                var back = Console.ReadLine();
                if (back != "0")
                {
                    goto MenuselectionPoint;
                }

            }




        }

        public void ProgramConfiguration()
        {
            utilits.MakeScreenSizeMAX();
            utilits.CreateAllFiles();
        }
    }
}
