using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.User.Dtos
{
    // Giriş işlemlerinde kullanılacak email ve password içerir
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
