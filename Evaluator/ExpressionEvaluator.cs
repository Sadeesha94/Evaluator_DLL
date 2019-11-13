using System;
using System.Configuration;
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
                Console.WriteLine("Ooooops...Expression has Undefined Variables or Doesn't have Valid Variables...!!");
                return 0;
            }
            
        }

        private Boolean isValidate()
        {
            string[] separators = ConfigurationManager.AppSettings["separators"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            String[] splitList = _expression.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int variableCount = 0;
            int validVariableCount = 0;
            int commonVariableCount = 0;

            int value;
            
            int i = 0;

            var regexPattern = new Regex(@"^[!@#$%^&A-Za-z_-][\w&\-\!\@\#\$\%\^]*$");

            foreach (String splitWord in splitList)
            {
                if (!int.TryParse(splitWord, out value))
                {
                    variableCount++;
                }
                if (_variables.ContainsKey(splitWord))
                {
                    commonVariableCount++;
                }
            }

            string[] variableList = new string[variableCount];

            foreach (String splitWord in splitList)
            {
                if (!int.TryParse(splitWord, out value))
                {
                    variableList[i] = splitWord;
                    i++;
                }
            }

            foreach(String splitWord in variableList)
            {
                if (regexPattern.IsMatch(splitWord))
                {
                    validVariableCount++;
                }
                else
                {
                    return false;
                }
                
            }

            if(validVariableCount != commonVariableCount)
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

