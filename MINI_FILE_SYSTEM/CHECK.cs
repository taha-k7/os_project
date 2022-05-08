using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_FILE_SYSTEM
{
    internal class Check
    {
        public static bool Check_arg(string arg)
        {
            string[] command = { "cd", "help", "dir", "quit", "copy", "cls", "del", "md", "rd", "rename", "type", "import", "export" };
            foreach (string i in command)
            {
                if (i == arg)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Call_command(string arg = "", string arg2 = "")
        {
            if (arg == "help")
            {
                if (arg2 != "")
                {
                    Command.Help(arg2);
                }
                else
                {
                    Command.Help();
                }
            }
            else if (arg == "quit")
            {
                Command.Quit();
            }
            else if (arg == "cls")
            {
                Command.Clear();
            }
            else if (arg == "md")
            {
                Command.CreateDirectory(arg2);
            }
            else if (arg == "cd")
            {
                Command.CD(arg2);
            }
            else if (arg == "rd")
            {
                Command.RD(arg2);
            }
        }

    }
}
