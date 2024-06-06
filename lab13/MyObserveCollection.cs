using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionLibrary;
using CustomLibrary;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab13
{
    public delegate void CollectionHandler(object source, CollectionEventArgs args);
    public class MyObserveCollection<T>: MyCollection<T> where T : IInit, ICloneable, IComparable, new()
    {
        public event CollectionHandler CollectionCountChanged;

        public event CollectionHandler CollectionCountMinus;

        public MyObserveCollection(int length):base(length) 
        {
            ToSearchTree(this);
        }

        public void OnAddItem(object source, CollectionEventArgs args)
        {
            CollectionCountChanged?.Invoke(this, args);
        }

        public void OnDeleteItem(object source, CollectionEventArgs args)
        {
            CollectionCountMinus?.Invoke(this, args);
        }

        public new void Add(T item)
        {
            base.Add(item);
            OnAddItem(this, new CollectionEventArgs(Id));
        }

        public new bool Remove(T data)
        {
            Node<T>? parent = null;
            Node<T>? current = root;

            // Поиск узла для удаления и его родителя
            while (current != null)
            {
                int comparison = data.CompareTo(current.Data);
                if (comparison == 0)
                    break;

                parent = current;
                if (comparison < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }

            // Узел для удаления не найден
            if (current == null)
                return false;

            OnDeleteItem(this, new CollectionEventArgs(Id));

            // Если у узла есть два потомка
            if (current.Left != null && current.Right != null)
            {
                // Находим наименьший узел в правом поддереве
                Node<T> minRight = current.Right;
                Node<T>? parentMinRight = current;

                while (minRight.Left != null)
                {
                    parentMinRight = minRight;
                    minRight = minRight.Left;
                }

                // Заменяем удаляемый узел на наименьший узел в правом поддереве
                current.Data = minRight.Data;

                // Переопределяем текущий узел и его родителя для последующего удаления
                current = minRight;
                parent = parentMinRight;
            }

            // Если у узла нет детей или только один ребенок
            Node<T>? child = (current.Left != null) ? current.Left : current.Right;

            // Удаляем узел из дерева
            if (parent == null)
                root = child;
            else if (parent.Left == current)
                parent.Left = child;
            else
                parent.Right = child;

            count--;
            return true;
        }

    }
}
