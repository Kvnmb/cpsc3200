// Kevin Bui
// October 21, 2023
// Last Revised: October 23, 2023
// Plan.cs
// IDE: Visual Studio


// Class Invariants: Plan object can start out empty and in valid state

// Plan Object stores the number of Formulas it has in a dynamic array that resizes when capacity is reached

// only add() function can call resize()

// Client passes in Formula objects to add or replace in Plan object

// Client must refer to the number of Formulas as 1 to x, not 0 to x when passing in parameters

// Dependency injection: client passes in an array of Formula objects to instantiate Plan's Formula array

// Deep copying through DeepCopy() method

// Exception handling added for functions when parameters are invalid, Apply and GetInput, etc. return 
// nulls instead for checking, part of class design and covers edge cases



// ExecutablePlan Class Invariants:

// base class methods are set to virtual for child class methods, dynamic binding

// currentStep starts at 1 for instantiation, all Formulas in array with element less
// than value currentStep have been completed

// A Formula being completed means they have called Apply() once

// Dependency injection of ExecutablePlan is calling the constructor injection in Parent Plan for instantiation

// Apply() must be called in order to advance currentStep, afterwards any Formulas previously applied can be called

// Replace(), Remove(), and Apply() are dependent on value of currentStep

// Plan can start out valid in empty state




using System;
using FormulaClass;

namespace PlanClass
{
    public class Plan
    {
        protected Formula [] formulaArr;
        protected uint numFormulas;
        protected uint maxSize = 10;
        private static readonly uint increase = 2;

        // Precondition: none
        public Plan()
        {
            numFormulas = 0;
            formulaArr = new Formula[maxSize];
        }
        // Postcondition: empty object

        // Precondition: client passes in array of formula objects for dependency injection and size of array 
        public Plan(Formula [] insert, uint size)
        {
            if(insert == null)
            {
                throw new ArgumentException("Array is null.");
            }

            if(size != (uint)insert.Length){
                throw new ArgumentException("Size is not equal to array length.");
            }

            numFormulas = 0;
            formulaArr = new Formula[maxSize];
            for(uint x = 0; x < size; x++){
                Add(insert[x]);
            }
        }
        // Postcondition: Plan object instantiated and nonempty, numFormulas altered by add()
        

        // Precondition: numFormulas = maxSize, add() is called
        private void Resize()
        {
            maxSize *= increase;
            Formula [] newArray = new Formula[maxSize];

            for(uint x = 0; x < numFormulas; x++){
                newArray[x] = formulaArr[x].Copy();
            }

            formulaArr = newArray;
        }
        // Postcondition: Formula array is copied into an array double the maxSize


        // Precondition: client has instantiated a Formula object to pass into function
        public void Add(Formula newFormula)
        {
            if(numFormulas == maxSize){
                Resize();
            }
            formulaArr[numFormulas] = newFormula.Copy();
            numFormulas++;
        }
        // Postcondition: copy of Formula object is passed into Plan's Formula array


        // Precondition: Formula array is not empty
        public virtual void Remove()
        {
            if(numFormulas == 0)
            {
                throw new InvalidOperationException("No Formulas in the array.");
            }
            numFormulas--;
        }
        // Postcondition: numFormulas decremented, or nothing happens if empty


        // Precondition: number is between 1 and numFormulas, Formula object instantiated
        public virtual void Replace(uint replaceNum, Formula replaceFormula)
        {
            if(numFormulas == 0)
            {
                throw new InvalidOperationException("No Formulas in the array.");
            }

            if((replaceNum - 1) >= numFormulas)
            {
                throw new ArgumentException("Invalid Formula number");
            }

            formulaArr[replaceNum - 1] = replaceFormula.Copy();
        }
        // Postcondition: Array object replaced with copy of parameter replaceFormula


        // Precondition: none
        public uint GetNumFormulas()
        {
            return numFormulas;
        }
        // Postcondition: none, returns numFormulas


        // Precondition: formulaNum is between 1 and numFormulas
        public int GetLevel(uint formulaNum)
        {
            if(numFormulas == 0)
            {
                throw new InvalidOperationException("No Formulas in the array.");
            }

            if((formulaNum - 1) >= numFormulas){
                throw new ArgumentException("Invalid Formula number");
            }

            return (int)formulaArr[formulaNum - 1].GetLevel();
        }
        // Postcondition: returns level of Formula object


