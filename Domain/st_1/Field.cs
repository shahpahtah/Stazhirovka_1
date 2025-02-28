using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace st_1
{
    public class Field
    {
        private Field(Guid id, string name,string datatype)
        {
            Id = id;
            Name = name;
            DataType = datatype;

        }
        public Guid Id { get; }
        public string Name { get; } // Название поля (например, "Любимый цвет")
        public string DataType { get;}
        public static Field Create(Guid id,string name,string datatype)
        {
            return new Field(id, name, datatype);
        }
    }
}
