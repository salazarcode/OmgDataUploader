using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DataFile
    {
        public int DataFileID { get; set; }
        public DataProvider DataProvider { get; set; }
        public string AbsolutePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
