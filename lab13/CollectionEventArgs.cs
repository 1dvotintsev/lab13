using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13
{
    public class CollectionEventArgs : EventArgs
    {
        public string Id { get; set; }

        public CollectionEventArgs(string id)
        {
            Id = id;
        }
    }
}
