using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForSitec
{
    public class FileHandlerRKK
    {        
        public List<string> ResponsibleExecutorRKK = new List<string>();
        public List<int> NumbersRKK = new List<int>();
        public List<string> ExecutorRKK = new List<string>();
        public List<string> DirectorInRKK = new List<string>();
        public List<string> ExecutorInRKK = new List<string>();
        public void FileProcessing1(string Path)
        {
            string text1;
            StreamReader sr1 = new StreamReader(Path);
            while (!sr1.EndOfStream)
            {
                text1 = sr1.ReadLine();
                string[] tx = text1?.Split('\t');

                for (int j = 0; j < tx?.Length; j++)
                {
                    if (j % 2 == 0)
                    {
                        DirectorInRKK.Add(tx[j]);
                    }
                }

                for (int j = 0; j < tx?.Length; j++)
                {
                    if (!(j % 2 == 0))
                    {
                        ExecutorInRKK.Add(tx[j]);
                    }
                }
            }
        }

        public List<string> SearchResposiblExecutor(List<string> DirectorInRKK, List<string> ExecutorInRKK)
        {
            for (int i = 0; i < this.DirectorInRKK.Count; i++)
            {
                if (this.DirectorInRKK[i] == "Климов Сергей Александрович")
                {
                    string[] words = this.ExecutorInRKK[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] poi = words[0].Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                    ExecutorRKK.Add(poi[0]);

                }
                else
                {
                    string[] SurnameDirector = this.DirectorInRKK[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string trry = $"{SurnameDirector[0]} {SurnameDirector[1][0]}.{SurnameDirector[2][0]}.";
                    ExecutorRKK.Add(trry);
                }
            }
            return ExecutorRKK;
        }
        public void CountingAppeals()
        {
            for (int i = 0; i < ExecutorRKK.Count; i++)
            {
                if (ExecutorRKK.Count == 0) break;
                else
                {
                    int number = 1;
                    for (int j = 1; j < ExecutorRKK.Count; j++)
                    {
                        if (ExecutorRKK[i] == ExecutorRKK[j])
                        {
                            number++;
                            ExecutorRKK.RemoveAt(j);
                            j--;
                        }
                    }
                    ResponsibleExecutorRKK.Add(ExecutorRKK[i]);
                    NumbersRKK.Add(number);
                    ExecutorRKK.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
