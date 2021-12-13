using System;
using System.Collections.Generic;

namespace FinalProject
{
    public class SubTaskNode<T, T1> : Node<T1>
    {
        private T method;
        private double operationArgument;
        public SubTaskNode(T typeOfMethod, List<Node<T1>> dataForWorking)
        { //for zip(reduce) operations
            method = typeOfMethod;
            Node<T1>[] arrNode = dataForWorking.ToArray();
            right = arrNode[1];
            left = arrNode[0];
        }
        public SubTaskNode(T typeOfMethod, List<Node<T1>> dataForWorking, double operation)
        { //for map operations
            method = typeOfMethod;
            operationArgument = operation;
        }
        override protected List<T1> calculate(List<T1> val1, List<T1> val2)
        {
            dynamic dval1 = val1, dval2 = val2;
            List<T1> result = new List<T1>();
            switch (method)
            {
                case (Map.divide):
                    foreach (var i in dval2)
                    {
                        foreach (var value in dval1)
                        {
                            result.Add(value / i);
                        }
                        break;
                    }
                    break;
                case (Map.add):
                    foreach (var i in dval2)
                    {
                        foreach (var value in dval1)
                        {
                            result.Add(value + i);
                        }
                        break;
                    }
                    break;
                case (Map.multiply):
                    foreach (var i in dval2)
                    {
                        foreach (var value in dval1)
                        {
                            result.Add(value * i);
                        }
                        break;
                    }
                    break;
                case (Map.exponentiate):
                    foreach (var i in dval2)
                    {
                        foreach (var value in dval1)
                        {
                            result.Add(Math.Pow(value, i));
                        }
                        break;
                    }
                    break;
                case (Reduce.length):
                    dynamic sum = val1.Count;
                    result.Add(sum);
                    break;
                case (Reduce.max):
                    dynamic max = 0L;
                    int counter = 0;
                    foreach(var val in val1)
                    {
                        if(counter == 0 || val > max)
                        {
                            max = val;
                            counter = 1;
                        }
                    }
                    result.Add(max);
                    break;
                case (Reduce.min):
                    dynamic min = 0L;
                    counter = 0;
                    foreach (var val in val1)
                    {
                        if (counter == 0 || val < min)
                        {
                            min = val;
                            counter = 1;
                        }
                    }
                    result.Add(min);
                    break;
                case (Reduce.overage):
                    dynamic aver = 0L;
                    foreach (var val in val1)
                    {
                        aver += val;
                    }
                    result.Add((T1)(aver/val1.Count));
                    break;
                case (Reduce.sum):
                    dynamic summ = 0L;
                    foreach (var val in val1)
                    {
                        summ += val;
                    }
                    result.Add((T1)summ);
                    break;
            }
            return result;
        }
    }

    /*public class Expr
    {
        public delegate double reduce(int a, int b);
        delegate List<Node> map(List<Node> nodeTask, int b);
        public Expr()
        {
            //SubTaskNode<Expr.reduce> sub = new SubTaskNode<Expr.reduce>(new Func<int, int>(reduce));
        }
    }*/

    public class DataNode<T>:Node<T>
    {
        private List<T> data;
        public DataNode(List<T> data)
        {
            this.data = data;
        }
        override protected List<T> calculate(List<T> val1, List<T> val2)
        {
            return data;
        }
    }
    public abstract class Node<T>
    {
        protected Node<T>? left = null;
        protected Node<T>? right = null;
        protected abstract List<T> calculate(List<T> t1, List<T> t2);

        public Node<T>? getLeft() { return left; }
        public Node<T>? getRight() { return right; }

        public List<T> makeCalculation()
        {
            List<T> leftResult = new List<T>();
            List<T> rightResult = new List<T>();

            if (left != null)
            {
                leftResult = left.makeCalculation();
            }
            if (right != null)
            {
                rightResult = right.makeCalculation();
            }
            if(left != null && right != null)
            {
                return this.calculate(leftResult, rightResult);
            }
            else if(left != null)
            {
                return this.calculate(leftResult, null);
            }
            else
            {
                return this.calculate(null, rightResult);
            }
        }
    }
    public class TreeOfAlgrorithm<T>
    {// T - type of data in DataNode

        private Node<T>? head;
        public TreeOfAlgrorithm(Node<T> head)
        {
            this.head = head;
        }
        public List<T> calculatingResult()
        {
            return head.makeCalculation();
        }
    }
}