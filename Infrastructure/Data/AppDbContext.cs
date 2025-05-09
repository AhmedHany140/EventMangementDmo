using Domain.Entities;
using System.Reflection;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using System.Reflection.Emit;
namespace Infrastructure.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options):IdentityDbContext<AppUser>(options)
	{

		public DbSet<ChatMessage> ChatMessages { get; set; }
		public DbSet<Event>  Events { get; set; }
		public DbSet<EventSponsor>  EventSponsors { get; set; }
		public DbSet<Poll> Polls { get; set; }
		public DbSet<Registrations>  Registrations { get; set; }
		public DbSet<Resource>  Resources { get; set; }
		public DbSet<Session>  Sessions { get; set; }
		public DbSet<SessionSpeaker>  SessionSpeakers { get; set; }
		public DbSet<Sponsor> Sponsors { get; set; }
		public DbSet<TicketType> TicketTypes { get; set; }
		public DbSet<VirtualRoom>  VirtualRooms { get; set; }

		public DbSet<EventSponsorDetails> EventSponsorDetails { get; set; }
		public DbSet<SessionSpeakerDetails> SessionSpeakerDetails { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			builder.Entity<EventSponsorDetails>(entity =>
			{
				entity.HasNoKey();  
				entity.ToView("vw_EventSponsorDetails");  
			});

			builder.Entity<SessionSpeakerDetails>(entity =>
			{
				entity.HasNoKey();
				entity.ToView("vw_SessionSpeakersDetails");
			});
		}
	}
}
