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
using Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _UserService;
        private readonly IFieldService _FieldService;
        public IJwtProvider JwtProvider { get; set; }
        public UserController(IUserService service,IJwtProvider jwtprovider,IFieldService fieldService)
        {
            _UserService = service;
            JwtProvider = jwtprovider;
            _FieldService = fieldService;
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
            User? new_user = _UserService.GetAllUsers().Where(i => i.Email == model.Email).FirstOrDefault();
            if (new_user == null)
            {
                var user = st_1.User.Create(new Guid(), "default", Image.Create("default"), new List<string>(), "default", new DateTime(), "default", "default", model.Email, model.Password, "default");
                _UserService.CreateUser(user);
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

            User? new_user = _UserService.GetAllUsers().Where(i => i.Email == model.Email).FirstOrDefault();
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

            var customFields = _FieldService.GetAllCustomFieldDefinitions();

            // Pass the custom fields to the view via ViewBag
            ViewBag.CustomFields = customFields;

            var new_user = _UserService.GetAllUsers().Where(i => i.Email == currentUser.Identity.Name).FirstOrDefault();
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
                    Role = new_user.Role,
                    AdditionalFields=new_user.AdditionalFields
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
            var customFields = new Dictionary<string, string>();
        foreach (string key in Request.Form.Keys)
    
        if (key.StartsWith("CustomFields["))
        {
            string fieldName = key.Substring("CustomFields[".Length).Replace("]", "");
        string fieldValue = Request.Form[key];
        customFields[fieldName] = fieldValue;
        }


        string jwtToken = Request.Cookies[".AspNetCore.Cookies"];

        if (string.IsNullOrEmpty(jwtToken))
            {
                return Unauthorized("JWT not found in cookie");
            }

            // 2. Распарсиваем JWT
            ClaimsPrincipal currentUser = this.User;


            //// 3. Извлекаем claim email
         

            var new_user = _UserService.GetAllUsers().Where(i => i.Email == currentUser.Identity.Name).FirstOrDefault();
            var user = st_1.User.Create(new_user.Id, model.NickName, new_user.Avatar, model.Knowlege.Split(",").ToList(), model.Gender, model.DateAndTimeOfBirth.Value, model.PlaceOfBirth, model.PlaceOfNowLiving, new_user.Email, new_user.Password, new_user.Role,customFields);
            
            _UserService.UpdateUser(user);
            return RedirectToAction("Index","Home");

            
        }
        public IActionResult AddNewField()
        {
            var field = new FieldModel();
            return View(field);
        }
        [HttpPost]
        public IActionResult AddNewField(FieldModel field)
        {
            var fieldModel= Field.Create(new Guid(), field.Name, field.DataType);
            _FieldService.CreateCustomFieldDefinition(fieldModel);

            return RedirectToAction("Cabinet");
        }
    }
}
