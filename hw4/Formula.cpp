// Kevin Bui
// October 5, 2023
// Last Revised: October 8, 2023
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


Formula::Formula(Resource inputInit[] = nullptr, unsigned int numInputs = 0, Resource outputInit[] = nullptr, unsigned int numOutputs = 0)
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