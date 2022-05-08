using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_FILE_SYSTEM
{
    public class Directory : Directory_Entry
    {
        public List<Directory_Entry> DirectoryTable;
        public Directory parent;
        public Directory(string filename, byte fileattr, int firstCluster, Directory parant) : base(filename, fileattr, firstCluster)
        {
            DirectoryTable = new List<Directory_Entry>();

            if (parant != null)// not root
            {
                this.parent = parant;

            }

        }
        public void UpdateContent(Directory_Entry d)
        {
            int index = SearchDirectory(new string(d.FileName));
            if (index != -1)
            {
                DirectoryTable.RemoveAt(index);
                DirectoryTable.Insert(index, d);
            }
        }
        public Directory_Entry GetDirectoryEntry()
        {
            Directory_Entry d = new Directory_Entry(new string(this.FileName), this.FileAttr, this.FirstCluster);
            return d;
        }
        public void WriteDirectory()
        {
            byte[] dirsorfilesBYTES = new byte[DirectoryTable.Count * 32];
            for (int i = 0; i < DirectoryTable.Count; i++)
            {
                byte[] b = Converter.Directory_EntryToBytes(this.DirectoryTable[i]);
                for (int j = i * 32, k = 0; k < b.Length; k++, j++)
                {
                    dirsorfilesBYTES[j] = b[k];
                }
            }
            List<byte[]> bytesls = Converter.SplitBytesToBlocks(dirsorfilesBYTES);
            int clusterFATIndex;
            if (this.FirstCluster != 0)
            {
                clusterFATIndex = this.FirstCluster;
            }
            else
            {
                clusterFATIndex = Fat.GetAvilableBlock();
                this.FirstCluster = clusterFATIndex;
            }
            int lastCluster = -1;
            for (int i = 0; i < bytesls.Count; i++)
            {
                if (clusterFATIndex != -1)//disk has space
                {
                    Virtual_Disk.WriteBlock(bytesls[i], clusterFATIndex, 0, bytesls[i].Length);
                    Fat.SetNext(clusterFATIndex, -1);//temp
                    if (lastCluster != -1)//
                    {
                        Fat.SetNext(lastCluster, clusterFATIndex);
                    }

                    lastCluster = clusterFATIndex;
                    clusterFATIndex = Fat.GetAvilableBlock();
                }
            }
            if (this.parent != null)
            {
                this.parent.UpdateContent(this.GetDirectoryEntry());//since fc changes from 0 to the used fc for example
                this.parent.WriteDirectory();
            }
            Fat.WriteFAT();
        }

        public void ReadDirectory()
        {
            if (this.FirstCluster != 0)
            {
                DirectoryTable = new List<Directory_Entry>();
                int cluster = this.FirstCluster;
                int next = Fat.GetNext(cluster);
                List<byte> ls = new List<byte>();//1024*count/32 bytes
                do
                {
                    ls.AddRange(Virtual_Disk.ReadBlock(cluster));
                    cluster = next;
                    if (cluster != -1)// not last
                    {
                        next = Fat.GetNext(cluster);
                    }
                }
                while (next != -1);
                for (int i = 0; i < ls.Count; i++)
                {
                    byte[] b = new byte[32];
                    for (int k = i * 32, m = 0; m < b.Length && k < ls.Count; m++, k++)
                    {
                        b[m] = ls[k];
                    }
                    if (b[0] == 0)
                    {
                        break;
                    }

                    DirectoryTable.Add(Converter.BytesToDirectory_Entry(b));
                }
            }
        }
        public void deleteDirectory()
        {
            if (this.FirstCluster != 0)
            {
                int cluster = this.FirstCluster;
                int next = Fat.GetNext(cluster);
                do
                {
                    Fat.SetNext(cluster, 0);
                    cluster = next;
                    if (cluster != -1)
                    {
                        next = Fat.GetNext(cluster);
                    }
                }
                while (cluster != -1);
            }
            if (this.parent != null)
            {
                int index = this.parent.SearchDirectory(new string(this.FileName));
                if (index != -1)
                {
                    this.parent.DirectoryTable.RemoveAt(index);
                    this.parent.WriteDirectory();
                    //Fat.writeFAT();
                }
            }
            if (Folder.current == this)
            {
                if (this.parent != null)
                {
                    Folder.current = this.parent;
                    Folder.currentPath = Folder.currentPath.Substring(0, Folder.currentPath.LastIndexOf('\\'));
                    Folder.current.ReadDirectory();
                }
            }
            Fat.WriteFAT();
        }
        public int SearchDirectory(string name)
        {
            if (name.Length < 11)
            {
                name += "\0";
                for (int i = name.Length + 1; i < 12; i++)
                {
                    name += " ";
                }
            }
            else
            {
                name = name.Substring(0, 11);
            }
            for (int i = 0; i < DirectoryTable.Count; i++)
            {
                string Name = new(DirectoryTable[i].FileName);
                if (Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
