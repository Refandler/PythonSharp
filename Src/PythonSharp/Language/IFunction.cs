﻿namespace PythonSharp.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IFunction
    {
        object Apply(IList<object> arguments);
        object Apply(Machine machine, BindingEnvironment environment, IList<object> arguments);
    }
}