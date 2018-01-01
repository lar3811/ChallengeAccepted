using Calculator.Operators.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operators
{
    public class Sub : Operator
    {
        public override double Execute(double? leftOperand, double? rightOperand)
        {
            if (leftOperand == null &&
                rightOperand == null)
                throw new ApplicationException(InvalidOperation_Orphaned);

            if (rightOperand == null)
                throw new ApplicationException(InvalidOperation_OrphanedRight);

            if (leftOperand == null)
                leftOperand = 0;

            return leftOperand.Value - rightOperand.Value;
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
