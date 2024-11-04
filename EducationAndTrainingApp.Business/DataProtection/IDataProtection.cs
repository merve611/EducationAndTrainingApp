using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.DataProtection
{
    public interface IDataProtection
    {
        string Protect(string text);    //şifrelenmiş metni dönecek metot 
        string UnProtect(string protectedText);     //Gönderilen şifreli metni açacak bir metot

    }
}
