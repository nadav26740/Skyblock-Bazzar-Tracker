using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock_Bazzar_Tracker
{
    public class JsonSaveFile
    {
        public DateTime CreationTime { get; set; }
        public List<Product> Products { get; set; } 
    }
}
