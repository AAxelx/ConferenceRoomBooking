using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.Common.Models.DTOs.Booking;
using ConferenceRoomBooking.Common.Models.DTOs.Room;
using ConferenceRoomBooking.Common.Models.DTOs.Service;
using ConferenceRoomBooking.Common.Models.DTOs.User;

namespace ConferenceRoomBooking.Common.Helpers;

public class DtoModelMapperProfile : Profile
{
    public DtoModelMapperProfile()
    {
        CreateMap<CreateRoomDto, RoomModel>();
        CreateMap<RoomDto, RoomModel>()
                .ReverseMap();
        
        CreateMap<CreateUserDto, UserModel>();
        CreateMap<UserDto, UserModel>()
            .ReverseMap();
        
        CreateMap<CreateBookingDto, BookingModel>();
        CreateMap<BookingDto, BookingModel>()
            .ReverseMap();
        
        CreateMap<ServiceDto, ServiceModel>()
            .ReverseMap();
    }
}