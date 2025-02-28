using st_1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st_1
{
    public interface IFieldService
    {
        List<Field> GetAllCustomFieldDefinitions();
        void CreateCustomFieldDefinition(Field customField);
    }
}
