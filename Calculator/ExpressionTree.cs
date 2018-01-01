using Calculator.Operators.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class ExpressionTree
    {
        public Node LastEvaluatedNode { get; private set; }

        public readonly Node Root;

        public ExpressionTree()
        {
            Root = new BracersNode(-1, this);
        }



        public abstract class Node
        {
            public readonly ExpressionTree Host;
            public readonly int Origin;

            public abstract double Value { get; }

            public Node Parent { get; set; }

            public Node(int origin, ExpressionTree host)
            {
                Origin = origin;
                Host = host;
            }
            
            public abstract void AttachNode(Node node);
            public abstract void InsertOperator(OperatorNode node);
        }



        public class OperatorNode : Node
        {
            public Node LeftNode { get; private set; }
            public Node RightNode { get; private set; }

            public Operator Operator { get; }

            public OperatorNode(Operator @operator, int origin, ExpressionTree host)
                : base(origin, host)
            {
                Operator = @operator;
            }

            public override double Value
            {
                get
                {
                    var left = LeftNode?.Value;
                    var right = RightNode?.Value;
                    Host.LastEvaluatedNode = this;
                    return Operator.Execute(left, right);
                }
            }

            public override void AttachNode(Node child)
            {
                AttachNodeRight(child);
            }

            public void AttachNodeRight(Node node)
            {
                if (node == null)
                    throw new ArgumentNullException();
                if (RightNode != null)
                    throw new InvalidOperationException();
                RightNode = node;
                node.Parent = this;
            }

            public void AttachNodeLeft(Node node)
            {
                if (node == null)
                    throw new ArgumentNullException();
                if (LeftNode != null)
                    throw new InvalidOperationException();
                LeftNode = node;
                node.Parent = this;
            }

            public override void InsertOperator(OperatorNode node)
            {
                if (node == null)
                    throw new ArgumentNullException();

                if (node.Operator.Priority > this.Operator.Priority)
                {
                    if (this.RightNode != null)
                    {
                        node.LeftNode = this.RightNode;
                        node.LeftNode.Parent = node;
                    }
                    node.Parent = this;
                    this.RightNode = node;
                }
                else
                {
                    Parent.InsertOperator(node);
                }
            }

            public override string ToString()
            {
                return $"{LeftNode} {Operator} {RightNode}";
            }
        }



        public class BracersNode : Node
        {
            public Node EnclosedExpression { get; private set; }

            public override double Value
            {
                get
                {
                    if (EnclosedExpression == null)
                        throw new ApplicationException($"Empty bracers at position [{Origin}].");
                    return EnclosedExpression.Value;
                }
            }

            public BracersNode(int origin, ExpressionTree host) 
                : base(origin, host) { }

            public override void AttachNode(Node node)
            {
                if (node == null)
                    throw new ArgumentNullException();

                if (EnclosedExpression == null)
                {
                    EnclosedExpression = node;
                    node.Parent = this;
                }
                else
                    throw new ApplicationException($"Missing operator at position [{node.Origin}].");
            }

            public override void InsertOperator(OperatorNode node)
            {
                if (node == null)
                    throw new ArgumentNullException();

                if (EnclosedExpression != null)
                {
                    var expression = EnclosedExpression;
                    EnclosedExpression.Parent = null;
                    EnclosedExpression = null;
                    node.AttachNodeLeft(expression);
                }
                AttachNode(node);
            }

            public override string ToString()
            {
                return $"({EnclosedExpression})";
            }
        }



        public class OperandNode : Node
        {
            public override double Value { get; }

            public OperandNode(double value, int origin, ExpressionTree host)
                : base(origin, host)
            {
                Value = value;
            }

            public override void AttachNode(Node child)
            {
                throw new InvalidOperationException();
            }

            public override void InsertOperator(OperatorNode node)
            {
                throw new InvalidOperationException();
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}
