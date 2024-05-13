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
        private ArrayList ingredientNames;
        private ArrayList ingredientQuantities;
        private ArrayList ingredientUnits;
        private ArrayList steps;

        public ScalingFunction(ArrayList ingredientNames, ArrayList ingredientQuantities, ArrayList ingredientUnits, ArrayList steps)
        {
            this.ingredientNames = ingredientNames;
            this.ingredientQuantities = ingredientQuantities;
            this.ingredientUnits = ingredientUnits;
            this.steps = steps;
        }

        public void ScaleIngredients(double scaleFactor)
        {
            for (int i = 0; i < ingredientQuantities.Count; i++)
            {
                double quantity = (double)ingredientQuantities[i]; // Cast to double
                ingredientQuantities[i] = quantity * scaleFactor;
            }
        }

        public void DisplayIngredients()
        {
            Console.WriteLine("\nRecipe Details:");
            Console.WriteLine("\nIngredients:");
            for (int i = 0; i < ingredientNames.Count; i++)
            {
                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }
    }
}
