
using Newtonsoft.Json.Linq;
using System.Net;
using EMP.Common.Dto.Auth_Dto;
using EMP.Common.Exceptions;
using EMP.DataAccess.Models;
using EMP.DataAccess.Repositories;
using EMP.Http.Interfaces;
using EMP.Services.Auth.Interfaces;
using EMP.Services.Auth.Services;
using EMP.Services.Services.Interfaces;

namespace EMP.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IManagerAPIService _managerAPI;
        private readonly IMathStatistic _mathStatistic;
        private readonly IHashService _hashService;
        private readonly JwtProvider _jwtProvider;
        public UserService(IUserRepository userRepository
                         , IManagerAPIService managerAPI
                         , IMathStatistic mathStatistic
                         , IHashService hashService
                         , JwtProvider jwtProvider) 
        { 
            _userRepository = userRepository;
            _managerAPI = managerAPI;
            _mathStatistic = mathStatistic;
            _hashService = hashService;
            _jwtProvider = jwtProvider;
        }         

        public async Task<string?> LoginUser_Async(LoginRequestDTO loginRequest)
        {
            User? user = await _userRepository.FindUserByPhone_Async(loginRequest.Phone);
            if (user == null)
                throw new CustomResponseException(HttpStatusCode.NotFound, $"User by Phone {loginRequest.Phone} not found.");

            if (!_hashService.Verify(loginRequest.Password, user.PasswordHash))
                throw new CustomResponseException(HttpStatusCode.BadRequest, "Bad credentials");


            string? externalToken = await _managerAPI.AuthenticateAsync(loginRequest.UserCardCode);

            string token = await GenerateToken_Async(user, externalToken);

            return token;
        }

        public async Task<string> RegisterUser_Async(RegisterRequestDTO registerRequest)
        {      
            //Check if user exists in EMP data base 
            User? user = await _userRepository.FindUserByPhone_Async(registerRequest.Phone);
            if(user != null)
                throw new CustomResponseException(HttpStatusCode.Conflict, $"There is exists user with the same phone :{registerRequest.Phone}.");

            //Get External token
            string? externalToken = await _managerAPI.AuthenticateAsync(registerRequest.UserCardCode);

            //Get all users from External API. 
            JObject? jObj = await _managerAPI.GetExternalUsers_Async(externalToken);

            //Create new User
            user = new User
            {            
                Phone = registerRequest.Phone,
                PasswordHash = _hashService.Generate(registerRequest.Password),
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Patronymic = registerRequest.Patronymic,
                IsActive=true,
                CreatedAt = DateTime.Now
                
            };

            //Get External ID
            user.ExternalId = await _mathStatistic.GetUserExternalID_Async(user, jObj);

            if(user.ExternalId == null)
                throw new CustomResponseException(HttpStatusCode.NotFound, $"There is no users with the carrent data");

            //Save to the DB 
            await _userRepository.CreateUser_Async(user);

            //Generate application token
            string token = await GenerateToken_Async(user, externalToken);

            return token;

        }

       
        public async Task<string> GetFullName_Async(int externalId)
        {
            User? user = await _userRepository.FindUser_ByExternalID_Async(externalId);

            return string.Concat(user?.LastName," ",user?.FirstName," ",user?.Patronymic);
        }



        public async Task<string> GenerateToken_Async(User user, string? externalToken)
        {

            return await Task.Run(() =>
            {
                return _jwtProvider.GenerateToken(user, externalToken);
            });
           
        }
    }
}
