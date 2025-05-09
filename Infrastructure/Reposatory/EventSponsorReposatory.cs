using Application.DTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Provider;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatory
{
	public class EventSponsorReposatory(AppDbContext appDb) : IProvider<EventSponsorDto, EventSponsorDetails>
	{
		private readonly AppDbContext context = appDb;
		public async Task<Response> Create(EventSponsorDto dto)
		{
			try
			{
			  	await context.Database
					.ExecuteSqlRawAsync("EXEC sp_CreateEventSponsor @EventId ,@SponsoreId , @Amount , @SponsorLevel",
					 new SqlParameter("@EventId",dto.EventId),
					  new SqlParameter("@SponsoreId", dto.SponsorId),
					   new SqlParameter("@Amount", dto.Amount),
						new SqlParameter("@SponsorLevel", dto.SponsorshipLevel));

				return new Response ("EventSponsor added successfully.", true);
			}
			catch (Exception ex)
			{
				return new Response(ex.Message, false);
			}

		}

		public async Task<List<EventSponsorDetails>> GetDetails()
		{
			try
			{
				var result = await context.EventSponsorDetails
					.FromSqlRaw("SELECT * FROM vw_EventSponsorDetails")
					.ToListAsync();

				return result;
			}
			catch (Exception ex)
			{
				throw new Exception("Error fetching event sponsor details", ex);
			}
		}


	}



}
