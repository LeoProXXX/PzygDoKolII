using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Drzewo BST odczytano w porządku pre-order i otrzymano 6, 3, 1, 2, 4, 5, 7. Czy
// możemy odtworzyć to drzewo? Napisz metodę odtwarzającą drzewo na podstawie
// odczytu pre-order, jeżeli drzewa nie można odtworzyć(dane są sprzeczne np. 6, 3,
// 1, 2, 4, 7, 5) metoda ma zwracać drzewo puste

/*
 * DrzewoBST
 *
 * Lista metod:
 *  - Show() - wyświetla jako pre-order z wcięciami
 *  - ShowPreOrder() - wyświetla jako pre-order z nawiasami (A(B(C)))
 *  - ShowPostOrder() - pokazuje post-order (ze spacjami)
 *  - ShowInOrder() - pokazuje in-order (ze spacjami)
 *  - ShowInOrderWithIndent - pokazuje in-order z wcięciami
 *  - Height() - Wysokość drzewa
 *  - Weight() - waga drzewa
 *
 *  - Insert() - wstawianie rekurencyjne
 *  - InsertIteratively() - wstawia iteracyjnie
 *  - Delete() - usuwa iteracyjnie podany węzeł
 *  - GetMaxElement() - zwraca najwiekszy element
 *  - GetMinElement() - zwraca najmniejszy element
 *  - Search() - szuka węzła o podanej wartości - rekurencyjnie
 *  - SearchIteratively() - szuka węzła iteracyjnie
 *
 *  - RightRotation() - (obrót) rotacja węzła w prawo - nie zmienia się IN-ORDER, zmiany można zaobserwować w pre-order
 *  - LeftRotation() - (obrót) rotacja węzła w lewo
 *  - InsertNewRoot() - wstawianie nowego węzła do korzenia przy wykorzystaniu obrotów
 *
 *  - FindSuccerssor() - szuka następnika, jeśli nie znajdzie zwraca podane value
 *  - FindPredecessor() - szuka poprzednika, jeśli nie znajdzie zwraca podane value
 *  
 * */

namespace Zadanie4
{
    class BST
    {
        class Node
        {
            public int value;
            public Node left;
            public Node right;

            public Node(int value)
            {
                this.value = value;
            }
        }

        Node root;

        public BST()
        {
            this.root = null;
        }

        #region wypisywanie
        // pre-order z wcięciem
        void Show(Node node, int level)
        {
            string indent = ""; // wciecie, akapit
            int p = level;
            while (p-- > 0)
            {
                indent += " ";
            }
            if (node == null)
            {
                Console.WriteLine(indent + "*");
            }
            else
            {
                Console.WriteLine(indent + node.value);
                if (node.left != null || node.right != null)
                {
                    Show(node.left, level + 1);
                    Show(node.right, level + 1);
                }
            }
        }

        // pre-order
        public void ShowPreOrder()
        {
            this.ShowPreOrder(this.root);
        }

        private void ShowPreOrder(Node node)
        {
            if (node == null)
            {
                return;
            }
            Console.Write("({0}", node.value);
            ShowPreOrder(node.left);
            ShowPreOrder(node.right);
            Console.Write(")");
        }

        // post-order
        public void ShowPostOrder()
        {
            this.ShowPostOrder(root);
        }

        private void ShowPostOrder(Node node)
        {
            if (node == null)
            {
                return;
            }
            ShowPostOrder(node.left);
            ShowPostOrder(node.right);
            Console.Write("({0} ", node.value);
        }

        // in-order
        public void ShowInOrder()
        {
            this.ShowInOrder(this.root);
        }

        private void ShowInOrder(Node node)
        {
            if (node == null)
            {
                return;
            }
            ShowInOrder(node.left);
            Console.Write("{0} ", node.value);
            ShowInOrder(node.right);
        }

        // in-oder z wcieciami
        public void ShowInOrderWithIndent()
        {
            ShowInOrderWithIndent(this.root, 0);
        }

