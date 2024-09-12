using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface.IAuth
{
    public interface IAuthService
    {
        Task<(bool Succeeded, string Message)> RegisterAsync(RegisterModel model);
        Task<(bool Succeeded, string Token)> LoginAsync(LoginModel model);
    }
}
