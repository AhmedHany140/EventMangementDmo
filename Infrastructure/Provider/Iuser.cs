using Application.DTOs;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Provider
{
	public interface Iuser
	{
		Task<Response> Register(RegisterDTO dto);
		Task<Response> Login(LoginDTO dto);
		Task<IEnumerable<UserDto>> GetAll();
		Task<UserDto> GetById(string id);
		Task<Response> RefreshTokenAsync(TokenRequest request);
	}



}
