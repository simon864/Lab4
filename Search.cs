using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Search
    {
        public static void Searching(string Path, string[] KeyWords)
        {
            string[] Files = Directory.GetFiles(Path, "*.txt");
            foreach (string file in Files)
            {
                bool Flag = true;
                foreach (string KeyWord in KeyWords)
                {
                    if (KeyWord != null)
                    {
                        if (!File.ReadAllText(file).Contains(KeyWord))
                        {
                            Flag = false;
                        }
                    }
                }
                if (Flag)
                {
                    Console.WriteLine(file);
                }
            }
        }
    }
}
