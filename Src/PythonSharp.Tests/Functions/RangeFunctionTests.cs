﻿namespace PythonSharp.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PythonSharp.Functions;
    using PythonSharp.Language;
    using PythonSharp.Exceptions;

    [TestClass]
    public class RangeFunctionTests
    {
        [TestMethod]
        public void CreateRangeWithOneParameter()
        {
            RangeFunction function = new RangeFunction();
            
            var result = function.Apply(null, new object[] { 2 }, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Range));

            var range = (Range)result;

            Assert.AreEqual(0, range.From);
            Assert.AreEqual(2, range.To);
            Assert.AreEqual(1, range.Step);
        }

        [TestMethod]
        public void CreateRangeWithTwoParameters()
        {
            RangeFunction function = new RangeFunction();

            var result = function.Apply(null, new object[] { 1, 2 }, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Range));

            var range = (Range)result;

            Assert.AreEqual(1, range.From);
            Assert.AreEqual(2, range.To);
            Assert.AreEqual(1, range.Step);
        }

        [TestMethod]
        public void CreateRangeWithThreeParameters()
        {
            RangeFunction function = new RangeFunction();

            var result = function.Apply(null, new object[] { 1, -2, -1 }, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Range));

            var range = (Range)result;

            Assert.AreEqual(1, range.From);
            Assert.AreEqual(-2, range.To);
            Assert.AreEqual(-1, range.Step);
        }

        [TestMethod]
        public void RaiseWhenNoParameters()
        {
            RangeFunction function = new RangeFunction();

            try
            {
                function.Apply(null, new object[] { }, null);
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TypeError));
                Assert.AreEqual("range expected 1 arguments, got 0", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseMoreThanThreeParameters()
        {
            RangeFunction function = new RangeFunction();

            try
            {
                function.Apply(null, new object[] { 1, 2, 3, 4 }, null);
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TypeError));
                Assert.AreEqual("range expected at most 3 arguments, got 4", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfSomeParameterIsFloat()
        {
            RangeFunction function = new RangeFunction();

            try
            {
                function.Apply(null, new object[] { 1.0, 2, 3 }, null);
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TypeError));
                Assert.AreEqual("'float' object cannot be interpreted as an integer", ex.Message);
            }
        }
    }
}