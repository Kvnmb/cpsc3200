
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormulaClass;

namespace FormulaTest
{
    [TestClass]
    public class FormulaTest
    {
        [TestMethod]
        public void TestFormulaConstructor()
        {
            Resource[] inputs = new Resource[2];

            inputs[0] = new Resource("iron", 2);
            inputs[1] = new Resource("coal", 3);

            Resource[] outputs = new Resource[1];

            outputs[0] = new Resource("steel", 1);

            Formula formula = new(inputs, 2, outputs, 1);

            Resource[] results = formula.GetInput();
            
            for(uint x = 0; x < results.Length; x++)
            {
                Assert.AreEqual(inputs[x].name, results[x].name);
                Assert.AreEqual(inputs[x].quantity, results[x].quantity);
            }

            results = formula.GetOutput();

            for (uint x = 0; x < results.Length; x++)
            {
                Assert.AreEqual(outputs[x].name, results[x].name);
                Assert.AreEqual(outputs[x].quantity, results[x].quantity);
            }
        }

        [TestMethod]
        public void TestInput()
        {
            Resource[] inputs = new Resource[3];

            inputs[0] = new Resource("iron", 2);
            inputs[1] = new Resource("coal", 3);

            Resource[] outputs = new Resource[1];

            outputs[0] = new Resource("steel", 1);

            Formula formula = new(inputs, 2, outputs, 1);

            Resource[] results = formula.GetInput();

            for (uint x = 0; x < results.Length; x++)
            {
                Assert.AreEqual(inputs[x].name, results[x].name);
                Assert.AreEqual(inputs[x].quantity, results[x].quantity);

            }
        }

        [TestMethod]
        public void TestOutput()
        {
            Resource[] inputs = new Resource[2];

            inputs[0] = new Resource("iron", 2);
            inputs[1] = new Resource("coal", 3);

            Resource[] outputs = new Resource[1];

            outputs[0] = new Resource("steel", 1);

            Formula formula = new(inputs, 2, outputs, 1);

            Resource[] results = formula.GetOutput();

            for (uint x = 0; x < results.Length; x++)
            {
                Assert.AreEqual(outputs[x].name, results[x].name);
                Assert.AreEqual(outputs[x].quantity, results[x].quantity);
            }
        }

        [TestMethod]
        public void TestLevel()
        {
            Resource[] inputs = new Resource[2];

            inputs[0] = new Resource("iron", 2);
            inputs[1] = new Resource("coal", 3);

            Resource[] outputs = new Resource[1];

            outputs[0] = new Resource("steel", 1);

            Formula formula = new(inputs, 2, outputs, 1);


            Assert.IsTrue(formula.GetLevel() == 0);

            for (uint x = 0; x < 10; x++)
            {
                formula.Apply();
            }

            Assert.IsTrue(formula.GetLevel() > 0);
        }

        [TestMethod]
        public void TestMaxLevel()
        {
            Resource[] inputs = new Resource[2];

            inputs[0] = new Resource("iron", 2);
            inputs[1] = new Resource("coal", 3);

            Resource[] outputs = new Resource[1];

            outputs[0] = new Resource("steel", 1);

            Formula formula = new(inputs, 2, outputs, 1);

            for (int x = 0; x < 100; x++)
            {
                formula.Apply();
            }

            Assert.IsTrue(formula.GetLevel() == 5);

        }

        [TestMethod]
        public void TestApply()
        {
            Resource[] inputs = new Resource[2];

            inputs[0] = new Resource("iron", 2);
            inputs[1] = new Resource("coal", 3);

            Resource[] outputs = new Resource[1];

            outputs[0] = new Resource("steel", 2);

            Formula formula = new(inputs, 2, outputs, 1);

            Resource [] list = formula.Apply();

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(list[0].name, outputs[0].name);

            if (formula.GetLevel() == 0)
            {
                Assert.AreEqual(list[0].quantity, (uint)0);
                Assert.AreEqual(list[0].quantity, (uint)1);

            }

            if (formula.GetLevel() == 1) Assert.AreEqual(list[0].quantity, (uint)2);

            if (formula.GetLevel() == 2) Assert.AreEqual(list[0].quantity, (uint)3);
        }

        [TestMethod]
        public void TestCopy()
        {
            Resource[] inputs = new Resource[2];

            inputs[0] = new Resource("iron", 2);
            inputs[1] = new Resource("coal", 3);

            Resource[] outputs = new Resource[1];

            outputs[0] = new Resource("steel", 2);

            Formula formula = new(inputs, 2, outputs, 1);

            Formula copy = formula.Copy();

            for (int x = 0; x < 4; x++)
            {
                formula.Apply();
            }
            Assert.AreNotEqual(formula.GetLevel(), copy.GetLevel());
        }
    }
}