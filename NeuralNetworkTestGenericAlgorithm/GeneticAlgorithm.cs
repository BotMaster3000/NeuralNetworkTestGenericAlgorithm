using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTestGenericAlgorithm
{
    public class GeneticAlgorithm
    {
        private int totalNetworksPerGeneration;
        private int totalBreedingNetworks;
        public NeuralNetwork[] Networks { get; set; }
        public int CurrentGeneration { get; set; }

        private double learningRate = 0.1;

        private int inputLayerAmount;
        private int hiddenLayerAmount;
        private int outputLayerAmount;

        Random rand = new Random();

        public GeneticAlgorithm(int totalNetworksPerGeneration, int totalBreedingNetworks, double learningRate)
        {
            this.totalNetworksPerGeneration = totalNetworksPerGeneration;
            this.totalBreedingNetworks = totalBreedingNetworks;
            this.learningRate = learningRate;
        }

        public int GetTotalNetworkAmount()
        {
            return totalNetworksPerGeneration;
        }

        public double GetTotalNetworkBreedingAmount()
        {
            return totalBreedingNetworks;
        }

        public void SetupNetworks(int inputLayerAmount, int hiddenLayerAmount, int outputLayerAmount)
        {
            this.inputLayerAmount = inputLayerAmount;
            this.hiddenLayerAmount = hiddenLayerAmount;
            this.outputLayerAmount = outputLayerAmount;

            Random rand = new Random();

            Networks = new NeuralNetwork[GetTotalNetworkAmount()];
            for (int i = 0; i < GetTotalNetworkAmount(); ++i)
            {
                Networks[i] = new NeuralNetwork(inputLayerAmount, hiddenLayerAmount, outputLayerAmount, rand);
            }
        }

        public void SetNetworkInputValues(double[] values)
        {
            foreach (NeuralNetwork network in Networks)
            {
                network.SetInputValues(values);
            }
        }

        public void PropagateAllNetworks()
        {
            foreach (NeuralNetwork network in Networks)
            {
                network.PropagateForward();
            }
        }

        public void CalculateAllNetworkErrors(double[] expectedResult)
        {
            foreach (NeuralNetwork network in Networks)
            {
                network.CalculateError(expectedResult);
            }
        }

        public void SortByFitness()
        {
            double[] fitness = new double[Networks.Length];
            for (int i = 0; i < Networks.Length; ++i)
            {
                fitness[i] = Networks[i].GetFitnesse();
            }

            NeuralNetwork[] sortedNetworks = new NeuralNetwork[Networks.Length];

            int currentHighestFitnessPosition = 0;
            for(int i = 0; i < fitness.Length; ++i)
            {
                double currentHighestFitness = 0;
                for(int j = 0; j < fitness.Length; ++j)
                {
                    if(fitness[j] != -1 && fitness[j] > currentHighestFitness)
                    {
                        currentHighestFitness = fitness[j];
                        fitness[j] = -1;
                        currentHighestFitnessPosition = j;
                    }
                }
                sortedNetworks[i] = Networks[currentHighestFitnessPosition];
            }
            Networks = sortedNetworks;
        }

        public void RebreedTopNetworks()
        {
            for (int i = totalBreedingNetworks; i < totalNetworksPerGeneration; ++i)
            {
                Networks[i] = null;
            }

            for (int i = totalBreedingNetworks; i < totalNetworksPerGeneration; ++i)
            {
                NeuralNetwork randomTopFitnessNetwork = Networks[rand.Next(0, totalBreedingNetworks)];
                List<double[]> modifiedInputWeights = ModifyWeights(randomTopFitnessNetwork.GetInputWeights(), randomTopFitnessNetwork.GetOutputErrors());
                List<double[]> modifiedHiddenWeights = ModifyWeights(randomTopFitnessNetwork.GetHiddenWeights(), randomTopFitnessNetwork.GetOutputErrors());
                List<double[]> modifiedOutputWeights = ModifyWeights(randomTopFitnessNetwork.GetOutputWeights(), randomTopFitnessNetwork.GetOutputErrors());

                NeuralNetwork network = new NeuralNetwork(inputLayerAmount, hiddenLayerAmount, outputLayerAmount);

                network.SetInputWeights(modifiedInputWeights);
                network.SetHiddenWeights(modifiedHiddenWeights);
                network.SetOutputWeights(modifiedOutputWeights);

                Networks[i] = network;
            }
            ++CurrentGeneration;
        }

        private List<double[]> ModifyWeights(List<double[]> weightsList, double[] errors)
        {
            double totalError = 0.0;
            foreach (double error in errors)
            {
                totalError += error;
            }

            List<double[]> modifiedWeightsList = new List<double[]>();
            foreach (double[] weights in weightsList)
            {
                double[] modifiedWeights = new double[weights.Length];
                for (int i = 0; i < weights.Length; ++i)
                {
                    bool negateModification = rand.Next(0, 2) == 0;

                    double modifier = 1 / (1 + totalError);
                    if (negateModification)
                    {
                        modifier *= -1;
                    }

                    modifiedWeights[i] = weights[i] + (modifier * learningRate);
                }
                modifiedWeightsList.Add(modifiedWeights);
            }

            return modifiedWeightsList;
        }

        public void PerformAutoBreed(int iterations, List<double[]> dataSets, List<double[]> expectedResults)
        {
            for (int i = 0; i < iterations; ++i)
            {
                SortByFitness();
                if (i != 0)
                {
                    RebreedTopNetworks();
                }

                int currentDataSetIndex = rand.Next(0, dataSets.Count);
                double[] currentDataSet = dataSets[currentDataSetIndex];
                double[] currentExpectedResults = expectedResults[currentDataSetIndex];
                SetNetworkInputValues(currentDataSet);
                PropagateAllNetworks();
                CalculateAllNetworkErrors(currentExpectedResults);
            }
            SortByFitness();
        }
    }
}
