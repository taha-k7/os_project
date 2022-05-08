using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//important
using MINI_FILE_SYSTEM;

namespace MINI_FILE_SYSTEM
{

    public static class Folder
    {
        public static Directory current;
        public static string currentPath;
        static void Main(string[] args)
        {
            Virtual_Disk.Initalize("virtualDisk");
            currentPath = new string(current.FileName);
            currentPath = currentPath.Trim(new char[] { '\0', ' ' });
            while (true)
            {
                //var CurrentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                Console.Write(currentPath + "\\" + ">");
                string input = Console.ReadLine();
                string[] input_list = input.Split(' ');
                if (input == "")
                {
                    continue;
                }
                List<string> ls = new List<string>();
                for (int i = 0; i < input_list.Length; i++)
                {
                    if (input_list[i] is not "" and not " ")
                    {
                        ls.Add(input_list[i]);
                    }
                }

                string[] arguments = ls.ToArray();
                arguments[0] = arguments[0].ToLower();
                int cont = arguments.Length;

                if (Check.Check_arg(arguments[0]) == false)
                {
                    Console.WriteLine("Error: " + arguments[0].ToUpper() + " This command is not supported by the Project.");
                }
                else
                {
                    if (cont > 1)
                    {
                        Check.Call_command(arguments[0], arguments[1]);
                    }
                    else
                    {
                        Check.Call_command(arguments[0]);
                    }
                }
            }
        }
    }
}