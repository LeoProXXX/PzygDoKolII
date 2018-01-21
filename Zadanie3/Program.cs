using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Zadanie 3
// Napisz metodę(iteracyjną), która za pomocą kolejki wypisuje zawartość drzewa
// kolejnymi poziomami.

namespace Zadanie3
{
    /* Class to represent Tree node */
    class Node
    {
        public int data;
        public Node left = null;
        public Node right = null;

        public Node(int data)
        {
            this.data = data;
        }

        //dodaje lewe dziecko
        public void AddLeft(Node leftChild)
        {
            this.left = leftChild;
        }

        //dodaje prawe dziecko
        public void AddRight(Node rightChild)
        {
            this.right = rightChild;
        }

        //PRE-ORDER
        public void ShowPreOrder()
        {
            Console.Write("({0}", this.data);
            if (this.left != null)
            {
                this.left.ShowPreOrder();
            }
            if (this.right != null)
            {
                this.right.ShowPreOrder();
            }
            Console.Write(")");
        }

        //PRE-ORDER bez uzycia rekurencji (wsk. wykorzystaj stos)
        public void ShowPreOrderNotRecursion()
        {
            Stack<Node> stack = new Stack<Node>();
            if (this != null)
            {
                stack.Push(this);
            }
            while (stack.Count > 0)
            {
                Node tmp = stack.Pop();
                Console.Write("{0} ", tmp.data);

                if (tmp.right != null)
                {
                    stack.Push(tmp.right);
                }
                if (tmp.left != null)
                {
                    stack.Push(tmp.left);
                }
            }
        }

        //POST-ORDER
        public void ShowPostOrder()
        {
            if (this.left != null)
            {
                this.left.ShowPostOrder();
            }
            if (this.right != null)
            {
                this.right.ShowPostOrder();
            }
            Console.Write("{0} ", this.data);
        }

        //IN-ORDER
        public void ShowInOrder()
        {
            if (this.left != null)
            {
                this.left.ShowInOrder();
            }
            Console.Write("{0} ", this.data);
            if (this.right != null)
            {
                this.right.ShowInOrder();
            }
        }

        //zwraca wysokosc
        public int GetHeight()
        {
            int height = 0;
            if (this.left != null)
            {
                height = Math.Max(height, this.left.GetHeight() + 1);
            }
            if (this.right != null)
            {
                height = Math.Max(height, this.right.GetHeight() + 1);
            }
            return height;
        }

        //przeszukuje drzewo
        public bool Search(int data)
        {
            if (this.data == data)
            {
                return true;
            }
            // przechodzimy przez odnogi drzewa - najpierw lewe, potem prawe
            bool left = false;
            bool right = false;

            if (this.left != null)
            {
                left = this.left.Search(data);
            }
            if (this.right != null)
            {
                right = this.right.Search(data);
            }
            return left || right;
        }

        //znajduje bezposrednio rodzica dla danego
        //metoda oparta na stosie i petli (tak jak PRE-ORDER bez uzycia rekurencji)
        public int FindParent(int childData)
        {
            Stack<Node> stack = new Stack<Node>();
            if (this != null)
            {
                stack.Push(this);
            }
            while (stack.Count > 0)
            {
                Node tmp = stack.Pop();
                if ((tmp.left != null && tmp.left.data == childData) || (tmp.right != null && tmp.right.data == childData))
                {
                    return tmp.data;
                }
                if (tmp.right != null)
                {
                    stack.Push(tmp.right);
                }
                if (tmp.left != null)
                {
                    stack.Push(tmp.left);
                }
            }
            return -1;
        }
    }

    class BinaryTree
    {
        public Node root = null;

        /////////////////////////////////////////////////////
        /// METHOD 1 (Use function to print a given level)
        /////////////////////////////////////////////////////


        /* function to print level order traversal of tree*/
        public void printLevelOrderF()
        {
            int h = root.GetHeight();
            int i;
            for (i = 0; i <= h; i++)
            {
                printGivenLevel(root, i);
                Console.WriteLine();                // Jak usuniemy to linijkie to wypisuje w jednej lini
            }
        }

        /* Print nodes at the given level */
        void printGivenLevel(Node root, int level)
        {
            if (root == null)
                return;
            if (level == 0)
                Console.Write(root.data + " ");
            else if (level > 0)
            {
                printGivenLevel(root.left, level - 1);
                printGivenLevel(root.right, level - 1);
            }
        }


        /////////////////////////////////////////////////////
        /// METHOD 2 (Use Queue)
        /////////////////////////////////////////////////////


        /* Given a binary tree. Print its nodes in level order
 using array for implementing queue  */
        public void printLevelOrderQ()
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                Node tempNode = queue.Dequeue();
                Console.Write(tempNode.data + " ");

                /*Enqueue left child */
                if (tempNode.left != null)
                {
                    queue.Enqueue(tempNode.left);
                }

                /*Enqueue right child */
                if (tempNode.right != null)
                {
                    queue.Enqueue(tempNode.right);
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Drzewo binarne\n");

            BinaryTree tree = new BinaryTree();
            tree.root = new Node(8);

            Node n1 = new Node(1);
            Node n2 = new Node(2);
            Node n3 = new Node(3);
            Node n4 = new Node(4);
            Node n5 = new Node(5);
            Node n6 = new Node(6);
            Node n7 = new Node(7);
            Node n8 = new Node(8);

            //Console.WriteLine(tree.root.GetHeight());

            n4.AddLeft(n3);
            n4.AddRight(n5);
            n2.AddLeft(n1);
            n2.AddRight(n4);
            n8.AddLeft(n7);
            n6.AddRight(n8);

            tree.root.AddLeft(n2);
            tree.root.AddRight(n6);

            Console.WriteLine("Pre-Order:");
            tree.root.ShowPreOrder();
            Console.WriteLine("\n");

            Console.WriteLine("Pre-Order without recursion (using stack)");
            tree.root.ShowPreOrderNotRecursion();
            Console.WriteLine("\n");

            Console.WriteLine("In-Order:");
            tree.root.ShowInOrder();
            Console.WriteLine("\n");

            Console.WriteLine("Wysokosc {0}", tree.root.GetHeight());
            Console.WriteLine();

            int valueToFind = 6;
            Console.WriteLine("Czy w drzewie jest wartosc {0}? => {1}", valueToFind, tree.root.Search(valueToFind));

            valueToFind = 10;
            Console.WriteLine("Czy w drzewie jest wartosc {0}? => {1}", valueToFind, tree.root.Search(valueToFind));
            Console.WriteLine();

            // bezposrednie rodzice danych elementow
            int[] arrayValuesToFind = { 3, 2, 5, 8, 7, 30 };
            foreach (var item in arrayValuesToFind)
            {
                Console.WriteLine("Rodzicem elementu {0} jest {1}", item, tree.root.FindParent(item));
            }

            Console.WriteLine("Level order traversal of binary tree is - ");

            Console.WriteLine();
            Console.WriteLine("METHOD 1 (Use function to print a given level)");
            tree.printLevelOrderF();

            Console.WriteLine();
            Console.WriteLine("METHOD 2 (Use Queue)");
            tree.printLevelOrderQ();

            Console.ReadKey();
        }
    }
}

