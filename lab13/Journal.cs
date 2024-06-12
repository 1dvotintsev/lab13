using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CollectionLibrary;
using CustomLibrary;

namespace lab13
{
    public class JournalEntry
    {
        public string Id { get; set; }
        public string ChangeType { get; set; }
        public string Item {  get; set; }

        public JournalEntry(string id, string changetype, string item) 
        { 
            Id = id;
            ChangeType = changetype;
            Item = item;
        }

        public override string ToString()
        {
            return $"В коллекции {Id} произошло {ChangeType}, объект: {Item}";
        }
    }
    
    public class Journal
    {
        public List<JournalEntry> journal = new List<JournalEntry>();

        public void WriteRecord(object source, CollectionHandlerEventArgs args) 
        {
            JournalEntry record = new JournalEntry(((MyObserveCollection<Emoji>)source).Id, args.ChangeType, args.Obj);
            journal.Add(record);
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
