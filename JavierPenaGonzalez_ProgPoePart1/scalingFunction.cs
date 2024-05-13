using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavierPenaGonzalez_ProgPoePart1
{
    class ScalingFunction
    {
        private List<double> originalQuantities;
        private List<double> scaledQuantities;

        public ScalingFunction(List<double> originalQuantities)
        {
            this.originalQuantities = originalQuantities;
            this.scaledQuantities = new List<double>(originalQuantities); //creates a copy of the list of stored ingredients
        }

        public void ScaleIngredients(double scaleFactor)
        {
            for (int i = 0; i < originalQuantities.Count; i++)
            {
                double quantity = originalQuantities[i];
                scaledQuantities[i] = quantity * scaleFactor;
            }
        }

        public void RevertScaling()
        {
            scaledQuantities.Clear();
            scaledQuantities.AddRange(originalQuantities); //takes the list copy
        }

        public void DisplayIngredients(List<string> ingredientNames, List<string> ingredientUnits)
        {
            for (int i = 0; i < scaledQuantities.Count; i++)
            {
                Console.WriteLine($"{scaledQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }
        }
    }
}
