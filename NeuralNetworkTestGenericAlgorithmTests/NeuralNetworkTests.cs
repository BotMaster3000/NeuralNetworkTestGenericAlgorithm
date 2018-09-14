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
    public class NeuralNetworkTests
    {
        private NeuralNetwork network;

        private const int inputNeurons = 2;
        private const int hiddenNeurons = 2;
        private const int outputNeurons = 2;

        private readonly double[] expectedInputValues = new double[] { 1, 2 };
        private readonly List<double[]> expectedWeights = new List<double[]> { new double[] { 0.1, 0.2, 0.3 }, new double[] { 0.5, 0.8, 0.05 } };
        private readonly List<double[]> expectedResults = new List<double[]> { new double[] { 0.5, 0.8, 0.05 }, new double[] { 0.1, 0.2, 0.3 } };


        private void SetupNeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes)
        {
            network = new NeuralNetwork(inputNodes, hiddenNodes, outputNodes);
        }

        [TestMethod]
        public void TestSetInputValues()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetInputValues(expectedInputValues);
            Assert.IsTrue(Helper.CompareArrayEquality(expectedInputValues, network.GetInputValues()));
        }

        [TestMethod]
        public void TestSetInputWeights()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetInputWeights(expectedWeights);
            Assert.IsTrue(Helper.CompareListArrayEquality(expectedWeights, network.GetInputWeights()));
        }

        [TestMethod]
        public void TestSetInputResults()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetInputResults(expectedResults);
            Assert.IsTrue(Helper.CompareListArrayEquality(expectedResults, network.GetInputResults()));
        }

        [TestMethod]
        public void TestSetHiddenValues()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetHiddenValues(expectedInputValues);
            Assert.IsTrue(Helper.CompareArrayEquality(expectedInputValues, network.GetHiddenValues()));
        }

        [TestMethod]
        public void TestSetHiddenWeights()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetHiddenWeights(expectedWeights);
            Assert.IsTrue(Helper.CompareListArrayEquality(expectedWeights, network.GetHiddenWeights()));
        }

        [TestMethod]
        public void TestSetHiddenResults()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetHiddenResults(expectedResults);
            Assert.IsTrue(Helper.CompareListArrayEquality(expectedResults, network.GetHiddenResults()));
        }

        [TestMethod]
        public void TestSetOutputValues()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetOutputValues(expectedInputValues);
            Assert.IsTrue(Helper.CompareArrayEquality(expectedInputValues, network.GetOutputValues()));
        }

        [TestMethod]
        public void TestSetOutputWeights()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetOutputWeights(expectedWeights);
            Assert.IsTrue(Helper.CompareListArrayEquality(expectedWeights, network.GetOutputWeights()));
        }

        [TestMethod]
        public void TestSetOutputResults()
        {
            SetupNeuralNetwork(inputNeurons, hiddenNeurons, outputNeurons);
            network.SetOutputResults(expectedResults);
            Assert.IsTrue(Helper.CompareListArrayEquality(expectedResults, network.GetOutputResults()));
        }



        [TestMethod]
        public void CheckInputNodeCount()
        {
            int nodeCount = 3;
            SetupNeuralNetwork(nodeCount, 0, 0);
            Assert.AreEqual(nodeCount, network.InputLayerLength);
        }

        [TestMethod]
        public void CheckHiddenNodeCount()
        {
            int nodeCount = 2;
            SetupNeuralNetwork(0, nodeCount, 0);
            Assert.AreEqual(nodeCount, network.HiddenLayerLength);
        }

        [TestMethod]
        public void CheckOutputNodeCount()
        {
            int nodeCount = 1;
            SetupNeuralNetwork(0, 0, nodeCount);
            Assert.AreEqual(nodeCount, network.OutputLayerLength);
        }

        [TestMethod]
        public void PropagateForward()
        {
            SetupNeuralNetwork(3, 2, 1);
            network.SetInputValues(new double[] { 0, 1, 2 });
            network.PropagateForward();
            foreach (double outputValue in network.GetOutputValues())
            {
                if (outputValue == 0.0)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void CalculateError()
        {
            SetupNeuralNetwork(3, 2, 3);

            double[] fakeExpectedValues = new double[] { 3, 2, 1 };
            network.CalculateError(fakeExpectedValues);

            foreach (double result in network.GetOutputErrors())
            {
                if (result == 0)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void GetFitness()
        {
            SetupNeuralNetwork(3, 2, 4);
            double fitnesse = network.GetFitnesse();
            Assert.IsTrue(fitnesse > 0);
        }
    }
}