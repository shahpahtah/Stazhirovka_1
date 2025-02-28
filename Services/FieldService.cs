using Data.EF;
using Microsoft.EntityFrameworkCore;
using st_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FieldService:IFieldService
    {
        private readonly AppDbContext _context;

        public FieldService(AppDbContext context)
        {
            _context = context;
        }

        public  List<Field> GetAllCustomFieldDefinitions()
        {
            return  _context.Fields.ToList().Select(i=>Mapper.Map(i)).ToList();
        }

        public void CreateCustomFieldDefinition(Field customField)
        {
            _context.Add(Mapper.Map(customField));
             _context.SaveChangesAsync();
        }
    }
}

