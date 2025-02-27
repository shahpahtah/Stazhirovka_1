using Microsoft.AspNetCore.Mvc;
using Services;
using Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using st_1;
using st_1.Data;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public IJwtProvider JwtProvider { get; set; }
        public UserController(IUserService service,IJwtProvider jwtprovider)
        {
            _service = service;
            JwtProvider = jwtprovider;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Input()
        {
            var model = new InputModel();
            return View(model);
        }
        public IActionResult Registr()
        {
            var model = new InputModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Registr(InputModel model)
        {
            User? new_user = _service.GetAllUsers().Where(i => i.Email == model.Email).FirstOrDefault();
            if (new_user == null)
            {
                var user = st_1.User.Create(new Guid(), "default", Image.Create("default"), new List<string>(), "default", new DateTime(), "default", "default", model.Email, model.Password, "default");
                _service.CreateUser(user);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Email) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Input");
            }
        }
        [HttpPost]
        public IActionResult Input(InputModel model)
        {

            User? new_user = _service.GetAllUsers().Where(i => i.Email == model.Email).FirstOrDefault();
            if (new_user != null)
            {
                if (new_user.Password != model.Password)
                {
                    
                    return View("Input");
                }
                else
                {
                  
                    var token = JwtProvider.GenerateToken(new_user);
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Registr");
            }
        }
        public IActionResult Cabinet()
        {
            ClaimsPrincipal currentUser = this.User;


            //// 3. Извлекаем claim email


            var new_user = _service.GetAllUsers().Where(i => i.Email == currentUser.Identity.Name).FirstOrDefault();
            if (new_user != null)
            {
                var model = new ModelCabinet()
                {
                    DateAndTimeOfBirth = new_user.DateAndTimeOfBirth,
                    Gender = new_user.Gender,
                    Knowlege = string.Join(",", new_user.Knowlenge),
                    NickName = new_user.NickName,
                    PlaceOfBirth = new_user.PlaceOfBirth,
                    PlaceOfNowLiving = new_user.PlaceOfNowLiving,
                    Role = new_user.Role
                        };
                return View(model);
            }
            else {
                var model = new ModelCabinet();
                return View(model);
            }
                

        }
        [HttpPost]
        public IActionResult Edit(ModelCabinet model)
        {
            string jwtToken = Request.Cookies[".AspNetCore.Cookies"];

            if (string.IsNullOrEmpty(jwtToken))
            {
                return Unauthorized("JWT not found in cookie");
            }

            // 2. Распарсиваем JWT
            ClaimsPrincipal currentUser = this.User;


            //// 3. Извлекаем claim email
         

            var new_user = _service.GetAllUsers().Where(i => i.Email == currentUser.Identity.Name).FirstOrDefault();
            var user = st_1.User.Create(new_user.Id, model.NickName, new_user.Avatar, model.Knowlege.Split(",").ToList(), model.Gender, model.DateAndTimeOfBirth.Value, model.PlaceOfBirth, model.PlaceOfNowLiving, new_user.Email, new_user.Password, new_user.Role);
            _service.UpdateUser(user);
            return RedirectToAction("Index","Home");

            
        }
        public IActionResult AddNewField()
        {
            var field = new Field();
            return View(field);
        }
        [HttpPost]
        public IActionResult AddNewField(Field field)
        {
            st_1.User.addFields(field.Key);
            ClaimsPrincipal currentUser = this.User;
            var new_user = _service.GetAllUsers().Where(i => i.Email == currentUser.Identity.Name).FirstOrDefault();
            new_user.UpgradeField();
            return RedirectToAction("Cabinet");
        }
    }
}
