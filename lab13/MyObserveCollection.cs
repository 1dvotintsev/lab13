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
using System.Xml.Linq;

namespace lab13
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
    public class MyObserveCollection<T>: MyCollection<T> where T : IInit, ICloneable, IComparable, new()
    {
        public event CollectionHandler CollectionCountChanged;

        public event CollectionHandler CollectionReferenceChanged;

        private List<T> list = new List<T>();

        public MyObserveCollection(int length):base(length) 
        {
            ToSearchTree(this);
            SyncElements();
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= list.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return list[index];
            }
            set
            {
                if (index < 0 || index >= list.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                T oldValue = list[index];
                list[index] = value;

                // Обновляем дерево, заменяя старое значение на новое
                ReplaceInTree(oldValue, value);

                OnReferenceChanged(this, new CollectionHandlerEventArgs("изменение по индексу", value.ToString()));
            }
        }

        public void OnCountChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionCountChanged?.Invoke(this, args);
        }

        public void OnReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionReferenceChanged?.Invoke(this, args);
        }

        public new void Add(T item, bool ok = true)
        {
            base.Add(item);
            SyncElements();
            if(ok)
            {
                OnCountChanged(this, new CollectionHandlerEventArgs("добавление", item.ToString()));
            }
        }

        public new bool Remove(T data, bool ok = true)
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

            if (ok)
            {
                OnCountChanged(this, new CollectionHandlerEventArgs("удаление", current.Data.ToString()));
            }
            

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
            SyncElements();
            return true;
        }

        private void SyncElements()
        {
            list.Clear();
            foreach (var item in this)
            {
                list.Add(item);
            }
        }

        private void ReplaceInTree(T oldValue, T newValue)
        {
            // Удаляем старое значение
            Remove(oldValue, false);
            // Добавляем новое значение
            Add(newValue, false);
            // Синхронизируем элементы
            //SyncElements();
        }
    }
}
