using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        public double Feed(string expression)
        {
            var builder = new ExpressionTreeBuilder();
            var tree = builder.Build(expression);
            try
            {
                return tree.Root.Value;
            }
            catch(Exception exception)
            {
                throw new ApplicationException($"An error occured at position [{tree.LastEvaluatedNode.Origin}]: {exception.Message}");
            }
        }
    }
}