        // Precondition: Formula array is non-empty
        public virtual Resource [] Apply(uint formulaNum)
        {
            if(numFormulas == 0 || (formulaNum - 1) >= numFormulas) return null;
    
            return formulaArr[formulaNum - 1].Apply();
        }
        // Postcondition: apply is called for specified object in array


        // Precondition: formulaNum is between 1 and numFormulas
        public Resource [] GetInput(uint formulaNum)
        {
            if(numFormulas == 0 || (formulaNum - 1) >= numFormulas) return null;

            return formulaArr[formulaNum - 1].GetInput();
        }
        // Postcondition: returns the inputs of the Formula object


        // Precondition: formulaNum is between 1 and numFormulas
        public Resource [] GetOutput(uint formulaNum)
        {
            if(numFormulas == 0 || (formulaNum - 1) >= numFormulas) return null;

            return formulaArr[formulaNum - 1].GetOutput();
        }
        // Postcondition: returns the outputs of the Formula object

        // Precondition: caller object instantiated
        public Plan DeepCopy()
        {
            Plan copy = new Plan();
            Copy(copy);

            return copy;
        }
        // Postcondition: returns a copy of the caller object

        // Precondition: none, called as helper function
        protected void Copy(Plan copy)
        {
            copy.numFormulas = this.numFormulas;
            copy.maxSize = this.maxSize;

            copy.formulaArr = new Formula[copy.maxSize];

            for(uint x = 0; x < copy.numFormulas; x++){
                copy.formulaArr[x] = this.formulaArr[x].Copy();
            }
        }
        // Postcondition: returns copy of Plan variables
    }

    public class ExecutablePlan : Plan
    {
        private uint currentStep;

        // Precondition: none
        public ExecutablePlan()
        {
            currentStep = 1;
        }
        // Postcondition: default Plan constructor called, currentStep initiated

        // Precondition: size is equal to length of insert, insert instantiated
        public ExecutablePlan(Formula [] insert, uint size) : base(insert, size)
        {
            currentStep = 1;
        }
        // Postcondition: ExecutablePlan object initiated through constructor injection

        // Precondition: none
        public uint QueryStep()
        {
            return currentStep;
        }
        // Postcondition: none

        // Precondition: formulaNum is less than currentStep
        public override Resource [] Apply(uint formulaNum)
        {
            if(formulaNum > currentStep) return null;

            Resource [] result = base.Apply(formulaNum);

            if(result != null && formulaNum == currentStep) currentStep++;

            return result;
        }
        // Postcondition: currentStep may be increased, Formula object may call apply() if valid

        // Precondition: Formula in the array has not been completed, replaceFormula is instantiated
        public override void Replace(uint replaceNum, Formula replaceFormula)
        {
            if(replaceNum < currentStep) return;

            base.Replace(replaceNum, replaceFormula);
        }
        // Postcondition: if valid, will replace Formula in the array, else will do nothing

        // Precondition: the current sequence of Formulas has not been completed
        public override void Remove()
        {
            if(base.GetNumFormulas() < currentStep) return;
            base.Remove();
        }
        // Postcondition: will do nothing if sequence is complete, else will remove last Formula

        // Precondition: none
        public ExecutablePlan Clone()
        {
            ExecutablePlan copy = new ExecutablePlan();
            Copy(copy);
            copy.currentStep = this.currentStep;

            return copy;
        }
        // Postcondition: none

    }

}



// Implementation Invariants: The client will pass in Formula objects to instantiate array, done
// through add function or constructors

// numFormulas will always be equal to or less than maxSize, when array is full the next call to add() will
// allocate a new array

// Plan's apply function runs the Formula apply() function on specified object in the Formula array

// Class stays in valid states of empty array or non-empty

// remove() function does not delete or modify array, only decrements the numFormulas variable

// input and output functions return copies of the real arrays

// functions will return values of specified object in array

// resize() function doubles the maxSize of the array



// ExecutablePlan Implementation Invariants:

// virtual methods allow child class to add preconditions before calling parent function

// currentStep will never decrease, only stay the same or increase

// Formula objects that are in position of array greater than or equal to currentStep cannot be modified,
// array must be applied in order

// bounds checking to ensure that function calls will never be beyond currentStep

// When the entire array is complete, remove will do nothing

// incrementing currentStep is only done by calling a successful Apply() function