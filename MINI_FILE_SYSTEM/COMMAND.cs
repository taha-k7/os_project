using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_FILE_SYSTEM
{
    public static class Command
    {
        public static void Help(string com = "ok")
        {
            bool fund = false;
            string[] command = { "cd", "help", "dir", "quit", "copy", "cls", "del", "md", "rd", "rename", "type", "import", "export" };
            foreach (string i in command)
            {
                if (i != com.ToLower())
                {
                    continue;
                }
                fund = true;
            }

            if (com != "ok" && fund)
            {
                if (com == "cd")
                {
                    Console.WriteLine("Change the current default directory to the directory given in the argument.");
                    Console.WriteLine("If the argument is not present, report the current directory.");
                    Console.WriteLine("If the directory does not exist an appropriate error should be reported.");
                    Console.WriteLine(com + " command syntax is \n cd \n or \n cd [directory]");
                    Console.WriteLine("[directory] can be directory name or fullpath of a directory");
                }
                else if (com == "cls")
                {
                    Console.WriteLine("Clear the screen.");
                    Console.WriteLine(com + " command syntax is \n cls");
                }
                else if (com == "dir")
                {
                    Console.WriteLine("List the contents of directory given in the argument.");
                    Console.WriteLine("If the argument is not present, list the content of the current directory.");
                    Console.WriteLine("If the directory does not exist an appropriate error should be reported.");
                    Console.WriteLine(com + " command syntax is \n dir \n or \n dir [directory]");
                    Console.WriteLine("[directory] can be directory name or fullpath of a directory");
                }
                else if (com == "quit")
                {
                    Console.WriteLine("Quit the shell.");
                    Console.WriteLine(com + " command syntax is \n quit");
                }
                else if (com == "copy")
                {
                    Console.WriteLine("Copies one or more files to another location.");
                    Console.WriteLine(com + " command syntax is \n copy [source]+ [destination]");
                    Console.WriteLine("+ after [source] represent that you can pass more than file Name (or fullpath of file) or more than directory Name (or fullpath of directory)");
                    Console.WriteLine("[source] can be file Name (or fullpath of file) or directory Name (or fullpath of directory)");
                    Console.WriteLine("[destination] can be directory name or fullpath of a directory");
                }
                else if (com == "del")
                {
                    Console.WriteLine("Deletes one or more files.");
                    Console.WriteLine("NOTE: it confirms the user choice to delete the file before deleting");
                    Console.WriteLine(com + " command syntax is \n del [file]+");
                    Console.WriteLine("+ after [file] represent that you can pass more than file Name (or fullpath of file)");
                    Console.WriteLine("[file] can be file Name (or fullpath of file)");
                }
                else if (com == "help")
                {
                    Console.WriteLine("Provides Help information for commands.");
                    Console.WriteLine(com + " command syntax is \n help \n or \n For more information on a specific command, type help [command]");
                    Console.WriteLine("command - displays help information on that command.");
                }
                else if (com == "md")
                {
                    Console.WriteLine("Creates a directory.");
                    Console.WriteLine(com + " command syntax is \n md [directory]");
                    Console.WriteLine("[directory] can be a new directory name or fullpath of a new directory");
                }
                else if (com == "rd")
                {
                    Console.WriteLine("Removes a directory.");
                    Console.WriteLine("NOTE: it confirms the user choice to delete the directory before deleting");
                    Console.WriteLine(com + " command syntax is \n rd [directory]");
                    Console.WriteLine("[directory] can be a directory name or fullpath of a directory");
                }
                else if (com == "rename")
                {
                    Console.WriteLine("Renames a file.");
                    Console.WriteLine(com + " command syntax is \n rd [fileName] [new fileName]");
                    Console.WriteLine("[fileName] can be a file name or fullpath of a filename ");
                    Console.WriteLine("[new fileName] can be a new file name not fullpath ");
                }
                else if (com == "type")
                {
                    Console.WriteLine("Displays the contents of a text file.");
                    Console.WriteLine(com + " command syntax is \n type [file]");
                    Console.WriteLine("NOTE: it displays the filename before its content for every file");
                    Console.WriteLine("[file] can be file Name (or fullpath of file) of text file");
                }
                else if (com == "import")
                {
                    Console.WriteLine("– import text file(s) from your computer ");
                    Console.WriteLine(com + " command syntax is \n import [destination] [file]+");
                    Console.WriteLine("+ after [file] represent that you can pass more than file Name (or fullpath of file) of text file");
                    Console.WriteLine("[file] can be file Name (or fullpath of file) of text file");
                    Console.WriteLine("[destination] can be directory name or fullpath of a directory in your implemented file system");
                }
                else if (com == "export")
                {
                    Console.WriteLine("– export text file(s) to your computer ");
                    Console.WriteLine(com + " command syntax is \n export [destination] [file]+");
                    Console.WriteLine("+ after [file] represent that you can pass more than file Name (or fullpath of file) of text file");
                    Console.WriteLine("[file] can be file Name (or fullpath of file) of text file in your implemented file system");
                    Console.WriteLine("[destination] can be directory name or fullpath of a directory in your computer");
                }
            }
            else if (com == "ok")
            {
                Console.WriteLine("cd       - Change the current default directory to .");
                Console.WriteLine("           If the argument is not present, report the current directory.");
                Console.WriteLine("           If the directory does not exist an appropriate error should be reported.");
                Console.WriteLine("cls      - Clear the screen.");
                Console.WriteLine("dir      - List the contents of directory .");
                Console.WriteLine("quit     - Quit the shell.");
                Console.WriteLine("copy     - Copies one or more files to another location");
                Console.WriteLine("del      - Deletes one or more files.");
                Console.WriteLine("help     - Provides Help information for commands.");
                Console.WriteLine("md       - Creates a directory.");
                Console.WriteLine("rd       - Removes a directory.");
                Console.WriteLine("rename   - Renames a file.");
                Console.WriteLine("type     - Displays the contents of a text file.");
                Console.WriteLine("import   – import text file(s) from your computer");
                Console.WriteLine("export   – export text file(s) to your computer");
            }
            else if (fund == false)
            {
                Console.WriteLine("Error: " + com + " This command is not supported by the project.");
            }
        }
        private static Directory moveTodir(string name, bool is_)
        {
            Directory d = null;
            string path;

            if (name != "..")
            {
                int i = Folder.current.SearchDirectory(name);
                if (i == -1)
                    return null;
                else
                {
                    string n = new string(Folder.current.DirectoryTable[i].FileName);
                    byte at = Folder.current.DirectoryTable[i].FileAttr;
                    int fc = Folder.current.DirectoryTable[i].FirstCluster;
                    d = new Directory(n, at, fc, Folder.current);
                    d.ReadDirectory();
                    path = Folder.currentPath;
                    path += "\\" + n.Trim(new char[] { '\0', ' ' });
                    if (is_)
                        Folder.currentPath = path;
                }
            }
            else
            {
                if (Folder.current.parent != null)
                {
                    d = Folder.current.parent;
                    d.ReadDirectory();
                    path = Folder.currentPath;
                    path = path.Substring(0, path.LastIndexOf('\\'));
                    if (is_)
                        Folder.currentPath = path;
                }
                else
                {
                    d = Folder.current;
                    d.ReadDirectory();
                }
            }
            return d;

        }
        public static void Clear()
        {
            Console.Clear();
        }
        public static void Quit()
        {
            Environment.Exit(0);
        }
 
        public static void CreateDirectory(string name)
        {
            if (Folder.current.SearchDirectory(name) == -1)
            {
                if (Fat.GetAvilableBlock() != -1)
                {
                    Directory_Entry d = new Directory_Entry(name, 0x10, 0);
                    Folder.current.DirectoryTable.Add(d);
                    Folder.current.WriteDirectory();
                    if (Folder.current.parent != null)
                    {
                        Folder.current.parent.UpdateContent(Folder.current.GetDirectoryEntry());
                        Folder.current.parent.WriteDirectory();
                    }
                    Fat.WriteFAT();
                }
                else
                {
                    Console.WriteLine($"Error : sorry the disk is full!");
                }
            }
            else
            {
                Console.WriteLine($"Error : this directory \" {name} \" is already exists!");
            }
        }
        public static void CD(string name)
        {
            Directory dir = moveTodir(name, true);
            if (dir != null)
            {
                dir.ReadDirectory();
                Folder.current = dir;
            }
            else
            {
                Console.WriteLine($"Error : this path \" {name} \" is not exists!");
            }
        }
        public static void RD(string name)
        {
            Directory dir = moveTodir(name, false);
            if (dir != null)
            {
                dir.deleteDirectory();
            }
            else
                Console.WriteLine($"Error : this directory \" {name} \" is not exists!");
        }

    }
}
