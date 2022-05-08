using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_FILE_SYSTEM
{
    internal class Virtual_Disk
    {
        public static FileStream Disk;
        public static void CREATEorOPEN_Disk(string path)
        {
            Disk = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
        public static int GetFreeSpace()
        {
            return (1024 * 1024) - (int)Disk.Length;
        }
        public static void Initalize(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    CREATEorOPEN_Disk(path);
                    byte[] b = new byte[1024];
                    for (int i = 0; i < b.Length; i++)
                    {
                        b[i] = 0;
                    }

                    WriteBlock(b, 0);
                    Fat.InitalizeFat();
                    Directory root = new Directory("K:", 0x10, 5, null);
                    root.WriteDirectory();
                    Fat.SetNext(5, -1);
                    Folder.current = root;
                    Fat.WriteFAT();
                }
                else
                {
                    CREATEorOPEN_Disk(path);
                    Fat.ReadFAT();
                    Directory root = new Directory("K:", 0x10, 5, null);
                    root.ReadDirectory();
                    Folder.current = root;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void WriteBlock(byte[] block, int Index, int offset = 0, int count = 1024)
        {
            Disk.Seek(Index * 1024, SeekOrigin.Begin);
            Disk.Write(block, offset, count);
            Disk.Flush();
        }
        public static byte[] ReadBlock(int Index)
        {
            Disk.Seek(Index * 1024, SeekOrigin.Begin);
            byte[] bytes = new byte[1024];
            Disk.Read(bytes, 0, 1024);
            return bytes;
        }
    }
}
