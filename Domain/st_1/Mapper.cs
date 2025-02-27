using st_1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st_1
{
    public static class Mapper
    {
        public static User Map(UserDb db)
        {
            return User.Create(db.Id, db.NickName, Mapper.Map(db.Avatar), db.Knowledge, db.Gender, db.DateAndTimeOfBirth.Value, db.PlaceOfBirth, db.PlaceOfNowLiving, db.Email, db.Password, db.Role);
        }
        public  static UserDb Map(User user)
        {
            return new UserDb { Role = user.Role, Avatar = Mapper.Map(user.Avatar), DateAndTimeOfBirth = user.DateAndTimeOfBirth, Email = user.Email, Knowledge = (List<string>)user.Knowlenge, Gender = user.Gender, Id = user.Id, NickName = user.NickName, Password = user.Password, PlaceOfBirth = user.PlaceOfBirth, PlaceOfNowLiving = user.PlaceOfNowLiving };
        }
        public  static Image Map(ImageDb db)
        {

            if (db == null)
            {
                db = Map(Image.Create("default"));
            }
            return Image.Create(db.FileName);
        }
        public static ImageDb Map(Image image)
        {
            return new ImageDb { FileName = image.FileName, UserId = image.UserId };
        }
    }
}
