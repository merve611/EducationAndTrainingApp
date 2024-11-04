using EducationAndTrainingApp.Business.DataProtection;
using EducationAndTrainingApp.Business.Operations.User.Dtos;
using EducationAndTrainingApp.Business.Types;
using EducationAndTrainingApp.Data.Entities;
using EducationAndTrainingApp.Data.Repositories;
using EducationAndTrainingApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _protector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protector = protector;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            //email adresinden başka varmı 
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            //hasMail in içinde hiç eleman varmı varsa hata mesajı ver
            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Email adresi zaten mecvut"
                };

            }
            //dto türü entity türüne dönüştürüldü
            var userEntity = new UserEntity()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = _protector.Protect(user.Password),
                BirthDate = user.BirthDate,
                UserType = Data.Enums.UserType.Student,
            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı kaydı sırasında bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };

        }

        //Giriş işleminin kodlaması
        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            // Gönderilen maille eşleşen bir bir entity var mı
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if(userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }
            // passwordü çözümleyip girilen password ile eşit mi diye kontrol ettik 

            var unprotectedPassword = _protector.UnProtect(userEntity.Password);

            if (unprotectedPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = true,
                    Data = new UserInfoDto
                    {
                        // Veri tabanından çekilen bilgiler controllera gönderiliyor
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        UserType = userEntity.UserType
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };

            }
        }
    }
}
