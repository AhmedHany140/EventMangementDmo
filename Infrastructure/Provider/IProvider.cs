using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Provider
{
	public interface IProvider<Tdto,TDetails>
	{
		Task<Response> Create(Tdto dto);
		Task<List< TDetails>> GetDetails();

	}
}
