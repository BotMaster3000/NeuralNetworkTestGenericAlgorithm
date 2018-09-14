using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTestGenericAlgorithm.Tests
{
    public static class Helper
    {
        public static bool CompareArrayEquality(double[] array1, double[] array2)
        {
            bool returnValue = array1.Length == array2.Length;
            if (returnValue)
            {
                for (int i = 0; i < array1.Length; ++i)
                {
                    if (array1[i] != array2[i])
                    {
                        returnValue = false;
                        break;
                    }
                }
            }
            return returnValue;
        }

        public static bool CompareListArrayEquality(List<double[]> firstArrayList, List<double[]> secondArrayList)
        {
            bool returnValue = firstArrayList.Count == secondArrayList.Count;
            if (returnValue)
            {
                for(int i = 0; i < firstArrayList.Count; ++i)
                {
                    returnValue = firstArrayList[i].Length == secondArrayList[i].Length;
                    if (returnValue)
                    {
                        for(int j = 0; j < firstArrayList[i].Length; ++j)
                        {
                            returnValue = firstArrayList[i][j] == secondArrayList[i][j];
                            if (!returnValue)
                            {
                                return returnValue;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return returnValue;
        }
    }
}
