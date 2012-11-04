﻿namespace PythonSharp.Tests.Commands
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PythonSharp.Commands;
    using PythonSharp.Expressions;
    using PythonSharp.Language;

    [TestClass]
    public class DefCommandTests
    {
        [TestMethod]
        public void CreateSimpleDefCommand()
        {
            IList<ParameterExpression> parameters = new ParameterExpression[] { new ParameterExpression("a", null, false), new ParameterExpression("b", null, false) };
            ICommand body = new SetCommand("c", new ConstantExpression(1));
            DefCommand command = new DefCommand("foo", parameters, body);

            Assert.AreEqual("foo", command.Name);
            Assert.AreEqual(parameters, command.ParameterExpressions);
            Assert.AreEqual(body, command.Body);
        }

        [TestMethod]
        public void ExecuteSimpleDefCommand()
        {
            IList<ParameterExpression> parameters = new ParameterExpression[] { new ParameterExpression("a", null, false), new ParameterExpression("b", null, false) };
            ICommand body = new SetCommand("c", new ConstantExpression(1));
            DefCommand command = new DefCommand("foo", parameters, body);

            Machine machine = new Machine();

            command.Execute(machine.Environment);

            var func = machine.Environment.GetValue("foo");

            Assert.IsNotNull(func);
            Assert.IsInstanceOfType(func, typeof(IFunction));
            Assert.IsInstanceOfType(func, typeof(DefinedFunction));

            var dfunc = (DefinedFunction)func;

            Assert.AreEqual(parameters.Count, dfunc.Parameters.Count);
            Assert.AreEqual(body, dfunc.Body);
        }
    }
}