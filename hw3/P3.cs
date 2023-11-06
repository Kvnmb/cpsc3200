// Kevin Bui
// September 25, 2023
// Last Revised: October 23, 2023
// P3.cs
// IDE: Visual Studio

using System;
using FormulaClass;
using PlanClass;


namespace P3{
    
    public class P3 {
        static void Main(string [] args){
            
            Plan [] collection = InitPlanArray();
            Console.WriteLine("Checking number of Formulas after instantiation:");
            PrintNumFormulas(collection);

            Console.WriteLine("Checking number of Formulas after Add:");
            AddFormula(collection);
            PrintNumFormulas(collection);

            Console.WriteLine("Checking inputs and outputs of Formulas before Replace:");
            PrintInputsAndOutputs(collection);

            Console.WriteLine("Checking number of Formulas after Replace:");
            ReplaceFormula(collection);
            PrintNumFormulas(collection);

            Console.WriteLine("Checking inputs and outputs of Formulas after Replace:");
            PrintInputsAndOutputs(collection);


            ApplyFormula(collection);

            Console.WriteLine("Checking number of Formulas after remove is called:");
            RemoveFormula(collection);
            PrintNumFormulas(collection);

            Console.WriteLine("Checking number of Formulas after remove is called again:");
            RemoveFormula(collection);
            PrintNumFormulas(collection);

        }

        static void PrintInputsAndOutputs(Plan[] collection)
        {
            Resource[] inputs;
            Resource[] outputs;

            for(int x = 0; x < collection.Length; x++)
            {
                inputs = collection[x].GetInput(1);
                outputs = collection[x].GetOutput(1);

                Console.WriteLine("Inputs of Object " + x + ":");
                for(int y = 0; y < inputs.Length; y++){
                    Console.WriteLine(inputs[y].quantity + " " + inputs[y].name);
                }

                Console.WriteLine("Outputs of Object " + x + ":");
                for(int z = 0; z < inputs.Length; z++){
                    Console.WriteLine(inputs[z].quantity + " " + inputs[z].name);
                }
            }
        }

        static void ApplyFormula(Plan[] collection)
        {
            for(int x = 0; x < collection.Length; x++)
            {
                collection[x].Apply(1);
                collection[x].Apply(1);
                collection[x].Apply(1);
            }
        }

        static void ReplaceFormula(Plan[] collection)
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("water", 2);
            input[1] = new Resource("seed", 1);

            Resource[] output = new Resource[1];
            output[0] = new Resource("tree", 1);

            Formula formula = new Formula(input, 2, output, 1);

            for(int x = 0; x < collection.Length; x++){
                collection[x].Replace(1, formula);
            }
        }

        static void RemoveFormula(Plan[] collection)
        {
            for(int x = 0; x < collection.Length; x++){
                collection[x].Remove();
            }
        }

        static void AddFormula(Plan[] collection)
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("sugar", 2);
            input[1] = new Resource("cacao", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("chocolate", 1);

            Formula formula = new Formula(input, 2, output, 1);

            for(int x = 0; x < collection.Length; x++){
                collection[x].Add(formula);
            }
        }

        static void PrintNumFormulas(Plan [] collection)
        {
            for(int x = 0; x < collection.Length; x++){
                Console.WriteLine("Number of Formulas in Object " + x + ": " + collection[x].GetNumFormulas());
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        // Initializes a Plan Array with a heterogeneous collection
        static Plan[] InitPlanArray()
        {
            Plan[] array = new Plan[3];

            Formula [] formulaOne = new Formula[1];
            Formula [] formulaTwo = new Formula[2];
            Formula [] formulaThree = new Formula[3];
            
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);
            

            formulaOne[0] = new Formula(input, 2, output, 1);
            formulaTwo[0] = new Formula(input, 2, output, 1);
            formulaThree[0] = new Formula(input, 2, output, 1);
            

            input = new Resource[1];
            input[0] = new Resource("rock", 2);

            output = new Resource[2];
            output[0] = new Resource("iron", 4);
            output[1] = new Resource("coal", 2);

            formulaTwo[1] = new Formula(input, 1, output, 2);
            formulaThree[1] = new Formula(input, 1, output, 2);


            input = new Resource[1];
            input[0] = new Resource("water", 1);

            output = new Resource[2];
            output[0] = new Resource("hydrogen", 2);
            output[1] = new Resource("oxygen", 1);

            formulaThree[2] = new Formula(input, 1, output, 2);

            array[0] = new ExecutablePlan(formulaOne, 1);
            array[1] = new Plan(formulaTwo, 2);
            array[2] = new ExecutablePlan(formulaThree, 3);

            return array;
        }
    }
}

