using System;
using System.Collections.Generic;
using Jace;

namespace Evaluator
{
    public class ExpressionEvaluator
    {
        public double Evaluate(Dictionary<string, double> variables, string expression)
        {
            CalculationEngine engine = new CalculationEngine();
            Func<Dictionary<string, double>, double> function = engine.Build(expression);
            return function(variables); ;
        }
    }
}
