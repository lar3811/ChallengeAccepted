using Calculator.Operators.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operators
{
    public class Mul : Operator
    {
        public Mul()
        {
            Priority = HighPriority;
        }

        public override double Execute(double? leftOperand, double? rightOperand)
        {
            if (leftOperand == null ||
                rightOperand == null)
                throw new ApplicationException(InvalidOperation_Orphaned);

            return leftOperand.Value * rightOperand.Value;
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
