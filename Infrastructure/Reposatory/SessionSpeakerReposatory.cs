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
             .Select(s => new SessionSpeakerDetails
             {
             	SessionId = s.SessionId,
             	EventId = s.EventId,
             	Title = s.Title,
             	StartTime = s.StartTime,
             	EndTime = s.EndTime,
             	MaxParticipants = s.MaxParticipants,
             	RecordingUrl = s.RecordingUrl,
             	SessionLink = s.SessionLink,
             	SessionStatus = s.SessionStatus,
             	SessionType = s.SessionType,
             	VirtualRoomId = s.VirtualRoomId,
             	Bio = s.Bio,
             	Email = s.Email,
             	UserName = s.UserName,
             	UserType = s.UserType,
             	CreatedAt = s.CreatedAt,
             	LastLogin = s.LastLogin,
             	Role = s.Role
             })
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
