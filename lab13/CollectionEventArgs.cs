using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13
{
    public class CollectionHandlerEventArgs : EventArgs
    {        
        public string ChangeType { get; set; }
        public string Obj {  get; set; }

        public CollectionHandlerEventArgs(string changeType, string obj)
        {
            ChangeType = changeType;
            Obj = obj;
        }
    }
}
