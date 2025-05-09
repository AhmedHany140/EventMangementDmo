using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Univerisity.Application.Loges;

namespace Infrastructure.Reposatory
{
	public class Reposatory<T>(AppDbContext dbContext ) : IService<T> where T : class
	{
		private readonly AppDbContext context = dbContext;

		private Response HandleExeption(string message)
		{
			LogExeptions.LogEx(new Exception(message));
			return new Response(message, false);
		}

		public async Task<Response> AddAsync(T Entity)
		{
			try
			{
				await context.Set<T>().AddAsync(Entity);
				await context.SaveChangesAsync();

			

				return new Response($"{typeof(T).Name} Created Successfully",true);
			}
			catch (Exception ex)
			{
				return HandleExeption(ex.Message);
			}
		}

		public async Task<Response> DeleteAsync(int id)
		{
			try
			{
				var Entry = await GetAsync(null,id);

				if(Entry is  null)
				{
					return new Response("Data Not Found By Default", false);
				}

				context.Set<T>().Remove(Entry);
				await context.SaveChangesAsync();

				return new Response("Deleted Success ", true);
			}
			catch (Exception ex)
			{
				return HandleExeption(ex.Message);

			}
		}
		public async Task<IEnumerable<T>> GetAllAsync(string[]? navigations)
		{
			try
			{
			
				var allItems = await context.Set<T>().ToListAsync();

	
				if (navigations != null)
				{
					foreach (var item in allItems)
					{
						var entry = context.Entry(item);
						foreach (var nav in navigations)
						{
							try
							{
								await entry.Reference(nav).LoadAsync(); 
							}
							catch
							{
								try
								{
									await entry.Collection(nav).LoadAsync(); 
								}
								catch
								{
									throw new Exception("Invalid Navigations");
								}
							}
						}
					}
				}

				return allItems;
			}
			catch(Exception ex)
			{
				throw new Exception($"{ex.Message}");
			}
		}





		public async Task<T?> GetAsync(string[]? navigations,int id)
		{
			try
			{
			
				var entity = await context.Set<T>().FindAsync(id);
				if (entity == null)
					return null;

		
				if (navigations != null)
				{
					var entry = context.Entry(entity);

					foreach (var nav in navigations)
					{
						try
						{
					
							await entry.Reference(nav).LoadAsync();
						}
						catch
						{
							try
							{
				
								await entry.Collection(nav).LoadAsync();
							}
							catch
							{
								throw new Exception("Invalid Navigations");
							}
						}
					}
				}

				return entity;
			}
			catch (Exception)
			{
				throw new Exception("Invalid Data");
			}
		}


		public async Task<Response> UpdateAsync(T Entity)
		{
			try
			{
				int EntityId = (int)Entity.GetType().GetProperty("Id")!.GetValue(Entity)!;

				var Entry = await GetAsync(null, EntityId);

				if (Entry is null)
					return new Response($"{Entity.GetType().Name} Not Found");

	
				var entryValues = context.Entry(Entry).CurrentValues;

			
				await context.SaveChangesAsync();
				return new Response("Update successful.", true);
			}
			catch (DbUpdateConcurrencyException ex)
			{
				var entry = ex.Entries.Single();

				var dbvalues = await entry.GetDatabaseValuesAsync(); 
				var clientvalues = entry.CurrentValues; 

				entry.OriginalValues.SetValues(dbvalues);
				entry.CurrentValues.SetValues(clientvalues);

				await context.SaveChangesAsync();
				return new Response("Data has been updated based on the latest changes. Please review to ensure accuracy.", true);
			}
			catch (Exception ex)
			{
				return HandleExeption(ex.Message);
			}
		}




	}
}
