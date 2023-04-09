using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForSitec
{
    public class TotalOfDocuments
    {
        public List<string> ResponsibleExecutor = new List<string>();
        public List<int> TotalOfNumber = new List<int>();
        public List<int> Appeals = new List<int>();
        public List<int> RKK = new List<int>();
        public void SearchTotalOfDocument(List<int> Numbers, List<string> ResponsibleExecutorAppeals, List<int> NumbersRKK, List<string> ResponsibleExecutorRKK)
        {
            List<int> vremAppleas = new List<int>();
            List<int> vremRKK = new List<int>();
            vremAppleas.AddRange(Numbers);
            vremRKK.AddRange(NumbersRKK);
            for (int i = 0; i < ResponsibleExecutorRKK.Count; i++)
            {                
                for (int j = 0; j < ResponsibleExecutorAppeals.Count; j++)
                {
                    if (ResponsibleExecutorRKK[i] == ResponsibleExecutorAppeals[j])
                    {
                        Appeals.Add(vremAppleas[j]);
                        RKK.Add(vremRKK[i]);
                        ResponsibleExecutor.Add(ResponsibleExecutorRKK[i]);
                        ResponsibleExecutorRKK.RemoveAt(i);
                        ResponsibleExecutorAppeals.RemoveAt(j);
                        TotalOfNumber.Add(vremRKK[i] + vremAppleas[j]);
                        vremAppleas.RemoveAt(j);
                        vremRKK.RemoveAt(i);
                        i = -1;
                        break;
                    }
                    else if (i == ResponsibleExecutorRKK.Count -1 && j == ResponsibleExecutorAppeals.Count - 1 && !(ResponsibleExecutorRKK[i] == ResponsibleExecutorAppeals[j]))
                    {
                        ResponsibleExecutor.AddRange(ResponsibleExecutorRKK);
                        ResponsibleExecutor.AddRange(ResponsibleExecutorAppeals);
                        TotalOfNumber.AddRange(vremRKK);
                        TotalOfNumber.AddRange(vremAppleas);
                        RKK.AddRange(vremRKK);
                        for (int q = 0; q < vremRKK.Count; q++)
                        {
                            Appeals.Add(0);
                        }
                        for (int w = 0; w < vremAppleas.Count; w++)
                        {
                            RKK.Add(0);
                        }
                        Appeals.AddRange(vremAppleas);
                    }
                }
            }
        }
    }
}
