﻿namespace PythonSharp.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using PythonSharp.Expressions;
    using PythonSharp.Language;

    public class DefCommand : ICommand
    {
        private string name;
        private IList<ParameterExpression> parameterExpressions;
        private ICommand body;

        public DefCommand(string name, IList<ParameterExpression> parameterExpressions, ICommand body)
        {
            this.name = name;
            this.parameterExpressions = parameterExpressions;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IList<ParameterExpression> ParameterExpressions { get { return this.parameterExpressions; } }

        public ICommand Body { get { return this.body; } }

        public void Execute(BindingEnvironment environment)
        {
            IList<Parameter> parameters = null;

            if (this.parameterExpressions != null && this.parameterExpressions.Count > 0)
            {
                parameters = new List<Parameter>();
                foreach (var parexpr in this.parameterExpressions)
                    parameters.Add((Parameter)parexpr.Evaluate(environment));
            }

            environment.SetValue(this.name, new DefinedFunction(parameters, this.body));
        }
    }
}