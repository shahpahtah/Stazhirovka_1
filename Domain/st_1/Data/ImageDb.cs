using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st_1.Data
{
    public class ImageDb
    {
        [Key]
        [ForeignKey("UserDb")]
        public Guid UserId { get; set; }
        public string? FileName { get; set; } = string.Empty;
    }
}
