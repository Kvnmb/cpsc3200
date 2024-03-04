// Kevin Bui
// October 5, 2023
// Last Revised: November 7, 2023
// Formula.cpp
// IDE: Visual Studio Code
// Implementation file for Formula.h

#include <iostream>
#include "Formula.h"
#include <random>

using namespace std;


Resource::Resource()
{
    name = "";
    quantity = 0;
}

Resource::Resource(string nameInit, int quantityInit)
{
    name = nameInit;
    quantity = quantityInit;
}


Formula::Formula()
{
    inputs = nullptr;
    outputs = nullptr;

    inputSize = 0;
    outputSize = 0;
}


Formula::Formula(Resource inputInit[], unsigned int numInputs, Resource outputInit[], unsigned int numOutputs)
{
    inputs = new Resource[numInputs];
    outputs = new Resource[numOutputs];

    inputSize = numInputs;
    outputSize = numOutputs;

    for(unsigned int x = 0; x < numInputs; x++){
        inputs[x].name = inputInit[x].name;
        inputs[x].quantity = inputInit[x].quantity;
    }

    for(unsigned int y = 0; y < numOutputs; y++){
        outputs[y].name = outputInit[y].name;
        outputs[y].quantity = outputInit[y].quantity;
    }
}

Formula::Formula(const Formula& src)
{
    copy(src);
}

void Formula::copy(const Formula& src)
{
    currentLevel = src.currentLevel;
    failPercentage = src.failPercentage;
    reducedPercentage = src.reducedPercentage;
    expectedPercentage = src.expectedPercentage;
    bonusPercentage = src.bonusPercentage;
    inputSize = src.inputSize;
    outputSize = src.outputSize;
    inputs = new Resource[inputSize];
    outputs = new Resource[outputSize];

    for(unsigned int x = 0; x < inputSize; x++){
        inputs[x].name = src.inputs[x].name;
        inputs[x].quantity = src.inputs[x].quantity;
    }

    for(unsigned int x = 0; x < outputSize; x++){
        outputs[x].name = src.outputs[x].name;
        outputs[x].quantity = src.outputs[x].quantity;
    }
}

Formula& Formula::operator=(const Formula& src)
{ 
    if (this == &src) return *this; // guard clause
    delete[] inputs;
    delete[] outputs;

    copy(src);
return *this;
}

void Formula::swap(Formula& lhs, Formula& rhs)
{
    Formula temp;

    temp.currentLevel = lhs.currentLevel;
    temp.failPercentage = lhs.failPercentage;
    temp.reducedPercentage = lhs.reducedPercentage;
    temp.expectedPercentage = lhs.expectedPercentage;
    temp.bonusPercentage = lhs.bonusPercentage;
    temp.inputs = lhs.inputs;
    temp.outputs = lhs.outputs;
    temp.inputSize = lhs.inputSize;
    temp.outputSize = lhs.outputSize;

    lhs.currentLevel = rhs.currentLevel;
    lhs.failPercentage = rhs.failPercentage;
    lhs.reducedPercentage = rhs.reducedPercentage;
    lhs.expectedPercentage = rhs.expectedPercentage;
    lhs.bonusPercentage = rhs.bonusPercentage;
    lhs.inputs = rhs.inputs;
    lhs.outputs = rhs.outputs;
    lhs.inputSize = rhs.inputSize;
    lhs.outputSize = rhs.outputSize;

    rhs.currentLevel = temp.currentLevel;
    rhs.failPercentage = temp.failPercentage;
    rhs.reducedPercentage = temp.reducedPercentage;
    rhs.expectedPercentage = temp.expectedPercentage;
    rhs.bonusPercentage = temp.bonusPercentage;
    rhs.inputs = temp.inputs;
    rhs.outputs = temp.outputs;
    rhs.inputSize = temp.inputSize;
    rhs.outputSize = temp.outputSize;
}

Formula::Formula(Formula&& src)
{
    swap(*this, src);
    src.currentLevel = 0;
    src.failPercentage = 0;
    src.reducedPercentage = 0;
    src.expectedPercentage = 0;
    src.bonusPercentage = 0;
    src.inputs = nullptr;
    src.outputs = nullptr;
    src.inputSize = 0;
    src.outputSize = 0;
}

Formula& Formula::operator=(Formula&& src)
{
    swap(*this, src);
    return *this;
}


Formula::~Formula()
{
    delete [] inputs;
    delete [] outputs;

    inputs = nullptr;
    outputs = nullptr;
}

Resource* Formula::apply()
{
    double chance = double(rand()) / RAND_MAX;

    if(chanceFailed(chance))
        return failedProcess();
    if(chanceReduced(chance))
        return reducedOutput();
    if(chanceExpected(chance))
        return expectedResult();
            
    return bonusResult();
}

bool Formula::chanceFailed(double chance)
        {
            return chance <= failPercentage;
        }


