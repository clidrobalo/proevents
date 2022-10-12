using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEvents.Application.Dtos
{
    public class EventDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Place { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory."),
        StringLength(50, MinimumLength = 4, ErrorMessage = "Interval allow from 4 to 50 characters")]
        public string EventDate { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory."),
        StringLength(50, MinimumLength = 4, ErrorMessage = "Interval allow from 4 to 50 characters")]
        public string Theme { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1, 120000, ErrorMessage = "{0} can be less than 1, and more than 120000.")]
        public int NumberOfPerson { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Phone(ErrorMessage = "The field {0} has invalid number.")]
        public string phone { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Invalid image extention. e.g. (gif, jpg, jpeg, bmp, png)")]
        public string ImageUrl { get; set; }
        public IEnumerable<LoteDTO> Lotes { get; set; }
        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }
        public IEnumerable<SpeakerDTO> Speakers { get; set; }
        public int UserId { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}