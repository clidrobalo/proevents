using AutoMapper;
using ProEvents.Application.Dtos;
using ProEvents.Domain;

namespace ProEvents.Application.Helpers
{
    public class ProEventsProfile : Profile
    {
        public ProEventsProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaDTO>().ReverseMap();
            CreateMap<Speaker, SpeakerDTO>().ReverseMap();
        }
    }
}