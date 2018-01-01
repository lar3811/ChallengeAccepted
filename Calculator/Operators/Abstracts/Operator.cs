using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operators.Abstracts
{
    public abstract class Operator
    {
        protected const int LowPriority = -10;
        protected const int NormalPriority = 0;
        protected const int HighPriority = 10;

        protected string InvalidOperation_Orphaned => $"Operator '{this}' requires operands.";
        protected string InvalidOperation_OrphanedRight => $"Operator '{this}' requires right operand.";
        
        public int Priority { get; protected set; }
        public abstract double Execute(double? leftOperand, double? rightOperand);
    }
}