bool Formula::chanceReduced(double chance)
        {
            return chance <= (reducedPercentage + failPercentage);
        }

bool Formula::chanceExpected(double chance)
    {
        return chance <= (expectedPercentage + reducedPercentage + failPercentage);
    }


Resource* Formula::failedProcess()
        {
            Resource* result = new Resource[outputSize];

            for(unsigned int x = 0; x < outputSize; x++){
                result[x].name = outputs[x].name;
                result[x].quantity = 0;
            }

            return result;
        }


Resource* Formula::reducedOutput()
{
    Resource* result = new Resource[outputSize];

    for(unsigned int x = 0; x < outputSize; x++){
        result[x].name = outputs[x].name;
        result[x].quantity = outputs[x].quantity * reduceQuantity;
    }
    
    return result;
}


Resource* Formula::expectedResult()
{
    Resource* result = new Resource[outputSize];

    for(unsigned int x = 0; x < outputSize; x++){
        result[x].name = outputs[x].name;
        result[x].quantity = outputs[x].quantity;
    }
    raiseLevel();
    return result;
}


Resource* Formula::bonusResult()
{
    Resource* result = new Resource[outputSize];

    for(unsigned int x = 0; x < outputSize; x++){
        result[x].name = outputs[x].name;
        result[x].quantity = outputs[x].quantity * bonusQuantity;
    }
    raiseLevel();
    raiseLevel();
    return result;
}


Resource* Formula::getInput() const
{
    Resource* copy = new Resource[inputSize];

    for(unsigned int x = 0; x < inputSize; x++){
        copy[x].name = inputs[x].name;
        copy[x].quantity = inputs[x].quantity;
    }
    return copy;
}

Resource* Formula::getOutput() const
{
    Resource* copy = new Resource[outputSize];

    for(unsigned int x = 0; x < outputSize; x++){
        copy[x].name = outputs[x].name;
        copy[x].quantity = outputs[x].quantity;
    }
    return copy;
}


unsigned int Formula::getLevel() const
{
    return currentLevel;
}


void Formula::raiseLevel()
{   
    if(currentLevel < maxLevel){
        currentLevel++;
        if(failPercentage != 0){
            failPercentage -= proficiencyIncrement;
            expectedPercentage += proficiencyIncrement; 
        }
        if (reducedPercentage != 0){
            reducedPercentage -= proficiencyIncrement;
            expectedPercentage += proficiencyIncrement;
        }
        if (expectedPercentage != 0){
            expectedPercentage -= proficiencyIncrement;
            bonusPercentage += proficiencyIncrement;
        }
    }
}  

unsigned int Formula::getInputSize() const
{
    return inputSize;
}

unsigned int Formula::getOutputSize() const
{
    return outputSize;
}

bool Formula::operator==(const Formula& src) const
{
    if (this == &src) return true;

    if (currentLevel == src.currentLevel
    && failPercentage == src.failPercentage
    && reducedPercentage == src.reducedPercentage
    && expectedPercentage == src.expectedPercentage
    && bonusPercentage == src.bonusPercentage
    && inputSize == src.inputSize
    && outputSize == src.outputSize){

        for (unsigned int x; x < inputSize; x++){
            if (inputs[x].name == src.inputs[x].name
            && inputs[x].quantity == src.inputs[x].quantity){
                continue;
            }
            return false;
        }

        for (unsigned int x; x < outputSize; x++){
            if (outputs[x].name == src.outputs[x].name
            && outputs[x].quantity == src.outputs[x].quantity){
                continue;
            }
            return false;
        }

        return true;
    }

    return false;
}

bool Formula::operator!=(const Formula& src) const
{
    if (this == &src) return false;

    if (currentLevel != src.currentLevel
    || failPercentage != src.failPercentage
    || reducedPercentage != src.reducedPercentage
    || expectedPercentage != src.expectedPercentage
    || bonusPercentage != src.bonusPercentage
    || inputSize != src.inputSize
    || outputSize != src.outputSize) return true;

    for (unsigned int x; x < inputSize; x++){
        if (inputs[x].name != src.inputs[x].name
        || inputs[x].quantity != src.inputs[x].quantity){
            return true;
        }
    }

    for (unsigned int x; x < outputSize; x++){
        if (outputs[x].name != src.outputs[x].name
        || outputs[x].quantity != src.outputs[x].quantity){
            return true;
        }
    }

    return false;
}

// Implementation Invariants: The client will pass in arrays of input and output resources as well as
// the array sizes

// Input, output, and apply functions return a pointer to array, client responsible for deallocation

// The sum of the output percentages must always be 1.0

// input and output functions return a Resource array

// Apply() function uses random double generation

// Formula object holds instantiated Resource for lifetime

// Resource quantities are nonnegative

// Input and Output queries as well as Apply() can be called infinite times

// input and output functions return copies of the real arrays