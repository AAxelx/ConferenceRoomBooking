using AutoMapper;
using ConferenceRoomBooking.Common.Models;
using ConferenceRoomBooking.DAL.DataAccess.Contexts;
using ConferenceRoomBooking.DAL.DataAccess.Repositories.Abstractions;
using ConferenceRoomBooking.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DAL.DataAccess.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ConferenceRoomBookingDbContext _context;
    private readonly IMapper _mapper;

    public RoomRepository(ConferenceRoomBookingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoomModel> AddAsync(RoomModel model)
    {
        var roomEntity = _mapper.Map<RoomEntity>(model);
        var addedEntity = await _context.Rooms.AddAsync(roomEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<RoomModel>(addedEntity.Entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var roomEntity = await _context.Rooms.FindAsync(id);
        if (roomEntity != null)
        {
            _context.Rooms.Remove(roomEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<RoomModel>> GetAllAsync()
    {
        var rooms = await _context.Rooms.AsNoTracking().ToListAsync();
        return _mapper.Map<List<RoomModel>>(rooms);
    }

    public async Task<RoomModel> GetByIdAsync(Guid id)
    {
        var room = await _context.Rooms.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        return _mapper.Map<RoomModel>(room);
    }

    public async Task<List<RoomModel>> GetAvailableRoomsAsync(DateTime startDateTime, DateTime endDateTime, int capacity)
    {
        var availableRooms = await _context.Rooms
            .Where(r => r.Capacity >= capacity && 
                        !r.Bookings.Any(b => 
                            b.StartTime < endDateTime && 
                            b.EndTime > startDateTime))
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<RoomModel>>(availableRooms);
    }

    public async Task UpdateAsync(RoomModel model)
    {
        var roomEntity = _mapper.Map<RoomEntity>(model);
        _context.Rooms.Update(roomEntity);
        await _context.SaveChangesAsync();
    }
}
