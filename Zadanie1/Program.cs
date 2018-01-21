using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Zadanie 1
// Chcemy reprezentować wielomiany w postaci listy jednokierunkowej par
// (współczynnik; wykładnik). Uwaga: przechowujemy tylko współczynniki niezerowe.
// Napisz zestaw metod do tworzenia takich wielomianów na podstawie tablicy, a także
// dodawania i odejmowania takich wielomianów oraz różniczkowania.
// Np.tworzenie z tablicy { 1,2,5,0,0,8}
// oznacza 8x^5 + 5x^2 + 2x + 1. Odpowiednia lista
// ma mieć postać(8;5)->(5;2)->(2;1)->(1;0)
namespace Zadanie1
{
    class Node
    {
        public int wspolczynnik;
        public int wykladnik;
        public Node next;

        public Node(int wspolczynnik, int wykladnik)
        {
            this.wspolczynnik = wspolczynnik;
            this.wykladnik = wykladnik;
        }
    }

    class ListaJednokierunkowa
    {
        Node head;
        Node rear;

        // FUNKCJA STATYCZNA - tworzy wielomian z tablicy
        public static ListaJednokierunkowa CreateFromArray(int[] data)
        {
            ListaJednokierunkowa result = new ListaJednokierunkowa();

            for (int wykladnik = 0; wykladnik < data.Length; wykladnik++)
            {
                int wspolczynnik = data[wykladnik];
                if (wspolczynnik == 0)
                {
                    continue;
                }
                result.AddFront(data[wykladnik], wykladnik);
            }
            return result;
        }

        // dodaje element na początku (przed wszystkimi innymi)
        private void AddFront(int wspolczynnik, int wykladnik)
        {
            if (head == null || rear == null)
            {
                this.AddFirstElement(wspolczynnik, wykladnik);
                return;
            }

            Node newNode = new Node(wspolczynnik, wykladnik);
            newNode.next = head;
            head = newNode;
            return;
        }

        // dodaje element na końcu (po wszystkich elementach)
        public void AddEnd(int wspolczynnik, int wykladnik)
        {
            if (head == null || rear == null)
            {
                this.AddFirstElement(wspolczynnik, wykladnik);
                return;
            }
            Node newNode = new Node(wspolczynnik, wykladnik);
            rear.next = newNode;
            rear = newNode;
            return;
        }

        // funkcja prywatna, tylko do uzytku wewnętrznego - dodaje pierwszy element (tzn. gdy lista byla wczesniej pusta)
        public void AddFirstElement(int wspolczynnik, int wykladnik)
        {
            Node newNode = new Node(wspolczynnik, wykladnik);
            head = newNode;
            rear = newNode;
        }

        //zwraca rozmiar listy ilosc elemento
        public int GetSize()
        {
            Node tmp = head;
            int counter = 0;
            while (tmp != null)
            {
                tmp = tmp.next;
                counter++;
            }
            return counter;
        }

        // wyswietla elementy listy w postaci (wspolczynnik ->wykladnik)
        public void Show()
        {
            Console.WriteLine();
            if (head == null)
            {
                Console.WriteLine("PUSTO");
            }

            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                Console.Write("({0};{1})", tmp.wspolczynnik, tmp.wykladnik);
                if (tmp.next != null)
                {
                    Console.Write("->");
                }
            }
            Console.WriteLine();
        }

        // Dodaje do wielomianu drugi wielomian
        public void Dodaj(ListaJednokierunkowa w2)
        {
            //przechodzimy po w2
            for (Node tmp = w2.head; tmp != null; tmp = tmp.next)
            {
                if (this.DejMnieWspolczynnik(tmp.wykladnik) != 0) // sumowanie
                {
                    this.DodajDoWspolczynnika(tmp.wykladnik, tmp.wspolczynnik);
                }
                else //dodawanie nowego
                {
                    this.DodajNowyWykladnik(tmp.wykladnik, tmp.wspolczynnik);
                }
            }
        }

