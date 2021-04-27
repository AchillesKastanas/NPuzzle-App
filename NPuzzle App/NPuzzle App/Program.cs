using System;
using System.Collections;

namespace NPuzzleApp2
{

    public class Node
    {
        private bool isRoot = false;
        public Node node1 = (Node)null;
        public Node node2 = (Node)null;
        public Node node3 = (Node)null;
        public Node node4 = (Node)null;
        private char[,] table = new char[3, 2]
        {
      {
        'n',
        'n'
      },
      {
        'n',
        'n'
      },
      {
        'n',
        'n'
      }
        };

        public void addChildren(Node node1, Node node2, Node node3, Node node4)
        {
            if (node1 != null)
                this.node1 = node1;
            if (node2 != null)
                this.node2 = node2;
            if (node3 != null)
                this.node3 = node3;
            if (node4 == null)
                return;
            this.node4 = node4;
        }

        public void updateTable(char[,] newTable) => this.table = newTable;

        public void setAsRootNode() => this.isRoot = true;

        public char[,] getTable() => this.table;
    }

    public static class Printer
    {
        public static void printTable(char[,] table)
        {
            for (int index1 = 0; index1 < table.GetLength(0); ++index1)
            {
                Console.Write("|");
                for (int index2 = 0; index2 < table.GetLength(1); ++index2)
                    Console.Write(table[index1, index2].ToString() + "\t");
                Console.WriteLine();
            }
        }
    }

    public class Solver
    {
        private char[,] goalTable = new char[3, 3]
        {
      {
        '1',
        '2',
        '3'
      },
      {
        '4',
        '5',
        '6'
      },
      {
        '7',
        '8',
        ' '
      }
        };
        private char[,] cTable;
        private Node cNode;

        public Solver(Node cNode)
        {
            this.cTable = cNode.getTable();
            this.cNode = cNode;
        }

        public void aStarAlgorithm()
        {
            ++Tree.treeHeight;
            Console.WriteLine("Parent Node: \n");
            Printer.printTable(this.cTable);
            Console.WriteLine("      || ");
            Console.WriteLine("      \\/ ");
            bool flag = true;
            for (int index1 = 0; index1 < 3; ++index1)
            {
                for (int index2 = 0; index2 < 3; ++index2)
                {
                    if (!this.cTable[index1, index2].Equals(this.goalTable[index1, index2]))
                        flag = false;
                }
            }
            if (flag)
            {
                Console.WriteLine("Goal Found. Program Finished.");
            }
            else
            {
                this.doChildren();
                Node node = this.cNode;
                int num1 = 100000;
                Console.WriteLine("Children Nodes: \n");
                if (this.cNode.node1 != null)
                {
                    int num2 = this.doManhattan(this.cNode.node1.getTable());
                    Printer.printTable(this.cNode.node1.getTable());
                    Console.WriteLine("\nG + H = " + num2.ToString() + "\n");
                    if (num2 < num1)
                    {
                        node = this.cNode.node1;
                        num1 = num2;
                    }
                }
                if (this.cNode.node2 != null)
                {
                    int num2 = this.doManhattan(this.cNode.node2.getTable());
                    Printer.printTable(this.cNode.node2.getTable());
                    Console.WriteLine("\nG + H = " + num2.ToString() + "\n");
                    if (num2 < num1)
                    {
                        node = this.cNode.node2;
                        num1 = num2;
                    }
                }
                if (this.cNode.node3 != null)
                {
                    int num2 = this.doManhattan(this.cNode.node3.getTable());
                    Printer.printTable(this.cNode.node3.getTable());
                    Console.WriteLine("\nG + H = " + num2.ToString() + "\n");
                    if (num2 < num1)
                    {
                        node = this.cNode.node3;
                        num1 = num2;
                    }
                }
                if (this.cNode.node4 != null)
                {
                    int num2 = this.doManhattan(this.cNode.node4.getTable());
                    Printer.printTable(this.cNode.node4.getTable());
                    Console.WriteLine("\nG + H = " + num2.ToString() + "\n");
                    if (num2 < num1)
                    {
                        node = this.cNode.node4;
                        num1 = num2;
                    }
                }
                if (node == this.cNode)
                    Console.WriteLine("There is a problem with the child nodes");
                Console.WriteLine("Traversing to the best child. G + H = " + num1.ToString());
                this.cNode = node;
                this.cTable = node.getTable();
                this.aStarAlgorithm();
            }
        }

        public int[] findEmpty()
        {
            int[] numArray = new int[2];
            for (int index1 = 0; index1 < this.cTable.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < this.cTable.GetLength(1); ++index2)
                {
                    if (this.cTable[index1, index2] == ' ')
                    {
                        numArray[0] = index1;
                        numArray[1] = index2;
                    }
                }
            }
            return numArray;
        }

