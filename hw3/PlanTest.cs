using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormulaClass;
using PlanClass;

namespace PlanTest
{
    [TestClass]
    public class PlanTest
    {
        [TestMethod]
        public void TestPlanConstructor()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula [] formula = new Formula[2];
            formula[0] = new Formula(input, 2, output, 1);

            input[0] = new Resource("water", 2);
            input[1] = new Resource("fire", 3);

            output = new Resource[1];
            output[0] = new Resource("steam", 1);

            formula[1] = new Formula(input, 2, output, 1);

            Plan test = new Plan(formula, 2);

            Assert.AreEqual(test.GetNumFormulas(), (uint)2);
        }

        [TestMethod]
        public void TestResize()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            Plan test = new Plan();

            for(int x = 0; x < 15; x++)
            {
                test.Add(formula);
            }

            Assert.IsTrue(test.GetNumFormulas() > 10);
        }

        [TestMethod]
        public void TestAdd()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            Plan test = new Plan();

            test.Add(formula);

            Resource[] inputResult = test.GetInput(1);
            Resource[] outputResult = test.GetOutput(1);

            Assert.AreEqual(inputResult[0].name, input[0].name);
            Assert.AreEqual(inputResult[1].name, input[1].name);

            Assert.AreEqual(inputResult[0].quantity, input[0].quantity);
            Assert.AreEqual(inputResult[1].quantity, input[1].quantity);

            Assert.AreEqual(outputResult[0].name, output[0].name);

            Assert.AreEqual(outputResult[0].quantity, output[0].quantity);
        }

        [TestMethod]
        public void TestRemove()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula[] formula = new Formula[2];
            formula[0] = new Formula(input, 2, output, 1);

            input[0] = new Resource("water", 2);
            input[1] = new Resource("fire", 3);

            output = new Resource[1];
            output[0] = new Resource("steam", 1);

            formula[1] = new Formula(input, 2, output, 1);

            Plan test = new Plan(formula, 2);

            Assert.AreEqual(test.GetNumFormulas(), (uint)2);

            test.Remove();

            Assert.AreEqual(test.GetNumFormulas(), (uint)1);
        }

        [TestMethod]
        public void TestReplace()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Resource[] replaceInput = new Resource[2];
            replaceInput[0] = new Resource("water", 4);
            replaceInput[1] = new Resource("fire", 5);

            Resource[] replaceOutput = new Resource[1];
            replaceOutput = new Resource[1];
            replaceOutput[0] = new Resource("steam", 7);

            Formula[] formula = new Formula[2];

            formula[0] = new Formula(input, 2, output, 1);
            formula[1] = new Formula(replaceInput, 2, replaceOutput, 1);

            Plan test = new Plan();

            test.Add(formula[0]);

            test.Replace(1, formula[1]);

            Resource[] inputResult = test.GetInput(1);
            Resource[] outputResult = test.GetOutput(1);

            Assert.AreNotEqual(inputResult[0].name, input[0].name);
            Assert.AreNotEqual(inputResult[1].name, input[1].name);

            Assert.AreNotEqual(inputResult[0].quantity, input[0].quantity);
            Assert.AreNotEqual(inputResult[1].quantity, input[1].quantity);

            Assert.AreNotEqual(outputResult[0].name, output[0].name);

            Assert.AreNotEqual(outputResult[0].quantity, output[0].quantity);
        }

        [TestMethod]
        public void TestGetNumFormulas()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula[] formula = new Formula[2];
            formula[0] = new Formula(input, 2, output, 1);

            input[0] = new Resource("water", 2);
            input[1] = new Resource("fire", 3);

            output = new Resource[1];
            output[0] = new Resource("steam", 1);

            formula[1] = new Formula(input, 2, output, 1);

            Plan test = new Plan(formula, 2);

            Assert.AreEqual(test.GetNumFormulas(), (uint)2);
        }


        [TestMethod]
        public void TestGetLevel()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            Plan test = new Plan();

            test.Add(formula);

            for (int x = 0; x < 15; x++)
            {
                test.Apply(1);
            }

            Assert.AreEqual(test.GetLevel(1), 5);
        }

        [TestMethod]
        public void TestApply()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            Plan test = new Plan();

            test.Add(formula);

            Resource[] result = test.Apply(1);

            Assert.AreEqual(result[0].quantity, (uint)1);

            Assert.AreEqual(test.GetLevel(1), 1);
        }

        [TestMethod]
        public void TestGetInput()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            Plan test = new Plan();

            test.Add(formula);

            Resource[] inputResult = test.GetInput(1);

            Assert.AreEqual(inputResult[0].name, input[0].name);
            Assert.AreEqual(inputResult[1].name, input[1].name);

            Assert.AreEqual(inputResult[0].quantity, input[0].quantity);
            Assert.AreEqual(inputResult[1].quantity, input[1].quantity);
        }

        [TestMethod]
        public void TestGetOutput()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            Plan test = new Plan();

            test.Add(formula);

            Resource[] outputResult = test.GetOutput(1);

            Assert.AreEqual(outputResult[0].name, output[0].name);
            Assert.AreEqual(outputResult[0].name, output[0].name);
        }

        [TestMethod]
        public void TestDeepCopy()
        {
            Resource[] input = new Resource[2];
            input[0] = new Resource("iron", 2);
            input[1] = new Resource("coal", 3);

            Resource[] output = new Resource[1];
            output[0] = new Resource("steel", 1);

            Formula formula = new Formula(input, 2, output, 1);

            Plan test = new Plan();

            test.Add(formula);
            Plan copy = test.DeepCopy();

            input[0] = new Resource("water", 4);
            input[1] = new Resource("fire", 5);

            output = new Resource[1];
            output[0] = new Resource("steam", 7);

            formula = new Formula(input, 2, output, 1);

            copy.Add(formula);

            Assert.AreNotEqual(test.GetNumFormulas(), copy.GetNumFormulas());
        }
    }
}