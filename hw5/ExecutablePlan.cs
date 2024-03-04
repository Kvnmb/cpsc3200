// Kevin Bui
// November 25, 2023
// Last Revised: November 28, 2023
// ExecutablePlan.cs
// IDE: Visual Studio



// Class Invariants:

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
using PlanClass;

namespace ExecutablePlanClass
{
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



// Implementation Invariants:

// virtual methods allow child class to add preconditions before calling parent function

// currentStep will never decrease, only stay the same or increase

// Formula objects that are in position of array greater than or equal to currentStep cannot be modified,
// array must be applied in order

// bounds checking to ensure that function calls will never be beyond currentStep

// When the entire array is complete, remove will do nothing

// incrementing currentStep is only done by calling a successful Apply() function