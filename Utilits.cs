using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Utilits
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;

        public void MakeScreenSizeMAX()
        {
            //Make Screen Size to MAX
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);

        }
        public void CreateAllFiles()
        {
            //Create Every File That is Going to be used in the system
            File.AppendAllText(@"./customer.txt", "");
            File.AppendAllText(@"./active_loans.txt", "");
            File.AppendAllText(@"./loan_plan.txt", "");
            File.AppendAllText(@"./loan_type.txt", "");
            File.AppendAllText(@"./payment_log.txt", "");
            File.AppendAllText(@"./userLogin.txt", "");

            

        }

        public ArrayList InputVarAndValidate(string VarName, string varType,string VarDescription)
        {
            // Data-type use only "int" or "string"
            bool empty = false;
            bool InvalidType = false;
            bool result = true;
            string msg = "";
            ArrayList value = new ArrayList();

            string inputedDataType = "";

            if (VarName == "")
            {
                msg = "Invalid Input.Please Input " + VarDescription + ": ";
                result = false;
                empty = true;
            } else if (varType == "password") { 
                //nothing
            }
            else
            {

                var isNumeric = double.TryParse(VarName, out _);
                inputedDataType = isNumeric ? "int" : "string";


                if (varType != inputedDataType)
                {
                    msg = "Invalid Input.Please Input Valid Information " + VarDescription + " : ";
                    result = false;
                    InvalidType = true;
                }


            }
            while (empty)
            {
                Console.Write(msg);
                VarName = Console.ReadLine();
                if (VarName.Length > 0)
                {
                    empty = false;
                }

            }
            while (InvalidType)
            {
                Console.Write(msg);
                VarName = Console.ReadLine();
                var isNumeric = double.TryParse(VarName, out _);
                inputedDataType = isNumeric ? "int" : "string";

                if (varType == inputedDataType)
                {
                    InvalidType = false;
                }

            }




            value.Add(result);
            value.Add(msg);
            value.Add(VarName);
            //Utilits.InputVarAndValidate(i, "string", "UserName")[2].ToString(); 
            return value;
        }
    }
}
