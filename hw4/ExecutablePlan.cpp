// Kevin Bui
// November 4, 2023
// Last Revised: November 7, 2023
// ExecutablePlan.cpp
// IDE: Visual Studio Code
// Implementation file for ExecutablePlan.h

#include <iostream>
#include "ExecutablePlan.h"
#include <memory>

using namespace std;


ExecutablePlan::ExecutablePlan()
{
    currentStep = 1;
}


ExecutablePlan::ExecutablePlan(Formula* insert, unsigned int size) : Plan(insert, size)
{
    currentStep = 1;
}

unsigned int ExecutablePlan::queryStep()
{
    return currentStep;
}

Resource* ExecutablePlan::apply(unsigned int formulaNum)
{
    if(formulaNum > currentStep || formulaNum == 0) return nullptr;

    Resource* result = Plan::apply(formulaNum);

    if(result && formulaNum == currentStep) currentStep++;

    return result;
}

void ExecutablePlan::replace(unsigned int replaceNum, const Formula& replaceFormula)
{
    if(replaceNum < currentStep) return;

    Plan::replace(replaceNum, replaceFormula);
}

void ExecutablePlan::remove()
{
    if(Plan::getNumFormulas() < currentStep) return;
    Plan::remove();
}


ExecutablePlan::ExecutablePlan(const ExecutablePlan& src) : Plan(src)
{
    currentStep = src.currentStep;
}


ExecutablePlan& ExecutablePlan::operator=(const ExecutablePlan& src)
{
    if(this == &src) return *this;

    Plan::operator=(src);
    currentStep = src.currentStep;
    return *this;
}

ExecutablePlan::ExecutablePlan(ExecutablePlan&& src) : Plan(src)
{
    src.currentStep = 0;
}

ExecutablePlan& ExecutablePlan::operator=(ExecutablePlan&& src)
{
    Plan::operator=(src);
    return *this;
}



bool ExecutablePlan::operator==(const ExecutablePlan& src) const
{
    if (this == &src) return true;

    if (currentStep == src.currentStep) return Plan::operator==(src);

    return false;
}



bool ExecutablePlan::operator!=(const ExecutablePlan& src) const
{
    if (this == &src) return false;

    if (currentStep != src.currentStep) return true;

    return Plan::operator!=(src);
}


unique_ptr<Stockpile> ExecutablePlan::apply(unique_ptr<Stockpile> ptr)
{
    Resource* inputs = getInput(currentStep);
    int inputSize = getInputSize(currentStep);

    for (int x = 0; x < inputSize; x++){
        if (ptr->getQuantity(inputs[x].name) < inputs[x].quantity){
            delete [] inputs;
            return ptr;
        }
    }



    for (int x = 0; x < inputSize; x++) ptr->decrease(inputs[x].name, inputs[x].quantity);

    int outputSize = getOutputSize(currentStep);
    Resource* result = apply(currentStep);
    

    for (int x = 0; x < outputSize; x++) ptr->increase(result[x].name, result[x].quantity);
    
    delete [] result;

    unique_ptr<Stockpile> newPtr = make_unique<Stockpile>(*ptr);
    return newPtr;
}


ExecutablePlan ExecutablePlan::operator+(const ExecutablePlan& other)
{
    ExecutablePlan result(*this);
    
    for(unsigned int x = 0; x < other.numFormulas; x++){
        result.add(other.formulaArr[x]);
    }

    return result;
}



ExecutablePlan& ExecutablePlan::operator+=(const ExecutablePlan& other)
{
    for(unsigned int x = 0; x < other.numFormulas; x++){
        add(other.formulaArr[x]);
    }

    return *this;
}


ExecutablePlan& ExecutablePlan::operator--()
{
    remove();
    return *this;
}




// Implementation Invariants:

// virtual methods allow child class to add preconditions before calling parent function

// currentStep will never decrease, only stay the same or increase

// Formula objects that are in position of array greater than or equal to currentStep cannot be modified,
// array must be applied in order

// bounds checking to ensure that function calls will never be beyond currentStep

// When the entire array is complete, remove will do nothing

// incrementing currentStep is only done by calling a successful Apply() function

// unique_ptr apply function will use move semantics to transfer ownership

// overloaded + and += operators call the add function to add the formulas of plan object together, ease of access

// operator-- calls remove with simpler syntax