        private void ShowInOrderWithIndent(Node node, int level)
        {
            string indent = ""; //wciecie, akapit
            int p = level; //poziom
            while (p-- > 0)
            {
                indent += " ";
            }
            if (node == null)
            {
                Console.WriteLine(indent + "*");
            }
            else
            {
                if (node.left != null || node.right != null)        //Czy napewno tak ???
                {
                    ShowInOrderWithIndent(node.left, level + 1);
                }
                Console.WriteLine(indent + node.value);
                if (node.left != null || node.right != null)
                {
                    ShowInOrderWithIndent(node.right, level + 1);
                }
            }
        }


        /* function to print level order traversal of tree*/
        public void printLevelOrderF()
        {
            int h = Height(); // lub int h = Height();
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
                Console.Write(root.value + " ");
            else if (level > 0)
            {
                printGivenLevel(root.left, level - 1);
                printGivenLevel(root.right, level - 1);
            }
        }


        #endregion



        #region duperele
        // Wysokosc drzewa
        public int Height()
        {
            return Height(this.root);
        }

        private int Height(Node node)
        {
            int height = 0;
            if (node == null)
            {
                return 0;
            }
            if (node.left != null)
            {
                return Math.Max(height, Height(node.left) + 1);
            }
            if (node.right != null)
            {
                return Math.Max(height, Height(node.right) + 1);
            }

            return height;
        }

        // Wstawianie rekurencyjne
        public void Insert(int value)
        {
            Node node = new Node(value);
            if (this.root == null)
            {
                this.root = node;
            }
            else
            {
                Insert(this.root, node);
            }
        }

        private void Insert(Node root, Node node)
        {
            if (root.value.CompareTo(node.value) > 0)
            {
                if (root.left == null)
                {
                    root.left = node;
                }
                else
                {
                    Insert(root.left, node);
                }
            }
            else
            {
                if (root.right == null)
                {
                    root.right = node;
                }
                else
                {
                    Insert(root.right, node);
                }
            }
        }

        // wstawianie iteracyjne
        public void InsertIteratively(int value)
        {
            if (this.root == null)
            {
                this.root = new Node(value);
            }
            else
            {
                Node temp = this.root;
                while (true)
                {
                    if (value.CompareTo(temp.value) < 0)
                    {
                        if (temp.left == null)
                        {
                            temp.left = new Node(value);
                            return;
                        }
                        else
                        {
                            temp = temp.left;
                        }
                    }
                    else
                    {
                        if (temp.right == null)
                        {
                            temp.right = new Node(value);
                            return;
                        }
                        else
                        {
                            temp = temp.right;
                        }
                    }
                }
            }
        }

        // Usuwa ITERACYJNIE podany wezel z drzew (metoda z wykladu, slabo napisana)
        public void Delete(int value)
        {
            if (this.root == null) return;
            Node temp = this.root;
            Node parent = null;

            while (temp != null)
            {
                if (temp.value.CompareTo(value) == 0)
                {
                    // tylko jedno dziecko, albo wcale
                    if (temp.left == null)
                    {
                        if (parent == null) // usuwamy korzeń
                        {
                            this.root = temp.right;
                        }
                        else
                        {
                            if (parent.left == temp) parent.left = temp.right;
                            else parent.right = temp.right;
                        }
                    }
                    else if (temp.right == null)
                    {
                        if (parent == null) // usuwamy korzeń
                        {
                            this.root = temp.left;
                        }
                        else
                        {
                            if (parent.left == temp) parent.left = temp.left;
                            else parent.right = temp.left;
                        }
                    }
                    else if (temp.left != null && temp.right != null)
                    {   //krok w lewo
                        Node qParent = temp;
                        Node q = temp.left;
                        while (q.right != null) // teraz do oporu w prawo
                        {
                            qParent = q;
                            q = q.right;
                        }
                        // Usuwamy q z jego miejsca, a na jego miejsce wstawiamy lewego potomka (moze byc null)
                        if (qParent.right == q) qParent.right = q.left;
                        else qParent.left = q.left;
                        // teraz q przenosimy na miejsce temp
                        if (parent == null) // usuwamy korzeń
                        {
                            this.root = q;
                        }
                        else
                        {
                            if (parent.left == temp) parent.left = q;
                            else parent.right = q;
                        }
                        // na koniec wstawiamy potomkow temp jako potomkow q
                        q.left = temp.left;
                        q.right = temp.right;
                    }
                    return;
                }
                parent = temp; // szukamy dalej
                if (temp.value.CompareTo(value) > 0) temp = temp.left;
                else temp = temp.right;
            }
            return; // nie zaleziono
        }

