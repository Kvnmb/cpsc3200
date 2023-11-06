// Kevin Bui
// September 25, 2023
// Last Revised: October 21, 2023
// Formula.cs
// IDE: Visual Studio


// Class Invariants: Resource arrays are instantiated after Formula constructor is run and input, output,
// and max level will stay the same throughout object lifetime

// Resource variables holds the data while Formula class manages operations

// Percentages and currentLevel (RaiseLevel()) are subject to change when ExpectedResult() and BonusResult() are called

// Deep copy semantics supported 

// Resource is a public class that client is able to use to instantiate Formulas

// Copy method implemented for deep copying

using System;
namespace FormulaClass
{
    public class Resource
    {
        public string name;
        public uint quantity;

        // Precondition: none
        public Resource()
        {
            name = "";
            quantity = 0;
        }
        // Postcondition: values are initialized to empty


        // Precondition: none
        public Resource(string nameInit, uint quantityInit)
        {
            name = nameInit;
            quantity = quantityInit;
        }
        // Postcondition: values are initialized
    }
    public class Formula
    {
        private uint currentLevel = 0;
        private double failPercentage = 0.2;
        private double reducedPercentage = 0.45;
        private double expectedPercentage = 0.3;
        private double bonusPercentage = 0.05;
        private readonly uint maxLevel = 5;

        private readonly double proficiencyIncrement = 0.05;
        private readonly double reduceQuantity = 0.45;
        private readonly double bonusQuantity = 1.5;

        private readonly Resource[] inputs;
        private readonly Resource[] outputs;
        private readonly uint inputSize;
        private readonly uint outputSize;

        private readonly Random rnd = new(0);

        // Precondition: none
        public Formula()
        {
            inputs = null;
            outputs = null;
            inputSize = 0;
            outputSize = 0;
        }
        // Postcondition: empty object

        // Precondition: client has passed two nonempty Resource arrays with correct input/output size
        public Formula(Resource[] inputInit = null, uint numInputs = 0, Resource[] outputInit = null, uint numOutputs = 0)
        {
            inputs = new Resource[numInputs];
            outputs = new Resource[numOutputs];

            inputSize = numInputs;
            outputSize = numOutputs;

            for (uint x = 0; x < numInputs; x++)
            {
                inputs[x] = new Resource(inputInit[x].name, inputInit[x].quantity);
            }

            for (uint y = 0; y < numOutputs; y++)
            {
                outputs[y] = new Resource(outputInit[y].name, outputInit[y].quantity);
            }
        }
        // Postcondition: object has been instantiated

        // Precondition: object has been instantiated 
        public Resource[] Apply()
        {
            if (inputs == null || outputs == null) return null;

            double chance = rnd.NextDouble();

            if (ChanceFailed(chance))
                return FailedProcess();
            if (ChanceReduced(chance))
                return ReducedOutput();
            if (ChanceExpected(chance))
                return ExpectedResult();

            return BonusResult();
        }
        // Postcondition: currentLevel may or may not have been incremented, percentages may or may not have been incremented


        // Precondition: None
        bool ChanceFailed(double chance)
        {
            return chance <= failPercentage;
        }
        // Postcondition: None

        // Precondition: None
        bool ChanceReduced(double chance)
        {
            return chance <= (reducedPercentage + failPercentage);
        }
        // Postcondition: None

        // Precondition: None
        bool ChanceExpected(double chance)
        {
            return chance <= (expectedPercentage + reducedPercentage + failPercentage);
        }
        // Postcondition: None

        // Precondition: Resource [] array has been declared to receive return
        Resource[] FailedProcess()
        {
            Resource[] result = new Resource[outputSize];

            for (uint count = 0; count < outputSize; count++)
            {
                result[count] = new Resource(outputs[count].name, 0);
            }

            return result;
        }
        // Postcondition: Resource [] array will be returned

        // Precondition: Resource [] array has been declared to receive return
        Resource[] ReducedOutput()
        {
            Resource[] result = new Resource[outputSize];

            for (uint count = 0; count < outputSize; count++)
            {
                result[count] = new Resource(outputs[count].name, (uint)Math.Round(reduceQuantity * outputs[count].quantity));
            }

            return result;
        }
        // Postcondition: Resource [] array will be returned

        // Precondition: Resource [] array has been declared to receive return
        Resource[] ExpectedResult()
        {
            Resource[] result = new Resource[outputSize];

            for (uint count = 0; count < outputSize; count++)
            {
                result[count] = new Resource(outputs[count].name, outputs[count].quantity);
            }
            RaiseLevel();

            return result;
        }
        // Postcondition: Resource [] array will be returned

        // Precondition: Resource [] array has been declared to receive return
        Resource[] BonusResult()
        {
            Resource[] result = new Resource[outputSize];

            for (uint count = 0; count < outputSize; count++)
            {
                result[count] = new Resource(outputs[count].name, (uint)Math.Round(bonusQuantity * outputs[count].quantity));
            }
            RaiseLevel();
            RaiseLevel();

            return result;
        }
        // Postcondition: Resource [] array will be returned

        // Precondition: Resource [] array has been declared
        public Resource[] GetInput()
        {
            Resource[] copy = new Resource[inputSize];

            for (uint x = 0; x < inputSize; x++)
            {
                copy[x] = new Resource(inputs[x].name, inputs[x].quantity);
            }
            return copy;
        }
        // Postcondition: Resource [] array will be returned

        // Precondition: Resource [] array has been declared
        public Resource[] GetOutput()
        {
            Resource[] copy = new Resource[outputSize];

            for (uint x = 0; x < outputSize; x++)
            {
                copy[x] = new Resource(outputs[x].name, outputs[x].quantity);
            }
            return copy;
        }
        // Postcondition: Resource [] array will be returned

        // Precondition: Object instantiated
        public uint GetLevel()
        {
            return currentLevel;
        }
        // Postcondition: value 0 or greater returned

        // Precondition: called by Apply() helper functions
        void RaiseLevel()
        {
            if (currentLevel < maxLevel)
            {
                currentLevel++;
                if (failPercentage != 0)
                {
                    failPercentage -= proficiencyIncrement;
                    expectedPercentage += proficiencyIncrement;
                }
                if (reducedPercentage != 0)
                {
                    reducedPercentage -= proficiencyIncrement;
                    expectedPercentage += proficiencyIncrement;
                }
                if (expectedPercentage != 0)
                {
                    expectedPercentage -= proficiencyIncrement;
                    bonusPercentage += proficiencyIncrement;
                }
            }
        }
        // Postcondition: Percentages and level may be altered

        // Precondition: none
        public Formula Copy()
        {
            Formula copy = new Formula(inputs, inputSize, outputs, outputSize);

            copy.currentLevel = this.currentLevel;
            copy.failPercentage = this.failPercentage;
            copy.reducedPercentage = this.reducedPercentage;
            copy.expectedPercentage = this.expectedPercentage;
            copy.bonusPercentage = this.bonusPercentage;

            return copy;
        }
        // Postcondition: returns a copy of Formula object
    }
}

// Implementation Invariants: The client will pass in arrays of input and output resources as well as
// the array sizes

// Input, output, and apply functions return a Resource array

// The sum of the output percentages must always be 1.0

// Apply() function uses random double generation

// Formula object holds instantiated Resource for lifetime

// Resource quantities are nonnegative

// Input and Output queries as well as Apply() can be called infinite times

// input and output functions return copies of the real arrays