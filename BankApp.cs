using System;
using System.Collections.Generic;
using System.IO;
namespace ConsoleApp1
{
    class BankApp
    {
        static string name;
        //Please change the file paths to desired path
        //The filename for the file that stores account details
        static string filename = @"D://accounts.txt";
        //The transaction_log for the file that records the logs
        //Check trans.txt file to know about all transactions 
        static string transaction_log = @"D://trans.txt";
        //Dictionary that stores username and his account balance
        static Dictionary<string, int> amt_dict = new Dictionary<string, int>();
        //Dictionary that stores username and his password
        static Dictionary<string, string> account = new Dictionary<string, string>();
        //List for recording logs
        static List<string> trans = new List<string>();
        static void init()
        {
            //This function ensures copying file contents into dictionaries

            if (File.Exists(filename))
            {
                //If file already exists
                string[] st = File.ReadAllLines(filename);
                foreach (string line in st)
                {
                    //Split the line based on ":" delimeter
                    string[] dets = line.Split(":");
                    //Store the values into corresponding dictionaries
                    amt_dict[dets[0]] = Convert.ToInt32(dets[2]);
                    account[dets[0]] = dets[1];
                }
            }
            else
            {
                //If file does not exist, create it.
                StreamWriter sw = new StreamWriter(filename);
                //Close the file
                sw.Close();
            }
            if (!File.Exists(transaction_log))
            {
                //If transction log file does not exist, create!
                StreamWriter st = new StreamWriter(transaction_log);
                //Closing the file reference
                st.Close();
            }
        }
        static void create_account()
        {
            //This function enables to create new account for new users
            string password;
            //Accepting details
            Console.WriteLine("Enter your details:");
            Console.WriteLine("Enter your Name:");
            name = Console.ReadLine();
            if (account.ContainsKey(name))
            {
                //If account already exists
                Console.WriteLine("Already Account exits!");
                Console.WriteLine("Do you wish to try again? or create account?\n 1.Login\t2.Create");
                int flag = Convert.ToInt32(Console.ReadLine());
                //Performing according to user choice
                if (flag == 1)
                    login();
                else
                    create_account();
            }
            //Create user password
            Console.WriteLine("Enter Password:");
            password = Console.ReadLine();
            Console.WriteLine("Re-enter Password:");
            if (password == Console.ReadLine())
            {
                //If passwords matched
                Console.WriteLine("Account created succesfully");
                //Adding account into dictionary
                account[name] = password;
                //Initializing new account with 0 amount
                amt_dict[name] = 0;
                //Writing into log
                trans.Add("Account " + name + " created successfully and logged in!");
                options();
            }
            else
            {
                //If passwords does not match
                Console.WriteLine("Password Unmatched!");
                create_account();
            }
        }
        static void login()
        {
            //This function performs login operation by verifying the user
            //Accepting user credentials
            string password;
            Console.WriteLine("Enter username:");
            name = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            password = Console.ReadLine();
            //Checking user credentials
            if (account.ContainsKey(name))
            {
                //If user exists
                if (account[name] == password)
                {
                    //If user enters valid password
                    Console.WriteLine("Login successfull!");
                    //Entering into log record
                    trans.Add("Account " + name + " logged in successfully!");
                    //Calling options method for displaying Menu 
                    options();
                }
                else
                {
                    //Invalid password
                    Console.WriteLine("Invalid details");
                    Console.WriteLine("Do you wish to try again? or create account?\n 1.Login\t2.Create");
                    int flag = Convert.ToInt32(Console.ReadLine());
                    //Checking user choice and performing accordingly
                    if (flag == 1)
                        //If user wants to login
                        login();
                    else
                        //If user wants to create an account
                        create_account();
                }
            }
            else
            {
                //Account does not exist
                Console.WriteLine("Invalid Details!");
                Console.WriteLine("Do you wish to try again? or create account?\n 1.Login\t2.Create");
                int flag = Convert.ToInt32(Console.ReadLine());
                //Checking user choice and performing accordingly
                if (flag == 1)
                    //If user wants to login
                    login();
                else
                    //If user wants to create an account 
                    create_account();
            }

        }
        static void view_bal()
        {
            //Function to view balance of the logged in user
            Console.WriteLine("Your Balance:" + Convert.ToString(amt_dict[name]));
            options();
        }
        static void deposit()
        {
            //Function to that helps in depositing the amount into account
            int amount;
            //
            Console.WriteLine("Enter amount you want to deposit");
            amount = Convert.ToInt32(Console.ReadLine());
            //Adding the amount into user account
            amt_dict[name] = amt_dict[name] + amount;
            Console.WriteLine("Your amount deposited successfully!");
            //Entering into log record
            trans.Add("Deposited " + Convert.ToString(amount) + " to " + name);
            options();
        }
        static void withdraw()
        {
            //This function enables user to withdraw amount from user
            Console.WriteLine("Enter amount you want to withdraw:");
            //Converting the input into integer
            int amount = Convert.ToInt32(Console.ReadLine());
            //If amount to withdraw is less than or equal to amount present in account
            if (amt_dict[name] >= amount)
            {
                //Debiting amount
                amt_dict[name] = amt_dict[name] - amount;
                //Prompting user
                Console.WriteLine(Convert.ToString(amount) + " is withdrawn successfully!");
            }
            else
            {
                //Insufficient funds
                Console.WriteLine("Insufficient Amount to withdraw!");
            }
            //Writing into log
            trans.Add("Withdrawn " + Convert.ToString(amount) + " from " + name);
            options();
        }
        static void transfer()
        {
            string rcvr;
            int amount;
            //Taking Reciever name to tranfer amount.Amount will be transferred only when account exists
            Console.WriteLine("Enter Receiver name:");
            rcvr = Console.ReadLine();
            //Requesting amount to transfer
            Console.WriteLine("Enter amount to transfer:");
            amount = Convert.ToInt32(Console.ReadLine());
            if (account.ContainsKey(rcvr))
            {
                //Only if the Receiver has valid account
                if (amt_dict[name] >= amount)
                {
                    //Only if sufficient amount is there to transfer
                    //Updating sender account 
                    amt_dict[rcvr] = amt_dict[rcvr] + amount;
                    //Updating receiver account 
                    amt_dict[name] = amt_dict[name] - amount;
                    Console.WriteLine("Transfer Successfull!");
                    //Adding the transferred transaction into transaction log
                    trans.Add("Transferred " + Convert.ToString(amount) + " from " + name + " to " + rcvr);

                }
                else
                {
                    //There are no sufficient funds in the account
                    Console.WriteLine("Insufficient amount to transfer!");
                }
            }
            else
            {
                //If reciever does not have an account
                Console.WriteLine("Reciver account does not exist!");
            }
            //Calling options method to print Menu 
            options();

        }
        static void options()
        {
            int option;
            //Menu
            Console.WriteLine("1.Deposit\n2.Withdraw\n3.Transfer\n4.View Balance\n5.Logout\n");
            //Accepting user option
            option = Convert.ToInt32(Console.ReadLine());
            //Switch case to perform operations
            switch (option)
            {
                case 1:
                    //Deposit
                    deposit();
                    break;
                case 2:
                    //Withdraw
                    withdraw();
                    break;
                case 3:
                    //Transfer
                    transfer();
                    break;
                case 4:
                    //View balance
                    view_bal();
                    break;
                case 5:
                    //Logout
                    Console.WriteLine("Logged out Successfully");
                    fileops();
                    //Displaying transaction summary
                    Console.WriteLine();
                    for (int i = 0; i < trans.Count; i++)
                    {
                        Console.WriteLine(trans[i]);
                        //File object to write logs into file
                        StreamWriter sw = File.AppendText(transaction_log);
                        //writing logs into file
                        sw.WriteLine(trans[i]);
                        //Flushing and closing the file
                        sw.Flush();
                        sw.Close();
                    }
                    //Exit
                    Environment.Exit(1);
                    break;
                default:
                    //When user enters invalid option
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
        static void fileops()
        {
            //This function executes during logging out,
            //Overwrites filename file 
            StreamWriter sw = new StreamWriter(filename);
            foreach (KeyValuePair<string, int> kvp in amt_dict)
                //Writing values in dictionaries into file
                sw.WriteLine(kvp.Key + ":" + account[kvp.Key] + ":" + Convert.ToString(kvp.Value));
            sw.Flush();
            sw.Close();
        }
        //Driver Code
        static void Main()
        {
            string have_account;
            Console.WriteLine("Welcome!");
            Console.WriteLine("Do you have an account?yes/no/ Type exit to Exit:");
            have_account = Console.ReadLine();
            init();
            if (have_account == "no")
            {
                //If user don't have account ,create_account method is called
                create_account();
            }
            else if (have_account == "yes")
            {
                //If user has acocunt and wants to login, login method is called
                login();
            }
            else
            {
                //If user wants to exit!
                //Transaction details will be displayed on console!
                Console.WriteLine();
                for (int i = 0; i < trans.Count; i++)
                {
                    Console.WriteLine(trans[i]);
                }
                Environment.Exit(1);
            }
        }
    }
}