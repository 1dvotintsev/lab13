using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CollectionLibrary;

namespace lab13
{
    public class Journal
    {
        List<string> journal = new List<string>();

        public void WriteAdd(object source, CollectionEventArgs args)
        {
            journal.Add($"В коллекцию {args.Id} был добавлен узел");
        }

        public void WriteDelete(object source, CollectionEventArgs args)
        {
            journal.Add($"В коллекции {args.Id} был удален узел");
        }

        public void WriteSet(object source, NodeEventArgs args)
        {
            journal.Add($"В коллекции {args.Id} был изменен узел");
        }

        public void PrintJournal()
        {
            if(journal.Count > 0)
            {
                foreach(var item in journal)
                {
                    Console.WriteLine(item);
                }
            }
            else 
            {
                Console.WriteLine("Журнал пустой");
            }
        }
    }
}