        public void doChildren()
        {
            int[] empty = this.findEmpty();
            ArrayList arrays = this.calculateArrays(empty[0] != 0 || empty[1] != 0 ? (empty[0] != 0 || empty[1] != 2 ? (empty[0] != 2 || empty[1] != 0 ? (empty[0] != 2 || empty[1] != 2 ? (empty[0] != 0 ? (empty[0] != 2 ? (empty[1] != 0 ? (empty[1] != 2 ? "udrl" : "ldu") : "udr") : "lur") : "ldr") : "ul") : "ur") : "ld") : "rd");
            if (arrays.Equals((object)null))
                return;
            for (int index = 0; index < arrays.Count; ++index)
            {
                if (!Tree.existingFormations.Contains((object)(char[,])arrays[index]))
                {
                    Tree.existingFormations.Add(arrays[index]);
                    switch (index)
                    {
                        case 0:
                            Node node1 = new Node();
                            this.cNode.addChildren(node1, (Node)null, (Node)null, (Node)null);
                            node1.updateTable((char[,])arrays[index]);
                            break;
                        case 1:
                            Node node2 = new Node();
                            this.cNode.addChildren((Node)null, node2, (Node)null, (Node)null);
                            node2.updateTable((char[,])arrays[index]);
                            break;
                        case 2:
                            Node node3 = new Node();
                            this.cNode.addChildren((Node)null, (Node)null, node3, (Node)null);
                            node3.updateTable((char[,])arrays[index]);
                            break;
                        case 3:
                            Node node4 = new Node();
                            this.cNode.addChildren((Node)null, (Node)null, (Node)null, node4);
                            node4.updateTable((char[,])arrays[index]);
                            break;
                    }
                }
            }
        }

        private ArrayList calculateArrays(string input)
        {
            ArrayList arrayList = new ArrayList();
            int[] empty = this.findEmpty();
            if (input.Contains("u"))
            {
                char[,] chArray = (char[,])this.cTable.Clone();
                chArray[empty[0], empty[1]] = chArray[empty[0] - 1, empty[1]];
                chArray[empty[0] - 1, empty[1]] = ' ';
                arrayList.Add((object)chArray);
            }
            if (input.Contains("d"))
            {
                char[,] chArray = (char[,])this.cTable.Clone();
                chArray[empty[0], empty[1]] = chArray[empty[0] + 1, empty[1]];
                chArray[empty[0] + 1, empty[1]] = ' ';
                arrayList.Add((object)chArray);
            }
            if (input.Contains("r"))
            {
                char[,] chArray = (char[,])this.cTable.Clone();
                chArray[empty[0], empty[1]] = chArray[empty[0], empty[1] + 1];
                chArray[empty[0], empty[1] + 1] = ' ';
                arrayList.Add((object)chArray);
            }
            if (input.Contains("l"))
            {
                char[,] chArray = (char[,])this.cTable.Clone();
                chArray[empty[0], empty[1]] = chArray[empty[0], empty[1] - 1];
                chArray[empty[0], empty[1] - 1] = ' ';
                arrayList.Add((object)chArray);
            }
            return arrayList;
        }

        public int doManhattan(char[,] cTable)
        {
            int num1 = 0;
            for (int index = 1; index < 9; ++index)
            {
                int[] num2 = this.findNum(index.ToString().ToCharArray(), cTable);
                switch (index - 1)
                {
                    case 0:
                        num1 += num2[0] + num2[1];
                        break;
                    case 1:
                        num1 += num2[0] + Math.Abs(num2[1] - 1);
                        break;
                    case 2:
                        num1 += num2[0] - 1 + Math.Abs(num2[1] - 2);
                        break;
                    case 3:
                        num1 += Math.Abs(num2[0] - 1) + num2[1];
                        break;
                    case 4:
                        num1 += Math.Abs(num2[0] - 1) + Math.Abs(num2[1] - 1);
                        break;
                    case 5:
                        num1 += Math.Abs(num2[0] - 1) + Math.Abs(num2[1] - 2);
                        break;
                    case 6:
                        num1 += Math.Abs(num2[0] - 2) + num2[1];
                        break;
                    case 7:
                        num1 += Math.Abs(num2[0] - 2) + Math.Abs(num2[1] - 1);
                        break;
                }
            }
            return Tree.treeHeight + num1;
        }

        private int[] findNum(char[] num, char[,] cTable)
        {
            int[] numArray = new int[2];
            for (int index1 = 0; index1 < cTable.GetLength(0); ++index1)
            {
                for (int index2 = 0; index2 < cTable.GetLength(1); ++index2)
                {
                    if (cTable[index1, index2].Equals(num[0]))
                    {
                        numArray[0] = index1;
                        numArray[1] = index2;
                    }
                }
            }
            return numArray;
        }
    }

    public static class Tree
    {
        public static int treeHeight = 0;
        public static ArrayList existingFormations = new ArrayList();
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            char[,] newTable = new char[3, 3]
            {
        {
          '1',
          '2',
          ' '
        },
        {
          '4',
          '5',
          '3'
        },
        {
          '7',
          '8',
          '6'
        }
            };
            Node cNode = new Node();
            cNode.updateTable(newTable);
            cNode.setAsRootNode();
            Tree.existingFormations.Add((object)newTable);
            new Solver(cNode).aStarAlgorithm();
        }
    }
}
