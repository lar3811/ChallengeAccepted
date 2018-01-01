using Calculator.Operators.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operators
{
    public class Div : Operator
    {
        public Div()
        {
            Priority = HighPriority;
        }

        public override double Execute(double? leftOperand, double? rightOperand)
        {
            if (leftOperand == null ||
                rightOperand == null)
                throw new ApplicationException(InvalidOperation_Orphaned);

            if (rightOperand.Value == 0)
                throw new ApplicationException($"Division by zero.");

            return leftOperand.Value / rightOperand.Value;
        }

        public override string ToString()
        {
            return "/";
        }
    }
}
