using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTestGenericAlgorithm
{
    class Program
    {
        static List<double[]> dataSets = new List<double[]>();
        static List<double[]> expectedResults = new List<double[]>();

        private static void SetupTestDataSetsAndResult()
        {
            // Creates Dataset with i and expected Result with to the Modulus of i with 2, so either 1 or 0
            for (int i = 0; i < 10000000; ++i)
            {
                dataSets.Add(new double[] { i });
                expectedResults.Add(new double[] { i % 2 });
            }
        }

        static void Main(string[] args)
        {
            SetupTestDataSetsAndResult();

            GeneticAlgorithm algorithm = new GeneticAlgorithm(100, 10, 0.1);
            algorithm.SetupNetworks(1, 2, 1);

            for (int generationIterator = 0; generationIterator < 1000000; ++generationIterator)
            {
                Console.WriteLine("Generation : " + generationIterator + (5 * generationIterator));
                algorithm.PerformAutoBreed(5, dataSets, expectedResults);
                const int AMOUNT_OF_NETWORKS_TO_DISPLAY = 10;
                for (int networkIterator = 0; networkIterator < AMOUNT_OF_NETWORKS_TO_DISPLAY; ++networkIterator)
                {
                    NeuralNetwork currentNetwork = algorithm.Networks[networkIterator];

                    Console.WriteLine("Input: {0} | Result: {1} | Expected: {2} | Fitness: {3} | Error: {4}",
                                      currentNetwork.GetInputValues()[0], currentNetwork.GetOutputErrors()[0], currentNetwork.GetInputValues()[0] % 2, currentNetwork.GetFitnesse(), currentNetwork.GetOutputErrors()[0]);
                }
                Console.WriteLine();
            }

            Console.ReadLine();



            //algorithm.PerformAutoBreed(1000, dataSets);
            //NeuralNetwork[] networks = algorithm.Networks;

            //string expectedAsString = "";
            //foreach (double number in networks[0].GetInputValues())
            //{
            //    expectedAsString += number + "|";
            //}

            //Console.WriteLine("Generation: " + algorithm.CurrentGeneration + " - Expected: " + expectedAsString);
            //for (int j = 0; j < 10; ++j)
            //{

            //    string output = "";
            //    foreach (double x in networks[j].GetOutputValues())
            //    {
            //        output += Math.Round(x, 0, MidpointRounding.AwayFromZero) + "|";
            //    }

            //    Console.WriteLine(j + ":" + networks[j].GetFitnesse() + " : " + output);
            //}
            //Console.WriteLine();
        }
    }

}
