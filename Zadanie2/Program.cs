using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Zadanie 2
// Opracuj strukturę danych Zbiór opartą na liście dowiązaniowej.Napisz metodę Suma
// (mnogościowa) tworzącą listę z elementów dwóch list Napisz także metodę Iloczyn
// (mnogościowy) tworzącą listę z elementów wspólnych(o takiej samej wartości
// klucza) z dwóch list.Dodaj metodę Różnica(mnogościowa). Rozważ dwa przypadki
// listy nieuporządkowane i uporządkowane.Jaka jest złożoność opracowanych
// algorytmów(metod)?

namespace Zadanie2
{
    class Node
    {
        public int klucz;
        public Node next;

        public Node(int klucz)
        {
            this.klucz = klucz;
        }
    }


    /* Lista nieuporządkowana   -   Singly-Linked List
     * Dodawanie - O(1)
     * Contains - O(n)
     * Space Complexity     - O(n)
     * Dla tych wyżej średnie i najgorsze sa takie same
     * 
     * 
     * SumaMnogosciowa      - O(2n)     \
     * IloczynMnogosciowy   - O(2n)      |- Niech to ktos zweryfikuje
     * RóżnicaMnogosciowa   - O(2n)     /
     * */

    class Zbior
    {
        Node head;
        Node rear;

        public void AddFront(int klucz)
        {
            Node node = new Node(klucz);

            if (head == null)
            {
                head = node;
                rear = node;

                return;
            }

            node.next = head;
            head = node;
        }

        public void AddEnd(int klucz)
        {
            Node node = new Node(klucz);

            if (head == null)
            {
                head = node;
                rear = node;

                return;
            }

            rear.next = node;
            rear = node;
        }

        public override string ToString()
        {
            string result = "";
            Node tmp = head;

            if (tmp == null)
            {
                result = "PUSTO";
            }
            else
            {
                while (tmp != null)
                {
                    result += tmp.klucz + " ";
                    tmp = tmp.next;
                }
            }

            return result;
        }

        public bool Contains(int klucz)
        {
            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                if (tmp.klucz == klucz)
                {
                    return true;
                }
            }

            return false;
        }

        public static Zbior SumaMnogosciowa(Zbior z1, Zbior z2)
        {
            Zbior result = new Zbior();

            for (Node tmp = z1.head; tmp != null; tmp = tmp.next)
            {
                result.AddEnd(tmp.klucz);
            }

            for (Node tmp = z2.head; tmp != null; tmp = tmp.next)
            {
                if (!result.Contains(tmp.klucz))
                {
                    result.AddEnd(tmp.klucz);
                }
            }

            return result;
        }

        public static Zbior IloczynMnogosciowy(Zbior z1, Zbior z2)
        {
            Zbior result = new Zbior();

            for (Node tmp = z1.head; tmp != null; tmp = tmp.next)
            {
                if (z2.Contains(tmp.klucz))
                {
                    result.AddEnd(tmp.klucz);
                }
            }

            return result;
        }

