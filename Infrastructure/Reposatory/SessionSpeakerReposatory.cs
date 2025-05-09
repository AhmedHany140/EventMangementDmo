using Application.DTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Provider;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Reposatory
{
	public class SessionSpeakerReposatory(AppDbContext appDb) : IProvider<SessionSpeakerDto, SessionSpeakerDetails>
	{
		private readonly AppDbContext context = appDb;
		public async Task<Response> Create(SessionSpeakerDto dto)
		{
			try
			{
				await context.Database
				  .ExecuteSqlRawAsync("EXEC sp_CreateSessionSpeaker @SessionId ,@SpeakerId , @Role",
				   new SqlParameter("@SessionId", dto.SessionId),
					new SqlParameter("@SpeakerId", dto.SpeakerId),
					 new SqlParameter("@Role", dto.Role));

				return new Response("SessionSpeaker added successfully.", true);
			}
			catch (Exception ex)
			{
				return new Response(ex.Message, false);
			}

		}

		public async Task<List<SessionSpeakerDetails>> GetDetails()
		{
			try
			{

				var result = await context.SessionSpeakerDetails
					.FromSqlRaw("SELECT * FROM vw_SessionSpeakersDetails")
					.ToListAsync();

				return result ;
			}
			catch (Exception ex)
			{

				throw new Exception("Error fetching event sponsor details", ex);
			}
		}
	}
}
