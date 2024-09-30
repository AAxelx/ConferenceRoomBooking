using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.DAL.Entities;

namespace ConferenceRoomBooking.DAL.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RoomModel, RoomEntity>()
            .ReverseMap();

        CreateMap<ServiceModel, ServiceEntity>()
            .ReverseMap();

        CreateMap<BookingModel, BookingEntity>()
            .ReverseMap();

        CreateMap<UserModel, UserEntity>()
            .ReverseMap();
    }
}