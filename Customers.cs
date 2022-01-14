using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Customers
    {

        public string borrowerFirstName;
        public string borrowerLastName;
        public string borrowerMiddleName;
        public string borrowerSex;
        public string borrowerAddres;
        public string borrowerPhone;
        public Boolean GoOut = false;
        public Customers()
        {
            try
            {

                MenuPoint:
                new Menu().customerMenu();
                Console.Write("Choose Selection:_\b");
                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        ViewAllCustomers();
                        break;
                    case "2":
                        GoOut = true;
                        break;
                    default:
                        break;

                }
                Console.WriteLine("Press Enter To Continue...");
                if (!GoOut)
                {
                    goto MenuPoint;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Database Empty.Hit Enter to Continue...");
                Console.Read();
                Console.Read();

            }




        }

        public static void AddCustomer(string borrowerFirstName, string borrowerLastName, string borrowerMiddleName, string borrowerSex, string borrowerAddres, string borrowerPhone)
        {

            var line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerSex + "|" + borrowerAddres + "|" + borrowerPhone;
            File.AppendAllText(@"./customer.txt", line + Environment.NewLine);
        }

        public void ViewAllCustomers()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t List Of Customer");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| {0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-20} | {6}", "ID","First Name", "Last Name", "Middle Name", "Sex", "Address", "Phone");
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
            foreach(string line in rows)
            {
                counter++;
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
               
                Console.WriteLine("| {0, -5} | {1,-15} | {2,-15} | {3,-15} | {4,-7} | {5,-20} | {6}", counter, eachLine[0], eachLine[1], eachLine[2], eachLine[3], eachLine[4], eachLine[5]);
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();
        }

        public string FindCustomer(int ID)
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
                    result = ID+"|"+eachLine[0] + "|" + eachLine[1] + "|" + eachLine[2] + "|" + eachLine[3] + "|" + eachLine[4] + "|" + eachLine[5];
                }
            }


            return result;
        }
    }
}