        // Zwraca najwiekszy element (Zad 5A lab 11)
        public int GetMaxElement()
        {
            return this.GetMaxElement(this.root).value;
        }

        private Node GetMaxElement(Node node)
        {
            Node tmp = node;

            while (tmp.right != null)
            {
                tmp = tmp.right;
            }

            return tmp;
        }

        // Szuka węzła o podanej wartości - REKURENCYJNIE
        public bool Search(int value)
        {
            return this.Search(this.root, value) != null;
        }

        private Node Search(Node root, int value)
        {
            if (root == null) return null;
            if (root.value.CompareTo(value) == 0) return root;
            if (root.value.CompareTo(value) < 0)
            {
                return this.Search(root.right, value);
            }
            else
            {
                return this.Search(root.left, value);
            }
        }

        // Zwraca najmniejszy element (Zad 5B lab11)
        public int GetMinElement()
        {
            return this.GetMinElement(this.root).value;
        }

        private Node GetMinElement(Node node)
        {
            Node tmp = node;
            while (tmp.left != null)
            {
                tmp = tmp.left;
            }
            return tmp;
        }

        // Szuka węzła o podanej wartości - ITERACYJNIE (zad 5C lab 11)
        public bool SearchIteratively(int value)
        {
            if (root == null) return false;
            Node temp = this.root;
            while (true)
            {
                if (temp.value.CompareTo(value) == 0) return true;
                if (temp.value.CompareTo(value) < 0)
                {
                    if (temp.right == null) return false;
                    temp = temp.right;
                }
                else
                {
                    if (temp.left == null) return false;
                    temp = temp.left;
                }
            }
        }
        #endregion

        #region rotacje
        // pobiera wartosc wezle, znajduje go w drzewie i wykonuje rotacje w PRAWO
        public void RightRotation(int value)
        {
            Node parent = this.FindParent(value);
            Node node = Search(this.root, value);
            Node tmp = node.left;
            node.left = tmp.right;
            tmp.right = node;
            node = tmp;

            // sprawdzanie, do ktorej "odnogi" musimy przyłaczyc wezel
            if (parent != null)
            {
                bool isRightChild = parent.right.value.CompareTo(value) == 0;
                if (isRightChild)
                {
                    parent.right = node;
                }
                else
                {
                    parent.left = node;
                }
            }
        }

        // pobiera wartość węzła, znajduje go w drzewie i wykonuje rotację w LEWO
        public void LeftRotation(int value)
        {
            Node parent = this.FindParent(value);
            Node node = Search(this.root, value);
            Node temp = node.right;
            node.right = temp.left;
            temp.left = node;
            node = temp;

            if (parent != null)
            {
                bool isRightChild = parent.right.value.CompareTo(value) == 0;
                if (isRightChild)
                {
                    parent.right = node;
                }
                else
                {
                    parent.left = node;
                }
            }
        }
        #endregion

        // Wstawia nowy korzeń przy pomocy obrotów
        // nie działa - sposób z wykładu
        public void InserNewRoot(int value)
        {
            Node newNode = new Node(value);
            InsertNewRoot(this.root, newNode);
        }

