using System;
using System.Collections.Generic;
using FinalProject;

namespace CheckTree
{
    class Program
    {
        static void Main(string[] args)
        {
            DataNode<int> dnode1 = new DataNode<int>(new List<int>() { 2,9,8 });
            DataNode<int> dnode2 = new DataNode<int>(new List<int>() { 3 });

            SubTaskNode<Map, int> node1 = new SubTaskNode<Map, int>(Map.multiply, new List<Node<int>>{ dnode1, dnode2 });
            SubTaskNode<Map, int> node3 = new SubTaskNode<Map, int>(Map.add, new List<Node<int>> { node1, dnode2 });
            SubTaskNode<Map, int> node2 = new SubTaskNode<Map,int>(Map.divide, new List<Node<int>> { node3, dnode2 });

            //SubTaskNode<Reduce, int> node4 = new SubTaskNode<Reduce, int>(Reduce.sum, new List<Node<int>> { node1, dnode2 });
            // SubTaskNode<Reduce> node3 = new SubTaskNode<Reduce>(Reduce.max, new List<Node>{ dnode1, dnode2 },2);

            TreeOfAlgrorithm<int> tree = new TreeOfAlgrorithm<int>(node2);
            List<int> result = tree.calculatingResult();
            result.ForEach(Console.WriteLine);

            //чтение данных из файла
            //добавить product zip 
            //исправить map
            //временная задержка для работы узлов(например, добавление элемента)
        }
    }
}
