using CollectionLibrary;
using CustomLibrary;
using System.Runtime.CompilerServices;

namespace lab13
{
    public class Program
    {
        static void Main(string[] args)
        {
            string menu = "Выберете одно из действий над коллекцией:\n\n1)Добавить случайный элемент\n2)Удалить элемент с заданным именем\n3)Пересоздать элемент с заданным именем\n4)Распечатать коллекцию\n5)Распечатать журнал изменений";

            int answer = 0;
            int current = 0;
            bool check = true;

            List<MyObserveCollection<Emoji>> list = new List<MyObserveCollection<Emoji>>();
            List<Journal> journals = new List<Journal>();

            MyObserveCollection<Emoji> collection1 = new MyObserveCollection<Emoji>(10);
            list.Add(collection1);
            MyObserveCollection<Emoji> collection2 = new MyObserveCollection<Emoji>(10);
            list.Add(collection2);

            Journal journal1 = new Journal();
            journals.Add(journal1);
            Journal journal2 = new Journal();
            journals.Add(journal2);

            collection1.CollectionCountChanged += journal1.WriteAdd;
            collection1.CollectionCountMinus += journal1.WriteDelete;
            collection1.CollectionReferenceChanged += journal1.WriteSet;

            collection2.CollectionCountChanged += journal2.WriteAdd;
            collection2.CollectionCountMinus += journal2.WriteDelete;
            collection2.CollectionReferenceChanged += journal2.WriteSet;

            while (true)
            {

                Console.WriteLine("Данная програма демнстрирует работу событий в коллекции типа бинарного дерева. На данный момент добавлено 2 дерева. Вы можете осуществлять над ними действия и проверить их отображения в журнале изменений\n");

                Console.WriteLine("Выберете над какой коллекцией вы хотите осуществлять действия:\n1) Коллекция 0\n2) Коллекция 1");

                current = ChooseAnswer(1, 2) - 1;

                Console.Clear();
                Console.WriteLine(menu);
                answer = ChooseAnswer(1, 5);
                switch (answer)
                {
                    case 1:
                        Console.Clear();

                        while (check)
                        {
                            try
                            {
                                Emoji emoji2 = new Emoji();
                                emoji2.RandomInit();

                                list[current].Add(emoji2);
                                check = false;
                            }
                            catch
                            {
                                check = true;
                            }
                        }
                        check = true;
                        Console.WriteLine("Элемент добавлен");
                        break;

                    case 2:
                        Console.Clear();

                        try
                        {
                            Emoji emoji1 = new Emoji();
                            emoji1.RandomInit();
                            Console.WriteLine("Введите имя удаляемого объекта:");
                            string newName = Console.ReadLine();
                            emoji1.Name = newName;

                            if (list[current].Remove(emoji1))
                            {
                                Console.WriteLine("Объект удален");
                            }
                            else
                            {
                                Console.WriteLine("Такого объекта нет");
                            }
                        }
                        catch
                        {
                            
                        }
                        break;

                    case 3:
                        Console.Clear();

                        Emoji emoji = new Emoji();
                        emoji.RandomInit();
                        Console.WriteLine("Введите имя объекта для его пересоздания:");
                        string name = Console.ReadLine();
                        emoji.Name = name;

                        Emoji temp = new Emoji();
                        temp.RandomInit();

                        if (list[current].Contains(emoji))
                        {
                            list[current].UpdateNodeData(list[current].Find(emoji), temp);
                            Console.WriteLine("Данные изменены");
                        }
                        else
                        {
                            Console.WriteLine("Такого объекта нет");
                        }

                        break;

                    case 4:
                        Console.Clear();

                        list[current].Show(list[current].root);

                        break;

                    case 5:
                        Console.Clear();
                        journals[current].PrintJournal();
                        break;

                    default:
                        Console.Clear();


                        break;
                }

            }
        }
        static int ChooseAnswer(int a, int b)   //выбор действия из целых
        {
            int answer = 0;
            bool checkAnswer;
            do
            {
                checkAnswer = int.TryParse(Console.ReadLine(), out answer);
                if ((answer > b || answer < a) || (!checkAnswer))
                {
                    Console.WriteLine("Вы некорректно ввели число, повторите ввод еще раз. Обратите внимание на то, что именно нужно ввести.");
                }
            } while ((answer > b || answer < a) || (!checkAnswer));

            return answer;
        }
    }
}