        public static Zbior RóżnicaMnogosciowa(Zbior z1, Zbior z2)
        {
            Zbior result = new Zbior();

            for (Node tmp = z1.head; tmp != null; tmp = tmp.next)
            {
                if (!z2.Contains(tmp.klucz))
                {
                    result.AddEnd(tmp.klucz);
                }
            }

            return result;
        }
    }

    /* Lista uporządkowana   -   Singly-Linked List
     * Dodawanie - O(n) - najgorszy
     * Contains - O(n)  - najgorszy
     * Space Complexity     - O(n)
     * 
     * 
     * SumaMnogosciowa      - O(2n)     \
     * IloczynMnogosciowy   - O(2n)      |- Niech to ktos zweryfikuje
     * RóżnicaMnogosciowa   - O(2n)     /
     * 
     * !!!!!!!!!!!!
     * Sortowanie - O(1)
     * */
    class ZbiorUporządkowany
    {
        Node head;
        Node rear;

        public void Add(int klucz)
        {
            Node node = new Node(klucz);

            // gdy lista jest pusta
            if (head == null)
            {
                head = node;
                rear = node;

                return;
            }

            // sprawdzamy czy nie wstawić elementu przed wszystkimi elementami
            if (head.klucz >= klucz)
            {
                node.next = head;
                head = node;
                return;
            }

            // sprawdzamy czy nie wstawić elementu za wszystkimi elementami
            if (rear.klucz <= klucz)
            {
                rear.next = node;
                rear = node;
                return;
            }

            //szukamy odpowiedniego miejsca na wstawienie elementu
            Node tmp;
            for (tmp = head; tmp.next != rear; tmp = tmp.next)
            {
                if (tmp.next.klucz > klucz)
                {
                    break;
                }
            }

            node.next = tmp.next;
            tmp.next = node;
        }

        public override string ToString()
        {
            string result = "";
            Node tmp = head;

            if (tmp == null)
            {
                result = "PUSTO";
            }
            else
            {
                while (tmp != null)
                {
                    result += tmp.klucz + " ";
                    tmp = tmp.next;
                }
            }

            return result;
        }

        public bool Contains(int klucz)
        {
            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                if (tmp.klucz == klucz)
                {
                    return true;
                }
            }

            return false;
        }

        public static ZbiorUporządkowany SumaMnogosciowa(ZbiorUporządkowany zu1, ZbiorUporządkowany zu2)
        {
            ZbiorUporządkowany result = new ZbiorUporządkowany();

            Node tmpzu1 = zu1.head;
            Node tmpzu2 = zu2.head;

            while (tmpzu1 != null && tmpzu2 != null)
            {
                int min;

                if (tmpzu1.klucz < tmpzu2.klucz)
                {
                    min = tmpzu1.klucz;
                    tmpzu1 = tmpzu1.next;
                }
                else
                {
                    min = tmpzu2.klucz;
                    tmpzu2 = tmpzu2.next;
                }

                result.Add(min);
            }

            while (tmpzu1 != null)
            {
                result.Add(tmpzu1.klucz);
                tmpzu1 = tmpzu1.next;
            }

            while (tmpzu2 != null)
            {
                result.Add(tmpzu2.klucz);
                tmpzu2 = tmpzu2.next;
            }

            return result;
        }

        public static ZbiorUporządkowany IloczynMnogosciowy(ZbiorUporządkowany zu1, ZbiorUporządkowany zu2)
        {
            ZbiorUporządkowany result = new ZbiorUporządkowany();

            Node tmpzu1 = zu1.head;
            Node tmpzu2 = zu2.head;

            while (tmpzu1 != null && tmpzu2 != null)
            {
                if (tmpzu1.klucz == tmpzu2.klucz)
                {
                    result.Add(tmpzu1.klucz);
                    tmpzu1 = tmpzu1.next;
                    tmpzu2 = tmpzu2.next;
                }
                else if (tmpzu1.klucz < tmpzu2.klucz)
                {
                    tmpzu1 = tmpzu1.next;
                }
                else
                {
                    tmpzu2 = tmpzu2.next;
                }
            }

            return result;
        }

        public static ZbiorUporządkowany RóżnicaMnogosciowa(ZbiorUporządkowany zu1, ZbiorUporządkowany zu2)
        {
            ZbiorUporządkowany result = new ZbiorUporządkowany();

            Node tmpzu1 = zu1.head;
            Node tmpzu2 = zu2.head;

            while (tmpzu1 != null && tmpzu2 != null)
            {
                if (tmpzu1.klucz == tmpzu2.klucz)
                {
                    tmpzu1 = tmpzu1.next;
                    tmpzu2 = tmpzu2.next;
                }
                else if (tmpzu1.klucz < tmpzu2.klucz)
                {
                    result.Add(tmpzu1.klucz);
                    tmpzu1 = tmpzu1.next;
                }
                else
                {
                    tmpzu2 = tmpzu2.next;
                }
            }

            while (tmpzu1 != null)
            {
                result.Add(tmpzu1.klucz);
                tmpzu1 = tmpzu1.next;
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ZBIÓR NIEUPORZADKOWANY \n");

            Zbior z1 = new Zbior();
            Zbior z2 = new Zbior();

            z1.AddEnd(1);
            z1.AddEnd(2);
            z1.AddEnd(3);
            z1.AddEnd(4);

            z2.AddEnd(3);
            z2.AddEnd(4);
            z2.AddEnd(5);
            z2.AddEnd(6);


            Console.WriteLine("Zbior z1: " + z1);
            Console.WriteLine("Zbior z2: " + z2);

            Console.WriteLine("z1 U z2:  " + Zbior.SumaMnogosciowa(z1, z2));
            Console.WriteLine("z1 n z2:  " + Zbior.IloczynMnogosciowy(z1, z2));
            Console.WriteLine("z1 \\ z2:  " + Zbior.RóżnicaMnogosciowa(z1, z2));

            Console.WriteLine("\n ZBIÓR UPORZADKOWANY \n");

            ZbiorUporządkowany zu1 = new ZbiorUporządkowany();
            ZbiorUporządkowany zu2 = new ZbiorUporządkowany();

            zu1.Add(4);
            zu1.Add(1);
            zu1.Add(2);
            zu1.Add(3);

            zu2.Add(4);
            zu2.Add(3);
            zu2.Add(6);
            zu2.Add(5);


            Console.WriteLine("Zbior Uporzadkowany zu1: " + zu1);
            Console.WriteLine("Zbior Uporzadkowany zu2: " + zu2);

            Console.WriteLine("zu1 U zu2:  " + ZbiorUporządkowany.SumaMnogosciowa(zu1, zu2));
            Console.WriteLine("zu1 n zu2:  " + ZbiorUporządkowany.IloczynMnogosciowy(zu1, zu2));
            Console.WriteLine("zu1 \\ zu2:  " + ZbiorUporządkowany.RóżnicaMnogosciowa(zu1, zu2));

            Console.ReadKey();
        }
    }
}