using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDayTasksLibrary.Model
{
    public struct Region
    {
        public byte Id { get; set; }
        
        public string Title { get; set; }

        public ushort X { get; set; }

        public ushort Y { get; set; }

        public ushort Width { get; set; }

        public ushort Height { get; set; }
    }
}
