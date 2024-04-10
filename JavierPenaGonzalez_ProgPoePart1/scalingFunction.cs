using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavierPenaGonzalez_ProgPoePart1
{
    class ScalingFunction
    {
        private string[] ingredientNames;
        private double[] ingredientQuantities;
        private string[] ingredientUnits;
        private string[] steps;

        public ScalingFunction(string[] ingredientNames, double[] ingredientQuantities, string[] ingredientUnits, string[] steps)
        {
            this.ingredientNames = ingredientNames;
            this.ingredientQuantities = ingredientQuantities;
            this.ingredientUnits = ingredientUnits;
            this.steps = steps;
        }

        public void ScaleRecipe()
        {
            Console.WriteLine("\nChoose a scaling factor:");
            Console.WriteLine("1. 0.5x");
            Console.WriteLine("2. 2x");
            Console.WriteLine("3. 3x");
            Console.Write("Enter your choice (1, 2, or 3): ");
            int choice = int.Parse(Console.ReadLine());

            double scaleFactor = 1.0;
            switch (choice)
            {
                case 1:
                    scaleFactor = 0.5;
                    break;
                case 2:
                    scaleFactor = 2.0;
                    break;
                case 3:
                    scaleFactor = 3.0;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to 1x scaling.");
                    break;
            }

            for (int i = 0; i < ingredientQuantities.Length; i++)
            {
                ingredientQuantities[i] *= scaleFactor;
            }
        }

        public void DisplayRecipe()
        {
            Console.WriteLine("\nRecipe Details:");
            Console.WriteLine("\nIngredients:");
            for (int i = 0; i < ingredientNames.Length; i++)
            {
                Console.WriteLine($"{ingredientQuantities[i]} {ingredientUnits[i]} of {ingredientNames[i]}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }
    }
}