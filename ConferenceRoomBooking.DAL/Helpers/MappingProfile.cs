using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.PartialModels;
using ConferenceRoomBooking.DAL.Entities;
using ConferenceRoomBooking.DAL.Entities.PatialEntities;

namespace ConferenceRoomBooking.DAL.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RoomModel, RoomEntity>()
            .ForMember(dest => dest.AvaliableServices, opt => opt.MapFrom(src => src.AvailableServices))
            .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings))
            .ReverseMap();

        CreateMap<RoomBookingsOnDateEntity, RoomBookingsOnDateModel>();

        CreateMap<ServiceModel, ServiceEntity>()
            .ReverseMap();

        CreateMap<BookingModel, BookingEntity>()
            .ReverseMap();

        CreateMap<UserModel, UserEntity>()
            .ReverseMap();
    }
}