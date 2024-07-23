using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmZone.DAL.Interfaces;
using FilmZone.DAL.Repositories;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using FilmZone.Domain.ViewModels;
using FilmZone.Service.Interfaces;

namespace FilmZone.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var films = await _userRepository.Select();
                if (films.Count == 0)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = films;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUserById(int id)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetById(id);
                if (user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = user;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUserById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUserByMail(string mail)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetByMail(mail);
                if (user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = user;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUserById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateUser(User user)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await _userRepository.Create(user);
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var user = await _userRepository.GetById(id);
                if (user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    baseResponse.Data = false;

                    return baseResponse;
                }

                await _userRepository.Delete(user);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUserByLogin(string name)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetByLogin(name);
                if (user == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;

                    return baseResponse;
                }
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = user;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUserByLogin] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> UpdateUserById(User _user)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetById(_user.Id);
                if (user == null)
                {
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    baseResponse.Description = "User not found";
                    return baseResponse;
                }
                user.FirstName = _user.FirstName;
                user.LastName = _user.LastName;
                user.Email = _user.Email;
                user.EmailConfirmation = _user.EmailConfirmation;
                user.Login = _user.Login;
                user.Password = _user.Password;
                await _userRepository.Update(user);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[EditUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            
        }
    }
}
