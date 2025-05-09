using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{

	public record UserDto(string UserNmae,string Email,string PhoneNumber);

	public record RegisterDTO(string UserName, string Email, 
		string Password, string PhoneNumber, UserType UserType,string Bio);
	public record LoginDTO(string Email, string Password);
	public record CreateEventDto(
		string Title,
		string Description,
		DateTime StartDate,
		DateTime EndDate,
		EvenentStatus EvenentStatus,
		EventType EventType,
		int? MaxAttendees,
		bool IsRecurring,
		RecurrencePattern? RecurrencePattern,
		string? CoverImageUrl,
		string OrganizerId,
		DateTime? RegistrationDeadline,
		string TimeZone
	);


	public record UpdateEventDto(
	int Id,
	string Title,
	string Description,
	DateTime StartDate,
	DateTime EndDate,
	EvenentStatus EvenentStatus,
	EventType EventType,
	int? MaxAttendees,
	bool IsRecurring,
	RecurrencePattern? RecurrencePattern,
	string? CoverImageUrl,
	string OrganizerId,
	DateTime? RegistrationDeadline,
	string TimeZone
    );

	public record EventDto(
	int Id,
	string Title,
	string Description,
	DateTime StartDate,
	DateTime EndDate,
	EvenentStatus EvenentStatus,
	EventType EventType,
	int? MaxAttendees,
	bool IsRecurring,
	RecurrencePattern? RecurrencePattern,
	string? CoverImageUrl,
	string OrganizerId,
	string TimeZone,
	DateTime? RegistrationDeadline
    );



	public record CreateChatMessageDto(
	int SessionId,
	string UserId,
	string Text,
	int? ParentMessageId
    );


	public record UpdateChatMessageDto(
		int Id,
       int SessionId,
      string UserId,
     string Text,
           int? ParentMessageId
     );

	public record ChatMessageDto(
	int Id,
	string Text,
	int SessionId,
	DateTimeOffset Timestamp,
	string UserId,
	string UserName,
	int? ParentMessageId
    );


public record CreatePollDto(
int SessionId,
string Question,
bool IsAnonymous,
PollType PollType
);

	public record PollDto(
	int Id,
	int SessionId,
	string Question,
	bool IsAnonymous,
	DateTime CreatedAt,
	PollType PollType

	);


	public record UpdatePollDto(
	int Id,
	string Question,
	bool IsAnonymous,
	PollType PollType

	);



	public record CreateRegistrationDto(
	string AttendanceId,
	int EventId,
	int TicketTypeId,
	DateTime RegistrationDate,
	CheckInStatus CheckInStatus
	);

	public record UpdateRegistrationDto(
	int Id,
	string AttendanceId,
	int EventId,
	int TicketTypeId,
	DateTime RegistrationDate,
	CheckInStatus CheckInStatus

	);


	public record RegistrationDto(
	string AttendanceId,
	string AttendanceName,
	int EventId,
	string EventTitle,
	int TicketTypeId,
	string TicketTypeName,
	DateTime RegistrationDate,
	CheckInStatus CheckInStatus,
	DateTime ChickInTime
	);


	public record CreateSessionDto(
int EventId,
string Title,
string Description,
DateTime StartTime,
DateTime EndTime,
int MaxParticipants,
string? RecordingUrl,
string? SessionLink,
SessionStatus SessionStatus,
SessionType SessionType,
int? VirtualRoomId
);


	public record UpdateSessionDto(
	int Id,
	string Title,
	string Description,
	DateTime StartTime,
	DateTime EndTime,
	int MaxParticipants,
	string? RecordingUrl,
	string? SessionLink,
	SessionStatus SessionStatus,
	SessionType SessionType,
	int? VirtualRoomId
	);


	public record SessionDto(
	int Id,
	string Title,
	string Description,
	DateTime StartTime,
	DateTime EndTime,
	int MaxParticipants,
	string? RecordingUrl,
	string? SessionLink,
	SessionStatus SessionStatus,
	SessionType SessionType,
	int? VirtualRoomId,
	string? VirtualRoomName,
	int EventId,
	string EventTitle
	);

	public record CreateSponsorDto(
	string Name,
	string? LogoUrl,
	string? Website,
	string? ContactPerson,
	string? Email,
	SponsorLevel SponsorLevel
    );


	public record UpdateSponsorDto(
	int Id,
	string Name,
	string? LogoUrl,
	string? Website,
	string? ContactPerson,
	string? Email,
	SponsorLevel SponsorLevel
    );

	public record SponsorDto(
	int Id,
	string Name,
	string? LogoUrl,
	string? Website,
	string? ContactPerson,
	string? Email,
	SponsorLevel SponsorLevel
    );


	public record CreateTicketTypeDto(
	int EventId,
	int QuantityAvailable,
	decimal Price,
	string Name,
	DateTime SalesStartDate,
	DateTime SalesEndDate

    );

	public record UpdateTicketTypeDto(
	 int Id,
	 int EventId,
	 int QuantityAvailable,
	 decimal Price,
	 string Name,
	 DateTime SalesStartDate,
	 DateTime SalesEndDate

    );


	public record TicketTypeDto(
	int Id,
	int EventId,
	int QuantityAvailable,
	decimal Price,
	string Name,
	DateTime SalesStartDate,
	DateTime SalesEndDate
    );


	public record CreateVirtualRoomDto(
	string Name,
	string Platform,
	int MaxCapacity,
	string AccessCode,
	int SessionId
    );

	public record UpdateVirtualRoomDto(
	int Id,
	string Name,
	string Platform,
	int MaxCapacity,
	string AccessCode,
	int SessionId
    );


	public record VirtualRoomDto(
	int Id,
	string Name,
	string Platform,
	int MaxCapacity,
	string AccessCode,
	int SessionId
    );


	public record EventSponsorDto(
	int EventId,
	int SponsorId,
	double Amount,
	SponsorLevel SponsorshipLevel
    );
	public record EventSponsorDetailsDto(
		int EventId,
		string EventTitle,
		string Description,
		DateTime StartDate,
		DateTime EndDate,
		int SponsorId,
		string SponsorName,
		string? ContactPerson,
		string? Email,
		double Amount,
		string SponsorshipLevel
	);

	public record SessionSpeakerDetailsDto(
	int SessionId,
	int EventId,
	string Title,
	DateTime StartTime,
	DateTime EndTime,
	int? MaxParticipants,
	string? RecordingUrl,
	string? SessionLink,
	string SessionStatus,
	string SessionType,
	int VirtualRoomId,
	string UserId, // User ID (Speaker)
	string? Bio,
	string Email,
	string UserName,
	string UserType,
	DateTime CreatedAt,
	DateTime? LastLogin,
	string Role
     );


	public record SessionSpeakerDto(
	int SessionId,
	string SpeakerId,
	string Role
    );


	public record CreateResourceDto(
	int SessionId,
	int EventId,
	string Name,
	string Type,
	string Url,
	AccessLevel AccessLevel
	);

	public record UpdateResourceDto(
	int Id,
	string Name,
	string Type,
	string Url,
	AccessLevel AccessLevel
	);


	public record ResourceDto(
	int Id,
	int SessionId,
	string SessionTitle,
	int EventId,
	string EventTitle,
	string Name,
	string Type,
	string Url,
	AccessLevel AccessLevel
	);

}
