
<h1>Education and Training API</h1>

**Patika.dev .NET Core Bootcamp** 3. bitirme projesi olarak **Education and Training API** isimli uygulamayı geliştirdim.  **Education and Training API** bir eğitim platformu API'sidir. 


* 🆕  **Register**: Kayıt olma işlemi Authentication kullanılarak uygulanmıştır. Kullanıcının kayıt esnasında girdiği şifre Data Protection kullanılarak database tarafına şifrelenmiş olarak gelmektedir. 
* ⬆️  **Login**: Kullanıcı email ve şifresi ile giriş yapar, herhangi birini yanlış girerse uyarı alır.
         Kullanıcıya Authorization kulllanılarak ile yetkilendirme verildi. Örneğin kullanıcı admin rolünde ise Jwt kullanılarak oluşturulan token Authorize edilerek yetki tanımlanması yapılmaktadır.
* ⬇️ **Get-Post-Put-Patch-Delete**: CoursesController içerisinde oluşturulan bu endpointler ile kurs ekleme, silme, listeleme ve güncelleme işlemleri yapılır.

* :star: Oluşturulan Model Validasyonlar ile ilgili özelliğe gerekli kısıtlamalar getirilmiştir.
* :star: Middleware kullanılarak API istenildiğinde bakıma sokulur hale getirildi.
* :star: Action Filter kullanılarak atılan endpoint isteğine göre kullanıcıya uyarı mesajı verilebilir hale geldi. 


 <h1 id="built-with">Proje Görünümü</h1>
 
<img src="https://github.com/merve611/EducationAndTrainingApp/blob/master/EducationAndTrainingApp.WebApi/wwwroot/images/api.JPG"/>
<h6>Kayıt Olma</h6>
<img src="https://github.com/merve611/EducationAndTrainingApp/blob/master/EducationAndTrainingApp.WebApi/wwwroot/images/register.JPG"/>

<h6>Giriş Yapma</h6>
<img src="https://github.com/merve611/EducationAndTrainingApp/blob/master/EducationAndTrainingApp.WebApi/wwwroot/images/admin_logini.JPG"/>
<h6>Database Tarafı</h6>
<img src="https://github.com/merve611/EducationAndTrainingApp/blob/master/EducationAndTrainingApp.WebApi/wwwroot/images/data_protection_sifreleme.JPG"/>











 <h1 id="built-with">Geliştirildiği Teknolojiler</h1>

<h5>ASP.NET Core Web API</h5>
<h5>Entity Framework</h5>
<h5>MSSQL Server</h5>
<h5>Authentication</h5>
<h5>Authorization</h5>
<h5>Middleware</h5>
<h5>Action Filter</h5>
<h5>Action Filter</h5>
<h5>Model validation </h5>





<h1 > İletişim</h1>

<p align="center">
</a>
<a href="https://www.linkedin.com/in/merve-akkoyunlu-2bb1881a8/">
  <img alt="merve's LinkdeIN" width="35px" src="https://image.flaticon.com/icons/png/512/174/174857.png" />
</a>

</p>













