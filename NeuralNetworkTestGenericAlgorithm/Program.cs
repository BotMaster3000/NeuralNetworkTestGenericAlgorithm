using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTestGenericAlgorithm
{
    class Program
    {
        static List<double[]> binarystrings
        {
            get
            {
                List<double[]> tempList = new List<double[]>();
                for (int i = 0; i <= 255; ++i)
                {
                    char[] tempChar = Convert.ToString(i, 2).PadLeft(8).ToCharArray();
                    double[] intArray = new double[8];
                    for (int j = 0; j < 8; ++j)
                    {
                        intArray[j] = tempChar[j] == '0' ? 0 : 1;
                    }
                    tempList.Add(intArray);
                }

                return tempList;
            }
        }

        static void Main(string[] args)
        {
            GeneticAlgorithm algorithm = new GeneticAlgorithm(100, 10, 0.1);
            algorithm.SetupNetworks(8, 10, 8);
            algorithm.PerformAutoBreed(100000, binarystrings);
            algorithm.SortByFitness();

            NeuralNetwork[] networks = algorithm.Networks;

            string expectedAsString = "";
            foreach (double number in networks[0].GetInputValues())
            {
                expectedAsString += number + "|";
            }

            Console.WriteLine("Generation: " + algorithm.CurrentGeneration + " - Expected: " + expectedAsString);
            for (int j = 0; j < 10; ++j)
            {
                string output = "";
                foreach (double x in networks[j].GetOutputValues())
                {
                    output += Math.Round(x, 0, MidpointRounding.AwayFromZero) + "|";
                }

                Console.WriteLine(j + ":" + networks[j].GetFitnesse() + " : " + output);
            }
            Console.WriteLine();
        }
    }

}
