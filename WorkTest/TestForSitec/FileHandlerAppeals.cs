using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForSitec
{
    public class FileHandlerAppeals
    {
        public List<string> ResponsibleExecutorAppeals = new List<string>();
        public List<int> Numbers = new List<int>();
        public List<string> Executor = new List<string>();
        public List<string> DirectorInAppeals = new List<string>();
        public List<string> ExecutorInAppeals = new List<string>();

        public void FileProcessing1(string Path1)
        {
            string text1;

            StreamReader sr1 = new StreamReader(Path1);
            while (!sr1.EndOfStream)
            {
                text1 = sr1.ReadLine();
                string[] tx = text1?.Split('\t');

                for (int j = 0; j < tx?.Length; j++)
                {
                    if (j % 2 == 0)
                    {
                        DirectorInAppeals.Add(tx[j]);
                    }
                }

                for (int j = 0; j < tx?.Length; j++)
                {
                    if (!(j % 2 == 0))
                    {
                        ExecutorInAppeals.Add(tx[j]);
                    }
                }
            }
        }

        public List<string> SearchResposiblExecutor(List<string> DirectorInAppeals, List<string> ExecutorInAppeals)
        {
            for (int i = 0; i < DirectorInAppeals.Count; i++)
            {
                if (DirectorInAppeals[i] == "Климов Сергей Александрович")
                {
                    string[] words = ExecutorInAppeals[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] poi = words[0].Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                    Executor.Add(poi[0]);
                }
                else
                {
                    string[] SurnameDirector = DirectorInAppeals[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string trry = $"{SurnameDirector[0]} {SurnameDirector[1][0]}.{SurnameDirector[2][0]}.";
                    Executor.Add(trry);
                }
            }
            return Executor;
        }
        public void CountingAppeals()
        {
            for (int i = 0; i < Executor.Count; i++)
            {
                if (Executor.Count == 0) break;
                else
                {
                    int number = 1;
                    for (int j = 1; j < Executor.Count; j++)
                    {
                        if (Executor[i] == Executor[j])
                        {
                            number++;
                            Executor.RemoveAt(j);
                            j--;
                        }
                    }
                    ResponsibleExecutorAppeals.Add(Executor[i]);
                    Numbers.Add(number);
                    Executor.RemoveAt(i);
                    i--;
                }

            }
        }
    }
}
