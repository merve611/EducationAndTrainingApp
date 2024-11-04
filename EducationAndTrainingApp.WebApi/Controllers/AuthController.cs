using Azure.Core;
using EducationAndTrainingApp.Business.Operations.User;
using EducationAndTrainingApp.Business.Operations.User.Dtos;
using EducationAndTrainingApp.WebApi.Jwt;
using EducationAndTrainingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationAndTrainingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]  //Kayıt olmak

        public async Task<IActionResult> Register(RegisterRequest reguest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Dto ya çevrildi
            var addUserDto = new AddUserDto
            {
                Email = reguest.Email,
                FirstName = reguest.FirstName,
                LastName = reguest.LastName,
                Password = reguest.Password,
                BirthDate = reguest.BirthDate,
            };

            //Business katmanına gönderildi

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Message);
        }
        //HTTPGET -> Veri url üzerinden taşınır - querystring
        // -> firewall ve benzeri uygulamalarınız url 'i loglar, böyle bir durumda da şifrenizi loglar
        // Güvenlilk açığı  Bu sebeplerden dolayı HTTPPOST kullanırız loginde de
        //Giriş yapmak

        [HttpPost("login")]

        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userService.LoginUser(new LoginUserDto { Email = request.Email, Password = request.Password });

            if (!result.IsSucced)
                return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });


            return Ok(new LoginResponce
            {
                Message = "Giriş Başarıyla Tamamlandı",
                Token = token
            });


        }

        [HttpGet("me")]
        [Authorize]     //Token yoksa cevap yok
        public IActionResult GetMyUser()
        {
            return Ok();
        }


    }
}
