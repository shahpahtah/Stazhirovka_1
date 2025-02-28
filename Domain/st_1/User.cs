using System.Runtime.InteropServices;

namespace st_1
{
    public class User
    {
        private User(Guid id,string nickname,Image? avatar, List<string> knowlenge,string gender,DateTime date,string placeofBIrth,string placeofliving,string password,string email,string? role,Dictionary<string,string>? fields=null)
        {
            Id = id;
            NickName = nickname;
            Avatar = avatar;
            Knowlenge = knowlenge;
            Gender = gender;
            DateAndTimeOfBirth = date;
            PlaceOfBirth = placeofBIrth;
            PlaceOfNowLiving = placeofliving;
            Role = role;
            Password = password;
            Email = email;
            if (fields != null) { AdditionalFields = fields; }
            
        }
        public static User Create(Guid id, string nickname, Image? avatar, List<string> knowlenge, string gender, DateTime date, string placeofBIrth, string placeofliving,string email,string password,string? role,Dictionary<string,string> fields=null)
        {
            
            if (role == null)
            {
                role = "User";
            }
            return new User(id, nickname, avatar, knowlenge, gender, date, placeofBIrth, placeofliving,password,email,role,fields);
        }
        public string Password { get; }
        public string Email { get; }
        public Guid Id { get; }
        public string NickName { get; } = string.Empty;
        public Image? Avatar { get; }
        public IReadOnlyCollection<string> Knowlenge { get; }
        public string Gender { get; }
        public DateTime DateAndTimeOfBirth { get; }
        public string PlaceOfBirth { get; }
        public string PlaceOfNowLiving { get; }
        public string Role { get; } = "User";
        public  Dictionary<string, string> AdditionalFields { get ; private set; } = new Dictionary<string, string>();
     
    }
}
