using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Quest_RIT
{
    public class Program
    {
        /// <summary>
        /// Т.к. в задании нет некоторых пояснений о таком списке, определю их сам.
        /// 1) Список может быть пустым
        /// 2) Значения в списке могут повторяться
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class SinglylinkedList<T>
        {
            public ListNode<T> Head;
            /// <summary>
            /// Добавление в список
            /// </summary>
            /// <param name="Value">Значение, которое будет добавлено в список</param>
            public void Add(T Value)
            {
                //Если список пустой, то головой данного списка будет являться 1 добавленный элемент
                if (Head == null)
                {
                    Head = new ListNode<T>(Value);
                }
                else
                {
                    ListNode<T> NewNode = new ListNode<T>(Value);
                    Head = AddRecursion(Head, NewNode);
                }
            }
            /// <summary>
            /// Замена элемента в списке
            /// </summary>
            /// <param name="Index">Индекс элемента</param>
            /// <param name="Value">Значение, которое на которое будет заменено</param>
            /// <exception cref="Exception">Список пуст или Данного индекса нет в списке</exception>
            public void Replace(int Index, T Value)
            {
                if (Head == null) { throw new Exception("Список пуст"); }
                else
                {
                    if (Index > Count - 1) { throw new Exception("Данного индекса нет в списке"); }
                    Head = ReplaceRecursion(Head, Index, Value, 0);
                }
            }
            /// <summary>
            /// Объединение 2 списков
            /// </summary>
            /// <param name="List">Список который будет присоединён</param>
            public void Extend(SinglylinkedList<T> List)
            {
                //Объединение это значит, что к концу 1 списка нужно в качестве следующего
                //поставить голову 2 списка
                this.Add(List.Head);
            }
            public void Print()
            {
                var CurrentNode = Head;
                while (CurrentNode != null)
                {
                    Console.Write($"{CurrentNode.Value} -> ");
                    CurrentNode = CurrentNode.Next;
                }
                Console.Write("\n");
            }

            public int Count
            {
                get
                {
                    int count = 0;
                    var CurrentNode = Head;
                    while (CurrentNode != null)
                    {
                        count++;
                        CurrentNode = CurrentNode.Next;
                    }
                    return count;
                }
                private set { Count = value; }
            }
            private ListNode<T> ReplaceRecursion(ListNode<T> CurrentNode, int Index, T Value, int RecursionCount)
            {
                ListNode<T> NewList = null;
                //Дойдём до конца списка
                if (CurrentNode.Next != null)
                {
                    NewList = ReplaceRecursion(CurrentNode.Next, Index, Value, RecursionCount + 1);
                }
                //Если текущий индекс рекурсии равен индексу элемента который мы должны заменить
                //То заменяем его
                if (Index == RecursionCount)
                {
                    NewList = new ListNode<T>(Value, NewList);
                }
                //Иначе копируем
                else
                {
                    NewList = new ListNode<T>(CurrentNode.Value, NewList);
                }
                return NewList;
            }
            private void Add(ListNode<T> Node)
            {
                //Если список пустой, то головой данного списка будет являться 1 добавленный элемент
                if (Head == null)
                {
                    Head = Node;
                }
                else
                {
                    Head = AddRecursion(Head, Node);
                }
            }
            private ListNode<T> AddRecursion(ListNode<T> CurrentNode, ListNode<T> Node)
            {
                ListNode<T> NewList = null;
                //Дойдём до конца списка
                if (CurrentNode.Next != null)
                {
                    NewList = AddRecursion(CurrentNode.Next, Node);
                }
                //Если мы в конце
                else
                {
                    NewList = new ListNode<T>(CurrentNode.Value, Node);
                    return NewList;
                }
                NewList = new ListNode<T>(CurrentNode.Value, NewList);
                return NewList;
            }
        }
        public class ListNode<T>
        {
            public readonly T Value;
            public readonly ListNode<T> Next;
            public ListNode(T Value)
            {
                this.Value = Value;
            }
            //Readonly мы можем менять поле либо в конструкторе, либо непосредственно при определении
            //Добавим конструктор, в котором мы можем создать такой элемент, в котором мы можем задать Next;
            public ListNode(T Value, ListNode<T> Next)
            {
                this.Value = Value;
                this.Next = Next;
            }
        }
        static void Main(string[] args)
        {
            //Небольшой набор тестов, как требуется в задании
            //Т.к. в задании требуется публикация проекта на онлайн-инструменте, то не получится сделать unit тесты

            //Создание списка
            Console.WriteLine("Создание списка: ");
            SinglylinkedList<int> List1 = new SinglylinkedList<int>();
            List1.Add(1);
            List1.Add(2);
            List1.Add(3);
            List1.Print();

            //Замена 2 элемента списка; (Нумерация с 0)
            Console.WriteLine("Замена 2 элемента списка на значение 4: ");
            List1.Replace(2, 4);
            List1.Print();

            //Создание 2 списка
            Console.WriteLine("Создание списка2: ");
            SinglylinkedList<int> List2 = new SinglylinkedList<int>();
            List2.Add(5);
            List2.Add(6);
            List2.Add(7);
            List2.Print();

            //Объединение 1 со 2 списком
            Console.WriteLine("Объединение 1 списка со 2");
            List1.Extend(List2);
            List1.Print();
        }
    }
}
