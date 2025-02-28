using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st_1.Data
{
    public class UserDb
    {
        public string? Password { get; set; }
        public string  Email { get; set; }
        [Key]
        public Guid Id { get; set; }
        public string? NickName { get; set; }
        public ImageDb? Avatar { get; set; }
        public string? KnowledgeJson { get; set; }
        [NotMapped]
        public List<string?> Knowledge
        {
            get => string.IsNullOrEmpty(KnowledgeJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(KnowledgeJson);
            set => KnowledgeJson = JsonConvert.SerializeObject(value);
        }
        public string? Gender { get; set; }
        public DateTime? DateAndTimeOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? PlaceOfNowLiving { get; set; }
        public string? Role { get; set; }
        // Dictionary for additional fields
        [NotMapped]
        public Dictionary<string,string> AdditionalFields
        {
            get
            {
                // Десериализуем из JSON при чтении
                return string.IsNullOrEmpty(AdditionalFieldsJson)
                    ? new Dictionary< string,string>()
                    : JsonConvert.DeserializeObject<Dictionary<string,string>>(AdditionalFieldsJson);
            }
            set
            {
                // Сериализуем в JSON при записи
                AdditionalFieldsJson = JsonConvert.SerializeObject(value);
            }
        }

        // Храним AdditionalFields в виде JSON в базе данных
        public string? AdditionalFieldsJson { get; set; }

    }
}
