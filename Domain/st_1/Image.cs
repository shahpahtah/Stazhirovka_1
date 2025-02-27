using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st_1
{
    public class Image
    {
        private Image(string filename)
        {
            FileName = filename;
        }
        public Guid UserId { get; set; }
        public string? FileName { get; set; } =string.Empty;
        public static Image Create(string? filename)
        {
            return new Image(filename);
        }
    }
}
