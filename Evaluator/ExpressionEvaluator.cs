using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Jace;

namespace Evaluator
{
    public class ExpressionEvaluator
    {
        private Dictionary<string, double> _variables = new Dictionary<string, double>();
        private string _expression;

        public ExpressionEvaluator(Dictionary<string, double> variables, string expression)
        {
            _variables = variables;
            _expression = expression;
        }
        public double Evaluate()
        {
            if(isValidate())
            {
                CalculationEngine engine = new CalculationEngine();
                Func<Dictionary<string, double>, double> function = engine.Build(_expression);
                return function(_variables);
            }
            else
            {
                Console.WriteLine("Ooooops...Expression has Undefined Variables...!!");
                return 0;
            }
            
        }

        private Boolean isValidate()
        {
            string[] spearators = { "+", "-", "/", "*", "(", ")", " ", "sin", "cos", "tan" };

            String[] splitList = _expression.Split(spearators, StringSplitOptions.RemoveEmptyEntries);

            int variableCount = 0;
            int commonVariableCount = 0;

            var regexPattern = new Regex(@"^[!@#$%^&A-Za-z_-][\w&\-\!\@\#\$\%\^]*$"); //[A-Za-z_-][A-Za-z0-9_-]*$

            foreach (String splitWord in splitList)
            {
                if (regexPattern.IsMatch(splitWord))
                {
                    variableCount++;
                }
                if (_variables.ContainsKey(splitWord))
                {
                    commonVariableCount++;
                }
            }
            if(variableCount != commonVariableCount)
            {
                //Console.WriteLine("Ooooops...Expression has Undefined Variables...!!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
