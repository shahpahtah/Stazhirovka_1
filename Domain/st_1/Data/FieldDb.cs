using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st_1.Data
{
    public class FieldDb
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // Название поля (например, "Любимый цвет")
        public string DataType { get; set; }
    }
}
