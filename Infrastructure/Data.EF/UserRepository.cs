
using Data.EF;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using st_1;
using System.Collections.Generic;



    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;
      

        public UserRepository(AppDbContext context)
        {
            _context = context;
            
        }

        public  List<User> GetAll()
        {
        var userDbs =  _context.UserProfiles.ToListAsync().Result.Select(i => Mapper.Map(i)).ToList();
        return userDbs;
        }

        public User GetById(Guid id)
        {
            var userDb =  _context.UserProfiles.Find(id);
            return Mapper.Map(userDb);
        }

        public void Add(User user)
        {
            var userDb = Mapper.Map(user);
            _context.UserProfiles.Add(userDb);
           _context.SaveChanges();
        }

        public void Update(User user)
        {
            var userDb = _context.UserProfiles.Find(user.Id);
            if (userDb != null)
            {
            userDb.Email=user.Email;
            userDb.DateAndTimeOfBirth = user.DateAndTimeOfBirth;
            userDb.PlaceOfBirth = user.PlaceOfBirth;
            userDb.PlaceOfNowLiving = user.PlaceOfNowLiving;
            userDb.Role = user.Role;
            userDb.Gender = user.Gender;
            userDb.AdditionalFieldsJson= JsonConvert.SerializeObject(user.AdditionalFields);
            userDb.Avatar = Mapper.Map(user.Avatar);
            userDb.NickName = user.NickName;
            userDb.KnowledgeJson= JsonConvert.SerializeObject(user.Knowlenge);
            userDb.AdditionalFields = user.AdditionalFields;
            _context.UserProfiles.Update(userDb);
                 _context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var userDb = _context.UserProfiles.Find(id);
            if (userDb != null)
            {
                _context.UserProfiles.Remove(userDb);
                _context.SaveChanges();
            }
        }
    }

