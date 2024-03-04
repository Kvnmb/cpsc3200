// Kevin Bui
// November 5, 2023
// Last Revised: November 8, 2023
// P4.cpp
// IDE: Visual Studio Code

#include <iostream>
#include <memory>
#include <unordered_map>
#include "ExecutablePlan.h"
#include "Plan.h"
#include "Formula.h"

using namespace std;

Formula* createFormulaArray(unsigned int arraySize);
Resource* createResourceArray( unsigned int resourceSize);
unique_ptr<Stockpile> testDecrement(unique_ptr<Stockpile> ptr);
unique_ptr<Stockpile> testIncrement(unique_ptr<Stockpile> ptr);
unique_ptr<Stockpile> testApply(unique_ptr<Stockpile> ptr, ExecutablePlan& obj);

int main()
{
    unsigned int arraySize = 4;
    unsigned int resourceSize = 2;

    Formula* insert = createFormulaArray(arraySize);

    ExecutablePlan obj(insert, arraySize);

    Resource* resource = createResourceArray(resourceSize);

    unique_ptr<Stockpile> ptr = make_unique<Stockpile>(resource, resourceSize);

    cout << "\nInventory:";

    ptr->printStockpile();

    ptr = testDecrement(move(ptr));

    ptr = testIncrement(move(ptr));

    ptr = testApply(move(ptr), obj);

    ExecutablePlan copy(obj);

    if(copy == obj) cout << "\n\nThese two objects are the same.\n" ;


    copy.apply(1);
    copy.apply(1);

    if(copy != obj) cout << "\n\nThese two objects are not the same.\n" ;

    cout << "\n\n" << copy.getNumFormulas();

    copy = copy + obj;

    cout << "\n\n" << copy.getNumFormulas();

    copy += obj;

    cout << "\n\n" << copy.getNumFormulas();

    --copy;

    cout << "\n\n" << copy.getNumFormulas();

    return 0;
}


unique_ptr<Stockpile> testApply(unique_ptr<Stockpile> ptr, ExecutablePlan& obj){
    cout << "\nInventory before apply: ";

    ptr->printStockpile();

    cout << "\nInventory after apply: ";

    ptr = obj.apply(move(ptr));

    ptr->printStockpile();

    return ptr;
}


unique_ptr<Stockpile> testIncrement(unique_ptr<Stockpile> ptr){
    cout << "\nIncreasing iron by 5:\n";

    ptr->increase("iron", 5);

    cout << endl << "Amount of iron: " << ptr->getQuantity("iron") << endl;

    return ptr;
}


unique_ptr<Stockpile> testDecrement(unique_ptr<Stockpile> ptr){
    cout << "\nDecrementing coal by 3:\n";

    ptr->decrease("coal", 3);

    cout << endl << "Amount of coal: " << ptr->getQuantity("coal") << endl;

    return ptr;
}

Resource* createResourceArray(unsigned int resourceSize)
{
    Resource* arr = new Resource[resourceSize];

    arr[0].name = "iron";
    arr[0].quantity = 10;
    arr[1].name = "coal";
    arr[1].quantity = 5;

    return arr;
}

Formula* createFormulaArray(unsigned int arraySize)
{   
    Formula* array = new Formula[arraySize];
    Resource input1[2];
    Resource output1[1];

    input1[0] = Resource("iron", 3);
    input1[1] = Resource("coal", 2);
    output1[0] = Resource("steel", 10);

    array[0] = Formula(input1, 2, output1, 1);

    Resource input2[1];
    Resource output2[1];

    input2[0] = Resource("ice", 3);
    output2[0] = Resource("water", 6);

    array[1] = Formula(input2, 1, output2, 1);

    Resource input3[2];
    Resource output3[1];

    input3[0] = Resource("milk", 1);
    input3[1] = Resource("chocolate", 2);
    output3[0] = Resource("chocolate milk", 1);

    array[2] = Formula(input3, 2, output3, 1);

    Resource input4[1];
    Resource output4[3];

    input4[0] = Resource("rock", 3);
    output4[0] = Resource("fossil", 1);
    output4[1] = Resource("crystals", 5);
    output4[2] = Resource("gemstone", 3);

    array[3] = Formula(input4, 1, output4, 3);

    return array;
}