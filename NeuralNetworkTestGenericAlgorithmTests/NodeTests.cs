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
    public class NodeTests
    {
        private double value;
        private double error;
        private double[] weights = new double[] { 0.5 };
        private double[] results = new double[] { 8 };

        [TestMethod]
        public void TestSetValue()
        {
            value = 0.7;
            Node node = new Node(value, weights);
            value = 0.3;
            node.InputValue = value;
            Assert.AreEqual(value, node.InputValue);
        }

        [TestMethod]
        public void TestSetWeights()
        {
            weights = new double[] { 0.5 };
            Node node = new Node(value, weights);
            weights = new double[] { 0.3 };
            node.Weights = weights;
            Assert.IsTrue(Helper.CompareArrayEquality(weights, node.Weights));
        }

        [TestMethod]
        public void TestSetResults()
        {
            Node node = new Node(value, weights);
            node.Results = results;
            Assert.AreEqual(results, node.Results);
        }

        [TestMethod]
        public void TestSetError()
        {
            error = 3;
            Node node = new Node(value, weights);
            node.Error = error;
            Assert.AreEqual(error, node.Error);
        }

        [TestMethod]
        public void TestCalculateWeightetInputResults()
        {
            value = 5;
            weights = new double[] { 0.5 };
            Node node = new Node(value, weights);
            node.CalculateWeightetInputResults();

            Assert.IsTrue(Helper.CompareArrayEquality(node.Results, new double[] { value * weights[0] }));
        }
    }
}