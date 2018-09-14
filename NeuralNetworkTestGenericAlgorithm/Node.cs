using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTestGenericAlgorithm
{
    public class Node
    {
        public double InputValue { get; set; }
        public double[] Weights { get; set; }
        public double Error { get; set; }
        public double[] Results { get; set; }

        public Node(double inputValue, double[] weights)
        {
            this.InputValue = inputValue;
            this.Weights = weights;
        }

        public void CalculateWeightetInputResults()
        {
            Results = new double[Weights.Length];
            for(int i = 0; i < Weights.Length; ++i)
            {
                Results[i] = InputValue * Weights[i];
            }
        }
    }
}