        private void InsertNewRoot(Node węzeł, Node nowy)
        {
            if (węzeł == null)
            {
                węzeł = nowy;
                return;
            }

            if (węzeł.value.CompareTo(nowy.value) > 0)
            {
                InsertNewRoot(węzeł.left, nowy);
                RightRotation(węzeł.value);
            }
            else
            {
                InsertNewRoot(węzeł.right, nowy);
                RightRotation(węzeł.value);
            }
            return;
        }

        public static bool canRepresentBST(int[] pre)
        {
            int n = pre.Length;
            // Create an empty stack
            Stack<int> s = new Stack<int>();

            // Initialize current root as minimum possible
            // value
            int root = int.MinValue;

            // Traverse given array
            for (int i = 0; i < n; i++)
            {
                // If we find a node who is on right side
                // and smaller than root, return false
                if (pre[i] < root)
                {
                    return false;
                }

                // If pre[i] is in right subtree of stack top,
                // Keep removing items smaller than pre[i]
                // and make the last removed item as new
                // root.
                while (s.Count > 0 && s.Peek() < pre[i])
                {
                    root = s.Peek();
                    s.Pop();
                }

                // At this point either stack is empty or
                // pre[i] is smaller than root, push pre[i]
                s.Push(pre[i]);
            }
            return true;
        }

        #region  poprzedniki i nastepniki
        // szuka następnika w drzewie - jeśli nie znajdzie zwraca podane value
        public int FindSuccerssor(int value)
        {
            Node node = this.Search(this.root, value);
            if (node.right != null)
            {
                return this.GetMinElement(node.right).value;
            }
            Node parent = this.FindParent(value);
            while (parent != null && parent.left != null && parent.left.value.CompareTo(node.value) != 0)
            {
                node = parent;
                parent = this.FindParent(parent.value);
            }

            if (parent == null) return value;
            else return parent.value;
        }

        // Szuka poprzednika  w drzewie - jeśli nie znajdzie zwraca podane value
        public int FindPredecessor(int value)
        {
            Node node = this.Search(this.root, value);
            if (node.left != null)
            {
                return this.GetMaxElement(node.left).value;
            }
            Node parent = this.FindParent(value);
            while (parent != null && parent.right != node)
            {
                node = parent;
                parent = this.FindParent(parent.value);
            }

            if (parent == null) return value;
            else return parent.value;
        }

        // metoda pomocnicza do poprzednika i następnika - znajduje rodzica danego węzła
        private Node FindParent(int childrenValue)
        {
            return FindParent(this.root, childrenValue);
        }

        private Node FindParent(Node node, int childrenValue) // metoda reukurencyjna, pomocnicza do FindParent(T childrenValue)
        {
            if (node.value.CompareTo(childrenValue) == 0) return null; // zabezpieczenie przed "nieznalezieniem dziecka"
            if (node.left == null && node.right == null)
                return null;

            if ((node.left != null && node.left.value.CompareTo(childrenValue) == 0)
                || (node.right != null && node.right.value.CompareTo(childrenValue) == 0))
                return node;

            if (node.value.CompareTo(childrenValue) < 0)
            {
                return this.FindParent(node.right, childrenValue);
            }
            else
            {
                return this.FindParent(node.left, childrenValue);
            }
        }
        #endregion

        ///////////////////////////////////////////////////////
        /// Method 1 ( O(n^2) time complexity )
        ///////////////////////////////////////////////////////
        // A recursive function to construct Full from pre[]. preIndex is used
        // to keep track of index in pre[].
        static Node constructTreeUtil(int[] pre, int low, int high)
        {

            // Base case
            if (low >= pre.Length || low > high)
            {
                return null;
            }

            // The first node in preorder traversal is root. So take the node at
            // preIndex from pre[] and make it root, and increment preIndex
            Node root = new Node(pre[low]);

            // If the current subarry has only one element, no need to recur
            if (low == high)
            {
                return root;
            }

            // Search for the first element greater than root
            int i;
            for (i = low; i <= high; ++i)
            {
                if (pre[i] > root.value)
                {
                    break;
                }
            }

            // Use the index of element found in preorder to divide preorder array in
            // two parts. Left subtree and right subtree
            root.left = constructTreeUtil(pre, low + 1, i - 1);
            root.right = constructTreeUtil(pre, i, high);

            return root;
        }

