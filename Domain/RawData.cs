using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RawData
    {
        public int RawDataID { get; set; }
        public DataFile DataFile { get; set; }
        public string TextLine { get; set; }
    }
}