        //Odejmuje od wielomianu drugi wielomian
        public void Odejmij(ListaJednokierunkowa w2)
        {
            // przechodzimy po w2
            for (Node tmp = w2.head; tmp != null; tmp = tmp.next)
            {
                if (this.DejMnieWspolczynnik(tmp.wykladnik) != 0)
                {
                    this.DodajDoWspolczynnika(tmp.wykladnik, tmp.wspolczynnik * -1); // tricky mnożenie * -1
                }
                else // dodawanie nowego
                {
                    this.DodajNowyWykladnik(tmp.wykladnik, tmp.wspolczynnik * -1);
                }
            }
        }

        // Rozniczkuje wielomian
        public void Rozniczkoj()
        {
            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                tmp.wspolczynnik *= tmp.wykladnik;
                tmp.wykladnik -= 1;
                if (tmp.wykladnik < 0)
                {
                    this.Delete(tmp.wykladnik);
                }
            }
        }

        private int DejMnieWspolczynnik(int wykladnik)
        {
            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                if (tmp.wykladnik == wykladnik)
                {
                    return tmp.wspolczynnik;
                }
            }
            return 0;
        }

        private void DodajDoWspolczynnika(int wykladnik, int doDodania)
        {
            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                if (tmp.wykladnik == wykladnik)
                {
                    tmp.wspolczynnik += doDodania;
                    if (tmp.wspolczynnik == 0) // to trzeba dziada usunac
                    {
                        this.Delete(wykladnik);
                    }
                }
            }
        }

        private void DodajNowyWykladnik(int wykladnik, int wspolczynnik)
        {
            if (wykladnik > head.wykladnik)
            {
                AddFront(wspolczynnik, wykladnik);
                return;
            }

            if (wykladnik < rear.wykladnik)
            {
                AddEnd(wspolczynnik, wykladnik);
                return;
            }

            // dodaje pomiedzy
            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                if (tmp.next.wykladnik < wykladnik)
                {
                    Node node = new Node(wspolczynnik, wykladnik);
                    node.next = tmp.next;
                    tmp.next = node;

                    return;
                }
            }
        }

        // usuwa podany wykladnik
        private void Delete(int wykladnik)
        {
            // usuwanie glowy
            if (this.head.wykladnik == wykladnik)
            {
                head = head.next;
                return;
            }

            // usuwanie ogona
            if (this.rear.wykladnik == wykladnik)
            {
                Node tmp = head;

                while (tmp.next != rear)
                {
                    tmp = tmp.next;
                }

                tmp.next = null;
                rear = tmp;

                return;
            }

            for (Node tmp = head; tmp != null; tmp = tmp.next)
            {
                // usuwanie wezla pomiedzy
                if (tmp.next.wykladnik == wykladnik)
                {
                    tmp.next = tmp.next.next;
                    return;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = { 1, 2, 5, 0, 0, 8 };
            int[] arr2 = { 3, 0, 4, 6, 0, 0, 10 };
            int[] arr3 = { 4, 0, 5, 6, 3, 4 };
            ListaJednokierunkowa wielomian1 = ListaJednokierunkowa.CreateFromArray(arr1);
            ListaJednokierunkowa wielomian2 = ListaJednokierunkowa.CreateFromArray(arr2);
            ListaJednokierunkowa wielomian3 = ListaJednokierunkowa.CreateFromArray(arr3);
            wielomian1.Show();
            Console.WriteLine();
            wielomian2.Show();
            Console.WriteLine();

            wielomian1.Dodaj(wielomian2);
            wielomian1.Show();

            wielomian1.Odejmij(wielomian3);
            wielomian1.Show();

            Console.WriteLine();
            Console.Write("Przed rozniczka:");
            wielomian2.Show();
            Console.Write("Po rozniczkowaniu:");
            wielomian2.Rozniczkoj();
            wielomian2.Show();

            Console.ReadKey();
        }
    }
}
