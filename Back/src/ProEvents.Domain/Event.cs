using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProEvents.Domain.Identity;

namespace ProEvents.Domain
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public DateTime? EventDate { get; set; }
        [Required]
        public string Theme { get; set; }
        [Required]
        public int NumberOfPerson { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string Email { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
