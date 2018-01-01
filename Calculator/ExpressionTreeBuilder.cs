using Calculator.Operators;
using Calculator.Operators.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    internal class ExpressionTreeBuilder
    {
        private Dictionary<char, Operator> _operators = new Operator[]
        {
            new Sum(), new Sub(), new Mul(), new Div()
        }
        .ToDictionary(op => op.ToString()[0]);

        public ExpressionTree Build(string expression)
        {
            var tree = new ExpressionTree();
            var node = tree.Root;
            var stack = new Stack<ExpressionTree.BracersNode>();

            var index = -1;
            var accumulator = new StringBuilder();
            var accumulating = false;

            var CompleteOperand = new Action(() => 
            {
                if (!accumulating) return;

                double number;
                try
                {
                    number = Convert.ToDouble(accumulator.ToString());
                }
                catch (FormatException)
                {
                    throw new ApplicationException($"Could not parse operand at position [{index}]: {accumulator.ToString()}");
                }

                var operand = new ExpressionTree.OperandNode(number, index, tree);
                node.AttachNode(operand);
                accumulating = false;
                accumulator.Clear();
                index = -1;
            });



            for (int i = 0; i < expression.Length; i++)
            {
                var input = expression[i];
                if (char.IsWhiteSpace(input)) continue;

                if (IsOperand(input))
                {
                    if (!accumulating)
                    {
                        accumulating = true;
                        index = i;
                    }
                    accumulator.Append(input);
                }
                else
                {
                    CompleteOperand();

                    if (IsOperator(input))
                    {
                        var @operator = new ExpressionTree.OperatorNode(_operators[input], i, tree);
                        node.InsertOperator(@operator);
                        node = @operator;
                    }
                    else
                    if (input == '(')
                    {
                        var bracers = new ExpressionTree.BracersNode(i, tree);
                        node.AttachNode(bracers);
                        node = bracers;
                        stack.Push(bracers);
                    }
                    else
                    if (input == ')')
                    {                        
                        if (stack.Count == 0)
                            throw new ApplicationException("Missing an openening bracer.");
                        node = stack.Pop().Parent;
                    }
                    else
                        throw new ApplicationException($"Symbol is not supported: {input}");
                }
            }

            CompleteOperand();

            if (stack.Count > 0)
                throw new ApplicationException("Missing a closing bracer.");

            return tree;
        }

        private static bool IsOperand(char input)
        {
            return char.IsDigit(input) || input == '.';
        }

        private static bool IsOperator(char input)
        {
            return input == '+' || input == '-' || input == '*' || input == '/';
        }
    }
}