        public static BST constructTree(int[] pre)
        {
            BST bst = new BST();
            if (BST.canRepresentBST(pre))
            {
                bst.root = constructTreeUtil(pre, 0, pre.Length - 1);
            }

            return bst;
        }
    }

    class Program
    {
        public static BST CreateTreeFromArray(int[] array)
        {
            BST tree = new BST();

            foreach (var item in array)
            {
                tree.Insert(item);
            }
            return tree;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Drzewo BST");

            //int[] array0 = new int[] { 11, 15, 6, 8, 5, 1, 7, 13, 17, 14 };
            //BST drzewo0 = CreateTreeFromArray(array0);
            //Console.WriteLine("In order:");
            //drzewo0.ShowInOrder();
            //Console.WriteLine();
            //Console.WriteLine("Pre-order: ");
            //drzewo0.ShowPreOrder();
            //Console.WriteLine();
            //drzewo0.LeftRotation(15);
            //Console.WriteLine("In order bez zmian");
            //drzewo0.ShowInOrder();
            //Console.WriteLine();
            //Console.WriteLine("Pre-order ze zmianami");
            //drzewo0.ShowPreOrder();


            //Console.WriteLine();

            //int[] array1 = new int[] { 16, 10, 6, 21, 20, 18, 13, 14, 17, 4, 11 };
            //BST drzewo1 = CreateTreeFromArray(array1);


            //int[] array2 = new int[] { 10, 16, 12, 7, 9, 2, 21, 6, 17, 1, 15 };
            //BST drzewo2 = CreateTreeFromArray(array2);


            int[] pre1 = new int[] { 10, 5, 1, 7, 40, 50 };
            int[] pre2 = new int[] { 6, 3, 1, 2, 4, 5, 7 };
            int[] pre3 = new int[] { 6, 3, 1, 2, 4, 7, 5 }; // Nieprawdilowa

            BST tree1 = BST.constructTree(pre1);
            BST tree2 = BST.constructTree(pre2);
            BST tree3 = BST.constructTree(pre3);

            Console.WriteLine("Podana tablica");
            Console.WriteLine(string.Join(" ", pre1));

            Console.Write("canRepresentBST ?: ");
            Console.WriteLine(BST.canRepresentBST(pre1));

            Console.WriteLine("Pre-order");
            tree1.ShowPreOrder();

            Console.WriteLine();
            Console.WriteLine("Inorder");

            tree1.ShowInOrder();
            Console.WriteLine();

            Console.WriteLine("Level order traversal");

            tree1.printLevelOrderF();
            Console.WriteLine();





            Console.WriteLine("Podana tablica");
            Console.WriteLine(string.Join(" ", pre2));

            Console.Write("canRepresentBST ?: ");
            Console.WriteLine(BST.canRepresentBST(pre2));

            Console.WriteLine("Pre-order");
            tree2.ShowPreOrder();

            Console.WriteLine();
            Console.WriteLine("Inorder");

            tree2.ShowInOrder();
            Console.WriteLine();

            Console.WriteLine("Level order traversal");

            tree2.printLevelOrderF();
            Console.WriteLine();





            Console.WriteLine("Podana tablica - ta jest niepoprawna");
            Console.WriteLine(string.Join(" ", pre3));

            Console.Write("canRepresentBST ?: ");
            Console.WriteLine(BST.canRepresentBST(pre3));

            Console.WriteLine("Pre-order");
            tree3.ShowPreOrder();

            Console.WriteLine();
            Console.WriteLine("Inorder");

            tree3.ShowInOrder();
            Console.WriteLine();

            Console.WriteLine("Level order traversal");

            tree3.printLevelOrderF();
            Console.WriteLine();




            Console.ReadKey();
        }
    }
}
