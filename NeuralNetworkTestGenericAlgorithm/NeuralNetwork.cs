using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTestGenericAlgorithm
{
    public class NeuralNetwork
    {
        private Random rand;

        private Node[] inputLayer;
        private Node[] hiddenLayer;
        private Node[] outputLayer;

        public NeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes, Random rand = null)
        {
            if(rand == null)
            {
                rand = new Random();
            }
            this.rand = rand;

            inputLayer = InitializeLayer(inputNodes, hiddenNodes);
            hiddenLayer = InitializeLayer(hiddenNodes, outputNodes);
            outputLayer = InitializeLayer(outputNodes, 0);
        }

        private Node[] InitializeLayer(int nodes, int nextLayerNodeCount)
        {
            Node[] layer = new Node[nodes];
            for (int i = 0; i < nodes; ++i)
            {
                double[] weights = new double[nextLayerNodeCount];
                for (int j = 0; j < weights.Length; ++j)
                {
                    weights[j] = rand.NextDouble();
                }
                layer[i] = new Node(0, weights);
            }

            return layer;
        }

        public int InputLayerLength
        {
            get { return inputLayer.Length; }
        }

        public int HiddenLayerLength
        {
            get { return hiddenLayer.Length; }
        }

        public int OutputLayerLength
        {
            get { return outputLayer.Length; }
        }

        private void SetValues(double[] values, Node[] layerToChange)
        {
            if (values.Length == layerToChange.Length)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    layerToChange[i].InputValue = values[i];
                }
            }
        }

        private void SetWeights(List<double[]> values, Node[] layerToChange)
        {
            for (int currentNodePos = 0; currentNodePos < layerToChange.Length; ++currentNodePos)
            {
                layerToChange[currentNodePos].Weights = values[currentNodePos];
            }
        }

        private void SetResults(List<double[]> values, Node[] layerToChange)
        {
            for (int currentNodePos = 0; currentNodePos < layerToChange.Length; ++currentNodePos)
            {
                layerToChange[currentNodePos].Results = values[currentNodePos];
            }
        }

        private double[] GetValues(Node[] layer)
        {
            double[] values = new double[layer.Length];
            for (int i = 0; i < values.Length; ++i)
            {
                values[i] = layer[i].InputValue;
            }
            return values;
        }

        private List<double[]> GetWeights(Node[] layer)
        {
            List<double[]> values = new List<double[]>();
            foreach (Node node in layer)
            {
                values.Add(node.Weights);
            }
            return values;
        }

        private List<double[]> GetResults(Node[] layer)
        {
            List<double[]> values = new List<double[]>();
            foreach (Node node in layer)
            {
                values.Add(node.Results);
            }
            return values;
        }

        public double[] GetInputValues()
        {
            return GetValues(inputLayer);
        }

        public void SetInputValues(double[] values)
        {
            SetValues(values, inputLayer);
        }

        public double[] GetHiddenValues()
        {
            return GetValues(hiddenLayer);
        }

        public void SetHiddenValues(double[] values)
        {
            SetValues(values, hiddenLayer);
        }

        public double[] GetOutputValues()
        {
            return GetValues(outputLayer);
        }

        public void SetOutputValues(double[] values)
        {
            SetValues(values, outputLayer);
        }

        public void SetInputWeights(List<double[]> weights)
        {
            SetWeights(weights, inputLayer);
        }

        public List<double[]> GetInputWeights()
        {
            return GetWeights(inputLayer);
        }

        public void SetHiddenWeights(List<double[]> weights)
        {
            SetWeights(weights, hiddenLayer);
        }

        public List<double[]> GetHiddenWeights()
        {
            return GetWeights(hiddenLayer);
        }

        public void SetOutputWeights(List<double[]> weights)
        {
            SetWeights(weights, outputLayer);
        }

        public List<double[]> GetOutputWeights()
        {
            return GetWeights(outputLayer);
        }

        public void SetInputResults(List<double[]> results)
        {
            SetResults(results, inputLayer);
        }

        public void SetHiddenResults(List<double[]> results)
        {
            SetResults(results, hiddenLayer);
        }

        public void SetOutputResults(List<double[]> results)
        {
            SetResults(results, outputLayer);
        }

        public List<double[]> GetInputResults()
        {
            return GetResults(inputLayer);
        }

        public List<double[]> GetHiddenResults()
        {
            return GetResults(hiddenLayer);
        }

        public List<double[]> GetOutputResults()
        {
            return GetResults(outputLayer);
        }

        public void CalculateError(double[] expected)
        {
            for (int i = 0; i < OutputLayerLength; ++i)
            {
                outputLayer[i].Error = Math.Pow(expected[i] - outputLayer[i].InputValue, 2);
            }
        }

        public double[] GetOutputErrors()
        {
            double[] results = new double[OutputLayerLength];
            for (int i = 0; i < results.Length; ++i)
            {
                results[i] = outputLayer[i].Error;
            }
            return results;
        }

        public void PropagateForward()
        {
            CalculateNodes(inputLayer, hiddenLayer);
            CalculateNodes(hiddenLayer, outputLayer);
        }

        private void CalculateNodes(Node[] layer, Node[] nextLayer)
        {
            foreach (Node currentLayerNode in layer)
            {
                currentLayerNode.CalculateWeightetInputResults();
            }

            for (int nextNodePos = 0; nextNodePos < nextLayer.Length; ++nextNodePos)
            {
                double totalValue = 0.0;
                foreach (Node currentLayerNode in layer)
                {
                    totalValue += currentLayerNode.Results[nextNodePos];
                }
                nextLayer[nextNodePos].InputValue = 1 / (1 + totalValue);
            }
        }

        public double GetFitnesse()
        {
            double totalFitness = 0;
            double totalError = 0;
            double[] outputErrors = GetOutputErrors();
            for (int i = 0; i < outputErrors.Length; i++)
            {
                double currentError = outputErrors[i];
                totalError += currentError;
            }

            // The higher the error, the lower the fitnesse
            totalFitness = 10000 * (1 / (1 + totalError));
            return totalFitness;
        }
    }
}
