using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkTestGenericAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTestGenericAlgorithm.Tests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        const int totalNetworks = 100;
        const int totalBreeders = 10;
        GeneticAlgorithm algorithm;

        int currentDatasetUsed = 0;

        Random rand = new Random();

        private void GeneticAlgorithmSetup()
        {
            algorithm = new GeneticAlgorithm(totalNetworks, totalBreeders, 0.1);
            SetupBinaryNumberStrings();
        }

        List<double[]> binaryNumberStrings;

        private void SetupBinaryNumberStrings()
        {
            List<double[]> tempList = new List<double[]>();
            for(int i = 0; i <= 255; ++i)
            {
                char[] tempChar = Convert.ToString(i, 2).PadLeft(8).ToCharArray();
                double[] intArray = new double[8];
                for(int j = 0; j < 8; ++j)
                {
                    intArray[j] = tempChar[j] == '0' ? 0 : 1;
                }
                tempList.Add(intArray);
            }

            binaryNumberStrings = tempList;
        }

        [TestMethod]
        public void GetTotalNetworkAmount()
        {
            GeneticAlgorithmSetup();
            Assert.IsTrue(algorithm.GetTotalNetworkAmount() == totalNetworks);
        }

        [TestMethod]
        public void GetTotalNetworkBreedingAmount()
        {
            GeneticAlgorithmSetup();
            Assert.IsTrue(algorithm.GetTotalNetworkBreedingAmount() == totalBreeders);
        }

        [TestMethod]
        public void SetupNetworks()
        {
            int inputLayerAmount = 8;
            int hiddenLayerAmount = 8;
            int outputLayerAmount = 8;
            GeneticAlgorithmSetup();
            algorithm.SetupNetworks(inputLayerAmount, hiddenLayerAmount, outputLayerAmount);
        }

        [TestMethod]
        public void SetNetworkInputValues()
        {
            currentDatasetUsed = rand.Next(0, 255);
            SetupNetworks();
            algorithm.SetNetworkInputValues(binaryNumberStrings[currentDatasetUsed]);
        }

        [TestMethod]
        public void PropagateAllNetworks()
        {
            GeneticAlgorithmSetup();
            SetupNetworks();
            algorithm.PropagateAllNetworks();
        }

        [TestMethod]
        public void CalculateAllNetworkErrors()
        {
            SetupNetworks();
            algorithm.CalculateAllNetworkErrors(binaryNumberStrings[currentDatasetUsed]);
        }

        [TestMethod]
        public void SortByFitness()
        {
            SetupNetworks();
            algorithm.CalculateAllNetworkErrors(binaryNumberStrings[currentDatasetUsed]);
            algorithm.SortByFitness();
        }

        [TestMethod]
        public void RebreedTopNetworks()
        {
            SortByFitness();
            algorithm.RebreedTopNetworks();
        }

        [TestMethod]
        public void PerformAutoBreed()
        {
            int iterations = 100;
            SetupNetworks();
            algorithm.PerformAutoBreed(iterations, binaryNumberStrings, binaryNumberStrings);
        }

        [TestMethod]
        public void TestGetNetworks()
        {
            SetupNetworks();
            NeuralNetwork[] networks = algorithm.Networks;
        }
    }
}