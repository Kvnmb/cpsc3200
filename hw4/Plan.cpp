// Kevin Bui
// October 5, 2023
// Last Revised: October 9, 2023
// Plan.cpp
// IDE: Visual Studio Code
// Implementation file for Plan.h

#include <iostream>
#include "Plan.h"

using namespace std;

Plan::Plan()
{
    numFormulas = 0;
    formulaArr = new Formula[maxSize];
}

Plan::~Plan()
{ 
    delete [] formulaArr;
}


Plan::Plan(Formula array[], unsigned int size)
{
    numFormulas = 0;
    formulaArr = new Formula[maxSize];
    for(unsigned int x = 0; x < size; x++){
        add(array[x]);
    }
}

void Plan::copy(const Plan& src)
{
    maxSize = src.maxSize;
    numFormulas= src.numFormulas;
    formulaArr = new Formula[maxSize];
    for(unsigned int x = 0; x < numFormulas; x++){
        formulaArr[x] = src.formulaArr[x];
    }
}
Plan::Plan(const Plan& src)
{ 
    copy(src);
}

Plan& Plan::operator=(const Plan& src)
{ 
    if (this == &src) return *this; // guard clause
    delete[] formulaArr;
    copy(src);
    return *this;
}

Plan::Plan(Plan&& src)
{
    swap(*this, src);

    src.maxSize = 0;
    src.numFormulas = 0;
    src.formulaArr = nullptr;
}

void Plan::swap(Plan &lhs, Plan &rhs)
{
    Plan temp;
    temp.formulaArr = lhs.formulaArr;
    temp.maxSize = lhs.maxSize;
    temp.numFormulas = lhs.numFormulas;

    lhs.formulaArr = rhs.formulaArr;
    lhs.maxSize = rhs.maxSize;
    lhs.numFormulas = rhs.numFormulas;

    rhs.formulaArr = temp.formulaArr;
    rhs.maxSize = temp.maxSize;
    rhs.numFormulas = temp.numFormulas;
}

Plan& Plan::operator=(Plan&& src)
{
    swap(*this, src);
    return *this;
}


void Plan::add(const Formula& newFormula)
{
    if(numFormulas == maxSize){
        resize();
    }
    formulaArr[numFormulas] = newFormula;
    numFormulas++;

}

void Plan::resize()
{
    maxSize = maxSize * increase;
    Formula* newArray = new Formula[maxSize];
    for(unsigned int x = 0; x < numFormulas; x++){
        newArray[x] = formulaArr[x];
    }
    delete [] formulaArr;
    formulaArr = newArray;
    newArray = nullptr;
}

void Plan::remove()
{
    if(numFormulas == 0) return;
    numFormulas--;
}

void Plan::replace(unsigned int replaceNum, const Formula& replaceFormula)
{
    if(numFormulas == 0 || (replaceNum - 1) >= numFormulas) return;

    formulaArr[replaceNum - 1] = replaceFormula;
}

unsigned int Plan::getNumFormulas() const
{
    return numFormulas;
}

int Plan::getLevel(unsigned int formulaNum) const
{
    if(numFormulas == 0 || (formulaNum - 1) >= numFormulas) return -1;

    return formulaArr[formulaNum - 1].getLevel();
}

Resource* Plan::apply(unsigned int formulaNum)
{
    if(numFormulas == 0 || (formulaNum - 1) >= numFormulas) return nullptr;
    
    return formulaArr[formulaNum - 1].apply();
    
}

Resource* Plan::getInput(uint formulaNum)
{
    if(numFormulas == 0 || (formulaNum - 1) >= numFormulas) return nullptr;

    return formulaArr[formulaNum - 1].getInput();
}

Resource* Plan::getOutput(uint formulaNum)
{
    if(numFormulas == 0 || (formulaNum - 1) >= numFormulas) return nullptr;

    return formulaArr[formulaNum - 1].getOutput();
}