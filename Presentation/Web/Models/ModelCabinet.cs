using Newtonsoft.Json;
using st_1.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Web.Models
{
    public class ModelCabinet
    {

    
        public string? NickName { get; set; }

        public string? Knowlege { get; set; }

        public List<string?> KnowlegeList
        {
            get
            {
                return Knowlege.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(v => v.Trim())
                                            .ToList() ?? new List<string>();
            }
        }
        public string? Gender { get; set; }
        public DateTime? DateAndTimeOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? PlaceOfNowLiving { get; set; }
        public string? Role { get; set; } = "User";
        public Dictionary<string?, string?> AdditionalFields
        {
            get; set;
        } = new Dictionary<string?, string?>();
        // Храним AdditionalFields в виде JSON в базе данных
    }
}
