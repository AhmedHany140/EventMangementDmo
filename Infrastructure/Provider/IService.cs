using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Provider
{
	public interface IService<T>
	{
		Task<T> GetAsync(string[]? Navigations,  int id);
		Task<IEnumerable<T>> GetAllAsync(string[]? Navigations);
		Task<Response> DeleteAsync(int id);
		Task<Response> AddAsync(T Entity);
		Task<Response> UpdateAsync(T Entity);

		

	}
}
